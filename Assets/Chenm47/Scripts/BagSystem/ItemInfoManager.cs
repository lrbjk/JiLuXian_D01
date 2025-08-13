using Helper;
using ns.ItemInfos;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ns.BagSystem
{
    /// <summary>
    /// 描述：获取物品信息
    /// </summary>
    public static class ItemInfoManager
    {
        private static Dictionary<int, ItemInfo> itemInfos;
        private static BiDictionary<string, int> nameToID;

        static ItemInfoManager()
        {
            itemInfos = new Dictionary<int, ItemInfo>();
            nameToID = new BiDictionary<string, int>();
            string basePath = "SO/ItemInfos/";
            int currentID = 1;
            //读取配置文件
            foreach (var itypeName in Enum.GetNames(typeof(ItemType)))
            {
                var t = Type.GetType("ns.ItemInfos." + itypeName + "ItemInfo");
                string path = basePath + itypeName;
                foreach (ItemInfo iteminfo in Resources.LoadAll<ItemInfo>(path))
                {
                    //自动配置ID从1开始
                    iteminfo.ItemID = currentID;
                    //Debug.Log(iteminfo.name);
                    //自动配置名称为物品名
                    if (string.IsNullOrEmpty(iteminfo.ItemName))
                    {
                        iteminfo.ItemName = iteminfo.name;
                    }
                    currentID++;
                    nameToID.Add(iteminfo.ItemName, iteminfo.ItemID);
                    itemInfos.Add(iteminfo.ItemID, iteminfo);
                }
            }
        }

        public static ItemInfo GetItemInfo(int itemID)
        {
            return itemInfos.TryGetValue(itemID, out var itemInfo) ? itemInfo : null;
        }
        public static ItemInfo GetItemInfo(string itemName)
        {
            return itemInfos.TryGetValue(nameToID.GetByKey(itemName), out var itemInfo) ? itemInfo : null;
        }
    }
}
