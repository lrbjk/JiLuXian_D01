using Common.UI;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentSelector : MonoBehaviour
{
    // 装备数据（可以在Inspector中赋值）
    public EuipmentItem selectedEquipment;
    public EquipmentBagUIList equipmentBagUIList;
    // UI引用（拖拽赋值）
    public Transform itemParent;  // 装备项生成父物体
    public GameObject equipmentItemPrefab;  // EuipmentItem预制体

    public List<EuipmentItem> rightHandWeaponsNum = new List<EuipmentItem>();
    public List<EuipmentItem> leftHandWeaponsNum = new List<EuipmentItem>();
    public List<EuipmentItem> headEquipmentsNum = new List<EuipmentItem>();
    public List<EuipmentItem> bodyEquipmentsNum = new List<EuipmentItem>();
    public List<EuipmentItem> KernelEquipmentsNum = new List<EuipmentItem>();

    // 当装备被选中时调用这个方法



    // 实例化装备项的方法
    private void SpawnEquipmentItem(int i)
    {
        if (  equipmentItemPrefab == null || itemParent == null)
        {
            Debug.LogWarning("缺少必要的引用！");
            return;
        }

        // 创建新装备项实例
        GameObject newItem = Instantiate(equipmentItemPrefab, itemParent);

        // 获取EuipmentItem组件并设置数据
        EuipmentItem itemComponent = newItem.GetComponent<EuipmentItem>();
        if (itemComponent != null)
        {
            itemComponent.equipImage.sprite = equipmentBagUIList.equipBagItems[i].image_1.sprite;
            itemComponent.nameText.text = equipmentBagUIList.equipBagItems[i].text_1.text;
            itemComponent.equipDescriptionText.text = equipmentBagUIList.equipBagItems[i].description_1.text;
        }
        else
        {
            Debug.LogError("预制体上缺少EuipmentItem组件！");
        }
    }
}