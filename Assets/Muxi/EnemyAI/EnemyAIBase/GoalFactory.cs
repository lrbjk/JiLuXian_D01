using System;
using System.Collections.Generic;

namespace EnemyAIBase
{
    public class GoalFactory
    {
        private Dictionary<string, Func<BaseEnemy, IAIGoal>> goalCreators =
            new Dictionary<string, Func<BaseEnemy, IAIGoal>>();

        public void RegisterGoalCreator(string goalName, Func<BaseEnemy, IAIGoal> creator)
        {
            goalCreators[goalName] = creator;
        }

        public IAIGoal CreateGoal(string goalName, BaseEnemy owner)
        {
            if (goalCreators.ContainsKey(goalName))
            {
                return goalCreators[goalName](owner);
            }

            return null;
        }
    }
}