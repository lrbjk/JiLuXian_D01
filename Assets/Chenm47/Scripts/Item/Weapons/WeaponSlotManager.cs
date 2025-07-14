using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*

*/
namespace ns.Item.Weapons
{
    /// <summary>
    /// 描述：
    /// </summary>
    public class WeaponSlotManager : MonoBehaviour
    {
        WeaponHolderSlot leftHandSlot;
        WeaponHolderSlot rightHandSlot;

        private void Awake()
        {
            var weaponHolderSlots = GetComponentsInChildren<WeaponHolderSlot>(true);
            foreach (var slot in weaponHolderSlots)
            {
                if (slot.IsLeftHandSlot)
                {
                    leftHandSlot = slot;
                }
                else if (slot.IsRightHandSlot)
                {
                    rightHandSlot = slot;
                }
            }
        }

        public void LoadWeaponOnSlot(WeaponInfo weaponInfo, bool isLeft)
        {
            if (isLeft)
                leftHandSlot.LoadWeaponModel(weaponInfo, isLeft);
            else
                rightHandSlot.LoadWeaponModel(weaponInfo, isLeft);
        }
    }
}
