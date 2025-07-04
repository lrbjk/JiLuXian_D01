using AI.FSM.Framework;
namespace AI.FSM
{
    /// <summary>
    /// 描述：
    /// </summary>
    public class RollInputDownTrigger : FSMTrigger
    {
        public override void Init()
        {
            triggerID = FSMTriggerID.RollInputDown;
        }


        public override bool HandleTrigger(FSMBase fSMBase)
        {
            var playerFSM = fSMBase as PlayerFSMBase;
            return (!playerFSM.animator.GetBool("IsInteracting")) &&
                playerFSM.playerInput.Roll &&
                playerFSM.playerInput.Movement > 0;
        }
    }
}
