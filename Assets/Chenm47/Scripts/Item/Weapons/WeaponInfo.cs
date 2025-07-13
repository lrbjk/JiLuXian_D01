using UnityEngine;

namespace ns.Item.Weapons
{
    [CreateAssetMenu(menuName = "Item/Weapon")]
    /// <summary>
    /// 描述：
    /// </summary>
    public class WeaponInfo : ItemInfo
    {
        public GameObject ModlePrefab;
        public bool IsUnarmed;
    }
}
