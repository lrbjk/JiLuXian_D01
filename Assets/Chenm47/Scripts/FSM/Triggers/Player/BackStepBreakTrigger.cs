using AI.FSM.Framework;
using ns.Character.Player;
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
    public class BackStepBreakTrigger : BackStepInputTrigger
    {
        public override bool HandleTrigger(FSMBase fSMBase)
        {
            var playerInfo = fSMBase.characterInfo;
            return playerInfo.IsInMovtionRecoveryFlag
                && base.HandleTrigger(fSMBase);
        }

        public override void Init()
        {
            triggerID = FSMTriggerID.BackStepBreak;
        }
    }
}
