using AI.FSM.Framework;
using UnityEngine;

/*

*/
namespace AI.FSM
{
    /// <summary>
    /// 描述：
    /// </summary>
    public class JumpInAirState : FSMState
    {
        public override void Init()
        {
            StateID = FSMStateID.JumpInAir;
        }

        public override void EnterState(FSMBase fSMBase)
        {
            base.EnterState(fSMBase);
            fSMBase.animator.Play("JumpInAir");
        }

    }
}
