namespace AI.FSM.Framework
{
    /// <summary>
    /// 描述：条件触发器:命名规则AI.FSM.+枚举名+Trigger
    /// </summary>
    public abstract class FSMTrigger
    {
        public FSMTriggerID triggerID;

        /// <summary>
        /// 必须给triggerID赋值
        /// </summary>
        public abstract void Init();

        public abstract bool HandleTrigger(FSMBase fSMBase);

        public FSMTrigger()
        {
            Init();
        }
    }
}
