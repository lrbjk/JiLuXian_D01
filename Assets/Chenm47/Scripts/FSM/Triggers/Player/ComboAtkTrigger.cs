using AI.FSM.Framework;
using UnityEngine;

namespace AI.FSM
{
    /// <summary>
    /// 描述：进入连招攻击状态的触发器
    /// </summary>
    public class ComboAtkTrigger : AttackInputTrigger
    {
        public override void Init()
        {
            triggerID = FSMTriggerID.ComboAtk;
        }
        public override bool HandleTrigger(FSMBase fSMBase)
        {
            //处于后摇期间，有攻击输入，并且与上次攻击输入相同，连击SkillID不为0
            var playerFSMBase = fSMBase as PlayerFSMBase;
            Debug.Log($"IsInAttackRecoveryTrigger:{playerFSMBase.playerInfo.IsInAttackRecoveryFlag};AtkInput:{base.HandleTrigger(fSMBase)}" +
                $"equal:{playerFSMBase.playerInfo.LastAttackType == playerFSMBase.playerInput.AtkInputType};ComboSkillID:{playerFSMBase.playerInfo.ComboSkillID}");
            return playerFSMBase.playerInfo.IsInAttackRecoveryFlag &&
                base.HandleTrigger(fSMBase) &&
                playerFSMBase.playerInfo.LastAttackType == playerFSMBase.playerInput.AtkInputType &&
                playerFSMBase.playerInfo.ComboSkillID != 0;
        }

    }
}
