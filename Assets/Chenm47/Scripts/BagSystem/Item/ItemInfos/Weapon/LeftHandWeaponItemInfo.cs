using ns.ItemInfos;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*

*/
namespace ns.ItemInfos
{
    [CreateAssetMenu(menuName = "Item/Weapon/LeftHandWeapon(Gun)")]
    public class LeftHandWeaponItemInfo : WeaponInfo
    {
        protected override void InitializeDefaults()
        {
            base.InitializeDefaults();
            iType = ItemType.LeftHandWeapon;
            wType = WeaponType.Gun;//左手武器只拿枪
        }
    }
}
