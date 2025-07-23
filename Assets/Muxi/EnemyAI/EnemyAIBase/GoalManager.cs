using System.Collections.Generic;

namespace EnemyAIBase
{
    public class GoalManager
    {
        private IAIGoal currentGoal;
        private Stack<IAIGoal> goalStack = new Stack<IAIGoal>();

        public IAIGoal CurrentGoal => currentGoal;

        public void SetGoal(IAIGoal goal)
        {
            if (currentGoal != null && currentGoal.Status == GoalStatus.Active)
            {
                currentGoal.Terminate();
            }

            currentGoal = goal;
            currentGoal.Activate();
        }

        public void PushGoal(IAIGoal goal)
        {
            if (currentGoal != null)
            {
                goalStack.Push(currentGoal);
            }

            SetGoal(goal);
        }

        public void PopGoal()
        {
            if (goalStack.Count > 0)
            {
                SetGoal(goalStack.Pop());
            }
        }

        public void Process()
        {
            if (currentGoal != null)
            {
                currentGoal.Process();
            }
        }
    }
}