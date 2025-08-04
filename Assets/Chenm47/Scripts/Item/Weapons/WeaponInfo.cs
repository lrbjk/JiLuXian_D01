using ns.Character;
using ns.Value;
using System.Collections.Generic;
using UnityEngine;

namespace ns.Item.Weapons
{
    public enum WeaponType
    {
        Sword,
        Gun
    }


    [CreateAssetMenu(menuName = "Item/Weapon")]
    /// <summary>
    /// 描述：
    /// </summary>
    public class WeaponInfo : ItemInfo
    {
        #region 直接配置数据
        public GameObject ModlePrefab;
        public WeaponType Type;
        [Header("对应起始动作ID")]
        public int LightAtkIDL;
        public int HeavyAtkIDL;
        public int SkillAtkIDL;//左手战技ID
        public int LightAtkIDR;
        public int HeavyAtkIDR;
        public int SkillAtkIDR;//战技
        [HideInInspector]//暂时隐藏
        [Header("初始能力加成配置")]
        public List<WeaponCharacterProperty> WCPOriginalProperties;//初始能力加成配置
        [Header("武器数值")]
        /// <summary>武器数值</summary>
        public WeaponValue WeaponValue;
        #endregion

        #region 运行时获取使用、修改
        [Header("运行时")]
        /// <summary>
        /// 当前实例化的武器GameObject
        /// </summary>
        [HideInInspector]
        public GameObject ModleGO;
        public int CurrentLV;
        [Tooltip("当前武器人物属性加成")]
        public List<WeaponCharacterProperty> WCPCurrentProperties;//当前武器人物属性加成
        [Tooltip("当前异常伤害类型")]
        public AbnormalType CurrentAbnormalType;//当前异常伤害类型
        #endregion
    }
}
