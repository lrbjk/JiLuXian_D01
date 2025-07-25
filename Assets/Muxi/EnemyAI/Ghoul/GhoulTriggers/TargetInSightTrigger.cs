using AI.FSM.Framework;
using EnemyAIBase;
using UnityEngine;

namespace AI.FSM
{
    /// <summary>
    /// 目标进入视野触发器（改进版，避免与TooCloseToTarget冲突）
    /// </summary>
    public class TargetInSightTrigger : FSMTrigger
    {
        private static float lastAttackTime = 0f; // 与攻击系统共享
        private float attackCooldown = 4f; // 攻击冷却时间
        private float tooCloseDistance = 1.2f; // 过近距离阈值

        public override void Init()
        {
            triggerID = FSMTriggerID.TargetInSight;
        }

        public override bool HandleTrigger(FSMBase fSMBase)
        {
            Ghoul ghoul = fSMBase.GetComponent<Ghoul>();
            if (ghoul == null) return false;

            Transform target = ghoul.GetTarget();
            if (target == null) return false;

            float distanceToTarget = Vector3.Distance(fSMBase.transform.position, target.position);

            // 如果距离过近且在冷却期，不应该触发移动
            float timeSinceLastAttack = Time.time - lastAttackTime;
            bool inCooldown = timeSinceLastAttack < attackCooldown;
            bool tooClose = distanceToTarget < tooCloseDistance;

            if (tooClose && inCooldown)
            {
                // 添加调试信息
                if (Time.frameCount % 60 == 0)
                {
                    Debug.Log($"[TargetInSight] 避免冲突 - Distance: {distanceToTarget:F2}, TooClose: {tooClose}, InCooldown: {inCooldown}");
                }
                return false; // 避免与TooCloseToTarget冲突
            }

            // 检查目标是否在视野范围内且距离合适
            if (distanceToTarget <= ghoul.sightRange && distanceToTarget >= tooCloseDistance)
            {
                // 检查视线是否被阻挡（可选的射线检测）
                Vector3 directionToTarget = (target.position - fSMBase.transform.position).normalized;
                RaycastHit hit;

                if (Physics.Raycast(fSMBase.transform.position, directionToTarget, out hit, ghoul.sightRange))
                {
                    // 如果射线击中的是目标，则视野清晰
                    if (hit.transform == target)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        // 静态方法用于更新攻击时间
        public static void UpdateLastAttackTime()
        {
            lastAttackTime = Time.time;
        }
    }
}
