using AI.FSM.Framework;

namespace AI.FSM
{
    /// <summary>
    /// 描述：
    /// </summary>
    public class NoMovementInputTrigger : FSMTrigger
    {
        public override bool HandleTrigger(FSMBase fSMBase)
        {
            PlayerFSMBase playerFSMBase = fSMBase as PlayerFSMBase;
            return playerFSMBase.playerInput.Movement <= 0.01f;
        }

        public override void Init()
        {
            triggerID = FSMTriggerID.NoMovementInput;
        }
    }
}
