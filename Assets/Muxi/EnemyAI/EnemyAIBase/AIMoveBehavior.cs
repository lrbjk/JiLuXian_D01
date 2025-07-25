using System;

namespace EnemyAIBase
{
    using UnityEngine;
    using UnityEngine.AI;

    /// <summary>
    /// 移动控制模式
    /// </summary>
    public enum MovementMode
    {
        RootMotion,    // 纯Root Motion（美观优先，适合巡逻、闲逛）
        NavMeshAgent,  // 纯NavMeshAgent（AI优先，适合追击、逃跑）
        Hybrid         // 混合模式（平衡，适合复杂场景）
    }

//TODO: decoupling combat and patrol movebehaviour

    [RequireComponent(typeof(NavMeshAgent), typeof(Animator))]
    public class AIMoveBehavior : MonoBehaviour
    {
        private NavMeshAgent navMeshAgent;
        private Animator animator;

        [Header("Movement Settings")] 
        [SerializeField] private float rotationSpeed = 10f;

        [Header("Hybrid Movement Settings")]
        [Tooltip("Root Motion位移的最小阈值，低于此值将使用NavMeshAgent补偿")]
        [SerializeField] private float rootMotionThreshold = 0.01f;

        [Tooltip("NavMeshAgent期望速度的最小阈值")]
        [SerializeField] private float navMeshVelocityThreshold = 0.1f;

        [Tooltip("NavMeshAgent补偿移动的最大速度（米/秒）")]
        [SerializeField] private float maxCompensationSpeed = 2f;

        [Tooltip("补偿移动的强度系数（0-1）")]
        [SerializeField] private float compensationStrength = 0.5f;

        [Tooltip("是否启用NavMeshAgent补偿")]
        [SerializeField] private bool enableNavMeshCompensation = true;

        [Tooltip("位置同步频率（秒）")]
        [SerializeField] private float positionSyncInterval = 0.1f;

        [Header("Movement Mode Settings")]
        [Tooltip("当前移动模式")]
        [SerializeField] private MovementMode currentMovementMode = MovementMode.Hybrid;

        [Tooltip("是否允许运行时切换模式")]
        [SerializeField] private bool allowRuntimeModeSwitch = true;

        [Header("Root Motion Assistance")]
        [Tooltip("Root Motion模式下是否启用转向辅助")]
        [SerializeField] private bool enableRootMotionSteering = true;

        [Tooltip("Root Motion转向速度")]
        [SerializeField] private float rootMotionSteeringSpeed = 1f; // 进一步降低转向速度

        [Header("Combat Settings")] [SerializeField]
        private float combatRotationSpeed = 15f;

        // [SerializeField] private string playerTag = "Player"; 
        [SerializeField] private float combatFaceDistance = 10f;

        private Vector3 lastTargetPosition;
        private bool isInCombat = false;
        private Transform playerTransform;

        // 位置同步相关
        private float lastSyncTime = 0f;

        private static readonly int MOVE_X = Animator.StringToHash("MoveX");
        private static readonly int MOVE_Y = Animator.StringToHash("MoveY");

        [SerializeField] private bool enableDebugLogs = true; // 临时启用调试

        private void Awake()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();

            // 根据设置的模式进行初始配置
            ConfigureMovementMode();

            Debug.Log($"[AIMoveBehavior] Initialized with {currentMovementMode} mode");
        }


        private void ExitCombat()
        {
            isInCombat = false;
            playerTransform = null;
            DebugLog("Exited combat mode");
        }

        private void Update()
        {
            // Root Motion下不需要位置同步
            if (currentMovementMode != MovementMode.RootMotion)
            {
                if (Time.time - lastSyncTime > positionSyncInterval)
                {
                    SyncNavMeshAgentPosition();
                    lastSyncTime = Time.time;
                }
            }

            // 战斗状态特殊处理可以在这里添加
            if (isInCombat && playerTransform != null)
            {
                // 战斗状态的特殊逻辑（如果需要）
            }
        }

