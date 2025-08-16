using AI.FSM.Framework;

namespace AI.FSM
{
    /// <summary>
    /// 描述：
    /// </summary>
    public class GunParryInputTrigger : FSMTrigger
    {
        public override bool HandleTrigger(FSMBase fSMBase)
        {
            return PlayerFSMBase.Instance.playerInput.SkillAttackL;//T键射击
        }

        public override void Init()
        {
            triggerID = FSMTriggerID.GunParryInput;
        }
    }
}
