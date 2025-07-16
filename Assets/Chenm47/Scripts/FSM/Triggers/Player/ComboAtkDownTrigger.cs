using AI.FSM.Framework;

namespace AI.FSM
{
    /// <summary>
    /// 描述：连击状态，返回到Default的Trigger
    /// </summary>
    public class ComboAtkDownTrigger : InteractingDownTrigger
    {
        public override void Init()
        {
            triggerID = FSMTriggerID.ComboAtkDown;
        }


        public override bool HandleTrigger(FSMBase fSMBase)
        {
            bool interactingBool = base.HandleTrigger(fSMBase);
            //此时上一动画结束，interactingBool为true
            //处于后摇阶段且动，画播放完毕interacting也为true
            PlayerFSMBase playerFSMBase = fSMBase as PlayerFSMBase;
            return interactingBool && playerFSMBase.playerInfo.IsInAttackRecoveryFlag;
        }
    }
}
