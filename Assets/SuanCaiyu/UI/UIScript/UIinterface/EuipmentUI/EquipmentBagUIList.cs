using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ns.BagSystem;
using Common.UI;
using ns.ItemInfos;

public class EquipmentBagUIList : MonoBehaviour
{
   [Header("����")]
    //[SerializeField] private GameObject slotPrefab; // ����SlotԤ����
    public Transform EquipItemParent;//��ʾ�ĸ�����Content
    public GameObject equipmentBagGridPrefab; //��������Ԥ����

    [SerializeField] private int Sum = 50; // ��ʼ��������

    /// <summary>
    /// ��ǰ��װ������
    /// </summary>
    public int CurrentEquipmentNumber = 0;//��ǰ��ʾ��װ������

    /// <summary>
    /// �����������ʾ�б�
    /// </summary>
    [Header("����ʱ����")]
    public List<EquipmentSlot> equipBagItems = new List<EquipmentSlot>(); //�����������ʾ�б�
    //public List<EquipmentSlot> equipmentData = new List<EquipmentSlot>(); //������������ݺͽ����б���Ҳ�����Ǻ���Ҫ�ˣ�

    /// <summary>
    /// ���������ö�٣������Ϸ�ѡ���ͨ��
    /// </summary>
    public enum EquipItemCategory
    { None, HeadEquipment , BodyEquipment , KernelEquipment , RightHandWeapon , LeftHandWeapon , Consumer } 
    public EquipItemCategory currentEquipCategory = EquipItemCategory.None;

    //���õ��ⲿ����
    [SerializeField] private EquipmentUIFunc equipmentUIFunc;

    private EquipmentSelector equipmentSelector;//ѡ����

    public List<EuipmentItem> rightHandBagList;
    public List<EuipmentItem> leftHandBagList;
    public List<EuipmentItem> headBagList;
    public List<EuipmentItem> bodyBagList;
    public List<EuipmentItem> kernelBagList;
    public List<EuipmentItem> consumerList;


    private void OnEnable()
    {
        UpdateEquipmentBag();
    }
    public void CreatEquipBagList()
    {

        equipmentUIFunc = UIManager.Instance.GetUILayerManager("EquipmentUI") as EquipmentUIFunc;
        equipmentSelector = equipmentUIFunc.equipmentSelector;

        //��ʼ����ȡѡ����
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
        Debug.Log($"�ѳɹ����� {equipBagItems.Count} ��Slot��Ϣ�������б�");
    }

