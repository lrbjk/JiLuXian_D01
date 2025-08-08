using AI.FSM.Framework;
using UnityEngine;

namespace AI.FSM
{
    /// <summary>
    /// 描述：
    /// </summary>
    public class ClimbMoveState : FSMState
    {
        private PlayerFSMBase playerFSMBase;

        public override void Init()
        {
            StateID = FSMStateID.ClimbMove;
        }

        public override void EnterState(FSMBase fSMBase)
        {
            base.EnterState(fSMBase);
            if (playerFSMBase == null)
            {
                playerFSMBase = fSMBase as PlayerFSMBase;
            }
            //取反
            //上的时候取反
            playerFSMBase.playerInfo.IsClimbLiftHandDown = !playerFSMBase.playerInfo.IsClimbLiftHandDown;
            //判断左右手
            fSMBase.animator.SetFloat("Horizontal", playerFSMBase.playerInfo.IsClimbLiftHandDown ? -1f : 1f);
            Debug.Log("Left?:" + playerFSMBase.playerInfo.IsClimbLiftHandDown);
            //判断上下输入
            fSMBase.animator.SetFloat("Vertical", playerFSMBase.playerInput.VerticalMove > 0 ? 1f : -1f);

            fSMBase.animationHandler.PlayTargetAnimation("ClimpMove", true, 0.2f);
            //此时长按，加快动画播放
            if (playerFSMBase.playerInput.RollHold)
                fSMBase.animator.speed = 1.5f;
            else
                fSMBase.animator.speed = 1f;
        }
        public override void ExitState(FSMBase fSMBase)
        {
            base.ExitState(fSMBase);
            fSMBase.animator.speed = 1f;
        }
    }
}
