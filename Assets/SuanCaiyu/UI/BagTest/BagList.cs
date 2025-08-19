using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ns.BagSystem;
using Common.UI;
using ns.ItemInfos;

public class BagList : MonoBehaviour
{
    [Header("配置")]
    //[SerializeField] private GameObject slotPrefab; // 拖入Slot预制体
    public Transform ItemParent;//显示的父物体Content
    public GameObject BagGridPrefab; //背包格子预制体

    [SerializeField] private int Sum = 50; // 初始格子数量

    /// <summary>
    /// 当前可装备数量
    /// </summary>
    public int CurrentItemNumber = 0;//当前显示的装备数量

    /// <summary>
    /// 背包物体的显示列表
    /// </summary>
    [Header("运行时数据")]
    public List<Slot> BagItems = new List<Slot>(); //背包物体的显示列表
    //public List<EquipmentSlot> equipmentData = new List<EquipmentSlot>(); //背包物体的数据和交互列表（也许不是很需要了）

    /// <summary>
    /// 背包分类的枚举，用于上方选项的通信
    /// </summary>
    public enum ItemCategory
    { None, HeadEquipment, BodyEquipment, KernelEquipment, RightHandWeapon, LeftHandWeapon, Consumable, Material, Currency, Spell, Key }
    public ItemCategory currentCategory = ItemCategory.None;

    //引用的外部变量
    [SerializeField] private EquipmentUIFunc equipmentUIFunc;


    public void CreatBagList()
    {

        equipmentUIFunc = UIManager.Instance.GetUILayerManager("EquipmentUI") as EquipmentUIFunc;



        for (int i = 0; i < Sum; i++)
        {
            GameObject newItem = Instantiate(BagGridPrefab, ItemParent);

              Slot Component = newItem.GetComponent<Slot>();

            BagItems.Add(Component);
        }
        Debug.Log($"已成功添加 {BagItems.Count} 个Slot信息到背包列表");
    }

