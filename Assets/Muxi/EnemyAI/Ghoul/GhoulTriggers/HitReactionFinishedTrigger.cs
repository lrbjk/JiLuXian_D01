using AI.FSM.Framework;
using UnityEngine;

namespace AI.FSM
{
    /// <summary>
    /// 受击反应完成触发器
    /// </summary>
    public class HitReactionFinishedTrigger : FSMTrigger
    {
        public override void Init()
        {
            triggerID = FSMTriggerID.HitReactionFinished;
        }

        public override bool HandleTrigger(FSMBase fSMBase)
        {
            // 检查当前状态是否为受击反应状态，并且反应已完成
            if (fSMBase.CurrentState is GhoulReactionToHitState reactionState)
            {
                return reactionState.IsReactionFinished();
            }
            
            // 如果不在受击反应状态，检查是否不在动作后摇阶段
            return !fSMBase.characterInfo.IsInMovtionRecoveryFlag;
        }
    }
}
