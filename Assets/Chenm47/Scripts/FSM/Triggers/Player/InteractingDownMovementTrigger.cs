using AI.FSM.Framework;

namespace AI.FSM
{
    /// <summary>
    /// 描述：
    /// </summary>
    public class InteractingDownMovementTrigger : MovementInputTrigger
    {
        public override bool HandleTrigger(FSMBase fSMBase)
        {
            return base.HandleTrigger(fSMBase) &&
                !PlayerFSMBase.Instance.animator.GetBool("IsInteracting");
        }

        public override void Init()
        {
            triggerID = FSMTriggerID.InteractingDownMovement;
        }
    }
}
