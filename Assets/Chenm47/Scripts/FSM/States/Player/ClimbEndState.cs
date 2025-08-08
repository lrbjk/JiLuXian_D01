using AI.FSM.Framework;
using UnityEngine;

namespace AI.FSM
{
    /// <summary>
    /// 描述：
    /// </summary>
    public class ClimbEndState : FSMState
    {
        private PlayerFSMBase playerFSMBase;
        private float deltaY;
        public override void Init()
        {
            StateID = FSMStateID.ClimbEnd;
        }

        public override void EnterState(FSMBase fSMBase)
        {
            base.EnterState(fSMBase);
            if (playerFSMBase == null)
            {
                playerFSMBase = fSMBase as PlayerFSMBase;
            }
            //判断左右手
            fSMBase.animator.SetFloat("Horizontal", playerFSMBase.playerInfo.IsClimbLiftHandDown ? -1f : 1f);
            //判断上下输入
            fSMBase.animator.SetFloat("Vertical", playerFSMBase.playerInput.VerticalMove > 0 ? 1f : -1f);
            deltaY = playerFSMBase.transform.position.y;
            fSMBase.animationHandler.PlayTargetAnimation("ClimbEnd", true, 0.4f);

        }

        public override void ExitState(FSMBase fSMBase)
        {
            base.ExitState(fSMBase);
            fSMBase.animator.SetFloat("Horizontal", 0);
            fSMBase.animator.SetFloat("Vertical", 0);
            playerFSMBase.playerInfo.IsClimbLiftHandDown = true;//恢复默认
            //关闭rootmovtion
            //PlayerFSMBase.Instance.playerRootMotion.ApplyAnimaMotionY = false;
            PlayerFSMBase.Instance.playerRootMotion.ApplyAnimatRotationY = false;
            PlayerFSMBase.Instance.playerRootMotion.ApplyAnimaMotionAll = false;
            PlayerFSMBase.Instance.playerMotor3D.SetRbGravity(true);
            deltaY = playerFSMBase.transform.position.y - deltaY;
            Debug.Log("ClimbEndState ExitState deltaY:" + deltaY);
        }

    }
}
