namespace AI.FSM
{
    /// <summary>
    /// 描述：
    /// </summary>
    public enum FSMTriggerID
    {
        RollInput,
        RollBreak,
        LockedRollInput,
        LockedRollBreak,
        MovementInput,
        NoMovementInput,
        InteractingDown,
        BackStepInput,
        AttackInput,
        AtkRecoverAtkInput,
        ComboAtk,
        ComboAtkDown,
        JumpInput,
        VyNegatived,
        OnGround,
        ToFallEndTimerAndOnGround,
        BackStab,
        ForwardStab,
        IsDamaged,
        IsDied,
        IsOnJumpTop,
        DntOnGround,
        BackStepBreak,

        ClimbStartInput,
        ClimbMoveInput,
        ClimbEndInput,

        // Ghoul  triggers
        TargetInSight,
        TargetInAttackRange,
        TargetLost,
        AttackFinished,
        HitReactionFinished,
        ShouldPatrol,
        ShouldChase,
        TooCloseToTarget,
        PatrolCompleted
    }
}
