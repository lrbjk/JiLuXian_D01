using AI.FSM.Framework;

namespace AI.FSM
{
    /// <summary>
    /// 描述：
    /// </summary>
    public class BackStabTrigger : AttackInputTrigger
    {
        public override bool HandleTrigger(FSMBase fSMBase)
        {
            PlayerFSMBase playerFSMBase = fSMBase as PlayerFSMBase;

            return base.HandleTrigger(fSMBase) &&
                playerFSMBase.playerAction.IsBackStabOrRiposte();//内部球形检测范围内是否有满足目标
        }

        public override void Init()
        {
            triggerID = FSMTriggerID.BackStab;
        }
    }
}
