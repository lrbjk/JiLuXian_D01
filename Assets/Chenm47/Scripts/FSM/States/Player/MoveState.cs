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
        private bool isMovementLocked;
        private bool isChangeToRun = false;

        public override void Init()
        {
            StateID = FSMStateID.Move;
        }

        public override void EnterState(FSMBase fSMBase)
        {
            base.EnterState(fSMBase);
            PlayerFSMBase.Instance.playerMotor3D.ApplyAnimaMotionAll = true;

            if (!PlayerFSMBase.Instance.playerInput.LockViewTrigger || PlayerFSMBase.Instance.playerInput.RollHoldTrigger)
            {
                PlayerFSMBase.Instance.animationHandler.SetFloatDamp("Vertical", movement, 0.001f, 0.1f, Time.deltaTime);
                //playerFSM.animator.Play("Movement");
                PlayerFSMBase.Instance.animationHandler.PlayTargetAnimation("Movement", false, 0.1f);
                isMovementLocked = false;
            }
            else
            {//锁定视角情况
                PlayerFSMBase.Instance.animationHandler.SetFloatDamp("Vertical", PlayerFSMBase.Instance.playerInput.VerticalMove, 0.001f, 0.1f, Time.deltaTime);
                PlayerFSMBase.Instance.animationHandler.SetFloatDamp("Horizontal", PlayerFSMBase.Instance.playerInput.HorizontalMove, 0.001f, 0.1f, Time.deltaTime);
                //playerFSM.animator.Play("LockedMovement");
                PlayerFSMBase.Instance.animationHandler.PlayTargetAnimation("LockedMovement", false, 0.1f);
                isMovementLocked = true;
            }

        }

        public override void ActionState(FSMBase fSMBase)
        {
            base.ActionState(fSMBase);
            var playerFSM = fSMBase as PlayerFSMBase;

            //人物移动控制
            MovementHandle(playerFSM);



            if (playerFSM.playerInput.LockView)
            {
                //切换动画
                if (playerFSM.playerInput.LockViewTrigger && !isMovementLocked)
                {
                    Debug.Log("移动中切换锁定移动");
                    PlayerFSMBase.Instance.animationHandler.PlayTargetAnimation("LockedMovement", false, 0.1f);
                    isMovementLocked = true;
                }
                else if (!playerFSM.playerInput.LockViewTrigger && isMovementLocked)
                {
                    Debug.Log("移动中切换正常移动");
                    PlayerFSMBase.Instance.animationHandler.PlayTargetAnimation("Movement", false, 0.1f);
                    isMovementLocked = false;
                }
            }

            if (PlayerFSMBase.Instance.playerInput.RollHoldTrigger)
            {
                if (playerFSM.playerInput.LockViewTrigger && isMovementLocked)
                {
                    Debug.Log("锁定中切换正常移动");
                    PlayerFSMBase.Instance.animationHandler.PlayTargetAnimation("Movement", false, 0.1f);
                    isMovementLocked = false;
                }
            }
            else
            {
                if (playerFSM.playerInput.LockViewTrigger && !isMovementLocked)
                {
                    Debug.Log("锁定中切换锁定移动");
                    PlayerFSMBase.Instance.animationHandler.PlayTargetAnimation("LockedMovement", false, 0.1f);
                    isMovementLocked = true;
                }
            }

            //移动动画参数设置
            if (playerFSM.playerInput.LockViewTrigger && !PlayerFSMBase.Instance.playerInput.RollHoldTrigger)//如果是锁定状态，八位移动
            {
                playerFSM.animator.SetFloat("Vertical", playerFSM.playerInput.VerticalMove, 0.01f, Time.deltaTime);
                playerFSM.animator.SetFloat("Horizontal", playerFSM.playerInput.HorizontalMove, 0.01f, Time.deltaTime);
            }
            else
            {
                playerFSM.animator.SetFloat("Vertical", movement, 0.1f, Time.deltaTime);
            }

        }

        public override void ExitState(FSMBase fSMBase)
        {
            base.ExitState(fSMBase);
            PlayerFSMBase.Instance.playerMotor3D.ApplyAnimaMotionAll = false;
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
            #region 使用刚体运动
            //if (!playerFSM.playerInput.LockViewTrigger || playerFSM.playerInput.RollHoldTrigger)
            //{
            //    //playerFSM.playerAction.Move(moveDir, moveSpeed);
            //}
            //else
            //{//锁定视角情况
            //    //Vector3 lookDir =
            //    //   playerInfo.LockedTargetTF.position - playerInfo.LockedTF.position;
            //    //lookDir.Set(lookDir.x, 0, lookDir.z);
            //    ////Debug.DrawRay(playerFSM.transform.position, lookDir.normalized * 3f, Color.red);
            //    //playerFSM.playerAction.LookAndMove(lookDir, moveDir, moveSpeed);//只在xz平面旋转即可
            //}
            #endregion

            #region Rootmovtion
            if (!playerFSM.playerInput.LockViewTrigger || playerFSM.playerInput.RollHoldTrigger)
            {
                playerFSM.playerMotor3D.LookAtVector(moveDir);
            }
            else
            {//锁定视角情况
                Vector3 lookDir =
                   playerInfo.LockedTargetTF.position - playerInfo.LockedTF.position;
                lookDir.Set(lookDir.x, 0, lookDir.z);
                //Debug.DrawRay(playerFSM.transform.position, lookDir.normalized * 3f, Color.red);
                //playerFSM.playerAction.LookAndMove(lookDir, moveDir, moveSpeed);//只在xz平面旋转即可
                //playerFSM.playerAction.LookDir(lookDir, 1f);
                playerFSM.playerMotor3D.LookAtVector(lookDir);
            }
            #endregion

        }

    }

}
