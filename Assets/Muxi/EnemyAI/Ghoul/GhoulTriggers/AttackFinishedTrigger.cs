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
                bool isInRecovery = fSMBase.characterInfo.IsInMovtionRecoveryFlag;
                bool isInteracting = false;
                var animator = fSMBase.GetComponent<Animator>();
                if (animator != null)
                {
                    isInteracting = animator.GetBool("IsInteracting");
                }

                bool reallyFinished = isFinished && isInRecovery && !isInteracting;


                if (Time.frameCount % 240 == 0) 
                {
                    Debug.Log($"[AttackFinishedTrigger] IsAttackFinished: {isFinished}, IsInRecovery: {isInRecovery}, IsInteracting: {isInteracting}, ReallyFinished: {reallyFinished}");
                }

                return reallyFinished;
            }

            // 如果不在攻击状态，检查是否不在动作后摇阶段
            return !fSMBase.characterInfo.IsInMovtionRecoveryFlag;
        }
    }
}
