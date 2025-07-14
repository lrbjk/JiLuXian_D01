using AI.FSM.Framework;

namespace AI.FSM
{
    /// <summary>
    /// 描述：
    /// </summary>
    public class AttackInputTrigger : FSMTrigger
    {
        public override bool HandleTrigger(FSMBase fSMBase)
        {
            var playerFSM = fSMBase as PlayerFSMBase;

            return playerFSM.playerInput.HeavyAttackL ||
                playerFSM.playerInput.HeavyAttackR ||
                playerFSM.playerInput.LightAttackL ||
                playerFSM.playerInput.LightAttackR ||
                playerFSM.playerInput.SkillAttackL ||
                playerFSM.playerInput.SkillAttackR;
        }

        public override void Init()
        {
            triggerID = FSMTriggerID.AttackInput;
        }
    }
}
