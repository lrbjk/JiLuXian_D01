using UnityEngine;

/*

*/
namespace ns.ItemInfos
{
    [CreateAssetMenu(menuName = "Item/Weapon/RightHandWeapon")]
    public class RightHandWeaponItemInfo : WeaponInfo
    {
        protected override void InitializeDefaults()
        {
            base.InitializeDefaults();
            iType = ItemType.RightHandWeapon;
            wType = WeaponType.Sword;
        }
    }
}
