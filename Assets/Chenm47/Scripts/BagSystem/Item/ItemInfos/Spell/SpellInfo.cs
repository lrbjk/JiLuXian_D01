using UnityEngine;

namespace ns.ItemInfos
{
    [CreateAssetMenu(menuName = "Item/SpellInfo")]
    /// <summary>
    /// 描述：
    /// </summary>
    public class SpellInfo : ItemInfo
    {
        protected override void InitializeDefaults()
        {
            base.InitializeDefaults();
            iType = ItemType.Spell;
        }
    }
}
