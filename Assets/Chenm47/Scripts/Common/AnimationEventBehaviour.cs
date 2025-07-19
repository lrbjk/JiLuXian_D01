using AI.FSM.Framework;
using ns.Movtion;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{

    public class AnimationEventArgs : EventArgs
    {
        public FSMBase fSMBase { get; set; }
        public AnimationEventArgs()
        {

        }
    }

    /// <summary>
    /// 描述：动画事件行为类、特定时机触发事件
    /// </summary>
    public class AnimationEventBehaviour : MonoBehaviour
    {
        private Dictionary<MovtionEventType, EventHandler<AnimationEventArgs>> _eventHandlers;


        private void Awake()
        {
            _eventHandlers = new Dictionary<MovtionEventType, EventHandler<AnimationEventArgs>>();
            //创建mon脚本供unity调用
            foreach (MovtionEventType t in Enum.GetValues(typeof(MovtionEventType)))
            {
                Type monoType = Type.GetType("ns.Movtion." + t.ToString() + "Event");
                gameObject.AddComponent(monoType);
                _eventHandlers.Add(t, null);
            }
        }

        public void RegisterEvent(MovtionEventType eventType, EventHandler<AnimationEventArgs> handler)
        {
            _eventHandlers[eventType] += handler;
        }

        public void UnRegisterEvent(MovtionEventType eventType, EventHandler<AnimationEventArgs> handler)
        {
            _eventHandlers[eventType] -= handler;
        }

        public EventHandler<AnimationEventArgs> GetEventHandler(MovtionEventType eventType)
        {
            if (_eventHandlers.TryGetValue(eventType, out var handler))
            {
                return handler;
            }
            return null;
        }
    }
}
