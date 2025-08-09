using UnityEngine;


namespace ns.ItemInfos
{
    [CreateAssetMenu(menuName = "Item/Equipment/Head")]
    public class HeadEquipmentItemInfo : EquipmentInfo
    {
        protected override void InitializeDefaults()
        {
            base.InitializeDefaults();
            iType = ItemType.HeadEquipMent;
        }
    }
}
