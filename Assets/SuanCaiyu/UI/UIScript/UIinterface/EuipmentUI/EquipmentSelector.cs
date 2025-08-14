using Common.UI;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentSelector : MonoBehaviour
{
    // 装备数据（可以在Inspector中赋值）
    //public EuipmentItem selectedEquipment;
    //public EquipmentBagUIList equipmentBagUIList;
    // UI引用（拖拽赋值）
    public Transform rightHandItemParent;  // 右手装备项生成父物体
    public Transform leftHandItemParent;
    public Transform headEquipmentItemParent;
    public Transform bodyEquipmentItemParent;
    public Transform kernelItemParent;


    public GameObject equipmentItemPrefab;  // EuipmentItem预制体

    public List<EuipmentItem> rightHandWeaponList = new List<EuipmentItem>();
    public List<EuipmentItem> leftHandWeaponList = new List<EuipmentItem>();
    public List<EuipmentItem> headEquipmentList = new List<EuipmentItem>();
    public List<EuipmentItem> bodyEquipmentList = new List<EuipmentItem>();
    public List<EuipmentItem> kernelEquipmentList = new List<EuipmentItem>();

    // 初始化装备列表
    public void UpdateEquipmentSelectList()
    {
        SpawnRightHandItem();
        SpawnLeftHandItem();
        SpawnHeadEquipmentItem();
        SpawnBodyEquipmentItem();
        SpawnKernelEquipmentItem();
    }


    /// <summary>
    /// 实例化右手武器的选择项
    /// </summary>
    private void SpawnRightHandItem()
    {
        if (  equipmentItemPrefab == null || rightHandItemParent == null)
        {
            Debug.LogWarning("缺少必要的引用！");
            return;
        }

        for(int i = 0;i < 2; i ++ )
        {
           // 创建新装备项实例
           GameObject newItem = Instantiate(equipmentItemPrefab, rightHandItemParent);
             
          // 获取EuipmentItem组件并设置数据
          EuipmentItem itemComponent = newItem.GetComponent<EuipmentItem>();
            //if (itemComponent != null)
            //{
            //  itemComponent.equipImage.sprite = equipmentBagUIList.equipBagItems[i].image_1.sprite;
            //  itemComponent.nameText.text = equipmentBagUIList.equipBagItems[i].text_1.text;
            //  itemComponent.equipDescriptionText.text = equipmentBagUIList.equipBagItems[i].description_1.text;
            //}
            //else
            //{
            //  Debug.LogError("预制体上缺少EuipmentItem组件！");
            // }
            rightHandWeaponList.Add(itemComponent);
            rightHandItemParent.gameObject.SetActive(false);
        }

        
    }

    /// <summary>
    /// 实例化左手武器的选择项
    /// </summary>
    private void SpawnLeftHandItem()
    {
        if (equipmentItemPrefab == null || leftHandItemParent == null)
        {
            Debug.LogWarning("缺少必要的引用！");
            return;
        }

        for (int i = 0; i < 2; i++)
        {
            // 创建新装备项实例
            GameObject newItem = Instantiate(equipmentItemPrefab, leftHandItemParent);

            // 获取EuipmentItem组件并设置数据
            EuipmentItem itemComponent = newItem.GetComponent<EuipmentItem>();
            //if (itemComponent != null)
            //{
            //  itemComponent.equipImage.sprite = equipmentBagUIList.equipBagItems[i].image_1.sprite;
            //  itemComponent.nameText.text = equipmentBagUIList.equipBagItems[i].text_1.text;
            //  itemComponent.equipDescriptionText.text = equipmentBagUIList.equipBagItems[i].description_1.text;
            //}
            //else
            //{
            //  Debug.LogError("预制体上缺少EuipmentItem组件！");
            // }

            leftHandWeaponList.Add(itemComponent);

            leftHandItemParent.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 实例化头部装备选择项
    /// </summary>
    private void SpawnHeadEquipmentItem()
    {
        if (equipmentItemPrefab == null || headEquipmentItemParent == null)
        {
            Debug.LogWarning("缺少必要的引用！");
            return;
        }

            // 创建新装备项实例
            GameObject newItem = Instantiate(equipmentItemPrefab, headEquipmentItemParent);
            
        // 获取EuipmentItem组件并设置数据
            EuipmentItem itemComponent = newItem.GetComponent<EuipmentItem>();

            headEquipmentList.Add(itemComponent);

            headEquipmentItemParent.gameObject.SetActive(false);
    }

    /// <summary>
    /// 实例化身体装备选择项
    /// </summary>
    private void SpawnBodyEquipmentItem()
    {
        if (equipmentItemPrefab == null || bodyEquipmentItemParent == null)
        {
            Debug.LogWarning("缺少必要的引用！");
            return;
        }

            // 创建新装备项实例
            GameObject newItem = Instantiate(equipmentItemPrefab, bodyEquipmentItemParent);
           
            // 获取EuipmentItem组件并设置数据
            EuipmentItem itemComponent = newItem.GetComponent<EuipmentItem>();

            bodyEquipmentList.Add(itemComponent);

            bodyEquipmentItemParent.gameObject.SetActive(false); 
    }

    /// <summary>
    /// 实例化核心选择项
    /// </summary>
    private void SpawnKernelEquipmentItem()
    {
        if (equipmentItemPrefab == null || kernelItemParent == null)
        {
            Debug.LogWarning("缺少必要的引用！");
            return;
        }
            // 创建新装备项实例
            GameObject newItem = Instantiate(equipmentItemPrefab, kernelItemParent);

           
           // 获取EuipmentItem组件并设置数据
            EuipmentItem itemComponent = newItem.GetComponent<EuipmentItem>();

            kernelEquipmentList.Add(itemComponent);

            kernelItemParent.gameObject.SetActive(false);


    }
}