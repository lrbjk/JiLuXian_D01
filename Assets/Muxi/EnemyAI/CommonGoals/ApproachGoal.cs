using EnemyAIBase;
using UnityEngine;
//Pseudo Goal 
public class Goal_Approach : AIGoal
{
    private float targetDistance;
    private float timeoutDuration = 5f;
    private float startTime;

    public Goal_Approach(BaseEnemy owner, float targetDistance) : base(owner)
    {
        this.targetDistance = targetDistance;
    }

    public override void Activate()
    {
        base.Activate();
        startTime = Time.time;
    }
    
    public override GoalStatus Process()
    {
        // Check timeout
        if (Time.time - startTime > timeoutDuration)
        {
            status = GoalStatus.Failed;
            return status;
        }
    
       
        float currentDistance = owner.GetDistanceToTarget();
        if (currentDistance <= targetDistance)
        {
            status = GoalStatus.Completed;
            return status;
        }
        
        var motor = owner.GetComponent<AIMoveBehavior>();
        motor.SetDestination(owner.GetTarget().position);
    
        return GoalStatus.Active;
    }

    public override bool HandleInterrupt(InterruptType Damage)
    {
       //实现打断逻辑
       return true;
    }
}