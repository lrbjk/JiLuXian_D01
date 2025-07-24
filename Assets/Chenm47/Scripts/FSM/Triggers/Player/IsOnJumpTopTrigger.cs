using AI.FSM.Framework;
using ns.Character.Player;

namespace AI.FSM
{
    /// <summary>
    /// 描述：
    /// </summary>
    public class IsOnJumpTopTrigger : FSMTrigger
    {
        public override bool HandleTrigger(FSMBase fSMBase)
        {
            PlayerInfo playerInfo = fSMBase.characterInfo as PlayerInfo;

            return playerInfo.IsOnTop;
        }

        public override void Init()
        {
            triggerID = FSMTriggerID.IsOnJumpTop;
        }
    }
}
