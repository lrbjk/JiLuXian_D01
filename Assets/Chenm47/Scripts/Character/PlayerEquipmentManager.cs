using AI.FSM;
using AI.FSM.Framework;
using ns.Item.Weapons;

namespace ns.Character.Player
{
    /// <summary>
    /// 描述：
    /// </summary>
    public class PlayerEquipmentManager : CharacterEquipmentManager
    {
        public override WeaponInfo GetCurrentAtkWeapon(FSMBase fSMBase)
        {
            //获取当前武器信息
            var playerFSMBase = fSMBase as PlayerFSMBase;
            //左手？右手？
            bool isLeft = playerFSMBase.playerInput.IsLeftAttackTrigger;
            var lweapon = playerFSMBase.playerInventory.LeftWeapon;
            var rweapon = playerFSMBase.playerInventory.RightWeapon;
            WeaponInfo currentWeponInfo = isLeft ? lweapon : rweapon;
            return currentWeponInfo;
        }
    }
}
