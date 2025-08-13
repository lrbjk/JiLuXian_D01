using AI.FSM.Framework;

namespace AI.FSM
{
    /// <summary>
    /// 描述：
    /// </summary>
    public class BreakMoveInputTrigger : MovementInputTrigger
    {
        public override void Init()
        {
            triggerID = FSMTriggerID.BreakMoveInput;
        }

        public override bool HandleTrigger(FSMBase fSMBase)
        {
            return PlayerFSMBase.Instance.playerInfo.IsInMovtionRecoveryFlag &&//处于后摇阶段
                base.HandleTrigger(fSMBase);
        }
    }
}
