using AI.FSM.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*

*/
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
            fSMBase.animationHandler.PlayTargetAnimation("FallEnd", true, 0.01f);
        }

    }
}
