using AI.FSM.Framework;
using Common;
using ns.Character.Player;
using ns.Movtion;
using UnityEngine;

/*

*/
namespace AI.FSM
{
    /// <summary>
    /// 描述：
    /// </summary>
    public class BackStepState : MovtionState
    {
        private PlayerFSMBase playerFSMBase;

        public override void Init()
        {
            StateID = FSMStateID.BackStep;
        }
        protected override MovtionInfo InitMovtionInfo(FSMBase fSMBase)
        {
            return fSMBase.movtionManager.GetMovtionInfo((fSMBase.characterInfo as PlayerInfo).BackStepMovtionID);
        }


        public override void EnterState(FSMBase fSMBase)
        {
            base.EnterState(fSMBase);
            playerFSMBase = fSMBase as PlayerFSMBase;
            PlayerInfo playerInfo = playerFSMBase.characterInfo as PlayerInfo;

            Vector3 moveDir = -playerFSMBase.transform.forward;
            moveDir.y = 0;
            moveDir.Normalize();
            //无需转向
            playerFSMBase.playerAction.LookAndMove(Vector3.zero, moveDir, playerInfo.BackStepSpeed);
        }

    }
}
