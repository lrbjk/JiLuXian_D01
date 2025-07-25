using AI.FSM.Framework;

namespace AI.FSM
{
    /// <summary>
    /// 描述：
    /// </summary>
    public class DntOnGroundTrigger : OnGroundTrigger
    {
        public override void Init()
        {
            triggerID = FSMTriggerID.DntOnGround;
        }

        public override bool HandleTrigger(FSMBase fSMBase)
        {
            return !base.HandleTrigger(fSMBase);
        }
    }
}
