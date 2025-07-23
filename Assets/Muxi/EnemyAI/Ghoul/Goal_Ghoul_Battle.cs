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
            // 初始化决策器
            this.decisionMaker = new DecisionMaker(owner);
            owner.TransitionToCombat();
            // 注册战斗中的可用行动 具体得看技能编辑器
            //decisionMaker.RegisterAction("meleeAttack", 30f, blahblah);

        }

        public override GoalStatus Process()
        {   
            
            
            // 检查战斗是否应该结束
            if (Time.time - lastDecisionTime > decisionInterval)
            {
                if (subGoals.Count == 0)
                {
                    MakeDecision();
                }
            }

            return GoalStatus.Active;
        }

        public void MakeDecision()
        {
            lastDecisionTime = Time.time;
            float distance = owner.GetDistanceToTarget();
            float hp = owner.GetSelfHealthPercent();
            float targetHp = owner.GetPlayerHealthPercent();
        }
    }
