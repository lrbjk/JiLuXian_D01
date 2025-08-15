using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ns.BagSystem;
using Common.UI;
using ns.ItemInfos;

public class EquipmentBagUIList : MonoBehaviour
{
   [Header("配置")]
    //[SerializeField] private GameObject slotPrefab; // 拖入Slot预制体
    public Transform EquipItemParent;//显示的父物体Content
    public GameObject equipmentBagGridPrefab; //背包格子预制体

    [SerializeField] private int Sum = 50; // 初始格子数量

    /// <summary>
    /// 当前可装备数量
    /// </summary>
    public int CurrentEquipmentNumber = 0;//当前显示的装备数量

    /// <summary>
    /// 背包物体的显示列表
    /// </summary>
    [Header("运行时数据")]
    public List<EquipmentSlot> equipBagItems = new List<EquipmentSlot>(); //背包物体的显示列表
    //public List<EquipmentSlot> equipmentData = new List<EquipmentSlot>(); //背包物体的数据和交互列表（也许不是很需要了）

    /// <summary>
    /// 背包分类的枚举，用于上方选项的通信
    /// </summary>
    public enum EquipItemCategory
    { None, HeadEquipment , BodyEquipment , KernelEquipment , RightHandWeapon , LeftHandWeapon , Consumer } 
    public EquipItemCategory currentEquipCategory = EquipItemCategory.None;

    //引用的外部变量
    [SerializeField] private EquipmentUIFunc equipmentUIFunc;

    private EquipmentSelector equipmentSelector;//选择插槽

    public List<EuipmentItem> rightHandBagList;
    public List<EuipmentItem> leftHandBagList;
    public List<EuipmentItem> headBagList;
    public List<EuipmentItem> bodyBagList;
    public List<EuipmentItem> kernelBagList;
    public List<EuipmentItem> consumerList;


