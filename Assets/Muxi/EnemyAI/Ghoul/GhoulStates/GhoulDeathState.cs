using AI.FSM.Framework;
using EnemyAIBase;
using ns.Movtion;
using UnityEngine;

namespace AI.FSM
{
    /// <summary>
    /// Ghoul死亡状态
    /// </summary>
    public class GhoulDeathState : MovtionState
    {
        private GhoulFSMBase ghoulFSMBase;
        private EnemyInfo enemyInfo;
        private bool deathAnimationStarted = false;
        private bool deathProcessComplete = false;

        public override void Init()
        {
            StateID = FSMStateID.GhoulDeath;
        }

        public override void EnterState(FSMBase fSMBase)
        {
            base.EnterState(fSMBase);
            ghoulFSMBase = fSMBase as GhoulFSMBase;
            enemyInfo = fSMBase.GetComponent<EnemyInfo>();
            
            // 重置死亡标志（进入状态后重置，避免重复触发）
            enemyInfo.IsDied = false;
            deathAnimationStarted = false;
            deathProcessComplete = false;
            
            // 停止所有移动和AI行为
            DisableAIBehaviors();
            
            Debug.Log("Ghoul进入死亡状态");
        }

        protected override MovtionInfo InitMovtionInfo(FSMBase fSMBase)
        {
            // 获取死亡动作信息
            return fSMBase.movtionManager.GetMovtionInfo(fSMBase.characterInfo.CurrentMovtionID);
        }

        // 动作事件处理方法
        protected virtual void OnPreMovtionEnd(object sender, Common.AnimationEventArgs args)
        {
            Debug.Log("Ghoul死亡前摇结束");
            fSMBase.characterInfo.IsInPreMovtionFlag = false;
        }

        protected virtual void OnMovtionStart(object sender, Common.AnimationEventArgs args)
        {
            Debug.Log("Ghoul死亡动作开始");
            deathAnimationStarted = true;
        }

        protected virtual void OnMovtionEnd(object sender, Common.AnimationEventArgs args)
        {
            Debug.Log("Ghoul死亡动作结束");
            deathProcessComplete = true;
            
            // 死亡后的处理
            HandleDeathComplete();
        }

        protected virtual void OnMovtionRecovery(object sender, Common.AnimationEventArgs args)
        {
            Debug.Log("Ghoul死亡后摇开始");
            fSMBase.characterInfo.IsInMovtionRecoveryFlag = true;
        }

        public override void ActionState(FSMBase fSMBase)
        {
            base.ActionState(fSMBase);

            // 确保死亡状态下停止移动
            UpdateAnimatorParameters();
            
            // 持续禁用AI行为
            DisableAIBehaviors();
        }

        private void DisableAIBehaviors()
        {
            var ghoul = ghoulFSMBase.GetComponent<Ghoul>();
            if (ghoul != null)
            {
                // 停止NavMeshAgent
                var navAgent = ghoul.GetComponent<UnityEngine.AI.NavMeshAgent>();
                if (navAgent != null && navAgent.enabled)
                {
                    navAgent.ResetPath();
                    navAgent.velocity = Vector3.zero;
                    navAgent.enabled = false; 
                }

               
                var perception = ghoul.GetComponent<AIPerception>();
                if (perception != null)
                {
                    perception.enabled = false;
                }

         
                ghoul.enabled = false;
        
            }
        }

        private void UpdateAnimatorParameters()
        {
            if (ghoulFSMBase?.animator == null) return;

            // 死亡时停止所有移动参数
            ghoulFSMBase.animator.SetFloat("Speed", 0f);

            if (HasAnimatorParameter("MoveX"))
            {
                ghoulFSMBase.animator.SetFloat("MoveX", 0f);
            }
            if (HasAnimatorParameter("MoveY"))
            {
                ghoulFSMBase.animator.SetFloat("MoveY", 0f);
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

        private void HandleDeathComplete()
        {
            Debug.Log("Ghoul死亡处理完成");
            
            // 可以在这里添加死亡后的特殊处理
            // 例如：掉落物品、经验值、音效等
            
            // 禁用碰撞体（可选）
            var colliders = ghoulFSMBase.GetComponentsInChildren<Collider>();
            foreach (var col in colliders)
            {
                if (!col.isTrigger) // 保留触发器碰撞体
                {
                    col.enabled = false;
                }
            }
            
            // 标记为完全死亡状态
            enemyInfo.IsInvincible = true; // 防止继续受击
        }

        public override void ExitState(FSMBase fSMBase)
        {
            base.ExitState(fSMBase);
            
            // 强制重置IsInteracting参数
            if (ghoulFSMBase?.animator != null)
            {
                ghoulFSMBase.animator.SetBool("IsInteracting", false);
                Debug.Log("Ghoul死亡状态退出：强制重置IsInteracting为false");
            }
            
            Debug.Log("Ghoul退出死亡状态");
        }

        // 检查死亡是否完成的公共方法
        public bool IsDeathComplete()
        {
            return deathProcessComplete;
        }

        // 检查死亡动画是否已开始
        public bool IsDeathAnimationStarted()
        {
            return deathAnimationStarted;
        }
    }
}
