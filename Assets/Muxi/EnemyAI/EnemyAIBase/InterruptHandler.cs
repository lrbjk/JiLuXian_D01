using System.Collections.Generic;

namespace EnemyAIBase
{
    public enum InterruptType
    {
        Damage,
        Threat,
    }

    public class InterruptHandler
    {
        private AIBrain brain;
        private Dictionary<InterruptType, System.Func<bool>> interruptChecks;

        public InterruptHandler(AIBrain brain)
        {
            this.brain = brain;
            interruptChecks = new Dictionary<InterruptType, System.Func<bool>>();
        }

        public void RegisterInterrupt(InterruptType type, System.Func<bool> check)
        {
            interruptChecks[type] = check;
        }

        public bool CheckInterrupts()
        {
            foreach (var kvp in interruptChecks)
            {
                if (kvp.Value())
                {
                    return HandleInterrupt(kvp.Key);
                }
            }

            return false;
        }

        private bool HandleInterrupt(InterruptType type)
        {
            var currentGoal = brain.GetGoalManager().CurrentGoal;
            if (currentGoal != null)
            {
                return currentGoal.HandleInterrupt(type);
            }

            return false;
        }
    }
}