        private void FaceTarget(Vector3 targetPosition)
        {
            Vector3 direction = (targetPosition - transform.position).normalized;
            direction.y = 0; 

            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation,
                    combatRotationSpeed * Time.deltaTime);
            }
        }



        public void SetDestination(Vector3 position)
        {
            if (navMeshAgent != null)
            {
                // 记录当前位置和目标位置
                Vector3 currentPos = transform.position;
                float distanceToTarget = Vector3.Distance(currentPos, position);
                //
                // Debug.Log($"[AIMoveBehavior] SetDestination: {position}");
                // Debug.Log($"[AIMoveBehavior] Current position: {currentPos}");
                // Debug.Log($"[AIMoveBehavior] Distance to target: {distanceToTarget:F2}");

                navMeshAgent.SetDestination(position);

                // 等一帧让NavMeshAgent计算路径
                StartCoroutine(CheckPathAfterFrame());
            }
        }

        private System.Collections.IEnumerator CheckPathAfterFrame()
        {
            yield return null; // 等一帧

            if (navMeshAgent != null)
            {
               
                Vector3 destination = navMeshAgent.destination;
                // bool targetOnNavMesh = IsPositionOnNavMesh(destination);
                // Debug.Log($"[AIMoveBehavior] Target position on NavMesh: {targetOnNavMesh}");
                // // 检查当前位置是否在NavMesh上
                // bool currentOnNavMesh = IsPositionOnNavMesh(transform.position);

            }
        }
        

        private void DebugLog(string message)
        {
            if (enableDebugLogs)
            {
                Debug.Log(message);
            }
        }

        // 公共方法，允许外部控制战斗状态
        public void SetCombatMode(bool combat, Transform target = null)
        {
            if (combat && target != null)
            {
                isInCombat = true;
                playerTransform = target;
            }
            else
            {
                ExitCombat();
            }
        }

        public bool IsInCombat => isInCombat;

        /// <summary>
        /// 获取当前移动模式
        /// </summary>
        public MovementMode CurrentMovementMode => currentMovementMode;

        /// <summary>
        /// 设置移动模式
        /// </summary>
        /// <param name="mode">目标移动模式</param>
        /// <param name="force">是否强制切换（忽略allowRuntimeModeSwitch设置）</param>
        public void SetMovementMode(MovementMode mode, bool force = false)
        {
            if (!allowRuntimeModeSwitch && !force)
            {
                Debug.LogWarning($"[AIMoveBehavior] Runtime mode switch is disabled. Current mode: {currentMovementMode}");
                return;
            }

            if (currentMovementMode == mode)
            {
                Debug.Log($"[AIMoveBehavior] Already in {mode} mode");
                return;
            }

            Debug.Log($"[AIMoveBehavior] Switching from {currentMovementMode} to {mode}");
            currentMovementMode = mode;
            ConfigureMovementMode();
        }

        /// <summary>
        /// 根据当前模式配置组件
        /// </summary>
        private void ConfigureMovementMode()
        {
            if (navMeshAgent == null || animator == null) return;

            switch (currentMovementMode)
            {
                case MovementMode.RootMotion:
                    ConfigureRootMotionMode();
                    break;

                case MovementMode.NavMeshAgent:
                    ConfigureNavMeshAgentMode();
                    break;

                case MovementMode.Hybrid:
                    ConfigureHybridMode();
                    break;
            }

            // Debug.Log($"[AIMoveBehavior] Configured for {currentMovementMode} mode");
        }

        /// <summary>
        /// 配置纯Root Motion模式
        /// </summary>
        private void ConfigureRootMotionMode()
        {
            // 真正的纯Root Motion：让动画完全控制移动
            animator.applyRootMotion = true;
            navMeshAgent.updatePosition = false; // 不让NavMeshAgent控制位置
            navMeshAgent.updateRotation = false; // 不让NavMeshAgent控制旋转

            Debug.Log("[AIMoveBehavior] Root Motion Mode: Pure animation-driven movement");
        }

        /// <summary>
        /// 配置纯NavMeshAgent模式
        /// </summary>
        private void ConfigureNavMeshAgentMode()
        {
            animator.applyRootMotion = false;
            navMeshAgent.updatePosition = true;
            navMeshAgent.updateRotation = true;

            Debug.Log("[AIMoveBehavior] NavMeshAgent Mode: AI drives movement, animation follows");
        }

        /// <summary>
        /// 配置混合模式
        /// </summary>
        private void ConfigureHybridMode()
        {
            animator.applyRootMotion = true;
            navMeshAgent.updatePosition = false;
            navMeshAgent.updateRotation = false;

            Debug.Log("[AIMoveBehavior] Hybrid Mode: Root Motion + NavMeshAgent compensation");
        }

        // 根据当前模式执行不同的移动逻辑
        private void OnAnimatorMove()
        {
            switch (currentMovementMode)
            {
                case MovementMode.RootMotion:
                    OnAnimatorMove_RootMotion();
                    break;

                case MovementMode.NavMeshAgent:
                    OnAnimatorMove_NavMeshAgent();
                    break;

                case MovementMode.Hybrid:
                    OnAnimatorMove_Hybrid();
                    break;
            }
        }

        /// <summary>
        /// 纯Root Motion移动逻辑（调试版本）
        /// </summary>
        private void OnAnimatorMove_RootMotion()
        {
            // 记录进入时的位置
            Vector3 positionBefore = transform.position;

            if (animator.applyRootMotion)
            {
                Vector3 deltaPosition = animator.deltaPosition;
                Quaternion deltaRotation = animator.deltaRotation;

                // 详细的动画状态调试
                AnimatorStateInfo currentState = animator.GetCurrentAnimatorStateInfo(0);

                // 尝试识别常见的动画状态
                string stateName = GetAnimationStateName(currentState.shortNameHash);
              
                // 检查动画参数
                float speedParam = animator.GetFloat("Speed");
              
                // 只有在有实际位移时才移动
                if (deltaPosition.magnitude > 0.0000001f)
                {
                    transform.position += deltaPosition;

                    // 同步NavMeshAgent位置，解决转向问题
                    SyncNavMeshAgentPosition();

                 
                }
             

                // 应用Root Motion旋转
                if (deltaRotation != Quaternion.identity)
                {
                    transform.rotation *= deltaRotation;
               
                }

                // 温和的转向辅助（在Root Motion基础上微调）
                if (enableRootMotionSteering && navMeshAgent != null && navMeshAgent.hasPath)
                {
                    ApplyGentleSteering();
                }
               
            }
           
            // 检查位置是否发生了变化
            Vector3 positionAfter = transform.position;
            Vector3 actualMovement = positionAfter - positionBefore;

            
        }

        /// <summary>
        /// 温和的转向辅助（提早转向，不影响Root Motion的自然移动）
        /// </summary>
        private void ApplyGentleSteering()
        {
            if (navMeshAgent == null || !navMeshAgent.hasPath) return;

            Vector3 targetDirection = Vector3.zero;
            string directionSource = "";

            // 策略1：优先使用NavMeshAgent的期望方向（如果有的话）
            if (navMeshAgent.desiredVelocity.magnitude > 0.1f)
            {
                targetDirection = navMeshAgent.desiredVelocity.normalized;
                directionSource = "desiredVelocity";
            }
            // 策略2：如果没有期望方向，直接朝向最终目标（提早转向）
            else if (navMeshAgent.remainingDistance > 0.5f)
            {
                Vector3 directionToDestination = (navMeshAgent.destination - transform.position).normalized;
                targetDirection = directionToDestination;
                directionSource = "destination";
            }
            // 策略3：如果很接近目标，朝向路径上的下一个点
            else if (navMeshAgent.path.corners.Length > 1)
            {
                Vector3 nextCorner = navMeshAgent.path.corners[1];
                targetDirection = (nextCorner - transform.position).normalized;
                directionSource = "nextCorner";
            }

            if (targetDirection != Vector3.zero)
            {
                Vector3 currentForward = transform.forward;
                float angleDifference = Vector3.Angle(currentForward, targetDirection);

          
                // 降低角度阈值，让转向更早开始
                if (angleDifference > 5f) // 从10°降低到5°
                {
                    Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

                    // 根据角度差调整转向速度：角度越大，转向越快
                    float dynamicSteeringSpeed = rootMotionSteeringSpeed * Mathf.Clamp(angleDifference / 45f, 0.5f, 2f);

                    transform.rotation = Quaternion.Slerp(
                        transform.rotation,
                        targetRotation,
                        dynamicSteeringSpeed * Time.deltaTime
                    );

                 
                }
               
            }
           
        }

        /// <summary>
        /// Root Motion模式下的转向辅助
        /// </summary>
        private void HandleRootMotionSteering()
        {
            if (navMeshAgent == null || !navMeshAgent.hasPath) return;

            // 计算到目标的方向
            Vector3 targetDirection = Vector3.zero;

            // 优先使用NavMeshAgent的期望方向
            if (navMeshAgent.desiredVelocity.magnitude > 0.1f)
            {
                targetDirection = navMeshAgent.desiredVelocity.normalized;
                Debug.Log($"[AIMoveBehavior] Steering towards NavMesh direction: {targetDirection}");
            }
            // 如果NavMeshAgent没有期望方向，直接朝向最终目标
            else if (navMeshAgent.remainingDistance > 0.1f)
            {
                targetDirection = (navMeshAgent.destination - transform.position).normalized;
                Debug.Log($"[AIMoveBehavior] Steering towards final destination: {targetDirection}");
            }

            // 应用转向
            if (targetDirection != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
                transform.rotation = Quaternion.Slerp(
                    transform.rotation,
                    targetRotation,
                    rootMotionSteeringSpeed * Time.deltaTime
                );

                // 计算转向角度用于调试
                float angle = Vector3.Angle(transform.forward, targetDirection);
                Debug.Log($"[AIMoveBehavior] Steering angle to target: {angle:F1}°");
            }
        }

        /// <summary>
        /// 判断是否应该继续移动（用于Root Motion模式）
        /// </summary>
        private bool ShouldContinueMoving()
        {
            if (navMeshAgent == null) return true;

            // 如果没有路径，停止移动
            if (!navMeshAgent.hasPath)
            {
                Debug.Log("[AIMoveBehavior] No path, should stop moving");
                return false;
            }

            // 如果距离目标很近，停止移动
            float distanceToTarget = navMeshAgent.remainingDistance;
            float stoppingDistance = navMeshAgent.stoppingDistance + 0.1f; // 添加一点缓冲

            if (distanceToTarget <= stoppingDistance)
            {
                Debug.Log($"[AIMoveBehavior] Near target (distance: {distanceToTarget:F2}, stopping distance: {stoppingDistance:F2}), should stop");
                return false;
            }

            return true;
        }

        /// <summary>
        /// 纯NavMeshAgent移动逻辑
        /// </summary>
        private void OnAnimatorMove_NavMeshAgent()
        {
            // NavMeshAgent模式下，OnAnimatorMove不需要做任何事
            // NavMeshAgent会自动处理位置和旋转
            // 这里可以根据NavMeshAgent的速度来调整动画播放速度

            if (navMeshAgent != null && animator != null)
            {
                float speed = navMeshAgent.velocity.magnitude;
                // 可以在这里设置动画参数，比如Speed
                // animator.SetFloat("Speed", speed);

                Debug.Log($"[AIMoveBehavior] NavMeshAgent velocity: {speed:F2}");
            }
        }

        /// <summary>
        /// 混合模式移动逻辑（原来的逻辑）
        /// </summary>
        private void OnAnimatorMove_Hybrid()
        {
            if (animator.applyRootMotion)
            {
                Vector3 deltaPosition = animator.deltaPosition;
                Quaternion deltaRotation = animator.deltaRotation;

                // 计算最终移动
                Vector3 finalMovement = CalculateSmartMovement(deltaPosition);

                // 直接应用到transform（避免使用navMeshAgent.Move()）
                if (finalMovement.magnitude > 0.001f)
                {
                    Vector3 newPosition = transform.position + finalMovement;

                    // 检查新位置是否在NavMesh上（可选的安全检查）
                    if (IsPositionOnNavMesh(newPosition))
                    {
                        transform.position = newPosition;

                        // 关键修复：同步NavMeshAgent的位置
                        SyncNavMeshAgentPosition();

                        Debug.Log($"[AIMoveBehavior] Hybrid movement: {finalMovement}");
                    }
                    else
                    {
                        Debug.Log($"[AIMoveBehavior] Movement blocked - would leave NavMesh: {finalMovement}");
                    }
                }

                // 智能旋转处理
                HandleSmartRotation(deltaRotation);
            }
        }

        /// <summary>
        /// 智能移动计算：Root Motion优先，NavMeshAgent补偿
        /// </summary>
        private Vector3 CalculateSmartMovement(Vector3 rootMotionDelta)
        {
            // 详细调试信息
            if (navMeshAgent != null)
            {
                float distanceToTarget = navMeshAgent.remainingDistance;
                bool hasPath = navMeshAgent.hasPath;
                bool pathComplete = navMeshAgent.pathStatus == UnityEngine.AI.NavMeshPathStatus.PathComplete;
                Vector3 desiredVel = navMeshAgent.desiredVelocity;

                Debug.Log($"[AIMoveBehavior] NavMesh Status - hasPath: {hasPath}, pathComplete: {pathComplete}, remainingDistance: {distanceToTarget:F2}, desiredVelocity: {desiredVel.magnitude:F2}");
            }

            // 如果Root Motion足够强，优先使用
            if (rootMotionDelta.magnitude > rootMotionThreshold)
            {
                Debug.Log($"[AIMoveBehavior] Using Root Motion: {rootMotionDelta} (magnitude: {rootMotionDelta.magnitude:F4})");
                return rootMotionDelta;
            }

            // Root Motion不足时，使用受限制的NavMeshAgent补偿（如果启用）
            if (enableNavMeshCompensation && navMeshAgent != null && navMeshAgent.hasPath)
            {
                // 检查是否接近目标但还没完全到达
                bool isNearTarget = navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance + 0.5f;
                bool hasDesiredVelocity = navMeshAgent.desiredVelocity.magnitude > navMeshVelocityThreshold;

                if (hasDesiredVelocity || (isNearTarget && navMeshAgent.remainingDistance > 0.1f))
                {
                    Vector3 desiredVelocity = navMeshAgent.desiredVelocity;

                    // 如果接近目标但NavMeshAgent停止了，手动计算朝向目标的方向
                    if (!hasDesiredVelocity && isNearTarget)
                    {
                        Vector3 directionToTarget = (navMeshAgent.destination - transform.position).normalized;
                        desiredVelocity = directionToTarget * (maxCompensationSpeed * 0.5f); // 接近时减速
                        Debug.Log($"[AIMoveBehavior] Near target, using manual direction: {directionToTarget}");
                    }

                    // 限制最大速度
                    if (desiredVelocity.magnitude > maxCompensationSpeed)
                    {
                        desiredVelocity = desiredVelocity.normalized * maxCompensationSpeed;
                    }

                    // 应用补偿强度系数
                    Vector3 compensationMovement = desiredVelocity * Time.deltaTime * compensationStrength;

                    Debug.Log($"[AIMoveBehavior] Using NavMesh compensation: {compensationMovement} (isNear: {isNearTarget}, hasVel: {hasDesiredVelocity})");
                    return compensationMovement;
                }
            }

            Debug.Log($"[AIMoveBehavior] No movement available - Root Motion: {rootMotionDelta.magnitude:F4}, NavMesh conditions not met");
            return Vector3.zero;
        }

        /// <summary>
        /// 智能旋转处理
        /// </summary>
        private void HandleSmartRotation(Quaternion rootMotionRotation)
        {
            // 如果NavMeshAgent有明确的移动方向，朝向该方向
            if (navMeshAgent != null && navMeshAgent.hasPath && navMeshAgent.desiredVelocity.magnitude > navMeshVelocityThreshold)
            {
                Vector3 desiredDirection = navMeshAgent.desiredVelocity.normalized;
                Quaternion targetRotation = Quaternion.LookRotation(desiredDirection);

                // 平滑旋转到目标方向
                transform.rotation = Quaternion.Slerp(
                    transform.rotation,
                    targetRotation,
                    rotationSpeed * Time.deltaTime
                );

                // Debug.Log($"[AIMoveBehavior] Rotating towards NavMesh direction: {desiredDirection}");
            }
            else if (rootMotionRotation != Quaternion.identity)
            {
                // 没有NavMesh方向时，使用Root Motion旋转
                transform.rotation *= rootMotionRotation;
                Debug.Log($"[AIMoveBehavior] Using Root Motion rotation");
            }
        }

        /// <summary>
        /// 检查位置是否在NavMesh上
        /// </summary>
        private bool IsPositionOnNavMesh(Vector3 position)
        {
            UnityEngine.AI.NavMeshHit hit;
            return UnityEngine.AI.NavMesh.SamplePosition(position, out hit, 1.0f, UnityEngine.AI.NavMesh.AllAreas);
        }

        /// <summary>
        /// 同步NavMeshAgent的位置到当前transform位置
        /// 解决Root Motion移动后位置不同步的问题
        /// </summary>
        private void SyncNavMeshAgentPosition()
        {
            if (navMeshAgent != null && navMeshAgent.isOnNavMesh)
            {
                Vector3 oldAgentPos = navMeshAgent.nextPosition;

                // 临时启用位置更新来同步位置
                bool originalUpdatePosition = navMeshAgent.updatePosition;
                navMeshAgent.updatePosition = true;

                // 将NavMeshAgent的位置设置为当前transform位置
                navMeshAgent.nextPosition = transform.position;

                // 恢复原始设置
                navMeshAgent.updatePosition = originalUpdatePosition;

            
            }
        }

        /// <summary>
        /// 尝试识别动画状态名称
        /// </summary>
        private string GetAnimationStateName(int stateHash)
        {
            // 常见的动画状态哈希值
            if (stateHash == Animator.StringToHash("Idle")) return "Idle";
            if (stateHash == Animator.StringToHash("Walking")) return "Walking";
            if (stateHash == Animator.StringToHash("Running")) return "Running";
            if (stateHash == Animator.StringToHash("Attack")) return "Attack";

            // 如果是我们看到的特定哈希值
            if (stateHash == 1744665739) return "Unknown_1744665739";

            return "Unknown";
        }

    }

}