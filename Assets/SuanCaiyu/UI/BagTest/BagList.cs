using UnityEngine;
using System.Collections.Generic;
using ns.BagSystem;


/// <summary>
/// 管理背包的格子（仅存储预制体信息，不实例化）
/// </summary>
public class BagList : MonoBehaviour
{
    [Header("配置")]
    [SerializeField] private GameObject slotPrefab; // 拖入Slot预制体
    [SerializeField] private int initialSlotCount = 50; // 初始格子数量

    [Header("运行时数据")]
    public List<SlotBase> bagItems = new List<SlotBase>();


    void Awake()
    {
        InitializeSlotPrefabReferences();
    }

    /// <summary>
    /// 初始化Slot预制体引用到列表
    /// </summary>
    private void InitializeSlotPrefabReferences()
    {
        bagItems.Clear();

        if (slotPrefab == null)
        {
            Debug.LogError("未分配Slot预制体！", this);
            return;
        }

        SlotBase prefabSlotComponent = slotPrefab.GetComponent<SlotBase>();
        if (prefabSlotComponent == null)
        {
            Debug.LogError("指定的预制体不包含Slot组件！", slotPrefab);
            return;
        }

        for (int i = 0; i < initialSlotCount; i++)
        {
            
            // bagItems.Add(prefabSlotComponent);是错误的写法，相当于将一个预制体复制了49份，但是数据来源都是相同的
            // 创建新的GameObject实例
            GameObject slotObj = Instantiate(slotPrefab);
            slotObj.SetActive(false); // 不激活显示

            // 获取新实例上的组件
            SlotBase newSlot = slotObj.GetComponent<SlotBase>();
            bagItems.Add(newSlot);
        }


        foreach (var slot in bagItems)
        {
            slot.text_1.text = "";
            slot.image_1.sprite = null;
        }

        //将玩家物品信息添加到背包
        InventoryManager.Instance.GetItemLst(ns.ItemInfos.ItemType.Material, out var itemLst);
        Debug.Log("获取到玩家物品信息");
        for (int i = 0; i < itemLst.Count && i < bagItems.Count; i++)
        {
            bagItems[i].text_1.text = itemLst[i].itemInfo.name;
            bagItems[i].image_1.sprite = itemLst[i].itemInfo.ItemIcon;
        }

        for (int i = 0; i < initialSlotCount; i++)
        {
            Debug.Log(bagItems[i].text_1.text);
        }



        Debug.Log($"已成功添加 {bagItems.Count} 个Slot信息到列表");
    }

    //private Slot InstantiateSlotInfo(Slot source, int index)
    //{
    //    // 创建新的Slot实例（仅数据，不是GameObject）
    //    Slot newSlot = new Slot();

    //    // 这里可以复制需要的属性
    //    // 例如：newSlot.text = source.text;
    //    // 注意：这需要修改Slot类使其可序列化

    //    return newSlot;
    //}

    /// <summary>
    /// 获取指定索引的Slot信息
    /// </summary>
    public SlotBase GetSlotInfo(int index)
    {
        if (index >= 0 && index < bagItems.Count)
        {
            return bagItems[index];
        }

        Debug.LogWarning($"请求的索引 {index} 超出范围 (0-{bagItems.Count - 1})");
        return null;
    }
}