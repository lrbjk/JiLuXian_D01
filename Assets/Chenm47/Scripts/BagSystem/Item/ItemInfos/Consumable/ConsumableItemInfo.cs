using UnityEngine;

namespace ns.ItemInfos
{
    [CreateAssetMenu(menuName = "Item/ConsumableItem/Consumable")]
    /// <summary>
    /// 描述：
    /// </summary>
    public class ConsumableItemInfo : ItemInfo
    {
        public int QuickMaxCount;

        protected override void InitializeDefaults()
        {
            base.InitializeDefaults();
            iType = ItemType.Consumable;
        }
    }
}
