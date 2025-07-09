using AI.FSM.Framework;
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
        public override void Init()
        {
            StateID = FSMStateID.BackStep;
        }


        public override void EnterState(FSMBase fSMBase)
        {
            base.EnterState(fSMBase);
            var playerFSM = fSMBase as PlayerFSMBase;

            Vector3 moveDir = -playerFSM.transform.forward;
            moveDir.y = 0;
            moveDir.Normalize();
            //无需转向
            playerFSM.playerAction.LookAndMove(Vector3.zero, moveDir, playerFSM.playerInfo.BackStepSpeed);

            playerFSM.playerAnimationHandler.PlayTargetAnimation("BackStep", true);
        }
    }
}
