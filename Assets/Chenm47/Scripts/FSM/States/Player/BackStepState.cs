using AI.FSM.Framework;
using Common;
using UnityEngine;

/*

*/
namespace AI.FSM
{
    /// <summary>
    /// 描述：
    /// </summary>
    public class BackStepState : FSMState
    {
        private PlayerFSMBase playerFSMBase;

        public override void Init()
        {
            StateID = FSMStateID.BackStep;
        }


        public override void EnterState(FSMBase fSMBase)
        {
            base.EnterState(fSMBase);
            playerFSMBase = fSMBase as PlayerFSMBase;

            Vector3 moveDir = -playerFSMBase.transform.forward;
            moveDir.y = 0;
            moveDir.Normalize();
            //无需转向
            playerFSMBase.playerAction.LookAndMove(Vector3.zero, moveDir, playerFSMBase.playerInfo.BackStepSpeed);

            //订阅事件
            playerFSMBase.animationEventBehaviour.OnAttackRecovery += OnAttackRecovery;

            playerFSMBase.playerAnimationHandler.PlayTargetAnimation("BackStep", true);
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

    }
}
