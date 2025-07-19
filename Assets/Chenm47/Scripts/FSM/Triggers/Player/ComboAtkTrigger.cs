using AI.FSM.Framework;
using ns.Character.Player;
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
            var playerInfo = playerFSMBase.characterInfo as PlayerInfo;


            //Debug.Log($"处于后摇阶段:{playerFSMBase.characterInfo.IsInMovtionRecoveryFlag};攻击输入:{base.HandleTrigger(fSMBase)}" +
            //    $"上次攻击输入与本次是否一致:{playerInfo.LastAttackType == playerFSMBase.playerInput.AtkInputType};下一连击动作ID:{playerInfo.ComboMovtionlID}");
            return playerInfo.IsInMovtionRecoveryFlag &&
                base.HandleTrigger(fSMBase) &&
                playerInfo.LastAttackType == playerFSMBase.playerInput.AtkInputType &&
                playerInfo.ComboMovtionlID != 0;
        }

    }
}
