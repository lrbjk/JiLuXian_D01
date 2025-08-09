using UnityEngine;

namespace ns.ItemInfos
{
    [CreateAssetMenu(menuName = "Item/KeyItem")]
    /// <summary>
    /// 描述：
    /// </summary>
    public class KeyItemInfo : ItemInfo
    {
        protected override void InitializeDefaults()
        {
            base.InitializeDefaults();
            iType = ItemType.Key;
        }
    }
}
