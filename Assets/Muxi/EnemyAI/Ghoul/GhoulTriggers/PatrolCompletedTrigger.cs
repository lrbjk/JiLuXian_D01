using AI.FSM.Framework;
using EnemyAIBase;
using UnityEngine;

namespace AI.FSM
{
    /// <summary>
    /// 巡逻完成触发器
    /// </summary>
    public class PatrolCompletedTrigger : FSMTrigger
    {
        public override void Init()
        {
            triggerID = FSMTriggerID.PatrolCompleted;
        }

        public override bool HandleTrigger(FSMBase fSMBase)
        {
            Ghoul ghoul = fSMBase.GetComponent<Ghoul>();
            if (ghoul == null) return false;

            // 检查当前Goal状态
            var currentGoal = ghoul.Brain?.GetGoalManager()?.CurrentGoal;
            bool isInBattleMode = currentGoal is Goal_Ghoul_Battle;
            
            // 如果在战斗模式，不检查巡逻完成
            if (isInBattleMode) return false;

            // 如果有敌人目标，不检查巡逻完成
            Transform target = ghoul.GetTarget();
            if (target != null) return false;

            // 检查是否有巡逻Goal且已完成
            bool patrolCompleted = IsPatrolCompleted(currentGoal);
            
            if (patrolCompleted)
            {
                Debug.Log("[PatrolCompleted] 巡逻任务完成，返回Idle状态");
            }
            
            return patrolCompleted;
        }

        // 检查巡逻是否完成
        private bool IsPatrolCompleted(IAIGoal currentGoal)
        {
            if (currentGoal == null) return false;

            // 检查是否是Peaceful Goal且巡逻相关的SubGoal已完成
            if (currentGoal is Goal_Ghoul_Peaceful peacefulGoal)
            {
                // 检查是否有巡逻相关的SubGoal但没有活跃的SubGoal
                bool hasPatrolGoal = peacefulGoal.HasSubGoalOfType<Goal_Ghoul_Patrol>();
                bool hasMoveGoal = peacefulGoal.HasSubGoalOfType<Goal_MoveToSomeWhere>();
                bool hasActiveSubGoals = peacefulGoal.HasActiveSubGoals();

                // 如果有巡逻Goal但没有活跃的SubGoal，说明巡逻完成
                if ((hasPatrolGoal || hasMoveGoal) && !hasActiveSubGoals)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
