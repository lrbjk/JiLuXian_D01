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
            //playerFSMbase.animator.SetFloat("Vertical", 0f, 0.1f, Time.deltaTime);
            //playerFSMbase.animator.SetFloat("Horizontal", 0f, 0.1f, Time.deltaTime);
            playerFSM.animator.SetFloat("Vertical", 0f);
            playerFSM.animator.SetFloat("Horizontal", 0f);
            playerFSM.animationHandler.PlayTargetAnimation("Idle", false, 0.1f);
            //playerFSM.animator.Play("Idle");
        }

        public override void ActionState(FSMBase fSMBase)
        {
            base.ActionState(fSMBase);
            var playerFSM = fSMBase as PlayerFSMBase;
            if (playerFSM.playerInput.LockViewTrigger)
            {//转向
                Vector3 lookDir =
                    playerFSM.characterInfo.LockedTargetTF.position - playerFSM.characterInfo.LockedTF.position;
                lookDir.Set(lookDir.x, 0, lookDir.z);
                playerFSM.playerAction.LookAndMove(lookDir, Vector3.zero, 0);
            }
        }

    }
}
