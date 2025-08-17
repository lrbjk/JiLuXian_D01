using AI.FSM.Framework;

namespace AI.FSM
{
    /// <summary>
    /// 描述：
    /// </summary>
    public class ClimbQuickDownInputTrigger : FSMTrigger
    {
        public override bool HandleTrigger(FSMBase fSMBase)
        {
            return !fSMBase.animator.GetBool("IsInteracting") &&
                PlayerFSMBase.Instance.playerInput.RawVerticalMove == -1f &&
                PlayerFSMBase.Instance.playerInput.RollHoldTrigger;
        }

        public override void Init()
        {
            triggerID = FSMTriggerID.ClimbQuickDownInput;
        }
    }
}
