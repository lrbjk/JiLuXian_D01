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
    public class ClimbQuickDownCancelTrigger : FSMTrigger
    {
        public override bool HandleTrigger(FSMBase fSMBase)
        {
            return !PlayerFSMBase.Instance.playerInput.RollHold;
        }

        public override void Init()
        {
            triggerID = FSMTriggerID.ClimbQuickDownCancel;
        }
    }
}
