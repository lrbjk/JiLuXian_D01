using AI.FSM.Framework;
using UnityEngine;

namespace AI.FSM
{
    /// <summary>
    /// Ghoul专用的Animator状态重置行为
    /// 用于在动画状态退出时重置IsInteracting参数
    /// </summary>
    public class GhoulAnimatorResetBehaviour : StateMachineBehaviour
    {
        [Header("重置参数配置")]
        [Tooltip("要重置的参数名")]
        public string parameterName = "IsInteracting";
        
        [Tooltip("重置的目标值")]
        public bool targetValue = false;
        
        [Tooltip("是否需要等待后摇阶段")]
        public bool waitForRecovery = true;
        
        [Tooltip("是否显示调试信息")]
        public bool showDebugInfo = true;

        // OnStateExit在状态转换结束时调用
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (waitForRecovery)
            {
                // 检查是否处于后摇阶段
                var fsmBase = animator.GetComponentInParent<FSMBase>();
                if (fsmBase != null && fsmBase.characterInfo.IsInMovtionRecoveryFlag)
                {
                    ResetParameter(animator, stateInfo);
                }
            }
            else
            {
                // 直接重置参数
                ResetParameter(animator, stateInfo);
            }
        }

        private void ResetParameter(Animator animator, AnimatorStateInfo stateInfo)
        {
            animator.SetBool(parameterName, targetValue);
            
            if (showDebugInfo)
            {
                Debug.Log($"[GhoulAnimatorReset] 状态 '{stateInfo.fullPathHash}' 退出，重置参数 '{parameterName}' 为 {targetValue}");
            }
        }

        // OnStateUpdate在每帧Update时调用（可选）
        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            // 检查动画是否播放完毕但参数仍未重置
            if (stateInfo.normalizedTime >= 1.0f && animator.GetBool(parameterName) != targetValue)
            {
                if (showDebugInfo && Time.frameCount % 60 == 0) // 每秒输出一次警告
                {
                    Debug.LogWarning($"[GhoulAnimatorReset] 动画已播放完毕但参数 '{parameterName}' 仍未重置！");
                }
            }
        }
    }
}
