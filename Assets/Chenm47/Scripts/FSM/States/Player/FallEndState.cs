using AI.FSM.Framework;

namespace AI.FSM
{
    /// <summary>
    /// 描述：
    /// </summary>
    public class FallEndState : FSMState
    {
        public override void Init()
        {
            StateID = FSMStateID.FallEnd;
        }

        public override void EnterState(FSMBase fSMBase)
        {
            base.EnterState(fSMBase);
            //使用rootmovtion
            //PlayerFSMBase.Instance.playerMotor3D.ApplyAnimaMotionXZ = true;
            fSMBase.animationHandler.PlayTargetAnimation("FallEnd", true, 0.1f);
        }
        //public override void ExitState(FSMBase fSMBase)
        //{
        //    base.ExitState(fSMBase);
        //    PlayerFSMBase.Instance.playerMotor3D.ApplyAnimaMotionXZ = false;
        //}
    }
}
