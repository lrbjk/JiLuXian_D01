using AI.FSM.Framework;
using UnityEngine;

namespace AI.FSM
{
    /// <summary>
    /// 描述：
    /// </summary>
    public class RollState : FSMState
    {
        public override void Init()
        {
            StateID = FSMStateID.Roll;
        }

        public override void EnterState(FSMBase fSMBase)
        {
            base.EnterState(fSMBase);
            var playerFSM = fSMBase as PlayerFSMBase;
            float moveX = playerFSM.playerInput.HorizontalMove;
            float moveY = playerFSM.playerInput.VerticalMove;
            Vector3 moveDir = playerFSM.cameraHandler.transform.right * moveX +
                            playerFSM.cameraHandler.transform.forward * moveY;
            moveDir.y = 0;
            moveDir.Normalize();

            playerFSM.playerAction.Move(moveDir, playerFSM.playerInfo.RollSpeed);
            playerFSM.playerAnimationHandler.PlayTargetAnimation("Roll", true);
        }

        public override void ActionState(FSMBase fSMBase)
        {
            base.ActionState(fSMBase);
            //速度衰减效果......
        }

    }
}