    public void CreatEquipBagList()
    {

        equipmentUIFunc = UIManager.Instance.GetUILayerManager("EquipmentUI") as EquipmentUIFunc;
        equipmentSelector = equipmentUIFunc.equipmentSelector;

        //初始化获取选择插槽
        rightHandBagList = equipmentSelector.rightHandWeaponList;
        leftHandBagList = equipmentSelector.leftHandWeaponList;
        headBagList = equipmentSelector.headEquipmentList;
        bodyBagList = equipmentSelector.bodyEquipmentList;
        kernelBagList = equipmentSelector.kernelEquipmentList;
        consumerList = equipmentSelector.consumerEquipmentList;

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
    public void UpdateEquipmentBag()
    {
        Debug.Log("武器背包刷新！");

        //初始化武器背包
        for (int i = 0; i < Sum; i++)
        {
            equipBagItems[i].description.text = null;
            equipBagItems[i].Displayimage.sprite = equipBagItems[i].EmptyImage;
            equipBagItems[i].ableToEquip = false;
            equipBagItems[i].isSelected = false;
            equipBagItems[i].SelectedImage.gameObject.SetActive(false);
            equipBagItems[i].HighLightImage.gameObject.SetActive(false);
        }

        //更新背包物品信息

        if (currentEquipCategory == EquipItemCategory.HeadEquipment)
        {
            InventoryManager.Instance.GetItemLst(ns.ItemInfos.ItemType.HeadEquipMent, out var itemLst);

            //根据插槽装备更新装备状态
            
            
                if (headBagList[0].isEquiped)
                {
                    //获取装备插槽中装备项的索引
                    int index = headBagList[0].EquipIndex;
                    equipBagItems[index].isSelected = true;
                }
            

            //根据角色背包装备更新背包物品信息
            for (int i = 0; i < itemLst.Count && i < equipBagItems.Count; i++)
            {
                //更新背包格子信息
                equipBagItems[i].description.text = itemLst[i].itemInfo.name;
                equipBagItems[i].Displayimage.sprite = itemLst[i].itemInfo.ItemIcon;
                //更新装备角标
                if (equipBagItems[i].isSelected)
                {
                    equipBagItems[i].SelectedImage.gameObject.SetActive(true);
                }
                //该格子有物品可装备
                equipBagItems[i].ableToEquip = true;

                //获取可装备物品唯一索引，后面会通过索引查找物品
                equipBagItems[i].BagIndex = i;
            }

            //更细当前可装备物品信息
            CurrentEquipmentNumber = itemLst.Count;
        }


        else if (currentEquipCategory == EquipItemCategory.BodyEquipment)
        {
            InventoryManager.Instance.GetItemLst(ns.ItemInfos.ItemType.BodyEquipment, out var itemLst);

            //根据插槽装备更新装备状态
                if (bodyBagList[0].isEquiped)
                {
                    //获取装备插槽中装备项的索引
                    int index = bodyBagList[0].EquipIndex;
                    equipBagItems[index].isSelected = true;
                }
            for (int i = 0; i < itemLst.Count && i < equipBagItems.Count; i++)
            {
                equipBagItems[i].description.text = itemLst[i].itemInfo.name;
                equipBagItems[i].Displayimage.sprite = itemLst[i].itemInfo.ItemIcon;
                //更新装备角标
                if (equipBagItems[i].isSelected)
                {
                    equipBagItems[i].SelectedImage.gameObject.SetActive(true);
                }
                equipBagItems[i].ableToEquip = true;
                equipBagItems[i].BagIndex = i;
            }
            CurrentEquipmentNumber = itemLst.Count;
        }
        else if (currentEquipCategory == EquipItemCategory.KernelEquipment)
        {
            InventoryManager.Instance.GetItemLst(ns.ItemInfos.ItemType.KernelEquipment, out var itemLst);

            // 根据插槽装备更新装备状态

                if (kernelBagList[0].isEquiped)
                {
                    //获取装备插槽中装备项的索引
                    int index = kernelBagList[0].EquipIndex;
                    equipBagItems[index].isSelected = true;
                }
            for (int i = 0; i < itemLst.Count && i < equipBagItems.Count; i++)
            {
                equipBagItems[i].description.text = itemLst[i].itemInfo.name;
                equipBagItems[i].Displayimage.sprite = itemLst[i].itemInfo.ItemIcon;

                if (equipBagItems[i].isSelected)
                {
                    equipBagItems[i].SelectedImage.gameObject.SetActive(true);
                }

                equipBagItems[i].ableToEquip = true;
                equipBagItems[i].BagIndex = i;
            }
            CurrentEquipmentNumber = itemLst.Count;
        }
        else if (currentEquipCategory == EquipItemCategory.RightHandWeapon)
        {
            InventoryManager.Instance.GetItemLst(ns.ItemInfos.ItemType.RightHandWeapon, out var itemLst);

            // 根据插槽装备更新装备状态
            for (int i = 0; i < 2; i++)
            {
                if (rightHandBagList[i].isEquiped)
                {
                    //获取装备插槽中装备项的索引
                    int index = rightHandBagList[i].EquipIndex;
                    equipBagItems[index].isSelected = true;
                }
            }

            for (int i = 0; i < itemLst.Count && i < equipBagItems.Count; i++)
            {
                equipBagItems[i].description.text = itemLst[i].itemInfo.name;
                equipBagItems[i].Displayimage.sprite = itemLst[i].itemInfo.ItemIcon;

                if (equipBagItems[i].isSelected)
                {
                    equipBagItems[i].SelectedImage.gameObject.SetActive(true);
                }

                equipBagItems[i].ableToEquip = true;
                equipBagItems[i].BagIndex = i;
            }
            CurrentEquipmentNumber = itemLst.Count;

        }
        else if (currentEquipCategory == EquipItemCategory.LeftHandWeapon)
        {
            InventoryManager.Instance.GetItemLst(ns.ItemInfos.ItemType.LeftHandWeapon, out var itemLst);

            // 根据插槽装备更新装备状态
            for (int i = 0; i < 2; i++)
            {
                if (leftHandBagList[i].isEquiped)
                {
                    //获取装备插槽中装备项的索引
                    int index = leftHandBagList[i].EquipIndex;
                    equipBagItems[index].isSelected = true;
                }
            }

            for (int i = 0; i < itemLst.Count && i < equipBagItems.Count; i++)
            {
                equipBagItems[i].description.text = itemLst[i].itemInfo.name;
                equipBagItems[i].Displayimage.sprite = itemLst[i].itemInfo.ItemIcon;

                if (equipBagItems[i].isSelected)
                {
                    equipBagItems[i].SelectedImage.gameObject.SetActive(true);
                }

                equipBagItems[i].ableToEquip = true;
                equipBagItems[i].BagIndex = i;
            }
            CurrentEquipmentNumber = itemLst.Count;
        }
        else if (currentEquipCategory == EquipItemCategory.Consumer)
        {
            // 根据插槽装备更新装备状态
            for (int i = 0; i < 8; i++)
            {
                if (consumerList[i].isEquiped)
                {
                    //获取装备插槽中装备项的索引
                    int index = consumerList[i].EquipIndex;
                    equipBagItems[index].isSelected = true;
                }
            }

            InventoryManager.Instance.GetItemLst(ns.ItemInfos.ItemType.Consumable, out var itemLst);
            Debug.Log(itemLst.Count);
                for (int i = 0; i < itemLst.Count && i < equipBagItems.Count; i++)
                {
                    equipBagItems[i].description.text = itemLst[i].itemInfo.name;
                    equipBagItems[i].Displayimage.sprite = itemLst[i].itemInfo.ItemIcon;

                    if (equipBagItems[i].isSelected)
                    {
                        equipBagItems[i].SelectedImage.gameObject.SetActive(true);
                    }

                    equipBagItems[i].ableToEquip = true;
                    equipBagItems[i].BagIndex = i;
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
