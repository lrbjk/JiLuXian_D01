using AI.FSM; 
using UnityEngine;

namespace EnemyAIBase
{
    [RequireComponent(typeof(EnemyInfo))]
    [RequireComponent(typeof(AIMoveBehavior))]
    [RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
    [RequireComponent(typeof(AIPerception))]
    public class Ghoul : BaseEnemy
    {
        //自己的独属字段
        private GhoulFSMBase fsmBase;
        private EnemyInfo enemyInfo;

        [Header("Ghoul Specific")]
        [Tooltip("玩家检测层")]
        public LayerMask playerLayer = 1 << 8;

        // 公共属性，让Goal可以访问brain
        public AIBrain Brain => brain;

        protected override void Awake()
        {
            base.Awake();
            fsmBase = GetComponent<GhoulFSMBase>();
            enemyInfo = GetComponent<EnemyInfo>();

            // 从EnemyInfo同步基础属性到BaseEnemy
            if (enemyInfo != null)
            {
                sightRange = enemyInfo.SightRange;
                attackRange = enemyInfo.AttackRange;
            }

            // 设置AIPerception事件监听
            SetupPerceptionEvents();
        }

        private void SetupPerceptionEvents()
        {
            if (perception != null)
            {
                perception.OnPlayerDetected += OnPlayerDetected;
                perception.OnPlayerLost += OnPlayerLost;
                perception.OnSuspicionChanged += OnSuspicionChanged;

                Debug.Log("Ghoul: AIPerception事件监听设置完成");
            }
        }

        private void OnPlayerDetected(Transform detectedPlayer)
        {
            target = detectedPlayer;
            // Debug.Log($"Ghoul: 开始检测到玩家 {detectedPlayer.name}，开始积累怀疑度");
        }

        private void OnPlayerLost(Transform lostPlayer)
        {
            target = null;
            Debug.Log($"Ghoul: 失去玩家 {lostPlayer.name}");

            // 切换回和平状态
            var currentGoal = brain.GetGoalManager().CurrentGoal;
            if (currentGoal is Goal_Ghoul_Battle)
            {
                var peacefulGoal = CreateGoalFromDecision("peaceful");
                if (peacefulGoal != null)
                {
                    brain.GetGoalManager().SetGoal(peacefulGoal);
                    Debug.Log("Ghoul: 失去玩家，切换到和平状态");
                }
            }
        }

        private void OnSuspicionChanged(float suspicionLevel)
        {
            // 可以根据怀疑度调整行为
            // 例如：怀疑度高时增加警戒状态
            if (suspicionLevel > 0.5f && suspicionLevel < 1f)
            {
                // 中等怀疑度：可以添加警戒行为
                Debug.Log($"Ghoul: 怀疑度上升 {suspicionLevel:F2}");
            }
        }

        protected override void InitializeAI()
        {
            // 配置高级决策器
            RegisterHighLevelDecisions();
            
            var initialGoal = CreateGoalFromDecision("peaceful");
            if (initialGoal != null)
            {
                brain.GetGoalManager().SetGoal(initialGoal);
            }

            Debug.Log("Ghoul AI初始化完成");
        }

        private void RegisterHighLevelDecisions()
        {
            var decisionMaker = brain.GetDecisionMaker();

            // 注册和平状态决策
            decisionMaker.RegisterAction("peaceful", 50f, (enemy) => {
                // 如果没有完全检测到玩家，倾向于和平状态
                if (perception != null && !perception.IsPlayerFullyDetected())
                {
                    return 1f;
                }
                return 0f;
            });

            // 注册战斗状态决策
            decisionMaker.RegisterAction("battle", 100f, (enemy) => {
                // 如果完全检测到玩家，强烈倾向于战斗状态
                if (perception != null && perception.IsPlayerFullyDetected())
                {
                    return 2f;
                }
                return 0f;
            });

            Debug.Log("Ghoul: 决策器配置完成");
        }

        protected override void Update()
        {
            base.Update();

            // 高级决策：在和平状态和战斗状态之间切换
            MakeHighLevelDecision();
        }

        private void MakeHighLevelDecision()
        {
            var currentGoal = brain.GetGoalManager().CurrentGoal;

            // 处理Goal完成后的状态切换
            if (currentGoal != null && currentGoal.Status == GoalStatus.Completed)
            {
                if (currentGoal is Goal_Ghoul_Peaceful)
                {
                    // 和平状态完成，切换到战斗状态
                    var battleGoal = CreateGoalFromDecision("battle");
                    if (battleGoal != null)
                    {
                        brain.GetGoalManager().SetGoal(battleGoal);
                        Debug.Log("Ghoul: 和平状态完成，切换到战斗状态");
                    }
                }
                else if (currentGoal is Goal_Ghoul_Battle)
                {
                    // 战斗状态完成，切换到和平状态
                    var peacefulGoal = CreateGoalFromDecision("peaceful");
                    if (peacefulGoal != null)
                    {
                        brain.GetGoalManager().SetGoal(peacefulGoal);
                        Debug.Log("Ghoul: 战斗状态完成，切换到和平状态");
                    }
                }
            }

            // 处理从战斗状态回到和平状态的情况（失去目标）
            if (GetTarget() == null && currentGoal is Goal_Ghoul_Battle)
            {
                var peacefulGoal = CreateGoalFromDecision("peaceful");
                if (peacefulGoal != null)
                {
                    brain.GetGoalManager().SetGoal(peacefulGoal);
                    Debug.Log("Ghoul: 失去目标，切换到和平状态");
                }
            }
        }

        protected override void RegisterGoals()
        {
            // 注册工厂
            goalFactory.RegisterGoalCreator("peaceful", (enemy) => new Goal_Ghoul_Peaceful(enemy as Ghoul));
            goalFactory.RegisterGoalCreator("battle", (enemy) => new Goal_Ghoul_Battle(enemy as Ghoul));
            goalFactory.RegisterGoalCreator("idle", (enemy) => new Goal_Ghoul_Idle(enemy as Ghoul));
            goalFactory.RegisterGoalCreator("patrol", (enemy) => new Goal_Ghoul_Patrol(enemy as Ghoul));
            goalFactory.RegisterGoalCreator("chase", (enemy) => new Goal_Ghoul_Chase(enemy as Ghoul));
            goalFactory.RegisterGoalCreator("attack", (enemy) => new Goal_Ghoul_Attack(enemy as Ghoul));
            goalFactory.RegisterGoalCreator("alert", (enemy) => new Goal_Ghoul_Alert(enemy as Ghoul));

            // 注册通用移动Goal 
            goalFactory.RegisterGoalCreator("moveTo", (enemy) => null);

            Debug.Log("Ghoul: 注册Goals完成");
        }

        public override void TransitionToCombat()
        {
            base.TransitionToCombat();
            Debug.Log("Ghoul进入战斗状态");
        }

        protected override void UpdatePerception()
        {
            base.UpdatePerception();
            
            // 目标设置由AIPerception的事件回调处理
            UpdateTargetDistance();
        }

        private void UpdateTargetDistance()
        {
            // 更新到目标的距离
            if (target != null)
            {
                distanceToTarget = Vector3.Distance(transform.position, target.position);
            }
            else
            {
                distanceToTarget = 0f;
            }
        }
        
        private void OnDrawGizmosSelected()
        {
            // 绘制视野范围
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, sightRange);

            // 绘制攻击范围
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackRange);
            
            if (target != null)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawLine(transform.position, target.position);
            }
        }
    }
}