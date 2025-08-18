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
    public class ClimbQuickDownState : FSMState
    {
        public override void Init()
        {
            StateID = FSMStateID.ClimbQuickDown;
        }

        public override void EnterState(FSMBase fSMBase)
        {
            base.EnterState(fSMBase);
            //播放下落动画
            fSMBase.animationHandler.PlayTargetAnimation("ClimbQuickDownStart", false, 0.1f);
        }

        public override void ExitState(FSMBase fSMBase)
        {
            base.ExitState(fSMBase);
            fSMBase.animator.SetFloat("Horizontal", 0f);
            fSMBase.animator.SetFloat("Vertical", -2f);
        }

    }
}
