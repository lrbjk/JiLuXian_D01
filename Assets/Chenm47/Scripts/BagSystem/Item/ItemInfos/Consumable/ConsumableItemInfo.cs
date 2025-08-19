using UnityEngine;

namespace ns.ItemInfos
{
    public enum ConsumableType
    {
       归还类,攻击类,回复类,消耗类
    }
    [CreateAssetMenu(menuName = "Item/ConsumableItem/Consumable")]
    /// <summary>
    /// 描述：
    /// </summary>
    public class ConsumableItemInfo : ItemInfo
    {
        public int QuickMaxCount;
        public ConsumableType cType;
        protected override void InitializeDefaults()
        {
            base.InitializeDefaults();
            iType = ItemType.Consumable;
        }
    }
}
