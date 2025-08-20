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
        InteractingDownMovement,
        BackStepInput,
        AttackInput,
        AtkRecoverAtkInput,
        GunParryInput,
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
        TakeMedicineInput,

        ClimbStartInput,
        ClimbMoveInput,
        ClimbMoveComboInput,
        ClimbQuickDownInput,
        ClimbQuickDownCancel,
        ClimbEndInput,

        //Break
        BreakAttackInput,
        BreakMoveInput,

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
