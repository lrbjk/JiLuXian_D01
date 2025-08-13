using AI.FSM.Framework;

namespace AI.FSM
{
    /// <summary>
    /// 描述：
    /// </summary>
    public class BreakAttackInputTrigger : AttackInputTrigger
    {
        public override void Init()
        {
            triggerID = FSMTriggerID.BreakAttackInput;
        }

        public override bool HandleTrigger(FSMBase fSMBase)
        {
            return PlayerFSMBase.Instance.playerInfo.IsInMovtionRecoveryFlag //玩家处于后摇阶段
                && base.HandleTrigger(fSMBase);
        }
    }
}
