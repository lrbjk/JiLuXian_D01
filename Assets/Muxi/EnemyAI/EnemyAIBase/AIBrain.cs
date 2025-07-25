namespace EnemyAIBase
{

public class AIBrain
{
    private BaseEnemy owner;
    private GoalManager goalManager;
    private DecisionMaker decisionMaker;
    private InterruptHandler interruptHandler;

    public AIBrain(BaseEnemy owner)
    {
        this.owner = owner;
        goalManager = new GoalManager();
        decisionMaker = new DecisionMaker(owner);
        interruptHandler = new InterruptHandler(this);
    }

    public void Update()
    {
        if (interruptHandler.CheckInterrupts())
        {
            return;
        }
    
        // Process current goal
        if (goalManager.CurrentGoal == null || goalManager.CurrentGoal.Status != GoalStatus.Active)
        {
            var decision = decisionMaker.MakeDecision();
            if (!string.IsNullOrEmpty(decision))
            {
                var newGoal = CreateGoalFromDecision(decision);
                if (newGoal != null)
                {
                    goalManager.SetGoal(newGoal);
                }
            }
        }
    
        goalManager.Process();
    }

    private IAIGoal CreateGoalFromDecision(string decision)
    {
        // 使用BaseEnemy的goalFactory来创建目标
        return owner.CreateGoalFromDecision(decision);
    }

    public GoalManager GetGoalManager() => goalManager;
    public DecisionMaker GetDecisionMaker() => decisionMaker;
}}