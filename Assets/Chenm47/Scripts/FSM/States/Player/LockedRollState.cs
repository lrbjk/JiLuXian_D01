using AI.FSM.Framework;
using ns.Movtion;

namespace AI.FSM
{
    /// <summary>
    /// 描述：
    /// </summary>
    public class LockedRollState : MovtionState
    {
        public override void Init()
        {
            StateID = FSMStateID.LockedRoll;
        }

        protected override MovtionInfo InitMovtionInfo(FSMBase fSMBase)
        {
            return fSMBase.movtionManager.GetMovtionInfo(PlayerFSMBase.Instance.playerInfo.LockedRollMovtionID);
        }

        protected override void PlayAnimation(FSMBase fSMBase)
        {
            //只需要播放动画，不用混合
            float horizontalMove = PlayerFSMBase.Instance.playerInput.HorizontalMove;
            if (horizontalMove != 0)
            {
                fSMBase.animator.SetFloat("Horizontal", horizontalMove > 0 ? 1 : -1);
                fSMBase.animator.SetFloat("Vertical", 0);
            }
            else
            {
                fSMBase.animator.SetFloat("Horizontal", 0);
                fSMBase.animator.SetFloat("Vertical", PlayerFSMBase.Instance.playerInput.VerticalMove > 0 ? 1 : -1);
            }

            //正常过度默认参数(offeset)无法循环播放
            fSMBase.animator.SetBool("IsInteracting", true);
            fSMBase.animator.CrossFade(movtionInfo.AnimationName, 0.05f, -1, 0f);
            //fSMBase.animator.Play(movtionInfo.AnimationName, -1, 0f);
        }

        public override void EnterState(FSMBase fSMBase)
        {
            base.EnterState(fSMBase);
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
