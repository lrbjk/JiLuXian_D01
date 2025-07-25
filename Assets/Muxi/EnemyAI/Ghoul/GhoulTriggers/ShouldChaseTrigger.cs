using AI.FSM.Framework;
using EnemyAIBase;
using UnityEngine;

namespace AI.FSM
{
    /// <summary>
    /// 应该追击触发器
    /// </summary>
    public class ShouldChaseTrigger : FSMTrigger
    {
        public override void Init()
        {
            triggerID = FSMTriggerID.ShouldChase;
        }

        public override bool HandleTrigger(FSMBase fSMBase)
        {
            Ghoul ghoul = fSMBase.GetComponent<Ghoul>();
            if (ghoul == null) return false;

            Transform target = ghoul.GetTarget();
            if (target == null)
            {
                // 没有目标时不应该追击
                return false;
            }

            float distanceToTarget = Vector3.Distance(fSMBase.transform.position, target.position);
            bool shouldChase = distanceToTarget <= ghoul.sightRange && distanceToTarget > ghoul.attackRange;

            // 添加调试信息（减少频率）
            if (Time.frameCount % 120 == 0) // 每2秒输出一次
            {
                Debug.Log($"[ShouldChaseTrigger] Target: {target.name}, Distance: {distanceToTarget:F2}, SightRange: {ghoul.sightRange}, AttackRange: {ghoul.attackRange}, ShouldChase: {shouldChase}");
            }

            return shouldChase;
        }
    }
}
