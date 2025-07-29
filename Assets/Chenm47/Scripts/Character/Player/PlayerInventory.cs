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
        public PlayerWeapon LeftWeapon;
        public PlayerWeapon RightWeapon;

        private WeaponSlotManager slotManager;
        private void Awake()
        {
            slotManager = GetComponentInChildren<WeaponSlotManager>(true);
        }

        public void LoadWeaponOnSlot(WeaponInfo info, CharacterInfo characterInfo, bool isLeft)
        {
            slotManager.LoadWeaponOnSlot(info, isLeft);
            if (isLeft)
                LeftWeapon = new PlayerWeapon(characterInfo as PlayerInfo, info);
            else
                RightWeapon = new PlayerWeapon(characterInfo as PlayerInfo, info);
        }

    }
}
