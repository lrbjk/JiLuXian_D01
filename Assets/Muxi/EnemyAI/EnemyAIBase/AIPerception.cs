using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace EnemyAIBase
{
    [RequireComponent(typeof(SphereCollider))]
    public class AIPerception : MonoBehaviour
    {
        [Header("Vision Settings")] [SerializeField]
        private float viewRadius = 10f;

        [SerializeField] private float viewAngle = 90f;
        [SerializeField] private LayerMask playerMask;
        [SerializeField] private LayerMask obstacleMask;
        [SerializeField] private float heightOffset = 1.5f;

        [Header("Detection Settings")] [SerializeField]
        private float detectionUpdateInterval = 0.2f;

        [SerializeField] private float suspicionIncreaseRate = 1f;
        [SerializeField] private float suspicionDecreaseRate = 0.5f;
        [SerializeField] private float maxSuspicion = 100f;

        [SerializeField] private AIMoveBehavior moveBehavior;

        public bool IsDetected = false;

        // 检测到的目标
        private Transform player;

        private Transform playerTransform;

        private float playerSuspicion = 0f;
        private bool isPlayerInView = false;

        // 简化事件
        public Action<Transform> OnPlayerDetected;
        public Action<Transform> OnPlayerLost;
        public Action<float> OnSuspicionChanged; // 只传递怀疑度

        private SphereCollider detectionCollider;
        private float lastDetectionTime;

        public bool HasDetectedPlayer => isPlayerInView;
        public Transform DetectedPlayer => isPlayerInView ? player : null;

        private void Awake()
        {

            detectionCollider = GetComponent<SphereCollider>();
            detectionCollider.isTrigger = true;
            detectionCollider.radius = viewRadius;
            GetPlayerTransform();
        }

        private void GetPlayerTransform()
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player")?.transform;
        }

        private void Update()
        {
            if (Time.time - lastDetectionTime >= detectionUpdateInterval)
            {
                lastDetectionTime = Time.time;
                UpdateVisibleTargets();
                UpdateSuspicionLevels();
            }
        }

        private void UpdateVisibleTargets()
        {
            isPlayerInView = false;

            // 获取视野范围内的玩家
            Collider[] playersInRadius = Physics.OverlapSphere(
                transform.position + Vector3.up * heightOffset,
                viewRadius,
                playerMask
            );

            if (playersInRadius.Length > 0)
            {
                Transform playerTransform = playersInRadius[0].transform; // 假设只有一个玩家
                Vector3 dirToPlayer = (playerTransform.position - (transform.position + Vector3.up * heightOffset))
                    .normalized;

                // 检查是否在视角范围内
                if (Vector3.Angle(transform.forward, dirToPlayer) < viewAngle / 2)
                {
                    float distToPlayer = Vector3.Distance(
                        transform.position + Vector3.up * heightOffset,
                        playerTransform.position
                    );

                    // 射线检测是否有障碍物
                    if (!Physics.Raycast(
                            transform.position + Vector3.up * heightOffset,
                            dirToPlayer,
                            distToPlayer,
                            obstacleMask))
                    {
                        isPlayerInView = true;

                        // 首次检测到玩家
                        if (player == null)
                        {
                            player = playerTransform;
                            OnPlayerDetected?.Invoke(player);
                            Debug.Log($"[AIPerception] Detected player: {player.name}");
                        }
                    }
                }
            }

            // 检查是否丢失玩家
            if (!isPlayerInView && player != null)
            {
                if (playerSuspicion <= 0)
                {
                    OnPlayerLost?.Invoke(player);
                    player = null;
                }
            }
        }

        private void UpdateSuspicionLevels()
        {
            if (player == null) return;

            if (isPlayerInView)
            {
                // 增加怀疑度
                playerSuspicion = Mathf.Min(
                    playerSuspicion + suspicionIncreaseRate * detectionUpdateInterval,
                    maxSuspicion
                );
            }
            else
            {
                // 减少怀疑度
                playerSuspicion = Mathf.Max(
                    playerSuspicion - suspicionDecreaseRate * detectionUpdateInterval,
                    0f
                );

                if (playerSuspicion <= 0 && player != null)
                {
                    OnPlayerLost?.Invoke(player);
                    player = null;
                }
            }

            OnSuspicionChanged?.Invoke(playerSuspicion / maxSuspicion);
        }

        public float GetPlayerSuspicionLevel()
        {
            return playerSuspicion / maxSuspicion;
        }

        public bool IsPlayerFullyDetected()
        {
            return player != null && playerSuspicion >= maxSuspicion;
        }

        // 设置怀疑度（用于受击等特殊情况）
        public void SetPlayerSuspicionLevel(float normalizedLevel)
        {
            playerSuspicion = Mathf.Clamp(normalizedLevel * maxSuspicion, 0f, maxSuspicion);
            Debug.Log($"AIPerception: 设置怀疑度为 {playerSuspicion}/{maxSuspicion} ({normalizedLevel:F2})");
        }

        // 增加怀疑度
        public void AddSuspicion(float amount)
        {
            playerSuspicion = Mathf.Clamp(playerSuspicion + amount, 0f, maxSuspicion);
        }

        // 可视化调试
        private void OnDrawGizmosSelected()
        {
            // 绘制视野范围
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position + Vector3.up * heightOffset, viewRadius);

            // 绘制视角
            Vector3 viewAngleA = DirFromAngle(-viewAngle / 2, false);
            Vector3 viewAngleB = DirFromAngle(viewAngle / 2, false);

            Gizmos.color = Color.white;
            Gizmos.DrawLine(transform.position + Vector3.up * heightOffset,
                transform.position + Vector3.up * heightOffset + viewAngleA * viewRadius);
            Gizmos.DrawLine(transform.position + Vector3.up * heightOffset,
                transform.position + Vector3.up * heightOffset + viewAngleB * viewRadius);

            // 绘制检测到的目标
            if (isPlayerInView)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(transform.position + Vector3.up * heightOffset, player.position);

                float suspicion = GetPlayerSuspicionLevel();
                Gizmos.color = Color.Lerp(Color.yellow, Color.red, suspicion);
                Gizmos.DrawWireSphere(player.position, 0.5f);
            }
        }

        private Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
        {
            if (!angleIsGlobal)
            {
                angleInDegrees += transform.eulerAngles.y;
            }

            return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
        }
    }
}