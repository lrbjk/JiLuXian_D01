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
    public class ToFallEndTimerAndOnGroundTrigger : OnGroundTrigger
    {
        public override void Init()
        {
            triggerID = FSMTriggerID.ToFallEndTimerAndOnGround;
        }

        public override bool HandleTrigger(FSMBase fSMBase)
        {
            PlayerInfo playerInfo = fSMBase.characterInfo as PlayerInfo;
            //Debug.Log(playerInfo.FallTimer);
            return base.HandleTrigger(fSMBase) &&
                playerInfo.FallTimer > 0.3f;//下落时间大于0.3f
        }
    }
}
