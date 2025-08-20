using Common.UI;
using ns.BagSystem.Freamwork;
using ns.ItemInfos;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentSelector : MonoBehaviour
{
  
    public Transform rightHandItemParent;  // ����װ�������ɸ�����
    public Transform leftHandItemParent;
    public Transform headEquipmentItemParent;
    public Transform bodyEquipmentItemParent;
    public Transform kernelItemParent;
    public Transform consumerItemParent;


    public GameObject equipmentItemPrefab;  // EuipmentItemԤ����

    public List<EuipmentItem> rightHandWeaponList = new List<EuipmentItem>();
    public List<EuipmentItem> leftHandWeaponList = new List<EuipmentItem>();
    public List<EuipmentItem> headEquipmentList = new List<EuipmentItem>();
    public List<EuipmentItem> bodyEquipmentList = new List<EuipmentItem>();
    public List<EuipmentItem> kernelEquipmentList = new List<EuipmentItem>();
    public List<EuipmentItem> consumerEquipmentList = new List<EuipmentItem>();//8��װ������һ���б�


    [Header("存储的item列表")]
    public List<Item> rightHandItems = new List<Item>();
    public List<Item> leftHandItems = new List<Item>();
    public List<Item> headEquipItems = new List<Item>();
    public List<Item> bodyEquipItems = new List<Item>();
    public List<Item> kernelEquipItems = new List<Item>();
    public List<Item> consumEquipItems = new List<Item>();


    // ��ʼ��װ���б�
    public void UpdateEquipmentSelectList()
    {
        SpawnRightHandItem();
        SpawnLeftHandItem();
        SpawnHeadEquipmentItem();
        SpawnBodyEquipmentItem();
        SpawnKernelEquipmentItem();
        SpawnConsumerEquipmentItem();
    }


    /// <summary>
    /// ʵ��������������ѡ����
    /// </summary>
    private void SpawnRightHandItem()
    {
        if (  equipmentItemPrefab == null || rightHandItemParent == null)
        {
            Debug.LogWarning("ȱ�ٱ�Ҫ�����ã�");
            return;
        }

        for(int i = 0;i < 2; i ++ )
        {
           // ������װ����ʵ��
           GameObject newItem = Instantiate(equipmentItemPrefab, rightHandItemParent);
             
          // ��ȡEuipmentItem�������������
          EuipmentItem itemComponent = newItem.GetComponent<EuipmentItem>();
            //if (itemComponent != null)
            //{
            //  itemComponent.equipImage.sprite = equipmentBagUIList.equipBagItems[i].image_1.sprite;
            //  itemComponent.nameText.text = equipmentBagUIList.equipBagItems[i].text_1.text;
            //  itemComponent.equipDescriptionText.text = equipmentBagUIList.equipBagItems[i].description_1.text;
            //}
            //else
            //{
            //  Debug.LogError("Ԥ������ȱ��EuipmentItem�����");
            // }
            rightHandWeaponList.Add(itemComponent);
            rightHandItemParent.gameObject.SetActive(false);
        }

        
    }

    /// <summary>
    /// ʵ��������������ѡ����
    /// </summary>
    private void SpawnLeftHandItem()
    {
        if (equipmentItemPrefab == null || leftHandItemParent == null)
        {
            Debug.LogWarning("ȱ�ٱ�Ҫ�����ã�");
            return;
        }

        for (int i = 0; i < 2; i++)
        {
            // ������װ����ʵ��
            GameObject newItem = Instantiate(equipmentItemPrefab, leftHandItemParent);

            // ��ȡEuipmentItem�������������
            EuipmentItem itemComponent = newItem.GetComponent<EuipmentItem>();
            //if (itemComponent != null)
            //{
            //  itemComponent.equipImage.sprite = equipmentBagUIList.equipBagItems[i].image_1.sprite;
            //  itemComponent.nameText.text = equipmentBagUIList.equipBagItems[i].text_1.text;
            //  itemComponent.equipDescriptionText.text = equipmentBagUIList.equipBagItems[i].description_1.text;
            //}
            //else
            //{
            //  Debug.LogError("Ԥ������ȱ��EuipmentItem�����");
            // }

            leftHandWeaponList.Add(itemComponent);

            leftHandItemParent.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// ʵ����ͷ��װ��ѡ����
    /// </summary>
    private void SpawnHeadEquipmentItem()
    {
        if (equipmentItemPrefab == null || headEquipmentItemParent == null)
        {
            Debug.LogWarning("ȱ�ٱ�Ҫ�����ã�");
            return;
        }

            // ������װ����ʵ��
            GameObject newItem = Instantiate(equipmentItemPrefab, headEquipmentItemParent);
            
        // ��ȡEuipmentItem�������������
            EuipmentItem itemComponent = newItem.GetComponent<EuipmentItem>();

            headEquipmentList.Add(itemComponent);

            headEquipmentItemParent.gameObject.SetActive(false);
    }

    /// <summary>
    /// ʵ��������װ��ѡ����
    /// </summary>
    private void SpawnBodyEquipmentItem()
    {
        if (equipmentItemPrefab == null || bodyEquipmentItemParent == null)
        {
            Debug.LogWarning("ȱ�ٱ�Ҫ�����ã�");
            return;
        }

            // ������װ����ʵ��
            GameObject newItem = Instantiate(equipmentItemPrefab, bodyEquipmentItemParent);
           
            // ��ȡEuipmentItem�������������
            EuipmentItem itemComponent = newItem.GetComponent<EuipmentItem>();

            bodyEquipmentList.Add(itemComponent);

            bodyEquipmentItemParent.gameObject.SetActive(false); 
    }

    /// <summary>
    /// ʵ��������ѡ����
    /// </summary>
    private void SpawnKernelEquipmentItem()
    {
        if (equipmentItemPrefab == null || kernelItemParent == null)
        {
            Debug.LogWarning("ȱ�ٱ�Ҫ�����ã�");
            return;
        }
            // ������װ����ʵ��
            GameObject newItem = Instantiate(equipmentItemPrefab, kernelItemParent);

           
           // ��ȡEuipmentItem�������������
            EuipmentItem itemComponent = newItem.GetComponent<EuipmentItem>();

            kernelEquipmentList.Add(itemComponent);

            kernelItemParent.gameObject.SetActive(false);

    }

    /// <summary>
    /// ʵ��������Ʒ���
    /// </summary>
    private void SpawnConsumerEquipmentItem()
    {
        if (equipmentItemPrefab == null || kernelItemParent == null)
        {
            Debug.LogWarning("ȱ�ٱ�Ҫ�����ã�");
            return;
        }

        for (int i = 0; i < 8; i++)
        {
            // ������װ����ʵ��
            GameObject newItem = Instantiate(equipmentItemPrefab, consumerItemParent);


            // ��ȡEuipmentItem�������������
            EuipmentItem itemComponent = newItem.GetComponent<EuipmentItem>();

            consumerEquipmentList.Add(itemComponent);

            consumerEquipmentList[i].gameObject.SetActive(false);

        }
    }
}