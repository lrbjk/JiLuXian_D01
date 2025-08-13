using AI.FSM.Framework;

namespace AI.FSM
{
    /// <summary>
    /// 描述：
    /// </summary>
    public class LockedRollBreakTrigger : LockedRollInputTrigger
    {
        public override void Init()
        {
            triggerID = FSMTriggerID.LockedRollBreak;
        }

        public override bool HandleTrigger(FSMBase fSMBase)
        {
            return PlayerFSMBase.Instance.playerInfo.IsInMovtionRecoveryFlag &&
                base.HandleTrigger(fSMBase);
        }

    }
}
