using AI.FSM.Framework;
using Common;
using System;
using UnityEngine;

namespace AI.FSM
{
    /// <summary>
    /// 描述：
    /// </summary>
    public class RollState : FSMState
    {
        private PlayerFSMBase playerFSMBase;

        public override void Init()
        {
            StateID = FSMStateID.Roll;
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
            //订阅事件
            playerFSMBase.animationEventBehaviour.OnAttackRecovery += OnAttackRecovery;

            playerFSMBase.playerAction.Move(moveDir, playerFSMBase.playerInfo.RollSpeed);
            playerFSMBase.playerAnimationHandler.PlayTargetAnimation("Roll", true);
        }

        public override void ExitState(FSMBase fSMBase)
        {
            base.ExitState(fSMBase);
            //取消订阅
            playerFSMBase.animationEventBehaviour.OnAttackRecovery -= OnAttackRecovery;

            //后摇结束
            playerFSMBase.playerInfo.IsInAttackRecoveryFlag = false;
        }

        private void OnAttackRecovery(object sender, AnimationEventArgs e)
        {
            Debug.Log("动画事件AttackRecovery");
            //后摇开始
            playerFSMBase.playerInfo.IsInAttackRecoveryFlag = true;
        }

        public override void ActionState(FSMBase fSMBase)
        {
            base.ActionState(fSMBase);
            //速度衰减效果......
        }

    }
}
