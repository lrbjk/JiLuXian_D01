using AI.FSM.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace AI.FSM
{
    /// <summary>
    /// 描述：
    /// </summary>
    public class JumpState : FSMState
    {
        public override void Init()
        {
            StateID = FSMStateID.Jump;
        }

        public override void EnterState(FSMBase fSMBase)
        {
            base.EnterState(fSMBase);
            PlayerFSMBase playerFSMBase = fSMBase as PlayerFSMBase;
            //此时由动画控制y速度，并记录当前其他轴速度
            Vector3 v = playerFSMBase.playerAction.GetVelocity();
            playerFSMBase.playerRootMotion.BeforeApplySpeed = v;
            playerFSMBase.playerRootMotion.ApplyAnimaMotionY = true;

            //playerFSMBase.playerAction.Jump();

            //播放动画
            fSMBase.animationHandler.PlayTargetAnimation("Jump", true, 0.1f);
        }

        public override void ActionState(FSMBase fSMBase)
        {
            base.ActionState(fSMBase);
            PlayerFSMBase playerFSMBase = (PlayerFSMBase)fSMBase;

            fSMBase.animator.SetFloat("Vy", playerFSMBase.playerAction.GetVelocity().y);
        }

        public override void ExitState(FSMBase fSMBase)
        {
            base.ExitState(fSMBase);
            PlayerFSMBase playerFSMBase = fSMBase as PlayerFSMBase;
            //此时取消由动画控制y速度
            //y速度恒定为最后此时速度，其他速度归零
            playerFSMBase.playerRootMotion.BeforeApplySpeed = Vector3.zero;
            playerFSMBase.playerRootMotion.ApplyAnimaMotionY = false;
            //playerFSMBase.playerAction.SetVelocity(new Vector3(0, lastFallVy, 0));
        }

    }
}
