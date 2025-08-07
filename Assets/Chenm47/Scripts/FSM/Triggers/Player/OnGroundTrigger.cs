using AI.FSM.Framework;
using ns.Character.Player;

namespace AI.FSM
{
    /// <summary>
    /// 描述：
    /// </summary>
    public class OnGroundTrigger : FSMTrigger
    {
        public override bool HandleTrigger(FSMBase fSMBase)
        {
            PlayerFSMBase playerFSMBase = fSMBase as PlayerFSMBase;
            //Debug.Log("isOnground" + playerFSMBase.playerAction.IsOnGround());
            var info = playerFSMBase.characterInfo as PlayerInfo;
            return info.IsOnGround;//在地面
        }

        public override void Init()
        {
            triggerID = FSMTriggerID.OnGround;
        }
    }
}
