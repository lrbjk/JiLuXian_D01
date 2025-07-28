using AI.FSM.Framework;
using EnemyAIBase;
using UnityEngine;

namespace AI.FSM
{
    /// <summary>
    /// Ghoul待机状态
    /// </summary>
    public class GhoulIdleState : FSMState
    {
        private GhoulFSMBase ghoulFSMBase;
        private EnemyInfo enemyInfo;
        private float idleTimer;

        public override void Init()
        {
            StateID = FSMStateID.GhoulIdle;
        }

        public override void EnterState(FSMBase fSMBase)
        {
            base.EnterState(fSMBase);
            ghoulFSMBase = fSMBase as GhoulFSMBase;
            enemyInfo = fSMBase.GetComponent<EnemyInfo>();
            
            if (fSMBase.animationHandler != null)
            {
                fSMBase.animationHandler.PlayTargetAnimation("Zombie Idle", false, 0.2f);
            }
           

            // 重置待机计时器
            idleTimer = 0f;

            // Debug.Log("Ghoul进入待机状态");
        }

        public override void ActionState(FSMBase fSMBase)
        {
            base.ActionState(fSMBase);

            // 更新待机计时器
            idleTimer += Time.deltaTime;

            // 更新Animator参数
            UpdateAnimatorParameters();
        }

        private void UpdateAnimatorParameters()
        {
            if (ghoulFSMBase?.animator == null) return;

            // 设置移动参数为0（待机状态）
            ghoulFSMBase.animator.SetFloat("Speed", 0f);

            // 只有在参数存在时才设置（兼容不同的Animator Controller）
            if (HasAnimatorParameter("MoveX"))
            {
                ghoulFSMBase.animator.SetFloat("MoveX", 0f);
            }
            if (HasAnimatorParameter("MoveY"))
            {
                ghoulFSMBase.animator.SetFloat("MoveY", 0f);
            }

            // 简单的待机变化动画（如果参数存在）
            if (HasAnimatorParameter("IdleVariation"))
            {
                float idleVariation = Mathf.Sin(Time.time * 0.5f) * 0.1f;
                ghoulFSMBase.animator.SetFloat("IdleVariation", idleVariation);
            }
        }

        private bool HasAnimatorParameter(string paramName)
        {
            if (ghoulFSMBase?.animator == null) return false;

            foreach (AnimatorControllerParameter param in ghoulFSMBase.animator.parameters)
            {
                if (param.name == paramName)
                    return true;
            }
            return false;
        }

        public override void ExitState(FSMBase fSMBase)
        {
            base.ExitState(fSMBase);
            // Debug.Log("Ghoul退出待机状态");
        }
    }
}
