using UnityEngine;

namespace ns.Item.Equipment
{

    [CreateAssetMenu(menuName = "Item/Equipment")]
    /// <summary>
    /// 描述：装备信息
    /// </summary>
    public class EquipmentInfo : ItemInfo
    {
        //防御力
        public int DEF;
    }
}
