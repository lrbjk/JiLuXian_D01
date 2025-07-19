using AI.FSM.Framework;
using ns.Character.Player;
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
            PlayerInfo playerInfo = fSMBase.characterInfo as PlayerInfo;
            float moveX = fSMBase.playerInput.HorizontalMove;
            float moveY = fSMBase.playerInput.VerticalMove;
            float movement = Mathf.Clamp01(Mathf.Abs(moveX) + Mathf.Abs(moveY));

            float moveSpeed = playerInfo.MoveBaseSpeed;
            if (fSMBase.playerInput.RollHoldTrigger) //如果是长按
            {
                moveSpeed = playerInfo.SprintSpeed;//冲刺状态
                movement = 2f;
            }

            Vector3 moveDir = fSMBase.cameraHandler.transform.right * moveX +
                fSMBase.cameraHandler.transform.forward * moveY;
            moveDir.y = 0;
            moveDir.Normalize();
            fSMBase.playerAction.Move(moveDir, moveSpeed);
            fSMBase.animator.SetFloat("Vertical", movement, 0.1f, Time.deltaTime);
        }

    }
}
