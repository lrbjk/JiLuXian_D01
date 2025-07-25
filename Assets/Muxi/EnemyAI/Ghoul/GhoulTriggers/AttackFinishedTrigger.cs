using AI.FSM.Framework;
using UnityEngine;

namespace AI.FSM
{
    /// <summary>
    /// 攻击完成触发器
    /// </summary>
    public class AttackFinishedTrigger : FSMTrigger
    {
        public override void Init()
        {
            triggerID = FSMTriggerID.AttackFinished;
        }

        public override bool HandleTrigger(FSMBase fSMBase)
        {
            // 检查当前状态是否为攻击状态，并且攻击已完成
            if (fSMBase.CurrentState is GhoulAttackState attackState)
            {
                bool isFinished = attackState.IsAttackFinished();

                // 添加调试信息（减少频率）
                if (Time.frameCount % 120 == 0) // 每2秒输出一次
                {
                    Debug.Log($"[AttackFinishedTrigger] IsAttackFinished: {isFinished}");
                }

                return isFinished;
            }

            // 如果不在攻击状态，检查是否不在动作后摇阶段
            return !fSMBase.characterInfo.IsInMovtionRecoveryFlag;
        }
    }
}
