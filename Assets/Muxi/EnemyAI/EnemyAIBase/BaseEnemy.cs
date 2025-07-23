using System.Collections.Generic;
using UnityEngine;

namespace EnemyAIBase
{
    public abstract class BaseEnemy : MonoBehaviour
    {
        //AI基础属性
        [Header("Basic Stats")] public float maxHealth = 100f;
        public float currentHealth;
        public float attackRange = 2f;
        public float sightRange = 10f;

        //TODO:整合子目标的实际执行 GAS System

        //AI基础组件
        [Header("AI Components")] protected AIBrain brain;
        protected AIPerception perception;
        protected AIMoveBehavior motor;
        protected GoalFactory goalFactory = new GoalFactory();
        // protected EnemyCombat combat;

        //玩家信息
        private float playerHp;
        private float playerHprate;
        private float convertValue;
        protected Transform target;
        protected float distanceToTarget;

        // AI State状态
        public enum AIState
        {
            Idle,
            Patrol,
            Combat,
            Alert,
            Retreat
        }

        public AIState currentState = AIState.Idle;

        //AI记忆层
        protected Dictionary<string, float> aiMemory = new Dictionary<string, float>();

        protected virtual void Awake()
        {
            brain = new AIBrain(this);
            perception = GetComponent<AIPerception>();
            motor = GetComponent<AIMoveBehavior>();
            // combat = GetComponent<EnemyCombat>();

            currentHealth = maxHealth;
            RegisterGoals();
            InitializeAI();
        }

        protected abstract void InitializeAI();
        protected abstract void RegisterGoals();

        public IAIGoal CreateGoalFromDecision(string decision)
        {
            return goalFactory.CreateGoal(decision, this);
        }

        protected virtual void Update()
        {
            UpdatePerception();
            brain.Update();
        }

        protected virtual void UpdatePerception()
        {
            // target = perception.GetNearestEnemy();

            if (target != null)
            {
                distanceToTarget = Vector3.Distance(transform.position, target.position);
            }
        }

        public float GetDistanceToTarget() => distanceToTarget;
        public Transform GetTarget() => target;
        public float GetSelfHealthPercent() => currentHealth / maxHealth;

        public float GetPlayerHealthPercent() => playerHprate;

        public void SetMemory(string key, float value)
        {
            aiMemory[key] = value;
        }

        public float GetMemory(string key, float defaultValue = 0f)
        {
            return aiMemory.ContainsKey(key) ? aiMemory[key] : defaultValue;
        }


        public virtual void TransitionToCombat()
        {
            currentState = AIState.Combat;

        }

        public virtual void TransitionToPatrol()
        {
            currentState = AIState.Patrol;

        }
    }
}