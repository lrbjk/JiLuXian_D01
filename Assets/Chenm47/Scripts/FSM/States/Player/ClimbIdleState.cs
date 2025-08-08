using AI.FSM.Framework;

namespace AI.FSM
{
    /// <summary>
    /// 描述：
    /// </summary>
    public class ClimbIdleState : FSMState
    {
        public override void Init()
        {
            StateID = FSMStateID.ClimbIdle;
        }

        public override void EnterState(FSMBase fSMBase)
        {
            base.EnterState(fSMBase);
            //-1 左手在下   +1左手在上
            fSMBase.animationHandler.PlayTargetAnimation("ClimbIdle", false, 0.2f);
        }

    }
}
