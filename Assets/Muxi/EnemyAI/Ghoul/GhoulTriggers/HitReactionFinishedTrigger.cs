using AI.FSM.Framework;
using EnemyAIBase;
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
    
            if (fSMBase.CurrentState is GhoulReactionToHitState reactionState)
            {
                bool isFinished = reactionState.IsReactionFinished();
                
                bool aiReady = CheckAISystemReady(fSMBase);
                

                return isFinished && aiReady;
            }

        
            return false;
        }

        private bool CheckAISystemReady(FSMBase fSMBase)
        {
            var ghoul = fSMBase.GetComponent<Ghoul>();
            if (ghoul?.Brain?.GetGoalManager() != null)
            {
                var currentGoal = ghoul.Brain.GetGoalManager().CurrentGoal;
                return currentGoal == null ||
                       currentGoal.Status == GoalStatus.Completed ||
                       currentGoal.Status == GoalStatus.Failed;
            }
            return true; 
        }
    }
}