    /// <summary>
    /// ��ʼ��SlotԤ�������õ��б�
    /// </summary>
    public void UpdateEquipmentBag()
    {
        Debug.Log("��������ˢ�£�");

        //��ʼ����������
        for (int i = 0; i < Sum; i++)
        {
            equipBagItems[i].description.text = null;
            equipBagItems[i].Displayimage.sprite = equipBagItems[i].EmptyImage;
            equipBagItems[i].ableToEquip = false;
            equipBagItems[i].isSelected = false;
            equipBagItems[i].SelectedImage.gameObject.SetActive(false);
            equipBagItems[i].HighLightImage.gameObject.SetActive(false);
            equipBagItems[i].currentItem = null;
        }

        //���±�����Ʒ��Ϣ

        if (currentEquipCategory == EquipItemCategory.HeadEquipment)
        {
            InventoryManager.Instance.GetItemLst(ns.ItemInfos.ItemType.HeadEquipMent, out var itemLst);

            //���ݲ��װ������װ��״̬
            
            
                if (headBagList[0].isEquiped)
                {
                    //��ȡװ�������װ���������
                    int index = headBagList[0].EquipIndex;
                    equipBagItems[index].isSelected = true;
                }


            //���ݽ�ɫ����װ�����±�����Ʒ��Ϣ
            for (int i = 0; i < itemLst.Count && i < equipBagItems.Count; i++)
            {
                //���±���������Ϣ
                equipBagItems[i].description.text = itemLst[i].itemInfo.name;
                equipBagItems[i].Displayimage.sprite = itemLst[i].itemInfo.ItemIcon;
                //����װ���Ǳ�
                if (equipBagItems[i].isSelected)
                {
                    equipBagItems[i].SelectedImage.gameObject.SetActive(true);
                }
                //�ø�������Ʒ��װ��
                equipBagItems[i].ableToEquip = true;

                //��ȡ��װ����ƷΨһ�����������ͨ������������Ʒ
                equipBagItems[i].BagIndex = i;

                //存储目前Item字段
                equipBagItems[i].currentItem = itemLst[i];
                //同步当前数量
                equipBagItems[i].currentCount = itemLst[i].CurrentCount;
            }

                //��ϸ��ǰ��װ����Ʒ��Ϣ
               CurrentEquipmentNumber = itemLst.Count;
        }


        else if (currentEquipCategory == EquipItemCategory.BodyEquipment)
        {
            InventoryManager.Instance.GetItemLst(ns.ItemInfos.ItemType.BodyEquipment, out var itemLst);

            //���ݲ��װ������װ��״̬
                if (bodyBagList[0].isEquiped)
                {
                    //��ȡװ�������װ���������
                    int index = bodyBagList[0].EquipIndex;
                    equipBagItems[index].isSelected = true;
                }
            for (int i = 0; i < itemLst.Count && i < equipBagItems.Count; i++)
            {
                equipBagItems[i].description.text = itemLst[i].itemInfo.name;
                equipBagItems[i].Displayimage.sprite = itemLst[i].itemInfo.ItemIcon;
                //����װ���Ǳ�
                if (equipBagItems[i].isSelected)
                {
                    equipBagItems[i].SelectedImage.gameObject.SetActive(true);
                }
                equipBagItems[i].ableToEquip = true;
                equipBagItems[i].BagIndex = i;

                //存储目前Item字段
                equipBagItems[i].currentItem = itemLst[i];
                //同步当前数量
                equipBagItems[i].currentCount = itemLst[i].CurrentCount;
            }
            CurrentEquipmentNumber = itemLst.Count;
        }
        else if (currentEquipCategory == EquipItemCategory.KernelEquipment)
        {
            InventoryManager.Instance.GetItemLst(ns.ItemInfos.ItemType.KernelEquipment, out var itemLst);

            // ���ݲ��װ������װ��״̬

                if (kernelBagList[0].isEquiped)
                {
                    //��ȡװ�������װ���������
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

                //存储目前Item字段
                equipBagItems[i].currentItem = itemLst[i];
                //同步当前数量
                equipBagItems[i].currentCount = itemLst[i].CurrentCount;
            }
            CurrentEquipmentNumber = itemLst.Count;
        }
        else if (currentEquipCategory == EquipItemCategory.RightHandWeapon)
        {
            InventoryManager.Instance.GetItemLst(ns.ItemInfos.ItemType.RightHandWeapon, out var itemLst);

            // ���ݲ��װ������װ��״̬
            for (int i = 0; i < 2; i++)
            {
                if (rightHandBagList[i].isEquiped)
                {
                    //��ȡװ�������װ���������
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

                //存储目前Item字段
                equipBagItems[i].currentItem = itemLst[i];
                //同步当前数量
                equipBagItems[i].currentCount = itemLst[i].CurrentCount;
            }
            CurrentEquipmentNumber = itemLst.Count;

        }
        else if (currentEquipCategory == EquipItemCategory.LeftHandWeapon)
        {
            InventoryManager.Instance.GetItemLst(ns.ItemInfos.ItemType.LeftHandWeapon, out var itemLst);

            // ���ݲ��װ������װ��״̬
            for (int i = 0; i < 2; i++)
            {
                if (leftHandBagList[i].isEquiped)
                {
                    //��ȡװ�������װ���������
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

                //存储目前Item字段
                equipBagItems[i].currentItem = itemLst[i];
                //同步当前数量
                equipBagItems[i].currentCount = itemLst[i].CurrentCount;
            }
            CurrentEquipmentNumber = itemLst.Count;
        }
        else if (currentEquipCategory == EquipItemCategory.Consumer)
        {
            // ���ݲ��װ������װ��״̬
            for (int i = 0; i < 8; i++)
            {
                if (consumerList[i].isEquiped)
                {
                    //��ȡװ�������װ���������
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

                //存储目前Item字段
                equipBagItems[i].currentItem = itemLst[i];
                //同步当前数量
                equipBagItems[i].currentCount = itemLst[i].CurrentCount;
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
    public EquipmentSlot GetSlotInfo(int index)
    {
        if (index >= 0 && index < equipBagItems.Count)
        {
            return equipBagItems[index];
        }
        Debug.LogWarning($"��������� {index} ������Χ (0-{equipBagItems.Count - 1})");
        return null;
    }
}
