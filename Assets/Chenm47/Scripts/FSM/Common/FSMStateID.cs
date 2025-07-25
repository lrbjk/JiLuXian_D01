namespace AI.FSM
{
    /// <summary>
    /// 描述：命名规则：AI.FSM.FSMStateID+"State"
    /// </summary>
    public enum FSMStateID
    {
        Default,
        Idle,
        Move,
        Test,
        Roll,
        BackStep,
        Attack,
        ComboAttack,
        MovtionState,
        Fall,
        FallEnd,
        Jump,
        BackStab,
        ForwardStab,
        Died,
        Damaged,
        DirectFall,
        // Ghoul specific states
        GhoulIdle,
        GhoulWalking,
        GhoulReactionToHit,
        GhoulAttack
    }
}
