using EnemyAIBase;
using AI.FSM;
using UnityEngine;

namespace EnemyAIBase
{
    /// <summary>
    /// Ghoul攻击目标 - 负责切换到攻击状态并执行攻击逻辑
    /// </summary>
    public class Goal_Ghoul_Attack : AIGoal
    {
        protected new Ghoul owner;
        private GhoulFSMBase fsmBase;
        private Transform target;
        private bool attackExecuted = false;
        private float attackStartTime;
        private float maxAttackTime = 3f; // 最大攻击时间，防止卡死

        public Goal_Ghoul_Attack(Ghoul owner) : base(owner)
        {
            this.owner = owner;
            this.fsmBase = owner.GetComponent<GhoulFSMBase>();
        }

        public override void Activate()
        {
            base.Activate();
            
            target = owner.GetTarget();
            if (target == null)
            {
                status = GoalStatus.Failed;
                Debug.Log("Goal_Ghoul_Attack: 没有攻击目标，攻击失败");
                return;
            }
            
            // 检查攻击范围
            float distanceToTarget = owner.GetDistanceToTarget();
            if (distanceToTarget > owner.attackRange)
            {
                status = GoalStatus.Failed;
                Debug.Log("Goal_Ghoul_Attack: 目标超出攻击范围，攻击失败");
                return;
            }
            
            // 切换到攻击状态
            if (fsmBase != null)
            {
                fsmBase.SwitchState(FSMStateID.GhoulAttack);
            }
            
            attackExecuted = false;
            attackStartTime = Time.time;
            Debug.Log("Goal_Ghoul_Attack: 激活攻击目标");
        }

        public override GoalStatus Process()
        {
            // 检查攻击超时
            if (Time.time - attackStartTime > maxAttackTime)
            {
                status = GoalStatus.Failed;
                Debug.Log("Goal_Ghoul_Attack: 攻击超时，攻击失败");
                return status;
            }
            
            // 检查目标是否还存在
            target = owner.GetTarget();
            if (target == null)
            {
                status = GoalStatus.Failed;
                Debug.Log("Goal_Ghoul_Attack: 攻击目标消失，攻击失败");
                return status;
            }
            
            // 检查攻击是否完成
            if (fsmBase != null && fsmBase.CurrentState is GhoulAttackState attackState)
            {
                if (attackState.IsAttackFinished())
                {
                    status = GoalStatus.Completed;
                    Debug.Log("Goal_Ghoul_Attack: 攻击完成");
                    return status;
                }
            }
            else if (fsmBase != null && fsmBase.CurrentState.StateID != FSMStateID.GhoulAttack)
            {
                // 如果不在攻击状态，说明攻击可能被打断或完成
                status = GoalStatus.Completed;
                Debug.Log("Goal_Ghoul_Attack: 不在攻击状态，攻击结束");
                return status;
            }

            return GoalStatus.Active;
        }

        public Transform GetAttackTarget()
        {
            return target;
        }

        public override void Terminate()
        {
            base.Terminate();
            target = null;
            attackExecuted = false;
            Debug.Log("Goal_Ghoul_Attack: 终止攻击目标");
        }

        public override bool HandleInterrupt(InterruptType type)
        {
            if (type == InterruptType.Damage)
            {
                // 受到伤害时中断攻击
                status = GoalStatus.Failed;
                return true;
            }
            return false;
        }
    }
}
