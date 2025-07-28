using ns.Item.Weapons;
using UnityEngine;


namespace ns.Character.Player
{
    /// <summary>
    /// 描述：
    /// </summary>
    public class PlayerInventory : MonoBehaviour
    {
        //测试，初始携带武器
        public Weapon LeftWeapon;
        public Weapon RightWeapon;

        private WeaponSlotManager slotManager;
        private void Awake()
        {
            slotManager = GetComponentInChildren<WeaponSlotManager>(true);
        }

        private void Start()
        {
            slotManager.LoadWeaponOnSlot(LeftWeapon.Info, true);
            slotManager.LoadWeaponOnSlot(RightWeapon.Info, false);
        }

    }
}
