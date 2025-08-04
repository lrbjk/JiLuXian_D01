using AI.FSM.Framework;
using UnityEngine;

namespace AI.FSM
{
    /// <summary>
    /// 描述：
    /// </summary>
    public class IsDiedTrigger : FSMTrigger
    {
        public override bool HandleTrigger(FSMBase fSMBase)
        {
            bool isDied = fSMBase.characterInfo.IsDied;

          
            if (isDied && Time.frameCount % 600 == 0) 
            {
                Debug.Log($"[IsDiedTrigger] 检测到死亡状态，当前状态: {fSMBase.CurrentStateName}");
            }

            return isDied;
        }

        public override void Init()
        {
            triggerID = FSMTriggerID.IsDied;
        }
    }
}
