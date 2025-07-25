using EnemyAIBase;
using AI.FSM;
using UnityEngine;
using UnityEngine.AI;

namespace EnemyAIBase
{
    /// <summary>
    /// 通用移动Goal - 使用NavMesh和Root Motion移动到指定位置
    /// 可以被其他Goal作为SubGoal使用
    /// </summary>
    public class Goal_MoveToSomeWhere : AIGoal
    {
        private Vector3 targetPosition;
        private float arrivalDistance = 0.5f;
        private float timeoutDuration = 10f;
        private float startTime;
        private AIMoveBehavior moveBehavior;
        private NavMeshAgent navAgent;
        private GhoulFSMBase fsmBase;
        
        // 移动状态
        private bool isMoving = false;
        private bool hasReachedDestination = false;
        
        public Goal_MoveToSomeWhere(BaseEnemy owner, Vector3 destination, float arrivalDistance = 0.5f, float timeout = 10f) : base(owner)
        {
            this.targetPosition = destination;
            this.arrivalDistance = arrivalDistance;
            this.timeoutDuration = timeout;
        }

        public override void Activate()
        {
            base.Activate();
            
            moveBehavior = owner.GetComponent<AIMoveBehavior>();
            navAgent = owner.GetComponent<NavMeshAgent>();
            fsmBase = owner.GetComponent<GhoulFSMBase>();
            
            
            // 目标位置是否可达
            NavMeshPath path = new NavMeshPath();
            if (!navAgent.CalculatePath(targetPosition, path) || path.status != NavMeshPathStatus.PathComplete)
            {
                Debug.LogWarning($"Goal_MoveToSomeWhere: 目标位置不可达 {targetPosition}");
                status = GoalStatus.Failed;
                return;
            }
            
            // 是否在战斗
            if (IsInBattleMode())
            {
                if (fsmBase != null)
                {
                    Debug.Log($"Goal_MoveToSomeWhere: 战斗模式下强制切换到Walking状态");
                    fsmBase.SwitchState(FSMStateID.GhoulWalking);
                }
            }
            StartMovement();
            
            startTime = Time.time;
           
        }

        private bool IsInBattleMode()
        {
            if (owner is Ghoul ghoul)
            {
                var currentGoal = ghoul.Brain?.GetGoalManager()?.CurrentGoal;
                return currentGoal is Goal_Ghoul_Battle;
            }
            return false;
        }

        public override GoalStatus Process()
        {
            // 检查超时
            if (Time.time - startTime > timeoutDuration)
            {
                status = GoalStatus.Failed;
                Debug.Log("Goal_MoveToSomeWhere: 移动超时");
                return status;
            }
            
            // 检查是否到达目标
            float distanceToTarget = Vector3.Distance(owner.transform.position, targetPosition);
            
            if (distanceToTarget <= arrivalDistance)
            {
                status = GoalStatus.Completed;
                Debug.Log("Goal_MoveToSomeWhere: 到达目标位置");
                return status;
            }
            
            // 检查NavMeshAgent状态
            if (navAgent != null)
            {
                if (!navAgent.pathPending && navAgent.remainingDistance < 0.1f && distanceToTarget > arrivalDistance)
                {
                    if (!RecalculatePath())
                    {
                        status = GoalStatus.Failed;
                        return status;
                    }
                }
            }
            
            // 更新移动状态
            UpdateMovement();
            
            return GoalStatus.Active;
        }

        private void StartMovement()
        {
            Debug.Log($"Goal_MoveToSomeWhere: StartMovement - moveBehavior: {moveBehavior != null}, navAgent: {navAgent != null}");

            if (moveBehavior != null && navAgent != null)
            {
                moveBehavior.SetDestination(targetPosition);
                isMoving = true;

                ConfigureNavMeshAgent();
            }
        }

        private void ConfigureNavMeshAgent()
        {
            if (navAgent != null)
            {
                navAgent.updatePosition = false; 
                navAgent.updateRotation = false; 
                
                var enemyInfo = owner.GetComponent<EnemyInfo>();
                if (enemyInfo != null)
                {
                    navAgent.speed = enemyInfo.MoveSpeed;
                    navAgent.angularSpeed = enemyInfo.RotationSpeed;
                }
            }
        }

        private void UpdateMovement()
        {
            if (!isMoving || navAgent == null) return;
            UpdateAnimatorParameters();
        }

        private void UpdateAnimatorParameters()
        {
            if (fsmBase?.animator == null) return;
            
            // 计算移动速度
            Vector3 velocity = navAgent.desiredVelocity;
            float speed = velocity.magnitude;
            
            if (speed > 0.1f)
            {
                Vector3 localVelocity = owner.transform.InverseTransformDirection(velocity);
                
                // 设置Animator参数 实际上现在没有这个blend的实现
                fsmBase.animator.SetFloat("MoveX", localVelocity.x);
                fsmBase.animator.SetFloat("MoveY", localVelocity.z);
                fsmBase.animator.SetFloat("Speed", speed);
            }
            else
            {
                fsmBase.animator.SetFloat("MoveX", 0f);
                fsmBase.animator.SetFloat("MoveY", 0f);
                fsmBase.animator.SetFloat("Speed", 0f);
            }
        }

        private bool RecalculatePath()
        {
            if (navAgent == null) return false;
            
            NavMeshPath newPath = new NavMeshPath();
            if (navAgent.CalculatePath(targetPosition, newPath) && newPath.status == NavMeshPathStatus.PathComplete)
            {
                moveBehavior.SetDestination(targetPosition);
                return true;
            }
            
            return false;
        }

        public override void Terminate()
        {
            base.Terminate();
            
            // 停止移动
            if (navAgent != null)
            {
                navAgent.ResetPath();
            }
            
            // 重置Animator参数
            if (fsmBase?.animator != null)
            {
                fsmBase.animator.SetFloat("MoveX", 0f);
                fsmBase.animator.SetFloat("MoveY", 0f);
                fsmBase.animator.SetFloat("Speed", 0f);
            }
            
            isMoving = false;
            Debug.Log("Goal_MoveToSomeWhere: 移动Goal终止");
        }

        public override bool HandleInterrupt(InterruptType type)
        {
            if (type == InterruptType.Damage)
            {
                // 受到伤害时可以选择继续移动或停止 等待讨论
        
                return false;
            }
            return false;
        }

        // 公共方法
        public Vector3 GetTargetPosition() => targetPosition;
        public float GetDistanceToTarget() => Vector3.Distance(owner.transform.position, targetPosition);
        public bool IsMoving() => isMoving;
        public bool HasReachedDestination() => hasReachedDestination;
        
        public void UpdateTargetPosition(Vector3 newTarget)
        {
            targetPosition = newTarget;
            if (isMoving && moveBehavior != null)
            {
                moveBehavior.SetDestination(targetPosition);
            }
        }
    }
}
