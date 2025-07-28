using AI.FSM.Framework;
using ns.Movtion;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace AI.FSM
{
    /// <summary>
    /// 描述：动作状态，进一步实现动作前后摇等动画事件的处理，内部在进入状态时已播放动画
    /// </summary>
    public abstract class MovtionState : FSMState
    {
        private static Dictionary<Type, Dictionary<string, MethodInfo>> _methodCache =
            new Dictionary<Type, Dictionary<string, MethodInfo>>();

        private Dictionary<MovtionEventType, EventHandler<Common.AnimationEventArgs>> _eventHandlers =
            new Dictionary<MovtionEventType, EventHandler<Common.AnimationEventArgs>>();

        protected FSMBase fSMBase;
        protected MovtionInfo movtionInfo;

        /// <summary>
        /// 需要继续初始化动作信息
        /// </summary>
        protected abstract MovtionInfo InitMovtionInfo(FSMBase fSMBase);

        public override void EnterState(FSMBase fSMBase)
        {
            base.EnterState(fSMBase);
            this.fSMBase = fSMBase;
            movtionInfo = InitMovtionInfo(fSMBase);

            // 检查MovtionInfo是否为null
            if (movtionInfo == null)
            {
                Debug.LogError($"MovtionState.EnterState: MovtionInfo为null！状态: {this.GetType().Name}");
                return;
            }

            // 检查MovtionEvents是否为null
            if (movtionInfo.MovtionEvents == null)
            {
                Debug.LogWarning($"MovtionState.EnterState: MovtionEvents为null，跳过事件监听。状态: {this.GetType().Name}");
                return;
            }

            //根据动作参数监听事件
            foreach (var movtionEvent in movtionInfo.MovtionEvents)
            {
                MovtionEventType eventType = movtionEvent.EventType;
                string handlerName = "On" + eventType.ToString(); // 构建方法名

                //获取方法信息
                Type t = this.GetType();
                if (!_methodCache.TryGetValue(t, out var methodDict))
                {
                    methodDict = new Dictionary<string, MethodInfo>();
                    _methodCache[t] = methodDict;
                }
                if (!methodDict.TryGetValue(handlerName, out MethodInfo method))
                {
                    // 通过反射查找匹配的方法
                    method = t.GetMethod(handlerName,
                        BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public,
                        null,
                        new Type[] { typeof(object), typeof(Common.AnimationEventArgs) },
                        null);
                    methodDict[handlerName] = method; // 缓存结果（含null情况）
                }

                //获取委托
                if (!_eventHandlers.TryGetValue(eventType, out var existingHandler))
                {
                    // 创建委托
                    var handler = (EventHandler<Common.AnimationEventArgs>)Delegate.CreateDelegate(
                        typeof(EventHandler<Common.AnimationEventArgs>),
                        this,
                        method);
                    existingHandler = handler;
                    _eventHandlers.Add(eventType, existingHandler);
                }

                //注册事件
                fSMBase.animationEventBehaviour.RegisterEvent(eventType, existingHandler);
            }

            //处理前后摇等标记
            fSMBase.characterInfo.IsInPreMovtionFlag = true;

            //播放相应动画
            var animationName = movtionInfo.AnimationName;
            Debug.Log("动作名称：" + movtionInfo.MovtionName + ";播放动画状态：" + animationName);
            fSMBase.animationHandler.PlayTargetAnimation(animationName, true, 0.2f);
        }

        public override void ExitState(FSMBase fSMBase)
        {
            base.ExitState(fSMBase);
            //取消订阅
            foreach (var movtionEvent in movtionInfo.MovtionEvents)
            {
                MovtionEventType eventType = movtionEvent.EventType;
                //取消订阅事件
                fSMBase.animationEventBehaviour.UnRegisterEvent(eventType, _eventHandlers[eventType]);
            }

            //后摇结束
            fSMBase.characterInfo.IsInMovtionRecoveryFlag = false;
        }
        /// <summary>
        /// 前摇结束
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void OnPreMovtionEnd(object sender, Common.AnimationEventArgs e)
        {
            fSMBase.characterInfo.IsInPreMovtionFlag = false;
        }
        /// <summary>
        /// 动作生效开始
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void OnMovtionStart(object sender, Common.AnimationEventArgs e) { }
        /// <summary>
        /// 动作生效结束
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void OnMovtionEnd(object sender, Common.AnimationEventArgs e) { }
        /// <summary>
        /// 后摇开始
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void OnMovtionRecovery(object sender, Common.AnimationEventArgs e)
        {
            //后摇开始
            fSMBase.characterInfo.IsInMovtionRecoveryFlag = true;
        }




    }
}
