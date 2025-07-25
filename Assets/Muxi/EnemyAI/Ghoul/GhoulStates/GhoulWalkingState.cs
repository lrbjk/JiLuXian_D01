using AI.FSM.Framework;
using EnemyAIBase;
using UnityEngine;
using UnityEngine.AI;

namespace AI.FSM
{
    /// <summary>
    /// Ghoul行走状态 - 支持NavMesh和Root Motion
    /// </summary>
    public class GhoulWalkingState : FSMState
    {
        private GhoulFSMBase ghoulFSMBase;
        private EnemyInfo enemyInfo;
        private Ghoul ghoul;
        private AIMoveBehavior moveBehavior;
        private NavMeshAgent navAgent;

        public override void Init()
        {
            StateID = FSMStateID.GhoulWalking;
        }

        public override void EnterState(FSMBase fSMBase)
        {
            base.EnterState(fSMBase);
            ghoulFSMBase = fSMBase as GhoulFSMBase;
            enemyInfo = fSMBase.GetComponent<EnemyInfo>();
            ghoul = fSMBase.GetComponent<Ghoul>();
            moveBehavior = fSMBase.GetComponent<AIMoveBehavior>();
            navAgent = fSMBase.GetComponent<NavMeshAgent>();

            // 播放行走动画
            fSMBase.animationHandler.PlayTargetAnimation("Walking", true, 0.2f);

            // 配置NavMeshAgent为Root Motion模式
            if (navAgent != null)
            {
                navAgent.updatePosition = false; // Root Motion控制位置
                navAgent.updateRotation = false; // 手动控制旋转
                navAgent.speed = enemyInfo.MoveSpeed;
                navAgent.angularSpeed = enemyInfo.RotationSpeed;

                // 设置停止距离为攻击范围的80%，避免重合
                float attackRange = ghoul.attackRange;
                navAgent.stoppingDistance = Mathf.Max(attackRange * 0.8f, 3.5f);

                // Debug.Log($"[GhoulWalkingState] Root Motion模式 - Speed: {navAgent.speed}, StoppingDistance: {navAgent.stoppingDistance}");
            }

            // Debug.Log("Ghoul进入行走状态");
        }

        public override void ActionState(FSMBase fSMBase)
        {
            base.ActionState(fSMBase);

            // 处理移动逻辑
            HandleMovement();

            // 更新Animator参数以支持Root Motion
            UpdateAnimatorParameters();
        }

        private void HandleMovement()
        {
            if (ghoul == null || navAgent == null) return;

            Transform target = ghoul.GetTarget();

            if (target != null)
            {
                // 有目标时，向目标移动
                HandleTargetMovement(target);
            }
            else
            {
                // 没有目标时，执行巡逻逻辑
                HandlePatrolMovement();
            }

            // 手动同步NavMeshAgent位置（Root Motion模式下需要）
            SyncNavMeshAgentPosition();
        }

        private void HandleTargetMovement(Transform target)
        {
            Vector3 targetPosition = target.position;
            float distanceToTarget = Vector3.Distance(ghoul.transform.position, targetPosition);

            // 如果距离太近，停止移动并可能后退
            if (distanceToTarget <= navAgent.stoppingDistance)
            {
                // 停止NavMeshAgent
                if (navAgent.isActiveAndEnabled && navAgent.isOnNavMesh)
                {
                    navAgent.ResetPath();
                }
                
                // 只进行旋转，不移动
                RotateTowardsTarget();
                return;
            }

            // 设置NavMeshAgent的目标用于路径规划
            if (navAgent.isActiveAndEnabled && navAgent.isOnNavMesh)
            {
                navAgent.SetDestination(targetPosition);
            }

            // 手动控制旋转朝向目标
            RotateTowardsTarget();
        }

