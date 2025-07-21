using AI.FSM.Framework;
using ns.Character.Player;

namespace AI.FSM
{
    /// <summary>
    /// 描述：
    /// </summary>
    public class RollBreakTrigger : RollInputTrigger
    {
        public override bool HandleTrigger(FSMBase fSMBase)
        {
            var playerInfo = fSMBase.characterInfo;
            return base.HandleTrigger(fSMBase) &&
                            playerInfo.IsInMovtionRecoveryFlag;//处于后摇阶段
        }

        public override void Init()
        {
            triggerID = FSMTriggerID.RollBreak;
        }
    }
}
