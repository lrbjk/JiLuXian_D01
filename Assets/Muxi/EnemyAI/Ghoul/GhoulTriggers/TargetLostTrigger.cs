using AI.FSM.Framework;
using EnemyAIBase;
using UnityEngine;

namespace AI.FSM
{
    /// <summary>
    /// 目标丢失触发器
    /// </summary>
    public class TargetLostTrigger : FSMTrigger
    {
        public override void Init()
        {
            triggerID = FSMTriggerID.TargetLost;
        }

        public override bool HandleTrigger(FSMBase fSMBase)
        {
            Ghoul ghoul = fSMBase.GetComponent<Ghoul>();
            if (ghoul == null) return false;

            // 检查当前Goal状态，区分战斗目标和巡逻状态
            var currentGoal = ghoul.Brain?.GetGoalManager()?.CurrentGoal;
            bool isInBattleMode = currentGoal is Goal_Ghoul_Battle;

            Transform target = ghoul.GetTarget();

            // 如果在战斗模式下且没有目标，则认为目标丢失
            if (isInBattleMode && target == null)
            {
                Debug.Log("[TargetLost] 战斗模式下失去敌人目标");
                return true;
            }

            // 如果不在战斗模式，不应该触发TargetLost（可能在巡逻）
            if (!isInBattleMode)
            {
                return false;
            }

            // 在战斗模式下，检查目标距离
            if (target != null)
            {
                float distanceToTarget = Vector3.Distance(fSMBase.transform.position, target.position);

                // 如果目标超出视野范围，则认为目标丢失
                if (distanceToTarget > ghoul.sightRange * 1.2f) // 给一点缓冲距离
                {
                    Debug.Log($"[TargetLost] 目标超出视野范围: {distanceToTarget:F2} > {ghoul.sightRange * 1.2f:F2}");
                    return true;
                }
            }

            return false;
        }
    }
}
