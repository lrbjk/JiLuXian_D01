using ns.ItemInfos;
using System;
using System.Collections.Generic;
using UnityEngine;
using Item = ns.BagSystem.Freamwork.Item;

namespace ns.BagSystem
{
    /// <summary>
    /// 描述：
    /// </summary>
    public class InventoryManager : MonoSingleton<InventoryManager>
    {
        [Header("测试初始载入的物品")]
        public Item[] TestHaveItems;

        public Dictionary<ItemType, List<Item>> ItemsCategoryDic;

        protected override void Init()
        {
            base.Init();
            var itemTypes = Enum.GetValues(typeof(ItemType));
            ItemsCategoryDic = new Dictionary<ItemType, List<Item>>(itemTypes.Length);
            foreach (ItemType itemType in itemTypes)
            {
                ItemsCategoryDic[itemType] = new List<Item>();
            }
            foreach (var item in TestHaveItems)
            {
                AddItem(item);
            }
        }

        public void AddItem(Item item)
        {
            var info = item.itemInfo;
            var categoryLst = ItemsCategoryDic[info.iType];
            if (info.MaxCount > 1)
            {
                int addCount = item.CurrentCount;
                // 查找可堆叠的物品
                foreach (var existItem in categoryLst)
                {
                    if (existItem.itemInfo.ItemID == info.ItemID && existItem.CurrentCount < info.MaxCount)
                    {
                        int canStack = info.MaxCount - existItem.CurrentCount;
                        int toAdd = Math.Min(addCount, canStack);
                        existItem.CurrentCount += toAdd;
                        addCount -= toAdd;
                        if (addCount <= 0)
                            break;
                    }
                }
                // 剩余部分，按最大堆叠上限分批新建
                while (addCount > 0)
                {
                    int batchCount = Math.Min(addCount, info.MaxCount);
                    var newItem = new Item(info);
                    newItem.CurrentCount = batchCount;
                    categoryLst.Add(newItem);
                    addCount -= batchCount;
                }
            }
            else
            {
                // 不可堆叠，直接添加
                categoryLst.Add(item);
            }
        }

        // 移除方法（通过itemID，从后往前移除）
        public bool RemoveItem(int itemID, int amount = 1)
        {
            // 尝试在所有分类中查找物品
            List<Item> targetList = null;

            ItemInfo itemInfo = ItemInfoManager.GetItemInfo(itemID);
            GetItemLst(itemInfo.iType, out targetList);

            if (targetList == null)
            {
                Debug.LogError("未找到物品类型：" + ItemInfoManager.GetItemInfo(itemID).iType);
                return false;
            }

            return RemoveItemFromList(itemInfo, amount, targetList);
        }
        private bool RemoveItemFromList(ItemInfo itemInfo, int amount, List<Item> targetList)
        {
            var ary = targetList.FindAll(item => item.itemInfo.ItemID == itemInfo.ItemID).ToArray();
            for (int i = ary.Length - 1; i >= 0 && amount > 0; i--)
            {
                int canRemove = Math.Min(ary[i].CurrentCount, amount);
                ary[i].CurrentCount -= canRemove;
                amount -= canRemove;
                if (ary[i].CurrentCount <= 0)
                {
                    targetList.Remove(ary[i]);
                }
            }
            return amount == 0;// 是否移除了指定数量
        }
        //// 全局移除方法（移除Item实例指定数量）
        //public bool RemoveItem(Item item, int amount = 1)
        //{
        //    List<Item> targetList = null;
        //    GetItemLst(item.itemInfo.iType, out targetList);
        //    if (targetList == null)
        //    {
        //        Debug.LogError("未找到物品类型：" + item.itemInfo.iType);
        //        return false;
        //    }
        //    return RemoveItemObjectFromList(item, amount, targetList);
        //}
        //private bool RemoveItemObjectFromList(Item item, int amount, List<Item> targetList)
        //{
        //    return targetList.Remove(item);
        //}
        public void GetItemLst(ItemType itemType, out List<Item> itemLst)
        {
            if (ItemsCategoryDic.TryGetValue(itemType, out itemLst))
            {
                return;
            }
            Debug.Log("未找到物品类型：" + itemType);
        }

        public IEnumerable<Item> GetAllItems()
        {
            foreach (var itemLst in ItemsCategoryDic.Values)
            {
                foreach (var item in itemLst)
                {
                    yield return item;
                }
            }
        }

    }
}
