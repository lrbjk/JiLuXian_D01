using UnityEngine;
using EnemyAIBase;
using AI.FSM;
using ns.Movtion;

namespace AI.FSM
{
    /// <summary>
    /// Ghoul测试辅助类，用于在编辑器中测试Ghoul的FSM功能
    /// </summary>
    public class GhoulTestHelper : MonoBehaviour
    {
        [Header("Test Settings")]
        [Tooltip("是否启用调试日志")]
        public bool enableDebugLog = true;
        
        [Tooltip("是否显示状态信息")]
        public bool showStateInfo = true;
        
        private Ghoul ghoul;
        private GhoulFSMBase ghoulFSM;
        private EnemyInfo enemyInfo;
        
        private void Start()
        {
            ghoul = GetComponent<Ghoul>();
            ghoulFSM = GetComponent<GhoulFSMBase>();
            enemyInfo = GetComponent<EnemyInfo>();
            
            
        }
        
        private void Update()
        {
            if (showStateInfo && ghoulFSM != null)
            {
                // 在屏幕上显示当前状态信息
                DisplayStateInfo();
            }
        }
        
        private void DisplayStateInfo()
        {
            if (enableDebugLog && Time.frameCount % 60 == 0) // 每秒显示一次
            {
                string stateInfo = $"FSM状态: {ghoulFSM.CurrentStateName}";

                // 显示当前Goal信息
                var currentGoal = ghoul.Brain.GetGoalManager().CurrentGoal;
                if (currentGoal != null)
                {
                    stateInfo += $" | Goal: {currentGoal.GetType().Name}";
                    stateInfo += $" | Goal状态: {currentGoal.Status}";
                }

                if (ghoul.GetTarget() != null)
                {
                    float distance = ghoul.GetDistanceToTarget();
                    stateInfo += $" | 目标距离: {distance:F2}";
                }
                else
                {
                    stateInfo += " | 无目标";
                }

                // Debug.Log($"[Ghoul AI] {stateInfo}");
            }
        }
        
        // 编辑器测试方法
        [ContextMenu("强制切换到Idle状态")]
        public void ForceIdleState()
        {
            if (ghoulFSM != null)
            {
                ghoulFSM.SwitchState(FSMStateID.GhoulIdle);
                Debug.Log("强制切换到Idle状态");
            }
        }
        
        [ContextMenu("强制切换到Walking状态")]
        public void ForceWalkingState()
        {
            if (ghoulFSM != null)
            {
                ghoulFSM.SwitchState(FSMStateID.GhoulWalking);
                Debug.Log("强制切换到Walking状态");
            }
        }
        
        [ContextMenu("强制切换到Attack状态")]
        public void ForceAttackState()
        {
            if (ghoulFSM != null)
            {
                ghoulFSM.SwitchState(FSMStateID.GhoulAttack);
                Debug.Log("强制切换到Attack状态");
            }
        }
        
        [ContextMenu("模拟受击")]
        public void SimulateDamage()
        {
            if (enemyInfo != null)
            {
                enemyInfo.IsDamaged = true;
                enemyInfo.CurrentMovtionID = enemyInfo.HitReactionMovtionID;
                Debug.Log("模拟受击，触发受击反应");
            }
        }

        [ContextMenu("强制设置和平Goal")]
        public void ForcePeacefulGoal()
        {
            if (ghoul != null)
            {
                var peacefulGoal = ghoul.CreateGoalFromDecision("peaceful");
                if (peacefulGoal != null)
                {
                    ghoul.Brain.GetGoalManager().SetGoal(peacefulGoal);
                    Debug.Log("强制设置和平Goal");
                }
            }
        }

        [ContextMenu("强制设置战斗Goal")]
        public void ForceBattleGoal()
        {
            if (ghoul != null)
            {
                var battleGoal = ghoul.CreateGoalFromDecision("battle");
                if (battleGoal != null)
                {
                    ghoul.Brain.GetGoalManager().SetGoal(battleGoal);
                    Debug.Log("强制设置战斗Goal");
                }
            }
        }

