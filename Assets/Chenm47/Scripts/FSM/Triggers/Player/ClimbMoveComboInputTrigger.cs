using AI.FSM.Framework;

namespace AI.FSM
{
    /// <summary>
    /// 描述：
    /// </summary>
    public class ClimbMoveComboInputTrigger : ClimbMoveInputTrigger
    {
        public override bool HandleTrigger(FSMBase fSMBase)
        {
            return base.HandleTrigger(fSMBase) &&
                           !fSMBase.animator.GetBool("IsInteracting");
        }

        public override void Init()
        {
            triggerID = FSMTriggerID.ClimbMoveComboInput;
        }
    }
}
