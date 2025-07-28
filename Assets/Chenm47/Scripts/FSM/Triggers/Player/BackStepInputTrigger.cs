using AI.FSM.Framework;
namespace AI.FSM
{
    /// <summary>
    /// 描述：
    /// </summary>
    public class BackStepInputTrigger : FSMTrigger
    {
        public override bool HandleTrigger(FSMBase fSMBase)
        {
            var playerFSM = fSMBase as PlayerFSMBase;
            var input = playerFSM.playerInput;

            //return (!playerFSM.animator.GetBool("IsInteracting")) &&
            return input.RollUp &&
                ((input.Movement == 0) || input.LockViewTrigger) &&//没有输入或处于视角锁定
                (!input.RollHoldTrigger);//短按
        }

        public override void Init()
        {
            triggerID = FSMTriggerID.BackStepInput;
        }
    }
}
