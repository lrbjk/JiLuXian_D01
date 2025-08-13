using AI.FSM.Framework;
using UnityEngine;

namespace AI.FSM
{
    /// <summary>
    /// 描述：
    /// </summary>
    public class IdleState : FSMState
    {
        public override void Init()
        {
            StateID = FSMStateID.Idle;
        }

        public override void EnterState(FSMBase fSMBase)
        {
            base.EnterState(fSMBase);
            var playerFSM = fSMBase as PlayerFSMBase;
            playerFSM.playerAction.StopMove();
            playerFSM.animator.SetFloat("Vertical", 0f, 0.1f, Time.deltaTime);
            playerFSM.animator.SetFloat("Horizontal", 0f, 0.1f, Time.deltaTime);
            playerFSM.animationHandler.PlayTargetAnimation("Idle", false, 0.25f);
            //playerFSM.animationHandler.PlayTargetAnimationFixed("Idle", false, 1f);
            //playerFSM.animator.CrossFadeInFixedTime("Idle", 1f, -1);
            //playerFSM.animator.Play("Idle");
        }

        public override void ActionState(FSMBase fSMBase)
        {
            base.ActionState(fSMBase);
            var playerFSM = fSMBase as PlayerFSMBase;
            playerFSM.animationHandler.SetFloatDamp("Vertical", 0f, 0.001f, 0.1f, Time.deltaTime);
            playerFSM.animationHandler.SetFloatDamp("Horizontal", 0f, 0.001f, 0.1f, Time.deltaTime);
            if (playerFSM.playerInput.LockViewTrigger)
            {//转向
                Vector3 lookDir =
                    playerFSM.characterInfo.LockedTargetTF.position - playerFSM.characterInfo.LockedTF.position;
                lookDir.Set(lookDir.x, 0, lookDir.z);
                playerFSM.playerMotor3D.LookAtVector(lookDir);
                //playerFSM.playerAction.LookAndMove(lookDir,Vector3.zero, 0);
            }
        }

    }
}
