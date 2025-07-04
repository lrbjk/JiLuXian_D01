using AI.FSM.Framework;
using UnityEngine;
namespace AI.FSM
{
    /// <summary>
    /// 描述：
    /// </summary>
    public class TestState : FSMState
    {
        public override void Init()
        {
            StateID = FSMStateID.Test;
        }

        public override void ActionState(FSMBase fSMBase)
        {
            var playerFSM = fSMBase as PlayerFSMBase;
            base.ActionState(playerFSM);
            //人物移动控制
            MoveMentHandle(playerFSM);
        }

        private static void MoveMentHandle(PlayerFSMBase fSMBase)
        {
            float moveX = fSMBase.playerInput.HorizontalMove;
            float moveY = fSMBase.playerInput.VerticalMove;
            float movement = Mathf.Clamp01(Mathf.Abs(moveX) + Mathf.Abs(moveY));
            float moveSpeed = fSMBase.playerInfo.MoveBaseSpeed;
            Vector3 moveDir = fSMBase.cameraHandler.transform.right * moveX +
                fSMBase.cameraHandler.transform.forward * moveY;
            moveDir.y = 0;
            moveDir.Normalize();
            fSMBase.playerAction.Move(moveDir, moveSpeed);
            fSMBase.animator.SetFloat("Vertical", movement, 0.05f, Time.deltaTime);
        }

    }
}
