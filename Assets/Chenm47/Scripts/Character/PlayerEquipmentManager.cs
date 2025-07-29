using AI.FSM;
using AI.FSM.Framework;
using ns.Item.Equipment;
using ns.Item.Weapons;
using System.Collections.Generic;
using UnityEngine;

namespace ns.Character.Player
{
    /// <summary>
    /// 描述：
    /// </summary>
    public class PlayerEquipmentManager : CharacterEquipmentManager
    {
        private PlayerInventory playerInventory;//装备武器
        [Tooltip("防具信息")]
        [SerializeField]
        private EquipmentInfo[] equipmentInfos;//装备防具信息
        [Tooltip("核心信息")]
        [SerializeField]
        private KernelInfo currentKernelInfo;
        private CharacterInfo characterInfo;
        protected override void Start()
        {
            base.Start();
            playerInventory = GetComponent<PlayerInventory>();
            characterInfo=GetComponent<CharacterInfo>();
            //测试用
            playerInventory.LoadWeaponOnSlot(playerInventory.LeftWeapon.WInfo, characterInfo, true);
            playerInventory.LoadWeaponOnSlot(playerInventory.RightWeapon.WInfo, characterInfo, false);
        }

        /// <summary>
        /// 装备武器
        /// </summary>
        /// <param name="weaponInfo"></param>
        /// <param name="isLeft"></param>
        public override void EquipWeapon(WeaponInfo weaponInfo, bool isLeft)
        {
            //创建
            playerInventory.LoadWeaponOnSlot(weaponInfo, fSMBase.characterInfo, isLeft);
        }

        public override Weapon GetCurrentAtkWeapon()
        {
            //获取当前武器信息
            var playerFSMBase = fSMBase as PlayerFSMBase;
            //左手？右手？
            bool isLeft = playerFSMBase.playerInput.IsLeftAttackTrigger;
            var lweapon = playerInventory.LeftWeapon;
            var rweapon = playerInventory.RightWeapon;
            Weapon currentWepon = isLeft ? lweapon : rweapon;
            return currentWepon;
        }

        public override IEnumerable<EquipmentInfo> GetEquipmentInfos()
        {
            foreach (var equipmentInfo in equipmentInfos)
            {
                yield return equipmentInfo;
            }
        }

        public Weapon GetHandingWeapon(bool isLeft)
        {
            if (isLeft)
                return playerInventory.LeftWeapon;
            return playerInventory.RightWeapon;
        }

        public override KernelInfo GetKernelInfo()
        {
            return currentKernelInfo;
        }
    }
}
