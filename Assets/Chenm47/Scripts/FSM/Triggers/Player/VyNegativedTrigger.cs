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
    public class VyNegativedTrigger : FSMTrigger
    {
        public override bool HandleTrigger(FSMBase fSMBase)
        {
            PlayerFSMBase playerFSMBase = fSMBase as PlayerFSMBase;
            var v = playerFSMBase.playerAction.GetVelocity();
            return v.y < 0f;
        }

        public override void Init()
        {
            triggerID = FSMTriggerID.VyNegatived;
        }
    }
}
