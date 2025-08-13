using ns.ItemInfos;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*

*/
namespace ns.ItemInfos
{
    [CreateAssetMenu(menuName = "Item/ConsumableItem/Currency")]
    /// <summary>
    /// 描述：
    /// </summary>
    public class CurrencyItemInfo : ItemInfo
    {
        public int QuickMaxCount;
        public int CurrencyValue;
        protected override void InitializeDefaults()
        {
            base.InitializeDefaults();
            iType = ItemType.Currency;
        }
    }
}
