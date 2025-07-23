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
    public class ForwardStabTrigger : AttackInputTrigger
    {
        public override bool HandleTrigger(FSMBase fSMBase)
        {
            PlayerFSMBase playerFSMBase = fSMBase as PlayerFSMBase;

            return base.HandleTrigger(fSMBase) &&
                playerFSMBase.playerAction.IsForwardStabOrRiposte();//内部球形检测范围内是否有满足目标
        }

        public override void Init()
        {
            triggerID = FSMTriggerID.ForwardStab;
        }
    }
}
