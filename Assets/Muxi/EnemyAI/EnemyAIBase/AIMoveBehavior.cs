using System;

namespace EnemyAIBase
{
    using UnityEngine;
    using UnityEngine.AI;

//TODO: decoupling combat and patrol movebehaviour

    [RequireComponent(typeof(NavMeshAgent), typeof(Animator))]
    public class AIMoveBehavior : MonoBehaviour
    {
        private NavMeshAgent navMeshAgent;
        private Animator animator;

        [Header("Movement Settings")] [SerializeField]
        private float minVelocityForMovement = 0.011f;

        [SerializeField] private float rotationSpeed = 10f;

        [Header("Combat Settings")] [SerializeField]
        private float combatRotationSpeed = 15f;

        // [SerializeField] private string playerTag = "Player"; 
        [SerializeField] private float combatFaceDistance = 10f;

        private Vector3 lastTargetPosition;
        private bool isInCombat = false;
        private Transform playerTransform;

        private static readonly int MOVE_X = Animator.StringToHash("MoveX");
        private static readonly int MOVE_Y = Animator.StringToHash("MoveY");

        [SerializeField] private bool enableDebugLogs = false;

        private void Awake()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();

            navMeshAgent.updatePosition = false;
            navMeshAgent.updateRotation = false;
            animator.applyRootMotion = true;
        }


        private void ExitCombat()
        {
            isInCombat = false;
            playerTransform = null;
            DebugLog("Exited combat mode");
        }

        private void Update()
        {

            // 战斗状态特殊处理
            if (isInCombat && playerTransform != null)
            {

                // 更新动画参数
                UpdateAnimatorParameters();
            }
            else
            {
                // 非战斗状态的正常更新
                UpdateAnimatorParameters();
            }
        }

        private void FaceTarget(Vector3 targetPosition)
        {
            Vector3 direction = (targetPosition - transform.position).normalized;
            direction.y = 0; // 保持水平

            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation,
                    combatRotationSpeed * Time.deltaTime);
            }
        }



        public void SetDestination(Vector3 position)
        {
            navMeshAgent.SetDestination(position);
        }

        private void UpdateAnimatorParameters()
        {
            Vector3 worldVelocity = navMeshAgent.velocity;
            float currentSpeed = worldVelocity.magnitude;

            if (currentSpeed > minVelocityForMovement)
            {
                Vector3 localVelocity = transform.InverseTransformDirection(worldVelocity);
                SetAnimatorMovement(localVelocity.x, localVelocity.z);
                DebugLog($"[AgentMoveBehavior] Moving: X={localVelocity.x:F2}, Y={localVelocity.z:F2}");
            }
            else
            {
                SetAnimatorMovement(0f, 0f);
                DebugLog("[AgentMoveBehavior] Stopped (speed too low)");
            }
        }

        private void SetAnimatorMovement(float x, float z)
        {
            animator.SetFloat(MOVE_X, x);
            animator.SetFloat(MOVE_Y, z);
        }


        private void OnAnimatorMove()
        {
            if (animator.applyRootMotion)
            {
                //  Root Motion 
                Vector3 newPosition = transform.position + animator.deltaPosition;
                navMeshAgent.nextPosition = newPosition;
                transform.position = newPosition;

                if (isInCombat && playerTransform != null)
                {

                    float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
                    if (distanceToPlayer <= combatFaceDistance)
                    {
                        FaceTarget(playerTransform.position);
                        return;
                    }
                }


                Vector3 desiredVelocity = navMeshAgent.desiredVelocity;
                if (desiredVelocity.sqrMagnitude > 0.01f)
                {
                    desiredVelocity.y = 0;
                    Quaternion targetRotation = Quaternion.LookRotation(desiredVelocity);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation,
                        rotationSpeed * Time.deltaTime);
                }
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
    }

}