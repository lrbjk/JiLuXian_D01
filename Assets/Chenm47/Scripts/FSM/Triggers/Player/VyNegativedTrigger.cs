using AI.FSM.Framework;
using UnityEngine;

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
            //Debug.Log(Time.frameCount.ToString() + "y" + v.y);
            return v.y < -0.1f;
        }

        public override void Init()
        {
            triggerID = FSMTriggerID.VyNegatived;
        }
    }
}
