using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*

*/
namespace ns.ItemInfos
{
    [CreateAssetMenu(menuName = "Item/Equipment/Body")]
    public class BodyEquipmentItemInfo : EquipmentInfo
    {
        protected override void InitializeDefaults()
        {
            base.InitializeDefaults();
            iType = ItemType.BodyEquipment;
        }
    }
}
