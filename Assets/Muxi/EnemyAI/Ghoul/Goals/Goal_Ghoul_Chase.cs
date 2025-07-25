using EnemyAIBase;
using AI.FSM;
using UnityEngine;

namespace EnemyAIBase
{
    /// <summary>
    /// Ghoul追击目标 - 使用Goal_MoveToSomeWhere进行NavMesh追击
    /// </summary>
    public class Goal_Ghoul_Chase : AIGoal
    {
        protected new Ghoul owner;
        private Transform target;
        private float loseTargetTime = 2f; // 失去目标多长时间后放弃追击
        private float lastSeenTime;
        private float updateInterval = 0.2f; // 更新追击目标的频率
        private float lastUpdateTime;
        private Goal_MoveToSomeWhere currentMoveGoal;

        public Goal_Ghoul_Chase(Ghoul owner) : base(owner)
        {
            this.owner = owner;
        }

        public override void Activate()
        {
            base.Activate();

            target = owner.GetTarget();
            if (target == null)
            {
                status = GoalStatus.Failed;
                Debug.Log("Goal_Ghoul_Chase: 没有追击目标");
                return;
            }

            lastSeenTime = Time.time;
            lastUpdateTime = Time.time;

            // 开始追击移动
            StartChaseMovement();

            Debug.Log("Goal_Ghoul_Chase: 激活追击目标");
        }

        public override GoalStatus Process()
        {
            // 添加调试信息
            if (Time.frameCount % 60 == 0) // 每秒输出一次
            {
                Debug.Log($"[Goal_Ghoul_Chase] Process - Status: {status}, HasTarget: {owner.GetTarget() != null}, SubGoals: {HasActiveSubGoals()}");
            }

            target = owner.GetTarget();

            // 检查目标是否还存在
            if (target == null)
            {
                // 检查是否超过失去目标的时间限制
                if (Time.time - lastSeenTime > loseTargetTime)
                {
                    status = GoalStatus.Failed;
                    Debug.Log("Goal_Ghoul_Chase: 失去目标太久，追击失败");
                    return status;
                }
            }
            else
            {
                lastSeenTime = Time.time;

                // 检查是否进入攻击范围
                float distanceToTarget = owner.GetDistanceToTarget();
                if (distanceToTarget <= owner.attackRange)
                {
                    status = GoalStatus.Completed;
                    Debug.Log("Goal_Ghoul_Chase: 进入攻击范围，追击完成");
                    return status;
                }

                // 检查目标是否超出视野范围太远
                if (distanceToTarget > owner.sightRange * 1.5f)
                {
                    status = GoalStatus.Failed;
                    Debug.Log("Goal_Ghoul_Chase: 目标距离太远，追击失败");
                    return status;
                }

                // 定期更新追击目标位置
                if (Time.time - lastUpdateTime > updateInterval)
                {
                    UpdateChaseTarget();
                    lastUpdateTime = Time.time;
                }
            }

            // 检查移动SubGoal状态
            if (currentMoveGoal != null && currentMoveGoal.Status == GoalStatus.Failed)
            {
                // 移动失败，重新尝试
                StartChaseMovement();
            }

            // 处理子目标
            return base.Process();
        }

        private void StartChaseMovement()
        {
            if (target != null)
            {
                // 创建移动SubGoal，追击时到达距离稍微大一些
                currentMoveGoal = new Goal_MoveToSomeWhere(owner, target.position, owner.attackRange * 0.8f, 10f);
                AddSubGoal(currentMoveGoal);
                Debug.Log($"Goal_Ghoul_Chase: 开始追击移动到 {target.position}，攻击范围: {owner.attackRange}");
                Debug.Log($"Goal_Ghoul_Chase: 创建Goal_MoveToSomeWhere，到达距离: {owner.attackRange * 0.8f}");
            }
            else
            {
                Debug.LogError("Goal_Ghoul_Chase: StartChaseMovement - target为null！");
            }
        }

        private void UpdateChaseTarget()
        {
            if (target != null && currentMoveGoal != null)
            {
                // 更新移动目标位置
                currentMoveGoal.UpdateTargetPosition(target.position);
            }
        }

        public Transform GetChaseTarget()
        {
            return target;
        }

        public override void Terminate()
        {
            base.Terminate();
            target = null;
            currentMoveGoal = null;
            Debug.Log("Goal_Ghoul_Chase: 终止追击目标");
        }

        public override bool HandleInterrupt(InterruptType type)
        {
            if (type == InterruptType.Damage)
            {
                // 受到伤害时不中断追击，继续追击
                return false;
            }
            return false;
        }
    }
}
