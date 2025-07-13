using ns.Item.Weapons;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ns.Character.Player
{
    /// <summary>
    /// 描述：
    /// </summary>
    public class PlayerInventory : MonoBehaviour
    {
        //测试，初始携带武器
        public WeaponInfo LeftWeapon;
        public WeaponInfo RightWeapon;

        private WeaponSlotManager slotManager;
        private void Awake()
        {
            slotManager = GetComponentInChildren<WeaponSlotManager>(true);
        }

        private void Start()
        {
            slotManager.LoadWeaponOnSlot(LeftWeapon, true);
            slotManager.LoadWeaponOnSlot(RightWeapon, false);
        }

    }
}
