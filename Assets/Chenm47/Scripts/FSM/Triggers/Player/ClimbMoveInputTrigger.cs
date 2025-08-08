using AI.FSM.Framework;

namespace AI.FSM
{
    /// <summary>
    /// 描述：
    /// </summary>
    public class ClimbMoveInputTrigger : FSMTrigger
    {
        private PlayerFSMBase playerFSMBase;
        public override bool HandleTrigger(FSMBase fSMBase)
        {
            if (playerFSMBase == null)
            {
                playerFSMBase = fSMBase as PlayerFSMBase;
            }
            //有垂直方向输入
            return playerFSMBase.playerInput.VerticalMove > 0.1f ||
                  playerFSMBase.playerInput.VerticalMove < -0.1f;
        }

        public override void Init()
        {
            triggerID = FSMTriggerID.ClimbMoveInput;
        }
    }
}
