using ns.ItemInfos;
using System;
using UnityEngine;

namespace ns.BagSystem.Freamwork
{
    [Serializable]
    /// <summary>
    /// 描述：物品基类
    /// </summary>
    public class Item
    {
        //public readonly ItemInfo itemInfo;
        public ItemInfo itemInfo;//暂时直接拖拽
        [SerializeField]
        private int currentCount;

        public int CurrentCount { get => currentCount; set => currentCount = value; }

        public Item(ItemInfo itemInfo, int count = 1)
        {
            this.itemInfo = itemInfo;
            currentCount = count;
        }

    }
}
