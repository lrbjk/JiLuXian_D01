using UnityEngine;

namespace ns.ItemInfos
{
    [CreateAssetMenu(menuName = "Item/Material")]
    /// <summary>
    /// 描述：
    /// </summary>
    public class MaterialItemInfos : ItemInfo
    {
        protected override void InitializeDefaults()
        {
            base.InitializeDefaults();
            iType = ItemType.Material;
        }
    }
}
