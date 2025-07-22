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
        // Check for interrupts
        if (interruptHandler.CheckInterrupts())
        {
            return;
        }
    
        // Process current goal
        if (goalManager.CurrentGoal == null || goalManager.CurrentGoal.Status != GoalStatus.Active)
        {
            // Make new decision
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
        // Factory method to create goals based on decision
        switch (decision)
        {
            
            default:
                return null;
        }
    }

    public GoalManager GetGoalManager() => goalManager;
    public DecisionMaker GetDecisionMaker() => decisionMaker;
}}