using AI.FSM.Framework;
using UnityEngine;

namespace AI.FSM
{
    /// <summary>
    /// 描述：
    /// </summary>
    public class OnGroundTrigger : FSMTrigger
    {
        public override bool HandleTrigger(FSMBase fSMBase)
        {
            PlayerFSMBase playerFSMBase = fSMBase as PlayerFSMBase;
            //Debug.Log("isOnground" + playerFSMBase.playerAction.IsOnGround());
            return playerFSMBase.playerAction.IsOnGround();//在地面
        }

        public override void Init()
        {
            triggerID = FSMTriggerID.OnGround;
        }
    }
}
