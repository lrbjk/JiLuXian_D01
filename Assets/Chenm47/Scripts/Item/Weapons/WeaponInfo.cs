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
        public GameObject ModlePrefab;
        public bool IsUnarmed;
        public WeaponType Type;
        [Header("对应起始动作ID")]
        public int LightAtkIDL;
        public int HeavyAtkIDL;
        public int SkillAtkIDL;//左手战技ID
        public int LightAtkIDR;
        public int HeavyAtkIDR;
        public int SkillAtkIDR;//战技
        /// <summary>
        /// 当前实例化的武器GameObject
        /// </summary>
        [HideInInspector]
        public GameObject ModleGO;
    }
}
