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
    public class IsDamagedTrigger : FSMTrigger
    {
        public override bool HandleTrigger(FSMBase fSMBase)
        {
            return fSMBase.characterInfo.IsDamaged;
        }

        public override void Init()
        {
            triggerID = FSMTriggerID.IsDamaged;
        }
    }
}
