using EnemyAIBase;
using AI.FSM;
using UnityEngine;

namespace EnemyAIBase
{
    /// <summary>
    /// Ghoul警戒目标 - 当AIPerception检测到可疑活动时警戒行为
    /// </summary>
    public class Goal_Ghoul_Alert : AIGoal
    {
        protected new Ghoul owner;
        private AIPerception perception;
        private float alertStartTime;
        private float alertDuration = 5f;
        private float lookAroundInterval = 1f;
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
            if (perception != null && perception.IsPlayerFullyDetected())
            {
                status = GoalStatus.Completed;
                Debug.Log("Goal_Ghoul_Alert: 完全检测到玩家，结束警戒");
                return status;
            }
            
            if (Time.time - alertStartTime > alertDuration)
            {
                // 检查当前怀疑度
                float suspicionLevel = perception?.GetPlayerSuspicionLevel() ?? 0f;
                if (suspicionLevel < 0.2f)
                {
                    status = GoalStatus.Completed;
                    return status;
                }
                else
                {
                    alertStartTime = Time.time;
                }
            }
            
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
            float randomAngle = Random.Range(-180f, 180f);
            Vector3 lookDirection = Quaternion.Euler(0, randomAngle, 0) * owner.transform.forward;
            RotateTowards(lookDirection);
        }

        private void LookTowardsLastKnownPosition()
        {
            Vector3 directionToLastKnown = (lastKnownPlayerPosition - owner.transform.position).normalized;
            directionToLastKnown.y = 0;
            
            if (directionToLastKnown != Vector3.zero)
            {
                RotateTowards(directionToLastKnown);
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
