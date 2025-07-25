using EnemyAIBase;
using AI.FSM;
using UnityEngine;

namespace EnemyAIBase
{
    /// <summary>
    /// Ghoul警戒目标 - 当AIPerception检测到可疑活动时的警戒行为
    /// </summary>
    public class Goal_Ghoul_Alert : AIGoal
    {
        protected new Ghoul owner;
        private AIPerception perception;
        private float alertStartTime;
        private float alertDuration = 5f; // 警戒持续时间
        private float lookAroundInterval = 1f; // 环顾间隔
        private float lastLookTime;
        private Vector3 lastKnownPlayerPosition;
        private bool hasLastKnownPosition = false;

        public Goal_Ghoul_Alert(Ghoul owner) : base(owner)
        {
            this.owner = owner;
            this.perception = owner.GetComponent<AIPerception>();
        }

        public override void Activate()
        {
            base.Activate();
            
            // 切换到待机状态但保持警戒
            var fsmBase = owner.GetComponent<GhoulFSMBase>();
            if (fsmBase != null)
            {
                fsmBase.SwitchState(FSMStateID.GhoulIdle);
            }
            
            alertStartTime = Time.time;
            lastLookTime = Time.time;
            
            // 记录最后已知的玩家位置
            if (perception != null && perception.DetectedPlayer != null)
            {
                lastKnownPlayerPosition = perception.DetectedPlayer.position;
                hasLastKnownPosition = true;
            }
            
            Debug.Log("Goal_Ghoul_Alert: 激活警戒状态");
        }

        public override GoalStatus Process()
        {
            // 检查是否完全检测到玩家
            if (perception != null && perception.IsPlayerFullyDetected())
            {
                status = GoalStatus.Completed;
                Debug.Log("Goal_Ghoul_Alert: 完全检测到玩家，结束警戒");
                return status;
            }
            
            // 检查警戒时间是否结束
            if (Time.time - alertStartTime > alertDuration)
            {
                // 检查当前怀疑度
                float suspicionLevel = perception?.GetPlayerSuspicionLevel() ?? 0f;
                if (suspicionLevel < 0.2f)
                {
                    status = GoalStatus.Completed;
                    Debug.Log("Goal_Ghoul_Alert: 警戒时间结束，怀疑度降低");
                    return status;
                }
                else
                {
                    // 怀疑度仍然较高，延长警戒时间
                    alertStartTime = Time.time;
                    Debug.Log("Goal_Ghoul_Alert: 怀疑度仍高，延长警戒时间");
                }
            }
            
            // 执行警戒行为
            PerformAlertBehavior();
            
            return GoalStatus.Active;
        }

        private void PerformAlertBehavior()
        {
            // 定期环顾四周
            if (Time.time - lastLookTime > lookAroundInterval)
            {
                LookAround();
                lastLookTime = Time.time;
            }
            
            // 如果有最后已知位置，偶尔朝那个方向看
            if (hasLastKnownPosition && Random.value < 0.3f)
            {
                LookTowardsLastKnownPosition();
            }
        }

        private void LookAround()
        {
            // 随机选择一个方向进行观察
            float randomAngle = Random.Range(-180f, 180f);
            Vector3 lookDirection = Quaternion.Euler(0, randomAngle, 0) * owner.transform.forward;
            
            // 缓慢转向该方向
            RotateTowards(lookDirection);
            
            Debug.Log($"Goal_Ghoul_Alert: 环顾四周，角度 {randomAngle:F1}");
        }

        private void LookTowardsLastKnownPosition()
        {
            Vector3 directionToLastKnown = (lastKnownPlayerPosition - owner.transform.position).normalized;
            directionToLastKnown.y = 0;
            
            if (directionToLastKnown != Vector3.zero)
            {
                RotateTowards(directionToLastKnown);
                // Debug.Log("Goal_Ghoul_Alert: 朝向最后已知玩家位置");
            }
        }

        private void RotateTowards(Vector3 direction)
        {
            if (direction == Vector3.zero) return;
            
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            owner.transform.rotation = Quaternion.Slerp(
                owner.transform.rotation, 
                targetRotation, 
                Time.deltaTime * 2f // 缓慢旋转
            );
        }

        public override void Terminate()
        {
            base.Terminate();
            hasLastKnownPosition = false;
            Debug.Log("Goal_Ghoul_Alert: 终止警戒状态");
        }

        public override bool HandleInterrupt(InterruptType type)
        {
            if (type == InterruptType.Damage)
            {
                // 受到伤害时立即结束警戒
                status = GoalStatus.Failed;
                return true;
            }
            return false;
        }

        // 公共方法
        public float GetAlertProgress()
        {
            return (Time.time - alertStartTime) / alertDuration;
        }

        public bool HasLastKnownPlayerPosition()
        {
            return hasLastKnownPosition;
        }

        public Vector3 GetLastKnownPlayerPosition()
        {
            return lastKnownPlayerPosition;
        }
    }
}