        [ContextMenu("打印当前Goal信息")]
        public void PrintCurrentGoalInfo()
        {
            if (ghoul != null)
            {
                var currentGoal = ghoul.Brain.GetGoalManager().CurrentGoal;
                if (currentGoal != null)
                {
                    Debug.Log($"当前Goal: {currentGoal.GetType().Name}, 状态: {currentGoal.Status}");

                    // 复合Goal显示子Goal信息
                    if (currentGoal is AIGoal aiGoal)
                    {
                        var subGoalsField = typeof(AIGoal).GetField("subGoals",
                            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                        if (subGoalsField != null)
                        {
                            var subGoals = subGoalsField.GetValue(aiGoal) as System.Collections.Queue;
                            Debug.Log($"子Goal数量: {subGoals?.Count ?? 0}");
                        }
                    }
                }
                else
                {
                    Debug.Log("当前0 Goal");
                }
            }
        }

        [ContextMenu("测试播放Zombie Idle动画")]
        public void TestZombieIdleAnimation()
        {
            if (ghoulFSM != null && ghoulFSM.animationHandler != null)
            {
                ghoulFSM.animationHandler.PlayTargetAnimation("Zombie Idle", false, 0.2f);
                Debug.Log("播放Zombie Idle动画");
            }
        }

        [ContextMenu("测试播放Walking动画")]
        public void TestWalkingAnimation()
        {
            if (ghoulFSM != null && ghoulFSM.animationHandler != null)
            {
                ghoulFSM.animationHandler.PlayTargetAnimation("Walking", true, 0.2f);
                Debug.Log("播放Walking动画");
            }
        }

        [ContextMenu("测试播放Zombie Attack动画")]
        public void TestZombieAttackAnimation()
        {
            if (ghoulFSM != null && ghoulFSM.animationHandler != null)
            {
                ghoulFSM.animationHandler.PlayTargetAnimation("Zombie Attack", true, 0.2f);
                Debug.Log("播放Zombie Attack动画");
            }
        }

        [ContextMenu("测试播放Zombie Reaction Hit动画")]
        public void TestZombieReactionHitAnimation()
        {
            if (ghoulFSM != null && ghoulFSM.animationHandler != null)
            {
                ghoulFSM.animationHandler.PlayTargetAnimation("Zombie Reaction Hit", true, 0.2f);
                Debug.Log("播放Zombie Reaction Hit动画");
            }
        }

        [ContextMenu("测试NavMesh移动")]
        public void TestNavMeshMovement()
        {
            if (ghoul == null) return;

            // 创建一个测试移动Goal
            Vector3 testPosition = ghoul.transform.position + ghoul.transform.forward * 5f;
            var moveGoal = new Goal_MoveToSomeWhere(ghoul, testPosition, 1f, 10f);

            // 设置为当前Goal
            ghoul.Brain.GetGoalManager().SetGoal(moveGoal);

            Debug.Log($"开始测试移动到位置: {testPosition}");
        }

        [ContextMenu("检查AIPerception状态")]
        public void CheckAIPerceptionStatus()
        {
            if (ghoul == null) return;

            var perception = ghoul.GetComponent<AIPerception>();
            if (perception == null)
            {
                Debug.LogError("未找到AIPerception组件！");
                return;
            }

            Debug.Log("=== AIPerception 状态检查 ===");
            Debug.Log($"是否检测到玩家: {perception.HasDetectedPlayer}");
            Debug.Log($"检测到的玩家: {perception.DetectedPlayer?.name ?? "无"}");
            Debug.Log($"怀疑度: {perception.GetPlayerSuspicionLevel():F2}");
            Debug.Log($"是否完全检测: {perception.IsPlayerFullyDetected()}");

            var sphereCollider = ghoul.GetComponent<SphereCollider>();
            if (sphereCollider != null)
            {
                Debug.Log($"检测碰撞体半径: {sphereCollider.radius}");
                Debug.Log($"是否为Trigger: {sphereCollider.isTrigger}");
            }
        }
        
        [ContextMenu("强制设置警戒Goal")]
        public void ForceAlertGoal()
        {
            if (ghoul != null)
            {
                var alertGoal = ghoul.CreateGoalFromDecision("alert");
                if (alertGoal != null)
                {
                    ghoul.Brain.GetGoalManager().SetGoal(alertGoal);
                    Debug.Log("强制设置警戒Goal");
                }
            }
        }

        [ContextMenu("模拟玩家检测")]
        public void SimulatePlayerDetection()
        {
            if (ghoul == null) return;

            var perception = ghoul.GetComponent<AIPerception>();
            if (perception == null)
            {
                Debug.LogError("未找到AIPerception组件！");
                return;
            }

            // 创建一个临时玩家对象进行测试
            GameObject tempPlayer = new GameObject("TempPlayer");
            tempPlayer.tag = "Player";
            tempPlayer.transform.position = ghoul.transform.position + ghoul.transform.forward * 3f;

            Debug.Log("创建临时玩家对象进行检测测试");
            Debug.Log("5秒后将自动删除临时对象");

            // 5秒后删除
            Destroy(tempPlayer, 5f);
        }

      
        private System.Collections.IEnumerator TestSpeedParameter(Animator animator)
        {
            Debug.Log("设置Speed = 0 (应该播放Idle)");
            animator.SetFloat("Speed", 0f);
            yield return new WaitForSeconds(2f);

            Debug.Log("设置Speed = 1 (应该播放Walking)");
            animator.SetFloat("Speed", 1f);
            yield return new WaitForSeconds(2f);

            Debug.Log("设置Speed = 0 (应该回到Idle)");
            animator.SetFloat("Speed", 0f);
            yield return new WaitForSeconds(1f);

            Debug.Log("✅ Speed参数测试完成");
        }

       
        private void OnDrawGizmos()
        {
            if (ghoul != null && showStateInfo)
            {
                // 在场景视图中显示状态信息
                Vector3 textPosition = transform.position + Vector3.up * 3f;
                
#if UNITY_EDITOR
                UnityEditor.Handles.Label(textPosition, $"State: {ghoulFSM?.CurrentStateName ?? "Unknown"}");
#endif
            }
        }
    }
}
