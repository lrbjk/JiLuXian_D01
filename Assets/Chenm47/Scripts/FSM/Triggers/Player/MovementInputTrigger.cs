using AI.FSM.Framework;
using ns.Character.Player;

namespace AI.FSM
{
    /// <summary>
    /// 描述：
    /// </summary>
    public class MovementInputTrigger : FSMTrigger
    {
        public override bool HandleTrigger(FSMBase fSMBase)
        {
            return fSMBase.GetComponent<PlayerInput>().Movement > 0.01f;
        }

        public override void Init()
        {
            triggerID = FSMTriggerID.MovementInput;
        }
    }
}
