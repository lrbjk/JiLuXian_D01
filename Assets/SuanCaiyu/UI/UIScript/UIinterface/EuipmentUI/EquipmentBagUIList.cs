using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ns.BagSystem;
using Common.UI;

public class EquipmentBagUIList : MonoBehaviour
{
   [Header("配置")]
    [SerializeField] private GameObject slotPrefab; // 拖入Slot预制体
    [SerializeField] private int initialSlotCount = 50; // 初始格子数量
    public int EquipmentNumber = 0;//当前显示的装备数量

    [Header("运行时数据")]
    public List<SlotBase> equipBagItems = new List<SlotBase>(); //背包物体的显示列表
    public List<EquipmentSlot> equipmentData = new List<EquipmentSlot>(); //背包物体的数据和交互列表

    /// <summary>
    /// 背包分类的枚举，用于上方选项的通信
    /// </summary>
    public enum EquipItemCategory
    { None, HeadEquipment , BodyEquipment , KernelEquipment , RightHandWeapon , LeftHandWeapon } 
    public EquipItemCategory currentEquipCategory = EquipItemCategory.None;

    public EquipmentBagScroller equipBagScroller;

    ///// <summary>
    ///// 根据插槽中的武器选中状态更新背包武器选中状态
    ///// </summary>
    //[SerializeField] private EquipmentUIFunc equipmentUIFunc;
    //private EquipmentSelector equipmentSelector;
    public void OnEnable()
    {
        InitializeSlotPrefabReferences();
    }


    /// <summary>
    /// 初始化Slot预制体引用到列表
    /// </summary>
    public void InitializeSlotPrefabReferences()
    {
        equipBagItems.Clear();
        equipmentData.Clear();//选中状态也会被重置

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
            equipBagItems.Add(newSlot);
        }


        //foreach (var slot in bagItems)
        //{
        //    slot.text_1.text = "";
        //    slot.image_1.sprite = null;
        //}


        //将玩家物品信息添加到装备背包
         if (currentEquipCategory == EquipItemCategory.HeadEquipment)
        {
            InventoryManager.Instance.GetItemLst(ns.ItemInfos.ItemType.HeadEquipMent, out var itemLst);
            for (int i = 0; i < itemLst.Count && i < equipBagItems.Count; i++)
            {
                equipBagItems[i].text_1.text = itemLst[i].itemInfo.name;
                equipBagItems[i].image_1.sprite = itemLst[i].itemInfo.ItemIcon;
                equipBagItems[i].ableToEquiped = true;
            }
            EquipmentNumber = itemLst.Count;
        }
        else if (currentEquipCategory == EquipItemCategory.BodyEquipment)
        {
            InventoryManager.Instance.GetItemLst(ns.ItemInfos.ItemType.BodyEquipment, out var itemLst);
            for (int i = 0; i < itemLst.Count && i < equipBagItems.Count; i++)
            {
                equipBagItems[i].text_1.text = itemLst[i].itemInfo.name;
                equipBagItems[i].image_1.sprite = itemLst[i].itemInfo.ItemIcon;
                equipBagItems[i].ableToEquiped = true;
            }
            EquipmentNumber = itemLst.Count;
        }
        else if (currentEquipCategory == EquipItemCategory.KernelEquipment)
        {
            InventoryManager.Instance.GetItemLst(ns.ItemInfos.ItemType.KernelEquipment, out var itemLst);
            for (int i = 0; i < itemLst.Count && i < equipBagItems.Count; i++)
            {
                equipBagItems[i].text_1.text = itemLst[i].itemInfo.name;
                equipBagItems[i].image_1.sprite = itemLst[i].itemInfo.ItemIcon;
                equipBagItems[i].ableToEquiped = true;
            }
            EquipmentNumber = itemLst.Count;
        }       
        else if (currentEquipCategory == EquipItemCategory.RightHandWeapon)
        {
            InventoryManager.Instance.GetItemLst(ns.ItemInfos.ItemType.RightHandWeapon, out var itemLst);
            for (int i = 0; i < itemLst.Count && i < equipBagItems.Count; i++)
            {
                equipBagItems[i].text_1.text = itemLst[i].itemInfo.name;
                equipBagItems[i].image_1.sprite = itemLst[i].itemInfo.ItemIcon;
                equipBagItems[i].ableToEquiped = true;
            }
            EquipmentNumber = itemLst.Count;

        }
        else if (currentEquipCategory == EquipItemCategory.LeftHandWeapon)
        {
            InventoryManager.Instance.GetItemLst(ns.ItemInfos.ItemType.LeftHandWeapon, out var itemLst);
            for (int i = 0; i < itemLst.Count && i < equipBagItems.Count; i++)
            {
                equipBagItems[i].text_1.text = itemLst[i].itemInfo.name;
                equipBagItems[i].image_1.sprite = itemLst[i].itemInfo.ItemIcon;
                equipBagItems[i].ableToEquiped = true;
            }
            EquipmentNumber = itemLst.Count;
        }
        //else if(currentCategory == EquipItemCategory.None)
        //{
        //    InventoryManager.Instance.GetItemLst(ns.ItemInfos.ItemType.Consumable, out var itemLst);
        //    for (int i = 0; i < itemLst.Count && i < equipBagItems.Count; i++)
        //    {
        //        equipBagItems[i].text_1.text = itemLst[i].itemInfo.name;
        //        equipBagItems[i].image_1.sprite = itemLst[i].itemInfo.ItemIcon;
        //    }
        //}


        //刷新装备背包列表
        equipBagScroller.UpdateEquipmentBagScrollList();

        Debug.Log($"已成功添加 {equipBagItems.Count} 个Slot信息到背包列表");
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
        if (index >= 0 && index < equipBagItems.Count)
        {
            return equipBagItems[index];
        }
        Debug.LogWarning($"请求的索引 {index} 超出范围 (0-{equipBagItems.Count - 1})");
        return null;
    }
}