    /// <summary>
    /// 初始化Slot预制体引用到列表
    /// </summary>
    public void UpdateBag()
    {
        Debug.Log("武器背包刷新！");

        //初始化武器背包
        for (int i = 0; i < Sum; i++)
        {
            BagItems[i].nametext = null;
            BagItems[i].descriptiontext = null;
            BagItems[i].effectText = null;
            BagItems[i].maxStorage = 0;
            BagItems[i].maxHold = 0;

            BagItems[i].Displayimage.sprite = BagItems[i].EmptyImage;
            BagItems[i].HighLightImage.gameObject.SetActive(false);
        }

        //更新背包物品信息

        if (currentCategory == ItemCategory.HeadEquipment)
        {
            InventoryManager.Instance.GetItemLst(ns.ItemInfos.ItemType.HeadEquipMent, out var itemLst);


            //根据角色背包装备更新背包物品信息
            for (int i = 0; i < itemLst.Count && i < BagItems.Count; i++)
            {
                HeadEquipmentItemInfo items = itemLst[i].itemInfo as HeadEquipmentItemInfo;
                //更新背包格子信息
                BagItems[i].nametext = items.ItemName;
                BagItems[i].descriptiontext = items.ItemDescription;
                BagItems[i].effectText = items.ItemEffectDescription;
                BagItems[i].maxStorage = items.MaxCount;
                BagItems[i].Displayimage.sprite = items.ItemIcon;

                //获取物品唯一索引，后面会通过索引查找物品
                BagItems[i].BagIndex = i;
            }

            //更新物品数量信息
            CurrentItemNumber = itemLst.Count;
        }


        else if (currentCategory == ItemCategory.BodyEquipment)
        {
            InventoryManager.Instance.GetItemLst(ns.ItemInfos.ItemType.BodyEquipment, out var itemLst);

            for (int i = 0; i < itemLst.Count && i < BagItems.Count; i++)
            {
                BodyEquipmentItemInfo items = itemLst[i].itemInfo as BodyEquipmentItemInfo;
                BagItems[i].nametext = items.ItemName;
                BagItems[i].descriptiontext = items.ItemDescription;
                BagItems[i].effectText = items.ItemEffectDescription;
                BagItems[i].maxStorage = items.MaxCount;
                BagItems[i].Displayimage.sprite = items.ItemIcon;
                BagItems[i].BagIndex = i;
            }
            CurrentItemNumber = itemLst.Count;
        }
        else if (currentCategory == ItemCategory.KernelEquipment)
        {
            InventoryManager.Instance.GetItemLst(ns.ItemInfos.ItemType.KernelEquipment, out var itemLst);

            for (int i = 0; i < itemLst.Count && i < BagItems.Count; i++)
            {
                KernelEquipmentItemInfo items = itemLst[i].itemInfo as KernelEquipmentItemInfo;
                BagItems[i].nametext = items.ItemName;
                BagItems[i].descriptiontext = items.ItemDescription;
                BagItems[i].effectText = items.ItemEffectDescription;
                BagItems[i].maxStorage = items.MaxCount;
                BagItems[i].Displayimage.sprite = items.ItemIcon;
                BagItems[i].BagIndex = i;
            }
            CurrentItemNumber = itemLst.Count;
        }
        else if (currentCategory == ItemCategory.RightHandWeapon)
        {
            InventoryManager.Instance.GetItemLst(ns.ItemInfos.ItemType.RightHandWeapon, out var itemLst);

            for (int i = 0; i < itemLst.Count && i < BagItems.Count; i++)
            {
                RightHandWeaponItemInfo items = itemLst[i].itemInfo as RightHandWeaponItemInfo;
                BagItems[i].nametext = items.ItemName;
                BagItems[i].descriptiontext = items.ItemDescription;
                BagItems[i].effectText = items.ItemEffectDescription;
                BagItems[i].maxStorage = items.MaxCount;
                BagItems[i].Displayimage.sprite = items.ItemIcon;

                BagItems[i].BagIndex = i;
            }
            CurrentItemNumber = itemLst.Count;

        }
        else if (currentCategory == ItemCategory.LeftHandWeapon)
        {
            InventoryManager.Instance.GetItemLst(ns.ItemInfos.ItemType.LeftHandWeapon, out var itemLst);


            for (int i = 0; i < itemLst.Count && i < BagItems.Count; i++)
            {
                LeftHandWeaponItemInfo items = itemLst[i].itemInfo as LeftHandWeaponItemInfo;
                BagItems[i].nametext = items.ItemName;
                BagItems[i].descriptiontext = items.ItemDescription;
                BagItems[i].effectText = items.ItemEffectDescription;
                BagItems[i].maxStorage = items.MaxCount;
                BagItems[i].Displayimage.sprite = items.ItemIcon;

                BagItems[i].BagIndex = i;
            }
            CurrentItemNumber = itemLst.Count;
        }
        else if (currentCategory == ItemCategory.Consumable)
        {

            InventoryManager.Instance.GetItemLst(ns.ItemInfos.ItemType.Consumable, out var itemLst);
            Debug.Log(itemLst.Count);
            for (int i = 0; i < itemLst.Count && i < BagItems.Count; i++)
            {
                ConsumableItemInfo items = itemLst[i].itemInfo as ConsumableItemInfo;
                BagItems[i].nametext = items.ItemName;
                BagItems[i].descriptiontext = items.ItemDescription;
                BagItems[i].effectText = items.ItemEffectDescription;
                BagItems[i].maxStorage = items.MaxCount;
                BagItems[i].maxHold = items.QuickMaxCount;
                BagItems[i].Displayimage.sprite = items.ItemIcon;
                BagItems[i].CType = items.cType.ToString();
                BagItems[i].BagIndex = i;
            }

        }
        else if (currentCategory == ItemCategory.Material)
        {

            InventoryManager.Instance.GetItemLst(ns.ItemInfos.ItemType.Material, out var itemLst);
            Debug.Log(itemLst.Count);
            for (int i = 0; i < itemLst.Count && i < BagItems.Count; i++)
            {
                MaterialItemInfos items = itemLst[i].itemInfo as MaterialItemInfos;
                BagItems[i].nametext = items.ItemName;
                BagItems[i].descriptiontext = items.ItemDescription;
                BagItems[i].effectText = items.ItemEffectDescription;
                BagItems[i].maxStorage = items.MaxCount;
                BagItems[i].Displayimage.sprite = items.ItemIcon;
                BagItems[i].BagIndex = i;
            }

        }
        else if (currentCategory == ItemCategory.Currency)
        {

            InventoryManager.Instance.GetItemLst(ns.ItemInfos.ItemType.Currency, out var itemLst);
            Debug.Log(itemLst.Count);
            for (int i = 0; i < itemLst.Count && i < BagItems.Count; i++)
            {
                CurrencyItemInfo items = itemLst[i].itemInfo as CurrencyItemInfo;
                BagItems[i].nametext = items.ItemName;
                BagItems[i].descriptiontext = items.ItemDescription;
                BagItems[i].effectText = items.ItemEffectDescription;
                BagItems[i].maxStorage = items.MaxCount;
                BagItems[i].maxHold = items.QuickMaxCount;
                BagItems[i].Displayimage.sprite = items.ItemIcon;
                BagItems[i].BagIndex = i;
            }

        }
        else if (currentCategory == ItemCategory.Spell)
        {

            InventoryManager.Instance.GetItemLst(ns.ItemInfos.ItemType.Spell, out var itemLst);
            Debug.Log(itemLst.Count);
            for (int i = 0; i < itemLst.Count && i < BagItems.Count; i++)
            {
                SpellInfo items = itemLst[i].itemInfo as SpellInfo;
                BagItems[i].nametext = items.ItemName;
                BagItems[i].descriptiontext = items.ItemDescription;
                BagItems[i].effectText = items.ItemEffectDescription;
                BagItems[i].maxStorage = items.MaxCount;
                BagItems[i].Displayimage.sprite = items.ItemIcon;
                BagItems[i].BagIndex = i;
            }

        }
        else if (currentCategory == ItemCategory.Key)
        {

            InventoryManager.Instance.GetItemLst(ns.ItemInfos.ItemType.Key, out var itemLst);
            Debug.Log(itemLst.Count);
            for (int i = 0; i < itemLst.Count && i < BagItems.Count; i++)
            {
                KeyItemInfo items = itemLst[i].itemInfo as KeyItemInfo;
                BagItems[i].nametext = items.ItemName;
                BagItems[i].descriptiontext = items.ItemDescription;
                BagItems[i].effectText = items.ItemEffectDescription;
                BagItems[i].maxStorage = items.MaxCount;
                BagItems[i].Displayimage.sprite = items.ItemIcon;
                BagItems[i].BagIndex = i;
            }
        }
        else if (currentCategory == ItemCategory.None)
        {

            InventoryManager.Instance.GetItemLst(ns.ItemInfos.ItemType.Consumable, out var itemLst);
            Debug.Log(itemLst.Count);
            for (int i = 0; i < itemLst.Count && i < BagItems.Count; i++)
            {
                ConsumableItemInfo items = itemLst[i].itemInfo as ConsumableItemInfo;
                BagItems[i].nametext = items.ItemName;
                BagItems[i].descriptiontext = items.ItemDescription;
                BagItems[i].effectText = items.ItemEffectDescription;
                BagItems[i].maxStorage = items.MaxCount;
                BagItems[i].maxHold = items.QuickMaxCount;
                BagItems[i].Displayimage.sprite = items.ItemIcon;
                BagItems[i].CType = items.cType.ToString();
                BagItems[i].BagIndex = i;
            }

        }
    }


    //刷新装备背包列表
    //equipBagScroller.UpdateEquipmentBagScrollList();


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
    public Slot GetSlotInfo(int index)
    {
        if (index >= 0 && index < BagItems.Count)
        {
            return BagItems[index];
        }
        Debug.LogWarning($"请求的索引 {index} 超出范围 (0-{BagItems.Count - 1})");
        return null;
    }
}
