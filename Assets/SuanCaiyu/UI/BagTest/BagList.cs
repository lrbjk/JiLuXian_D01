using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ns.BagSystem;
using Common.UI;
using ns.ItemInfos;

public class BagList : MonoBehaviour
{
    [Header("����")]
    //[SerializeField] private GameObject slotPrefab; // ����SlotԤ����
    public Transform ItemParent;//��ʾ�ĸ�����Content
    public GameObject BagGridPrefab; //��������Ԥ����

    [SerializeField] private int Sum = 50; // ��ʼ��������

    /// <summary>
    /// ��ǰ��װ������
    /// </summary>
    public int CurrentItemNumber = 0;//��ǰ��ʾ��װ������

    /// <summary>
    /// �����������ʾ�б�
    /// </summary>
    [Header("����ʱ����")]
    public List<Slot> BagItems = new List<Slot>(); //�����������ʾ�б�
    //public List<EquipmentSlot> equipmentData = new List<EquipmentSlot>(); //������������ݺͽ����б���Ҳ�����Ǻ���Ҫ�ˣ�

    /// <summary>
    /// ���������ö�٣������Ϸ�ѡ���ͨ��
    /// </summary>
    public enum ItemCategory
    { None, HeadEquipment, BodyEquipment, KernelEquipment, RightHandWeapon, LeftHandWeapon, Consumable, Material, Currency, Spell, Key }
    public ItemCategory currentCategory = ItemCategory.None;

    //���õ��ⲿ����
    [SerializeField] private EquipmentUIFunc equipmentUIFunc;


    private void OnEnable()
    {
        UpdateBag();
    }
    public void CreatBagList()
    {

        equipmentUIFunc = UIManager.Instance.GetUILayerManager("EquipmentUI") as EquipmentUIFunc;



        for (int i = 0; i < Sum; i++)
        {
            GameObject newItem = Instantiate(BagGridPrefab, ItemParent);

            Slot Component = newItem.GetComponent<Slot>();

            BagItems.Add(Component);
        }
        Debug.Log($"�ѳɹ����� {BagItems.Count} ��Slot��Ϣ�������б�");
    }

    /// <summary>
    /// ��ʼ��SlotԤ�������õ��б�
    /// </summary>
    public void UpdateBag()
    {
        Debug.Log("��������ˢ�£�");

        //��ʼ����������
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

        //���±�����Ʒ��Ϣ

        if (currentCategory == ItemCategory.HeadEquipment)
        {
            InventoryManager.Instance.GetItemLst(ns.ItemInfos.ItemType.HeadEquipMent, out var itemLst);


            //���ݽ�ɫ����װ�����±�����Ʒ��Ϣ
            for (int i = 0; i < itemLst.Count && i < BagItems.Count; i++)
            {
                HeadEquipmentItemInfo items = itemLst[i].itemInfo as HeadEquipmentItemInfo;
                //���±���������Ϣ
                BagItems[i].nametext = items.ItemName;
                BagItems[i].descriptiontext = items.ItemDescription;
                BagItems[i].effectText = items.ItemEffectDescription;
                BagItems[i].maxStorage = items.MaxCount;
                BagItems[i].Displayimage.sprite = items.ItemIcon;

                //��ȡ��ƷΨһ�����������ͨ������������Ʒ
                BagItems[i].BagIndex = i;
            }

            //������Ʒ������Ϣ
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


    //ˢ��װ�������б�
    //equipBagScroller.UpdateEquipmentBagScrollList();


    //private Slot InstantiateSlotInfo(Slot source, int index)
    //{
    //    // �����µ�Slotʵ���������ݣ�����GameObject��
    //    Slot newSlot = new Slot();

    //    // ������Ը�����Ҫ������
    //    // ���磺newSlot.text = source.text;
    //    // ע�⣺����Ҫ�޸�Slot��ʹ������л�

    //    return newSlot;
    //}

    /// <summary>
    /// ��ȡָ��������Slot��Ϣ
    /// </summary>
    public Slot GetSlotInfo(int index)
    {
        if (index >= 0 && index < BagItems.Count)
        {
            return BagItems[index];
        }
        Debug.LogWarning($"��������� {index} ������Χ (0-{BagItems.Count - 1})");
        return null;
    }
}
