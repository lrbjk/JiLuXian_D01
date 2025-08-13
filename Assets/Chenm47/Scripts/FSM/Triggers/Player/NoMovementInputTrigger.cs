using AI.FSM.Framework;

namespace AI.FSM
{
    /// <summary>
    /// 描述：
    /// </summary>
    public class NoMovementInputTrigger : MovementInputTrigger
    {
        public override bool HandleTrigger(FSMBase fSMBase)
        {
            //PlayerFSMBase playerFSMBase = fSMBase as PlayerFSMBase;
            //return playerFSMBase.playerInput.Movement < 0.01f;
            return !base.HandleTrigger(fSMBase) &&//插值Movement>=0.01f
                            PlayerFSMBase.Instance.playerInput.RawMovement == 0;//当前确实没有输入
        }

        public override void Init()
        {
            triggerID = FSMTriggerID.NoMovementInput;
        }
    }
}
