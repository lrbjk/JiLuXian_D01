using AI.FSM;
using ns.Item.Equipment;
using ns.Item.Weapons;
using System.Collections.Generic;

namespace ns.Character.Player
{
    /// <summary>
    /// 描述：
    /// </summary>
    public class PlayerEquipmentManager : CharacterEquipmentManager
    {
        public override Weapon GetCurrentAtkWeapon()
        {
            //获取当前武器信息
            var playerFSMBase = fSMBase as PlayerFSMBase;
            //左手？右手？
            bool isLeft = playerFSMBase.playerInput.IsLeftAttackTrigger;
            var lweapon = playerFSMBase.playerInventory.LeftWeapon;
            var rweapon = playerFSMBase.playerInventory.RightWeapon;
            Weapon currentWepon = isLeft ? lweapon : rweapon;
            return currentWepon;
        }

        public override IEnumerable<EquipmentInfo> GetEquipmentInfos()
        {
            yield return null;
        }

    }
}
