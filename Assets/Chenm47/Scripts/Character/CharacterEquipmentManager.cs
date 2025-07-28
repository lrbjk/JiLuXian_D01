using AI.FSM.Framework;
using ns.Item.Equipment;
using ns.Item.Weapons;
using System.Collections.Generic;
using UnityEngine;

namespace ns.Character
{
    /// <summary>
    /// 描述：角色装备管理
    /// </summary>
    public abstract class CharacterEquipmentManager : MonoBehaviour, IGetCurrentAtkWeapon
    {
        protected FSMBase fSMBase;

        private void Start()
        {
            fSMBase = GetComponent<FSMBase>();
        }

        /// <summary>
        /// 获取当前正在攻击的武器信息
        /// </summary>
        /// <param name="fSMBase"></param>
        /// <returns></returns>
        public abstract Weapon GetCurrentAtkWeapon();

        public abstract IEnumerable<EquipmentInfo> GetEquipmentInfos();

    }
}
