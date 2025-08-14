using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ns.BagSystem;
using Common.UI;

public class EquipmentBagUIList : MonoBehaviour
{
   [Header("配置")]
    //[SerializeField] private GameObject slotPrefab; // 拖入Slot预制体
    public Transform EquipItemParent;//显示的父物体Content
    public GameObject equipmentBagGridPrefab; //背包格子预制体

    [SerializeField] private int Sum = 50; // 初始格子数量

    public int CurrentEquipmentNumber = 0;//当前显示的装备数量

    [Header("运行时数据")]
    public List<EquipmentSlot> equipBagItems = new List<EquipmentSlot>(); //背包物体的显示列表
    //public List<EquipmentSlot> equipmentData = new List<EquipmentSlot>(); //背包物体的数据和交互列表（也许不是很需要了）

    /// <summary>
    /// 背包分类的枚举，用于上方选项的通信
    /// </summary>
    public enum EquipItemCategory
    { None, HeadEquipment , BodyEquipment , KernelEquipment , RightHandWeapon , LeftHandWeapon } 
    public EquipItemCategory currentEquipCategory = EquipItemCategory.None;

    public EquipmentBagScroller equipBagScroller;


    private void Start()
    {
        //CreatEquipBagList();
    }
    ///// <summary>
    ///// 根据插槽中的武器选中状态更新背包武器选中状态
    ///// </summary>
    //[SerializeField] private EquipmentUIFunc equipmentUIFunc;
    //private EquipmentSelector equipmentSelector;
    //public void OnEnable()
    //{
    //    InitializeSlotPrefabReferences();
    //}

    public void CreatEquipBagList()
    {
        //if (equipmentItemPrefab == null || equipmentItemPrefab == null)
        //{
        //    Debug.Log("缺少必要的引用！");
        //    return;
        //}
        for (int i = 0; i < Sum; i++)
        {
            GameObject newItem = Instantiate(equipmentBagGridPrefab, EquipItemParent);

            EquipmentSlot EquipComponent = newItem.GetComponent<EquipmentSlot>();

            equipBagItems.Add(EquipComponent);
        }
        Debug.Log($"已成功添加 {equipBagItems.Count} 个Slot信息到背包列表");
    }

    /// <summary>
    /// 初始化Slot预制体引用到列表
    /// </summary>
    public void InitializeSlotPrefabReferences()
    {
        Debug.Log("武器背包刷新！");

        //equipBagItems.Clear();
        //equipmentData.Clear();//选中状态也会被重置

        for(int i  = 0; i < Sum; i++)
        {
            equipBagItems[i].description.text = null;
            equipBagItems[i].Displayimage.sprite = null;
            equipBagItems[i].ableToEquip = false;
        }

         if (currentEquipCategory == EquipItemCategory.HeadEquipment)
        {
            InventoryManager.Instance.GetItemLst(ns.ItemInfos.ItemType.HeadEquipMent, out var itemLst);
            for (int i = 0; i < itemLst.Count && i < equipBagItems.Count; i++)
            {
                //更新背包格子信息
                equipBagItems[i].description.text = itemLst[i].itemInfo.name;
                equipBagItems[i].Displayimage.sprite = itemLst[i].itemInfo.ItemIcon;

                //该格子有物品可装备
                equipBagItems[i].ableToEquip = true;
            }
            CurrentEquipmentNumber = itemLst.Count;
        }
        else if (currentEquipCategory == EquipItemCategory.BodyEquipment)
        {
            InventoryManager.Instance.GetItemLst(ns.ItemInfos.ItemType.BodyEquipment, out var itemLst);
            for (int i = 0; i < itemLst.Count && i < equipBagItems.Count; i++)
            {
                equipBagItems[i].description.text = itemLst[i].itemInfo.name;
                equipBagItems[i].Displayimage.sprite = itemLst[i].itemInfo.ItemIcon;

                equipBagItems[i].ableToEquip = true;
            }
            CurrentEquipmentNumber = itemLst.Count;
        }
        else if (currentEquipCategory == EquipItemCategory.KernelEquipment)
        {
            InventoryManager.Instance.GetItemLst(ns.ItemInfos.ItemType.KernelEquipment, out var itemLst);
            for (int i = 0; i < itemLst.Count && i < equipBagItems.Count; i++)
            {
                equipBagItems[i].description.text = itemLst[i].itemInfo.name;
                equipBagItems[i].Displayimage.sprite = itemLst[i].itemInfo.ItemIcon;

                equipBagItems[i].ableToEquip = true;
            }
            CurrentEquipmentNumber = itemLst.Count;
        }       
        else if (currentEquipCategory == EquipItemCategory.RightHandWeapon)
        {
            InventoryManager.Instance.GetItemLst(ns.ItemInfos.ItemType.RightHandWeapon, out var itemLst);
            for (int i = 0; i < itemLst.Count && i < equipBagItems.Count; i++)
            {
                equipBagItems[i].description.text = itemLst[i].itemInfo.name;
                equipBagItems[i].Displayimage.sprite = itemLst[i].itemInfo.ItemIcon;

                equipBagItems[i].ableToEquip = true;
            }
            CurrentEquipmentNumber = itemLst.Count;

        }
        else if (currentEquipCategory == EquipItemCategory.LeftHandWeapon)
        {
            InventoryManager.Instance.GetItemLst(ns.ItemInfos.ItemType.LeftHandWeapon, out var itemLst);
            for (int i = 0; i < itemLst.Count && i < equipBagItems.Count; i++)
            {
                equipBagItems[i].description.text = itemLst[i].itemInfo.name;
                equipBagItems[i].Displayimage.sprite = itemLst[i].itemInfo.ItemIcon;

                equipBagItems[i].ableToEquip = true;
            }
            CurrentEquipmentNumber = itemLst.Count;
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
        //equipBagScroller.UpdateEquipmentBagScrollList();

       
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
    public EquipmentSlot GetSlotInfo(int index)
    {
        if (index >= 0 && index < equipBagItems.Count)
        {
            return equipBagItems[index];
        }
        Debug.LogWarning($"请求的索引 {index} 超出范围 (0-{equipBagItems.Count - 1})");
        return null;
    }
}
