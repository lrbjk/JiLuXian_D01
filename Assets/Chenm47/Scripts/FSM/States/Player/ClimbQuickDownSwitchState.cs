using AI.FSM.Framework;
using UnityEngine;

namespace AI.FSM
{
    public class ClimbQuickDownSwitchState : FSMState
    {
        public override void Init()
        {
            StateID = FSMStateID.ClimbQuickDownSwitch;
        }

        public override void EnterState(FSMBase fSMBase)
        {
            base.EnterState(fSMBase);
            Debug.Log("EndState:IsClimbLiftHandDown" + PlayerFSMBase.Instance.playerInfo.IsClimbLiftHandDown);
            fSMBase.animator.SetFloat("Horizontal", PlayerFSMBase.Instance.playerInfo.IsClimbLiftHandDown ? -1 : 1);
            fSMBase.animationHandler.PlayTargetAnimation("ClimbDownFastToUp", true, 0.2f);
        }

    }
}
