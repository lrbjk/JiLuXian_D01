using AI.FSM.Framework;
using ns.Character.Player;
using UnityEngine;


namespace AI.FSM
{
    /// <summary>
    /// 描述：
    /// </summary>
    public class MoveState : FSMState
    {
        private float movement;

        public override void Init()
        {
            StateID = FSMStateID.Move;
        }

        public override void ActionState(FSMBase fSMBase)
        {
            base.ActionState(fSMBase);
            var playerFSM = fSMBase as PlayerFSMBase;

            //人物移动控制
            MovementHandle(playerFSM);

            //移动动画设置
            if (playerFSM.playerInput.LockViewTrigger)//如果是锁定状态，八位移动
            {
                playerFSM.animator.SetFloat("Vertical", movement, 0.1f, Time.deltaTime);
                playerFSM.animator.SetFloat("Horizontal", playerFSM.playerInput.HorizontalMove, 0.1f, Time.deltaTime);
                playerFSM.animator.Play("LockedMovement");
            }
            else
            {
                playerFSM.animator.SetFloat("Vertical", movement, 0.1f, Time.deltaTime);
                playerFSM.animator.Play("Movement");
            }
        }


        private void MovementHandle(PlayerFSMBase playerFSM)
        {
            PlayerInfo playerInfo = playerFSM.characterInfo as PlayerInfo;

            //移动处理
            float moveX = playerFSM.playerInput.HorizontalMove;
            float moveY = playerFSM.playerInput.VerticalMove;
            movement = Mathf.Clamp01(Mathf.Abs(moveX) + Mathf.Abs(moveY));
            float moveSpeed = playerInfo.MoveBaseSpeed;

            if (playerFSM.playerInput.RollHoldTrigger && playerFSM.playerInput.MovementHoldTrigger)
            {
                moveSpeed = playerInfo.SprintSpeed;//冲刺状态
                movement = 2f;
            }

            Vector3 moveDir = playerFSM.cameraHandler.transform.right * moveX +
                playerFSM.cameraHandler.transform.forward * moveY;

            moveDir.y = 0;
            moveDir.Normalize();
            if (!playerFSM.playerInput.LockViewTrigger || playerFSM.playerInput.RollHoldTrigger)
            {
                playerFSM.playerAction.Move(moveDir, moveSpeed);
            }
            else
            {//锁定视角情况
                Vector3 lookDir =
                   playerInfo.LockedTargetTF.position - playerInfo.LockedTF.position;
                lookDir.Set(lookDir.x, 0, lookDir.z);
                //Debug.DrawRay(playerFSM.transform.position, lookDir.normalized * 3f, Color.red);
                playerFSM.playerAction.LookAndMove(lookDir, moveDir, moveSpeed);//只在xz平面旋转即可
            }
        }

    }

}
