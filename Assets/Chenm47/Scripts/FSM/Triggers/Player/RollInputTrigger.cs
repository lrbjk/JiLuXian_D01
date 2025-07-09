using AI.FSM.Framework;

namespace AI.FSM
{
    /// <summary>
    /// 描述：
    /// </summary>
    public class RollInputTrigger : FSMTrigger
    {
        public override void Init()
        {
            triggerID = FSMTriggerID.RollInput;
        }


        public override bool HandleTrigger(FSMBase fSMBase)
        {
            var playerFSM = fSMBase as PlayerFSMBase;
            var input = playerFSM.playerInput;

            //Debug.Log("RollInputTrigger: " + input.RollUp + ", " + input.Movement + ", " + input.RollHoldTrigger);

            return (!playerFSM.animator.GetBool("IsInteracting")) &&
                input.RollUp &&
                input.Movement > 0 &&
                (!input.RollHoldTrigger);//短按
        }
    }
}
