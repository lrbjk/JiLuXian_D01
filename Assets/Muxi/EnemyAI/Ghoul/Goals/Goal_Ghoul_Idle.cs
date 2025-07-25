using EnemyAIBase;
using AI.FSM;
using UnityEngine;

namespace EnemyAIBase
{
    /// <summary>
    /// Ghoul待机目标 - 负责切换到待机状态并监控环境
    /// </summary>
    public class Goal_Ghoul_Idle : AIGoal
    {
        protected new Ghoul owner;
        private GhoulFSMBase fsmBase;
        private float idleStartTime;
        private float maxIdleTime = 3f;

        public Goal_Ghoul_Idle(Ghoul owner) : base(owner)
        {
            this.owner = owner;
            this.fsmBase = owner.GetComponent<GhoulFSMBase>();
        }

        public override void Activate()
        {
            base.Activate();
            
            // 切换到待机状态
            if (fsmBase != null)
            {
                fsmBase.SwitchState(FSMStateID.GhoulIdle);
            }
            
            idleStartTime = Time.time;
            // Debug.Log("Goal_Ghoul_Idle: 激活待机目标");
        }

        public override GoalStatus Process()
        {
           
            if (owner.GetTarget() != null)
            {
                status = GoalStatus.Completed;
                // Debug.Log("Goal_Ghoul_Idle: 发现目标，待机结束");
                return status;
            }

            
            if (Time.time - idleStartTime > maxIdleTime)
            {
                status = GoalStatus.Completed;
                // Debug.Log("Goal_Ghoul_Idle: 待机时间过长，需要巡逻");
                return status;
            }

            return GoalStatus.Active;
        }

        public override void Terminate()
        {
            base.Terminate();
            // Debug.Log("Goal_Ghoul_Idle: 终止待机目标");
        }

        public override bool HandleInterrupt(InterruptType type)
        {
            if (type == InterruptType.Damage)
            {
                // 受到伤害时立即结束待机
                status = GoalStatus.Failed;
                return true;
            }
            return false;
        }
    }
}
