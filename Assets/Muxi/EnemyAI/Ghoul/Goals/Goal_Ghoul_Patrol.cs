using EnemyAIBase;
using AI.FSM;
using UnityEngine;

namespace EnemyAIBase
{
    /// <summary>
    /// Ghoul巡逻目标 - 使用Goal_MoveToSomeWhere进行NavMesh巡逻
    /// 集成AIPerception进行巡逻时的感知
    /// </summary>
    public class Goal_Ghoul_Patrol : AIGoal
    {
        protected new Ghoul owner;
        private AIPerception perception;
        private Vector3 patrolTarget;
        private float patrolRadius = 5f;
        private bool hasPatrolTarget = false;
        private Goal_MoveToSomeWhere currentMoveGoal;

        // 感知相关
        private float lastSuspicionLevel = 0f;
        private float patrolSpeedMultiplier = 1f;

        public Goal_Ghoul_Patrol(Ghoul owner) : base(owner)
        {
            this.owner = owner;
            this.perception = owner.GetComponent<AIPerception>();
        }

        public override void Activate()
        {
            base.Activate();

            // 设置随机巡逻目标点并开始移动
            SetRandomPatrolTarget();
            StartPatrolMovement();

            // 初始化感知状态
            lastSuspicionLevel = perception?.GetPlayerSuspicionLevel() ?? 0f;

            Debug.Log("Goal_Ghoul_Patrol: 激活巡逻目标");
        }

        public override GoalStatus Process()
        {
            // 检查AIPerception是否检测到玩家
            if (perception != null && perception.IsPlayerFullyDetected())
            {
                status = GoalStatus.Completed;
                Debug.Log("Goal_Ghoul_Patrol: AIPerception检测到玩家，巡逻结束");
                return status;
            }

            // 检查传统目标检测（备用）
            if (owner.GetTarget() != null)
            {
                status = GoalStatus.Completed;
                Debug.Log("Goal_Ghoul_Patrol: 发现目标，巡逻结束");
                return status;
            }

            // 监控感知状态并调整巡逻行为
            MonitorPerceptionDuringPatrol();

            // 处理移动SubGoal
            if (currentMoveGoal != null)
            {
                if (currentMoveGoal.Status == GoalStatus.Completed)
                {
                    // 到达巡逻点，决定下一步行动
                    if (ShouldContinuePatrol())
                    {
                        // 继续巡逻到新位置
                        SetRandomPatrolTarget();
                        StartPatrolMovement();
                    }
                    else
                    {
                        // 结束巡逻
                        status = GoalStatus.Completed;
                        return status;
                    }
                }
                else if (currentMoveGoal.Status == GoalStatus.Failed)
                {
                    // 移动失败，尝试新的巡逻点
                    SetRandomPatrolTarget();
                    StartPatrolMovement();
                }
            }

            // 处理子目标
            return base.Process();
        }

        private void MonitorPerceptionDuringPatrol()
        {
            if (perception == null) return;

            float currentSuspicion = perception.GetPlayerSuspicionLevel();

            // 检查怀疑度变化
            if (currentSuspicion > lastSuspicionLevel + 0.1f)
            {
                Debug.Log($"Goal_Ghoul_Patrol: 巡逻中怀疑度上升 {currentSuspicion:F2}");

                // 怀疑度上升时，调整巡逻行为
                if (currentSuspicion > 0.3f)
                {
                    // 减慢移动速度，更仔细地巡逻
                    patrolSpeedMultiplier = 0.7f;

                    // 缩小巡逻半径，在当前区域更仔细搜索
                    patrolRadius = Mathf.Max(patrolRadius * 0.8f, 2f);
                }
            }
            else if (currentSuspicion < lastSuspicionLevel - 0.1f)
            {
               

                // 怀疑度下降时，恢复正常巡逻
                patrolSpeedMultiplier = 1f;
                patrolRadius = 5f; // 恢复默认半径
            }

            lastSuspicionLevel = currentSuspicion;
        }

        private bool ShouldContinuePatrol()
        {
            // 根据怀疑度决定是否继续巡逻
            float suspicionLevel = perception?.GetPlayerSuspicionLevel() ?? 0f;

            if (suspicionLevel > 0.4f)
            {
                // 高怀疑度时，更倾向于继续巡逻
                return Random.value > 0.2f;
            }
            else
            {
                // 正常情况下的随机决策
                return Random.value > 0.5f;
            }
        }

        private void SetRandomPatrolTarget()
        {
            // 在当前位置周围随机选择一个巡逻点
            Vector3 randomDirection = Random.insideUnitSphere * patrolRadius;
            randomDirection.y = 0; // 保持在同一水平面
            patrolTarget = owner.transform.position + randomDirection;
            hasPatrolTarget = true;

            // Debug.Log($"Goal_Ghoul_Patrol: 设置新巡逻点 {patrolTarget}");
        }

        private void StartPatrolMovement()
        {
            if (hasPatrolTarget)
            {
                // 创建移动SubGoal
                currentMoveGoal = new Goal_MoveToSomeWhere(owner, patrolTarget, 1f, 15f);
                AddSubGoal(currentMoveGoal);
                // Debug.Log($"Goal_Ghoul_Patrol: 开始移动到巡逻点 {patrolTarget}");
            }
        }

        public Vector3 GetPatrolTarget()
        {
            return patrolTarget;
        }

        public bool HasPatrolTarget()
        {
            return hasPatrolTarget;
        }

        public override void Terminate()
        {
            base.Terminate();
            hasPatrolTarget = false;
            currentMoveGoal = null;
        }

        public override bool HandleInterrupt(InterruptType type)
        {
            if (type == InterruptType.Damage)
            {
                // 受到伤害时立即结束巡逻
                status = GoalStatus.Failed;
                return true;
            }
            return false;
        }
    }
}
