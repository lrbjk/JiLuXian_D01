using EnemyAIBase;
using UnityEngine;

namespace EnemyAIBase
{
    /// <summary>
    /// Ghoul和平状态目标 - 负责处理非战斗状态下的行为（待机和巡逻）
    /// 集成AIPerception进行玩家检测
    /// </summary>
    public class Goal_Ghoul_Peaceful : AIGoal
    {
        protected new Ghoul owner;
        private DecisionMaker decisionMaker;
        private AIPerception perception;

        // 决策参数
        private float lastDecisionTime;
        private float decisionInterval = 2f; // 和平状态下决策频率较低

        // 感知状态
        private bool wasPlayerDetected = false;

        public Goal_Ghoul_Peaceful(Ghoul owner) : base(owner)
        {
            this.owner = owner;
            this.perception = owner.GetComponent<AIPerception>();
        }

        public override void Activate()
        {
            base.Activate();

            // 初始化决策器
            this.decisionMaker = new DecisionMaker(owner);

            // 注册和平状态下的可用行动
            RegisterPeacefulActions();

            // 设置感知事件监听
            SetupPerceptionEvents();

            Debug.Log("Goal_Ghoul_Peaceful: 激活和平目标");
        }

        private void SetupPerceptionEvents()
        {
            if (perception != null)
            {
                Debug.Log("Goal_Ghoul_Peaceful: 感知系统已就绪");
            }
        }

        private void RegisterPeacefulActions()
        {
            // 注册待机动作
            decisionMaker.RegisterAction("idle", 30f, (enemy) => {
                // 如果刚刚完成了巡逻或其他活动，倾向于待机
                // 如果有轻微怀疑，降低待机权重
                float suspicionLevel = perception?.GetPlayerSuspicionLevel() ?? 0f;
                return suspicionLevel > 0.3f ? 0.5f : 1f;
            });

            // 注册巡逻动作
            decisionMaker.RegisterAction("patrol", 20f, (enemy) => {
                // 随机巡逻，保持一定的活跃度
                // 如果有怀疑，增加巡逻权重
                float suspicionLevel = perception?.GetPlayerSuspicionLevel() ?? 0f;
                float baseWeight = Random.Range(0.5f, 1.5f);
                return suspicionLevel > 0.2f ? baseWeight * 1.5f : baseWeight;
            });

          
            decisionMaker.RegisterAction("alert", 25f, (enemy) => {
                // 当有中等怀疑度时，进行警戒
                float suspicionLevel = perception?.GetPlayerSuspicionLevel() ?? 0f;
                if (suspicionLevel > 0.3f && suspicionLevel < 0.8f)
                {
                    return 2f; // 高权重
                }
                return 0f;
            });
        }

        public override GoalStatus Process()
        {
            // 只有当AIPerception完全检测到玩家时才切换到战斗状态
   
            if (perception != null && perception.IsPlayerFullyDetected())
            {
                status = GoalStatus.Completed;
                Debug.Log("Goal_Ghoul_Peaceful: AIPerception完全检测到玩家（怀疑度满），切换到战斗状态");
                return status;
            }
            
  
            MonitorPerceptionChanges();

            // 显示怀疑度状态（
            if (Time.frameCount % 120 == 0 && perception != null) // 每2秒显示一次
            {
                float suspicionLevel = perception.GetPlayerSuspicionLevel();
                bool hasPlayer = perception.HasDetectedPlayer;
                bool fullyDetected = perception.IsPlayerFullyDetected();
                // Debug.Log($"[Goal_Ghoul_Peaceful] 怀疑度: {suspicionLevel:F2}, 检测到玩家: {hasPlayer}, 完全检测: {fullyDetected}");
            }

           
            if (Time.time - lastDecisionTime > decisionInterval)
            {
                if (subGoals.Count == 0)
                {
                    MakeDecision();
                }
                lastDecisionTime = Time.time;
            }

            // 处理子目标
            return base.Process();
        }

        private void MonitorPerceptionChanges()
        {
            if (perception == null) return;

            bool currentlyDetected = perception.HasDetectedPlayer;
            float suspicionLevel = perception.GetPlayerSuspicionLevel();

           
            if (currentlyDetected != wasPlayerDetected)
            {
                wasPlayerDetected = currentlyDetected;

                if (currentlyDetected)
                {
                    
                    lastDecisionTime = 0f;
                }
               
            }

            
            if (suspicionLevel > 0.5f)
            {
                
                decisionInterval = 1f;
            }
            else
            {
               
                decisionInterval = 2f;
            }
        }

        private void MakeDecision()
        {
            
            string selectedAction = decisionMaker.MakeDecision();

            if (!string.IsNullOrEmpty(selectedAction))
            {
             
                
                IAIGoal subGoal = CreateSubGoalFromAction(selectedAction);
                if (subGoal != null)
                {
                    AddSubGoal(subGoal);
                    Debug.Log($"Goal_Ghoul_Peaceful: 添加SubGoal {subGoal.GetType().Name}");
                }
            }
            else
            {
                Debug.Log("Goal_Ghoul_Peaceful: 决策器没有返回动作");
            }
        }

        private IAIGoal CreateSubGoalFromAction(string action)
        {
            switch (action)
            {
                case "idle":
                    return new Goal_Ghoul_Idle(owner);

                case "patrol":
                    return new Goal_Ghoul_Patrol(owner);

                case "alert":
                    return new Goal_Ghoul_Alert(owner);

                default:
                    Debug.LogWarning($"Goal_Ghoul_Peaceful: 未知动作 {action}");
                    return null;
            }
        }

        public override void Terminate()
        {
            base.Terminate();
            Debug.Log("Goal_Ghoul_Peaceful: 终止idle目标");
        }

        public override bool HandleInterrupt(InterruptType type)
        {
            if (type == InterruptType.Damage)
            {
                // 受到伤害时立即结束idle状态
                status = GoalStatus.Failed;
                return true;
            }
            return false;
        }
    }
}
