using AI.FSM.Framework;
using EnemyAIBase;
using UnityEngine;

namespace AI.FSM
{
    /// <summary>
    /// 目标进入攻击范围触发器（包含攻击冷却检查）
    /// </summary>
    public class TargetInAttackRangeTrigger : FSMTrigger
    {
        private static float lastAttackTime = 0f; // 与GhoulAttackState共享
        private float attackCooldown = 4f; // 攻击冷却时间

        public override void Init()
        {
            triggerID = FSMTriggerID.TargetInAttackRange;
        }

        public override bool HandleTrigger(FSMBase fSMBase)
        {
            Ghoul ghoul = fSMBase.GetComponent<Ghoul>();
            if (ghoul == null) return false;

            Transform target = ghoul.GetTarget();
            if (target == null) return false;

            float distanceToTarget = Vector3.Distance(fSMBase.transform.position, target.position);

            // 检查目标是否在攻击范围内
            bool inRange = distanceToTarget <= ghoul.attackRange;

            if (!inRange) return false;

            // 检查攻击冷却时间
            float timeSinceLastAttack = Time.time - lastAttackTime;
            bool canAttack = timeSinceLastAttack >= attackCooldown;

            // 添加调试信息（减少频率）
            if (Time.frameCount % 60 == 0) // 每秒输出一次
            {
                Debug.Log($"[TargetInAttackRange] InRange: {inRange}, CanAttack: {canAttack}, Cooldown: {timeSinceLastAttack:F1}/{attackCooldown}");
            }

            return inRange && canAttack;
        }

        // 静态方法用于更新攻击时间（从GhoulAttackState调用）
        public static void UpdateLastAttackTime()
        {
            lastAttackTime = Time.time;
        }

        // 静态方法用于获取攻击时间（供其他系统使用）
        public static float GetLastAttackTime()
        {
            return lastAttackTime;
        }
    }
}
