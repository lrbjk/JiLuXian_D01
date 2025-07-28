using AI.FSM.Framework;
using EnemyAIBase;
using UnityEngine;

namespace AI.FSM
{
    /// <summary>
    /// 距离目标过近触发器（用于从Walking切换到Idle）
    /// </summary>
    public class TooCloseToTargetTrigger : FSMTrigger
    {
        private static float lastAttackTime = 0f; // 与攻击系统共享
        private float attackCooldown = 4f; // 攻击冷却时间
        private float tooCloseDistance = 1.2f; // 过近距离阈值

        public override void Init()
        {
            triggerID = FSMTriggerID.TooCloseToTarget;
        }

        public override bool HandleTrigger(FSMBase fSMBase)
        {
            Ghoul ghoul = fSMBase.GetComponent<Ghoul>();
            if (ghoul == null) return false;

            Transform target = ghoul.GetTarget();
            if (target == null) return false;

            float distanceToTarget = Vector3.Distance(fSMBase.transform.position, target.position);
            
            // 检查是否距离过近
            bool tooClose = distanceToTarget < tooCloseDistance;
            
            if (!tooClose) return false;

            // 检查是否在攻击冷却期间
            float timeSinceLastAttack = Time.time - lastAttackTime;
            bool inCooldown = timeSinceLastAttack < attackCooldown;

            // 只有在距离过近且不能攻击时才触发
            bool shouldIdle = tooClose && inCooldown;

            // 添加调试信息（减少频率）
            if (Time.frameCount % 60 == 0) // 每秒输出一次
            {
                Debug.Log($"[TooCloseToTarget] Distance: {distanceToTarget:F2}, TooClose: {tooClose}, InCooldown: {inCooldown}, ShouldIdle: {shouldIdle}, Cooldown: {timeSinceLastAttack:F1}/{attackCooldown}");
            }

            return shouldIdle;
        }

        // 静态方法用于更新攻击时间（与攻击系统同步）
        public static void UpdateLastAttackTime()
        {
            lastAttackTime = Time.time;
        }
    }
}
