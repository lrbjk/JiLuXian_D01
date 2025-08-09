using ns.ItemInfos;
using UnityEngine;

/*

*/
namespace ns.BagSystem.Freamwork
{
    /// <summary>
    /// 描述：
    /// </summary>
    public static class ItemFactory
    {
        public static Item CreateItem(ItemInfo itemInfo, int count = 1)
        {
            if (itemInfo == null)
            {
                Debug.LogError("ItemInfo is null, cannot create item.");
                return null;
            }
            var item = new Item(itemInfo);
            switch (itemInfo.iType)
            {
                case ItemType.Consumable:
                    break;
                case ItemType.Material:
                    break;
                case ItemType.Currency:
                    break;
                case ItemType.HeadEquipMent:
                    break;
                case ItemType.BodyEquipment:
                    break;
                case ItemType.KernelEquipment:
                    break;
                case ItemType.Spell:
                    break;
                case ItemType.Key:
                    break;
                case ItemType.LeftHandWeapon:
                    break;
                case ItemType.RightHandWeapon:
                    break;
                default:
                    break;
            }


            item.CurrentCount = Mathf.Clamp(count, 1, itemInfo.MaxCount);
            return item;
        }
        public static Item CreateItem(int itemID, int count = 1)
        {
            var itemInfo = ItemInfoManager.GetItemInfo(itemID);
            return CreateItem(itemInfo, count);
        }

        public static Item CreateItem(string itemName, int count = 1)
        {
            var itemInfo = ItemInfoManager.GetItemInfo(itemName);
            return CreateItem(itemInfo, count);
        }
    }
}
