using AI.FSM.Framework;
using EnemyAIBase;
using ns.Movtion;
using UnityEngine;

namespace AI.FSM
{
    /// <summary>
    /// Ghoul受击反应状态
    /// </summary>
    public class GhoulReactionToHitState : MovtionState
    {
        private GhoulFSMBase ghoulFSMBase;
        private EnemyInfo enemyInfo;
        private bool reactionFinished = false;

        public override void Init()
        {
            StateID = FSMStateID.GhoulReactionToHit;
        }

        public override void EnterState(FSMBase fSMBase)
        {
            base.EnterState(fSMBase);
            ghoulFSMBase = fSMBase as GhoulFSMBase;
            enemyInfo = fSMBase.GetComponent<EnemyInfo>();
            
            // 重置受击标志
            enemyInfo.IsDamaged = false;
            reactionFinished = false;
            
            Debug.Log("Ghoul进入受击反应状态");
        }

        protected override MovtionInfo InitMovtionInfo(FSMBase fSMBase)
        {
            // 获取受击动作信息
            return fSMBase.movtionManager.GetMovtionInfo(fSMBase.characterInfo.CurrentMovtionID);
        }

        // 动作事件处理方法
        public void OnPreMovtionEnd(Common.AnimationEventArgs args)
        {
            Debug.Log("Ghoul受击前摇结束");
        }

        public void OnMovtionStart(Common.AnimationEventArgs args)
        {
            Debug.Log("Ghoul受击动作开始");
        }

        public void OnMovtionEnd(Common.AnimationEventArgs args)
        {
            Debug.Log("Ghoul受击动作结束");
            reactionFinished = true;
        }

        public void OnMovtionRecovery(Common.AnimationEventArgs args)
        {
            Debug.Log("Ghoul受击后摇开始");
            fSMBase.characterInfo.IsInMovtionRecoveryFlag = true;
        }

        public override void ActionState(FSMBase fSMBase)
        {
            base.ActionState(fSMBase);
            
            UpdateAnimatorParameters();

            // 在击状态中Ghoul的特殊行为
           
            
        }

        private void UpdateAnimatorParameters()
        {
            if (ghoulFSMBase?.animator == null) return;

            // 受击时停止移动参数
            ghoulFSMBase.animator.SetFloat("Speed", 0f);

            // 只有在参数存在时才设置
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

        public override void ExitState(FSMBase fSMBase)
        {
            base.ExitState(fSMBase);
            
            // 清理受击状态
            reactionFinished = false;
            
            Debug.Log("Ghoul退出受击反应状态");
        }

        // 检查受击反应是否完成的公共方法
        public bool IsReactionFinished()
        {
            return reactionFinished;
        }
    }
}
