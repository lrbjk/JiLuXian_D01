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
        public override void Init()
        {
            StateID = FSMStateID.BackStep;
        }
        protected override MovtionInfo InitMovtionInfo(FSMBase fSMBase)
        {
            return fSMBase.movtionManager.GetMovtionInfo((fSMBase.characterInfo as PlayerInfo).BackStepMovtionID);
        }
        override protected void PlayAnimation(FSMBase fSMBase)
        {
            //正常过度默认参数(offeset)无法循环播放
            fSMBase.animator.SetBool("IsInteracting", true);
            fSMBase.animator.CrossFade(movtionInfo.AnimationName, 0.1f, -1, 0f);
        }

        public override void EnterState(FSMBase fSMBase)
        {
            base.EnterState(fSMBase);

            //playerFSMBase = fSMBase as PlayerFSMBase;
            //PlayerInfo playerInfo = playerFSMBase.characterInfo as PlayerInfo;

            //Vector3 moveDir = -playerFSMBase.transform.forward;
            //moveDir.y = 0;
            //moveDir.Normalize();
            ////无需转向
            //playerFSMBase.playerAction.LookAndMove(Vector3.zero, moveDir, playerInfo.BackStepSpeed);

            //直接应用rootmotion
            PlayerFSMBase.Instance.playerRootMotion.ApplyAnimaMotionAll = true;
        }

        public override void ExitState(FSMBase fSMBase)
        {
            base.ExitState(fSMBase);
            PlayerFSMBase.Instance.playerRootMotion.ApplyAnimaMotionAll = false;
        }

    }
}
