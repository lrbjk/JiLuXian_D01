using AI.FSM.Framework;

namespace AI.FSM
{
    /// <summary>
    /// 描述：
    /// </summary>
    public class ClimbEndInputTrigger : FSMTrigger
    {
        public override bool HandleTrigger(FSMBase fSMBase)
        {
            //进入相应box，并且有相应输入，并且当前处于动画播完
            if (PlayerFSMBase.Instance.playerInfo.IsInUpClimbBox &&
                PlayerFSMBase.Instance.playerInput.RawVerticalMove == -1f &&
                !fSMBase.animator.GetBool("IsInteracting"))
                return true;
            else if (PlayerFSMBase.Instance.playerInfo.IsInDownClimbBox &&
                PlayerFSMBase.Instance.playerInput.RawVerticalMove == 1f &&
                !fSMBase.animator.GetBool("IsInteracting"))
                return true;
            return false;
        }

        public override void Init()
        {
            triggerID = FSMTriggerID.ClimbEndInput;
        }
    }
}
