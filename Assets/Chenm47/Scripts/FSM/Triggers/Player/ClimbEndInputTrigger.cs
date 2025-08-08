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
            //进入相应box，并且有相应输入
            if (PlayerFSMBase.Instance.playerInfo.IsInUpClimbBox && PlayerFSMBase.Instance.playerInput.VerticalMove <-0.1f)
                return true;
            else if (PlayerFSMBase.Instance.playerInfo.IsInDownClimbBox && PlayerFSMBase.Instance.playerInput.VerticalMove >0.1f)
                return true;
            return false;
        }

        public override void Init()
        {
            triggerID = FSMTriggerID.ClimbEndInput;
        }
    }
}
