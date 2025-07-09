using System;
using System.Collections.Generic;

namespace AI.FSM.Framework
{
    /// <summary>
    /// 描述：状态命名规则：命名规则AI.FSM.+枚举名+State
    /// </summary>
    public abstract class FSMState
    {
        public FSMStateID StateID { get; protected set; }
        /// <summary>
        /// 当前状态的转换条件=>下一状态
        /// </summary>

        private Dictionary<FSMTriggerID, FSMStateID> map;

        private List<FSMTrigger> triggers;

        /// <summary>
        /// 初始化时必须给stateID赋值
        /// </summary>
        public abstract void Init();

        public FSMState()
        {
            map = new Dictionary<FSMTriggerID, FSMStateID>();
            triggers = new List<FSMTrigger>();
            Init();
        }

        /// <summary>
        /// 添加条件=>状态映射
        /// </summary>
        /// <param name="triggerID"></param>
        /// <param name="stateID"></param>
        public void AddMap(FSMTriggerID triggerID, FSMStateID stateID)
        {
            //动态创建对象
            FSMTrigger fSMTrigger = CreateTrigger(triggerID);
            triggers.Add(fSMTrigger);
            map.Add(triggerID, stateID);
        }

        private static FSMTrigger CreateTrigger(FSMTriggerID triggerID)
        {
            Type t = Type.GetType("AI.FSM." + triggerID + "Trigger");
            FSMTrigger fSMTrigger = Activator.CreateInstance(t) as FSMTrigger;
            return fSMTrigger;
        }

        /// <summary>
        /// 判断是否满足条件来切换当前状态
        /// </summary>
        /// <param name="fSMBase"></param>
        public void Reason(FSMBase fSMBase)
        {
            foreach (var trigger in triggers)
            {
                if (trigger.HandleTrigger(fSMBase))
                {
                    fSMBase.SwitchState(map[trigger.triggerID]);
                    break;
                }
            }
        }
        /// <summary>
        /// 进入状态时行为
        /// </summary>
        /// <param name="fSMBase"></param>
        public virtual void EnterState(FSMBase fSMBase) { }
        /// <summary>
        /// 在状态中行为
        /// </summary>
        /// <param name="fSMBase"></param>
        public virtual void ActionState(FSMBase fSMBase) { }
        /// <summary>
        /// 退出状态行为
        /// </summary>
        /// <param name="fSMBase"></param>
        public virtual void ExitState(FSMBase fSMBase) { }

    }
}
