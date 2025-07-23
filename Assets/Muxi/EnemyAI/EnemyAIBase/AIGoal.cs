using System.Collections.Generic;

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
                    currentSubGoal.Activate();
                }

                var subStatus = currentSubGoal.Process();

                if (subStatus == GoalStatus.Completed || subStatus == GoalStatus.Failed)
                {
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

        public virtual bool HandleInterrupt(InterruptType type)
        {
            return false;
        }
    }
}