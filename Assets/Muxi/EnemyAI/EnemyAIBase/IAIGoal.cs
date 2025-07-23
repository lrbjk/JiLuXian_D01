namespace EnemyAIBase
{
    public interface IAIGoal
    {
        GoalStatus Status { get; }
        void Activate();
        GoalStatus Process();
        void Terminate();
        void AddSubGoal(IAIGoal subGoal);
        bool HandleInterrupt(InterruptType type);
    }

}