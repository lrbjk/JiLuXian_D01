using Common.Helper;
using Common.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static EquipmentBagUIList;

/// <summary>
/// 总的装备管理,作为桥梁
/// </summary>
public class EquipmentController : MonoBehaviour
{
    

    public Button rightHandButton;
    public Button leftHandButton;
    public Button headEquipButton;
    public Button bodyEquipButton;
    public Button KernelEquipButton;

    //获取UI方法，从中获取需要的物体和类
    public EquipmentUIFunc equipmentUIFunc;

    //与装备UI连接接口
    [SerializeField] private EquipmentBagUIList equipBagUIList;
    [SerializeField] private EquipmentSelector equipmentSelector;   
    public static event Action<EquipmentBagUIList> OnCategoryChanged;
    


    [Header("选项面板控制")]
    [SerializeField] private GameObject righthandSelector;
    [SerializeField] private GameObject lefthandSelector;
    [SerializeField] private GameObject headSelector;
    [SerializeField] private GameObject bodySelector;
    [SerializeField] private GameObject kernelSelector;

    public void AddEquipment(EuipmentItem item)
    {

    }

    private void Start()
    {
        rightHandButton.onClick.AddListener(EquipRightHand);
        leftHandButton.onClick.AddListener(EquipLeftHand);
        headEquipButton.onClick.AddListener(EquipHead);
        bodyEquipButton.onClick.AddListener(EquipBody);
        KernelEquipButton.onClick.AddListener(EquipKernel);


        equipmentUIFunc = UIManager.Instance.GetUILayerManager("EquipmentUI") as EquipmentUIFunc;

        //提前加载好插槽列表
        equipmentSelector = equipmentUIFunc.equipmentSelector;
        equipmentSelector.UpdateEquipmentSelectList();

        //提前创建好格子
        equipBagUIList = equipmentUIFunc.equipBagUIList;
        equipBagUIList.CreatEquipBagList();

        //提前找到选择插槽
        righthandSelector = equipmentUIFunc.righthandSelector;
        lefthandSelector = equipmentUIFunc .lefthandSelector;
        headSelector = equipmentUIFunc .headSelector;
        bodySelector = equipmentUIFunc .bodySelector;
        kernelSelector = equipmentUIFunc .kernelSelector;
        Debug.Log("找到装备背包");


    }

    private void EquipRightHand()
    {
        equipmentUIFunc.NextUI("BagEquipment");
        Debug.Log("装备右手");
        equipBagUIList.currentEquipCategory = EquipItemCategory.RightHandWeapon;
        equipBagUIList.InitializeSlotPrefabReferences();

        righthandSelector.SetActive(true);
    }

    private void EquipLeftHand()
    {
        equipmentUIFunc.NextUI("BagEquipment");
        Debug.Log("装备左手");
        equipBagUIList.currentEquipCategory = EquipItemCategory.LeftHandWeapon;
        equipBagUIList.InitializeSlotPrefabReferences();

        lefthandSelector.SetActive(true);
    }

    private void EquipHead()
    {
        equipmentUIFunc.NextUI("BagEquipment");
        Debug.Log("装备头");
        equipBagUIList.currentEquipCategory = EquipItemCategory.HeadEquipment;
        equipBagUIList.InitializeSlotPrefabReferences();

        headSelector.SetActive(true);

    }

    private void EquipBody()
    {
        equipmentUIFunc.NextUI("BagEquipment");
        Debug.Log("装备身体");
        equipBagUIList.currentEquipCategory = EquipItemCategory.BodyEquipment;
        equipBagUIList.InitializeSlotPrefabReferences();

        bodySelector.SetActive(true);
    }

    private void EquipKernel()
    {
        equipmentUIFunc.NextUI("BagEquipment");
        Debug.Log("装备核心");
        equipBagUIList.currentEquipCategory = EquipItemCategory.KernelEquipment;
        equipBagUIList.InitializeSlotPrefabReferences();

        kernelSelector.SetActive(true);
    }
}
