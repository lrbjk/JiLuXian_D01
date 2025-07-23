using AI.FSM.Framework;

namespace AI.FSM
{
    /// <summary>
    /// 描述：
    /// </summary>
    public class IsDiedTrigger : FSMTrigger
    {
        public override bool HandleTrigger(FSMBase fSMBase)
        {
            return fSMBase.characterInfo.IsDied;
        }

        public override void Init()
        {
            triggerID = FSMTriggerID.IsDied;
        }
    }
}
