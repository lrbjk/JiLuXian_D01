using AI.FSM.Framework;

namespace AI.FSM
{
    /// <summary>
    /// 描述：
    /// </summary>
    public class LockedRollInputTrigger : RollInputTrigger
    {
        public override void Init()
        {
            triggerID = FSMTriggerID.LockedRollInput;
        }
        public override bool HandleTrigger(FSMBase fSMBase)
        {
            return PlayerFSMBase.Instance.playerInput.LockViewTrigger &&//处于视角锁定
                base.HandleTrigger(fSMBase);
        }
    }
}
