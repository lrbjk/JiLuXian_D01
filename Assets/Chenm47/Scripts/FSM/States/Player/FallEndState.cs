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
            fSMBase.animationHandler.PlayTargetAnimation("FallEnd", true, 0.1f);
        }
    }
}