        private void HandlePatrolMovement()
        {
            // 简单的巡逻逻辑：在当前位置周围随机移动
            if (navAgent == null || !navAgent.isActiveAndEnabled || !navAgent.isOnNavMesh) return;

            // 如果已经到达目标或没有目标，选择新的巡逻点
            if (!navAgent.hasPath || navAgent.remainingDistance < 1f)
            {
                Vector3 randomDirection = Random.insideUnitSphere * 5f; // 5米半径
                randomDirection.y = 0; // 保持在同一水平面
                Vector3 newTarget = ghoul.transform.position + randomDirection;

                // 确保目标在NavMesh上
                if (NavMesh.SamplePosition(newTarget, out NavMeshHit hit, 5f, NavMesh.AllAreas))
                {
                    navAgent.SetDestination(hit.position);
                    // Debug.Log($"[GhoulWalkingState] 设置巡逻目标: {hit.position}");
                }
            }

            // 手动控制旋转朝向路径方向
            RotateTowardsTarget();
        }

        private void RotateTowardsTarget()
        {
            if (navAgent == null || !navAgent.hasPath) return;

            // 获取NavMeshAgent的期望移动方向
            Vector3 desiredVelocity = navAgent.desiredVelocity;

            if (desiredVelocity.magnitude > 0.1f)
            {
                // 计算目标旋转
                Vector3 lookDirection = desiredVelocity.normalized;
                lookDirection.y = 0; // 保持水平旋转

                if (lookDirection != Vector3.zero)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(lookDirection);

                    // 平滑旋转
                    float rotationSpeed = navAgent.angularSpeed * Mathf.Deg2Rad;
                    ghoul.transform.rotation = Quaternion.RotateTowards(
                        ghoul.transform.rotation,
                        targetRotation,
                        rotationSpeed * Time.deltaTime
                    );
                }
            }
        }

        private void SyncNavMeshAgentPosition()
        {
            // 在Root Motion模式下，需要手动同步NavMeshAgent的位置
            if (navAgent != null && navAgent.isActiveAndEnabled && navAgent.isOnNavMesh)
            {
                navAgent.nextPosition = ghoul.transform.position;
            }
        }

        private void UpdateAnimatorParameters()
        {
            if (navAgent == null || ghoulFSMBase?.animator == null) return;

            
            float desiredSpeed = navAgent.desiredVelocity.magnitude;
            float normalizedSpeed = navAgent.hasPath && desiredSpeed > 0.1f ? 1f : 0f;

            // 设置动画参数
            ghoulFSMBase.animator.SetFloat("Speed", normalizedSpeed);

            // 如果有方向参数，设置为前进
            if (HasAnimatorParameter("MoveX"))
            {
                ghoulFSMBase.animator.SetFloat("MoveX", 0f);
            }
            if (HasAnimatorParameter("MoveY"))
            {
                ghoulFSMBase.animator.SetFloat("MoveY", normalizedSpeed);
            }

            // 确保Animator的applyRootMotion为true
            if (ghoulFSMBase.animator.applyRootMotion != true)
            {
                ghoulFSMBase.animator.applyRootMotion = true;
                Debug.Log("[GhoulWalkingState] 启用Root Motion");
            }


            if (Time.frameCount % 60 == 0) 
            {
                 // Debug.Log($"[GhoulWalkingState] DesiredSpeed: {desiredSpeed:F2}, NormalizedSpeed: {normalizedSpeed:F2}, HasPath: {navAgent.hasPath}");
            }
        }

        private bool HasAnimatorParameter(string paramName)
        {
            if (ghoulFSMBase?.animator == null) return false;

            foreach (AnimatorControllerParameter param in ghoulFSMBase.animator.parameters)
            {
                if (param.name == paramName)
                    return true;
            }
            return false;
        }



        public override void ExitState(FSMBase fSMBase)
        {
            base.ExitState(fSMBase);

            // 重置动画参数
            if (ghoulFSMBase?.animator != null)
            {
                ghoulFSMBase.animator.SetFloat("MoveX", 0f);
                ghoulFSMBase.animator.SetFloat("MoveY", 0f);
                ghoulFSMBase.animator.SetFloat("Speed", 0f);
            }

            // Debug.Log("Ghoul退出行走状态");
        }
    }
}
