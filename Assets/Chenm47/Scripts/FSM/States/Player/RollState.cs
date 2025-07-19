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

            playerFSMBase.playerAction.Move(moveDir, playerInfo.RollSpeed);

        }

        public override void ActionState(FSMBase fSMBase)
        {
            base.ActionState(fSMBase);
            //速度衰减效果......
        }
    }
}
