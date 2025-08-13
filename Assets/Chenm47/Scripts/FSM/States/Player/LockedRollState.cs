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
            float horizontalMove = PlayerFSMBase.Instance.playerInput.RawHorizontalMove;
            float verticalMove = PlayerFSMBase.Instance.playerInput.RawVerticalMove;
            //八向
            fSMBase.animator.SetFloat("Vertical", verticalMove);
            fSMBase.animator.SetFloat("Horizontal", horizontalMove);
            //if (verticalMove != 0)
            //{//纵轴优先
            //    fSMBase.animator.SetFloat("Vertical", verticalMove);
            //    fSMBase.animator.SetFloat("Horizontal", 0);
            //}
            //else
            //{
            //    fSMBase.animator.SetFloat("Horizontal", horizontalMove);
            //    fSMBase.animator.SetFloat("Vertical", 0);
            //}

            //正常过度默认参数(offeset)无法循环播放
            fSMBase.animator.SetBool("IsInteracting", true);
            fSMBase.animator.CrossFade(movtionInfo.AnimationName, 0.05f, -1, 0f);
            //fSMBase.animator.Play(movtionInfo.AnimationName, -1, 0f);
        }

        public override void EnterState(FSMBase fSMBase)
        {
            base.EnterState(fSMBase);
            //直接应用rootmotion
            PlayerFSMBase.Instance.playerMotor3D.ApplyAnimaMotionAll = true;
        }
        public override void ExitState(FSMBase fSMBase)
        {
            base.ExitState(fSMBase);
            PlayerFSMBase.Instance.playerMotor3D.ApplyAnimaMotionAll = false;
        }
    }
}
