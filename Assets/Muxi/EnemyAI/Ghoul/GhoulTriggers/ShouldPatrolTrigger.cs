using AI.FSM.Framework;
using EnemyAIBase;
using UnityEngine;

namespace AI.FSM
{
    /// <summary>
    /// 应该巡逻触发器
    /// </summary>
    public class ShouldPatrolTrigger : FSMTrigger
    {
        private float lastIdleTime = 0f;
        private float idleThreshold = 3f; // 待机3秒后开始巡逻

        public override void Init()
        {
            triggerID = FSMTriggerID.ShouldPatrol;
        }

        public override bool HandleTrigger(FSMBase fSMBase)
        {
            Ghoul ghoul = fSMBase.GetComponent<Ghoul>();
            if (ghoul == null) return false;

            // 检查当前Goal状态
            var currentGoal = ghoul.Brain?.GetGoalManager()?.CurrentGoal;
            bool isInBattleMode = currentGoal is Goal_Ghoul_Battle;

            // 如果在战斗模式，不应该巡逻
            if (isInBattleMode)
            {
                lastIdleTime = 0f;
                return false;
            }

            // 如果有敌人目标，不应该巡逻
            Transform target = ghoul.GetTarget();
            if (target != null)
            {
                // 重置待机时间
                lastIdleTime = 0f;
                return false;
            }

            // 检查是否有活跃的巡逻Goal
            bool hasPatrolGoal = HasActivePatrolGoal(currentGoal);

            // 检查当前状态是否为待机状态
            if (fSMBase.CurrentState is GhoulIdleState)
            {
                // 如果已经有巡逻Goal但还在Idle状态，应该切换到Walking
                if (hasPatrolGoal)
                {
                    Debug.Log("[ShouldPatrol] 检测到活跃的巡逻Goal，切换到Walking");
                    return true;
                }

                // 检查待机时间是否超过阈值（创建新的巡逻）
                if (lastIdleTime == 0f)
                {
                    lastIdleTime = Time.time;
                }

                bool shouldStartPatrol = Time.time - lastIdleTime > idleThreshold;

                // 添加调试信息
                // if (Time.frameCount % 120 == 0) // 每2秒输出一次
                // {
                //     Debug.Log($"[ShouldPatrol] HasTarget: {target != null}, HasPatrolGoal: {hasPatrolGoal}, IdleTime: {Time.time - lastIdleTime:F1}/{idleThreshold}, ShouldPatrol: {shouldStartPatrol}");
                // }

                return shouldStartPatrol;
            }
            else
            {
                // 重置待机时间
                lastIdleTime = 0f;
                return false;
            }
        }

        // 检查是否有活跃的巡逻相关Goal
        private bool HasActivePatrolGoal(IAIGoal currentGoal)
        {
            if (currentGoal == null) return false;

            // 检查是否是Peaceful Goal且有巡逻相关的SubGoal
            if (currentGoal is Goal_Ghoul_Peaceful peacefulGoal)
            {
                // 检查是否有巡逻或移动相关的SubGoal
                bool hasPatrolGoal = peacefulGoal.HasSubGoalOfType<Goal_Ghoul_Patrol>();
                bool hasMoveGoal = peacefulGoal.HasSubGoalOfType<Goal_MoveToSomeWhere>();
                bool hasActiveSubGoals = peacefulGoal.HasActiveSubGoals();

                return (hasPatrolGoal || hasMoveGoal) && hasActiveSubGoals;
            }

            return false;
        }
    }
}
