using AI.FSM.Framework;

namespace AI.FSM
{
    /// <summary>
    /// 描述：
    /// </summary>
    public class InteractingDownTrigger : FSMTrigger
    {
        public override bool HandleTrigger(FSMBase fSMBase)
        {
            PlayerFSMBase playerFSMBase = fSMBase as PlayerFSMBase;
            return !playerFSMBase.animator.GetBool("IsInteracting");
        }

        public override void Init()
        {
            triggerID = FSMTriggerID.InteractingDown;
        }
    }
}
