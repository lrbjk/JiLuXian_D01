using System.Collections.Generic;
using UnityEngine;

namespace EnemyAIBase
{
    public abstract class AIGoal : IAIGoal
    {
        protected BaseEnemy owner;
        protected Queue<IAIGoal> subGoals = new Queue<IAIGoal>();
        protected GoalStatus status = GoalStatus.Inactive;

        public GoalStatus Status => status;

        public AIGoal(BaseEnemy owner)
        {
            this.owner = owner;
        }

        public virtual void Activate()
        {
            status = GoalStatus.Active;
        }

        public virtual GoalStatus Process()
        {
            if (subGoals.Count > 0)
            {
                var currentSubGoal = subGoals.Peek();

                if (currentSubGoal.Status == GoalStatus.Inactive)
                {
                    Debug.Log($"[AIGoal] 激活SubGoal: {currentSubGoal.GetType().Name}");
                    currentSubGoal.Activate();
                }

                var subStatus = currentSubGoal.Process();

                if (subStatus == GoalStatus.Completed || subStatus == GoalStatus.Failed)
                {
                    // Debug.Log($"[AIGoal] SubGoal完成: {currentSubGoal.GetType().Name}, Status: {subStatus}");
                    currentSubGoal.Terminate();
                    subGoals.Dequeue();
                }
            }

            return status;
        }

        public virtual void Terminate()
        {
            foreach (var subGoal in subGoals)
            {
                if (subGoal.Status == GoalStatus.Active)
                {
                    subGoal.Terminate();
                }
            }

            subGoals.Clear();
        }

        public virtual void AddSubGoal(IAIGoal subGoal)
        {
            subGoals.Enqueue(subGoal);
        }

        // 添加公共方法来检查SubGoals
        public virtual bool HasActiveSubGoals()
        {
            return subGoals.Count > 0 && subGoals.Peek().Status == GoalStatus.Active;
        }

        public virtual bool HasSubGoalOfType<T>() where T : class
        {
            foreach (var subGoal in subGoals)
            {
                if (subGoal is T)
                {
                    return true;
                }
            }
            return false;
        }

        public virtual IAIGoal GetCurrentSubGoal()
        {
            return subGoals.Count > 0 ? subGoals.Peek() : null;
        }

        public virtual bool HandleInterrupt(InterruptType type)
        {
            return false;
        }
    }
}