using AI.FSM;
using EnemyAIBase;
using UnityEngine;

    public class Goal_Ghoul_Battle:AIGoal
    {   
        protected new Ghoul owner; 
        
        
        public Goal_Ghoul_Battle(Ghoul owner) : base(owner)
        {
            this.owner = owner;
        }
        
        private DecisionMaker decisionMaker; 
        
        //决策参数
        private float lastDecisionTime; 
        private float decisionInterval = 0.5f;
        
        public override void Activate()
        {
            base.Activate();
            
            this.decisionMaker = new DecisionMaker(owner);
            owner.TransitionToCombat();
            
            RegisterBattleActions();

            Debug.Log("Goal_Ghoul_Battle: 激活战斗目标");
        }

        private void RegisterBattleActions()
        {
            // 注册动作
            decisionMaker.RegisterAction("chase", 40f, (enemy) => {
                float distance = enemy.GetDistanceToTarget();
                Transform target = enemy.GetTarget();
                if (target == null) return 0f;

               
                float timeSinceLastAttack = Time.time - TargetInAttackRangeTrigger.GetLastAttackTime();
                bool canAttack = timeSinceLastAttack >= 4f;

                // 如果目标在攻击范围内但在冷却期，chase权重很高（保持距离）
                if (distance <= enemy.attackRange && !canAttack)
                    return 1.8f;

                // 如果目标在视野内但不在攻击范围内，追击权重很高
                if (distance > enemy.attackRange && distance <= enemy.sightRange)
                    return 1.5f;

                return 0.1f;
            });

            // 注册攻击动作
            decisionMaker.RegisterAction("attack", 50f, (enemy) => {
                float distance = enemy.GetDistanceToTarget();
                Transform target = enemy.GetTarget();
                if (target == null) return 0f;

                // 检查攻击冷却时间
                float timeSinceLastAttack = Time.time - TargetInAttackRangeTrigger.GetLastAttackTime();
                bool canAttack = timeSinceLastAttack >= 4f; // 攻击冷却时间

                // 如果目标在攻击范围内且不在冷却期，攻击权重最高
                if (distance <= enemy.attackRange && canAttack)
                    return 2f;

                // 如果在冷却期，降低攻击权重，让chase有机会执行
                if (distance <= enemy.attackRange && !canAttack)
                    return 0.1f;

                return 0f;
            });
            
            decisionMaker.RegisterAction("idle", 10f, (enemy) => {
                Transform target = enemy.GetTarget();
                if (target == null) return 1f; // 没有目标时待机
                return 0.1f; // 有目标时降低待机权重
            });
        }

        public override GoalStatus Process()
        {
            // 检查是否已死亡
            if (CheckIfDead())
            {
                return status;
            }

            // 检查战斗是否应该结束
            if (Time.time - lastDecisionTime > decisionInterval)
            {
                if (subGoals.Count == 0)
                {
                    MakeDecision();
                }
                lastDecisionTime = Time.time;
            }


            base.Process();

            return GoalStatus.Active;
        }

        public void MakeDecision()
        {
            lastDecisionTime = Time.time;

            // 使用决策器选择动作
            string selectedAction = decisionMaker.MakeDecision();

            if (!string.IsNullOrEmpty(selectedAction))
            {
                Debug.Log($"Goal_Ghoul_Battle: 选择动作 {selectedAction}");
                IAIGoal subGoal = CreateSubGoalFromAction(selectedAction);
                if (subGoal != null)
                {
                    AddSubGoal(subGoal);
                    Debug.Log($"Goal_Ghoul_Battle: 添加SubGoal {subGoal.GetType().Name}");
                }
            }
        }

        private IAIGoal CreateSubGoalFromAction(string action)
        {
            switch (action)
            {
                case "chase":
                    return new Goal_Ghoul_Chase(owner);

                case "attack":
                    return new Goal_Ghoul_Attack(owner);

                case "idle":
                    return new Goal_Ghoul_Idle(owner);

                default:
                    return null;
            }
        }

        public override void Terminate()
        {
            base.Terminate();
            Debug.Log("Goal_Ghoul_Battle: 终止战斗目标");
        }

        public override bool HandleInterrupt(InterruptType type)
        {
            if (type == InterruptType.Damage)
            {
                // 受到伤害时，战斗状态不中断，但会传递给SubGoals
                var currentSubGoal = GetCurrentSubGoal();
                if (currentSubGoal != null)
                {
                    return currentSubGoal.HandleInterrupt(type);
                }
                return false;
            }
            return false;
        }
    }
