using AI.FSM.Framework;

namespace AI.FSM
{
    /// <summary>
    /// 描述：
    /// </summary>
    public class ClimbStartInputTrigger : FSMTrigger
    {
        private PlayerFSMBase playerFSMBase;
        public override bool HandleTrigger(FSMBase fSMBase)
        {
            if (playerFSMBase == null)
            {
                playerFSMBase = fSMBase as PlayerFSMBase;
            }
            //进入了BoxTrigger，并且按下交互键
            return playerFSMBase.playerInput.Interacting &&
                (PlayerFSMBase.Instance.playerInfo.IsInDownClimbBox ||
                PlayerFSMBase.Instance.playerInfo.IsInUpClimbBox);
        }

        public override void Init()
        {
            triggerID = FSMTriggerID.ClimbStartInput;
        }
    }
}
