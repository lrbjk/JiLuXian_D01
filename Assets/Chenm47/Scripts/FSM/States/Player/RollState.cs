using AI.FSM.Framework;
using ns.Character.Player;
using ns.Movtion;
using UnityEngine;

namespace AI.FSM
{
    /// <summary>
    /// 描述：
    /// </summary>
    public class RollState : MovtionState
    {
        private PlayerFSMBase playerFSMBase;
        private PlayerInfo playerInfo;
        public override void Init()
        {
            StateID = FSMStateID.Roll;
        }
        protected override MovtionInfo InitMovtionInfo(FSMBase fSMBase)
        {
            playerInfo = fSMBase.characterInfo as PlayerInfo;

            return fSMBase.movtionManager.GetMovtionInfo(playerInfo.RollMovtionID);
        }

        protected override void PlayAnimation(FSMBase fSMBase)
        {
            //正常过度默认参数(offeset)无法循环播放
            fSMBase.animator.SetBool("IsInteracting", true);
            fSMBase.animator.CrossFade(movtionInfo.AnimationName, 0.1f, -1, 0f);
        }

        public override void EnterState(FSMBase fSMBase)
        {
            base.EnterState(fSMBase);
            playerFSMBase = fSMBase as PlayerFSMBase;
            float moveX = playerFSMBase.playerInput.HorizontalMove;
            float moveY = playerFSMBase.playerInput.VerticalMove;
            Vector3 moveDir = playerFSMBase.cameraHandler.transform.right * moveX +
                            playerFSMBase.cameraHandler.transform.forward * moveY;
            moveDir.y = 0;
            moveDir.Normalize();

            //playerFSMBase.playerAction.Move(moveDir, playerInfo.RollSpeed);

            //转向
            playerFSMBase.playerMotor3D.LookAtVector(moveDir, 25);

            //直接应用rootmotion
            playerFSMBase.playerRootMotion.ApplyAnimaMotionAll = true;
        }

        public override void ExitState(FSMBase fSMBase)
        {
            base.ExitState(fSMBase);
            playerFSMBase.playerRootMotion.ApplyAnimaMotionAll = false;
        }
    }
}
