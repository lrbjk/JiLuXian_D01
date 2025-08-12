using Common.Helper;
using Common.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static EquipmentBagUIList;

public class EquipmentController : MonoBehaviour
{
    public List<EuipmentItem> rightHandWeaponsNum = new List<EuipmentItem>();
    public List<EuipmentItem> leftHandWeaponsNum = new List<EuipmentItem>();
    public List<EuipmentItem> headEquipmentsNum = new List<EuipmentItem>();
    public List<EuipmentItem> bodyEquipmentsNum = new List<EuipmentItem>();
    public List<EuipmentItem> KernelEquipmentsNum = new List<EuipmentItem>();

    public Button rightHandButton;
    public Button leftHandButton;
    public Button headEquipButton;
    public Button bodyEquipButton;
    public Button KernelEquipButton;

    public EquipmentUIFunc equipmentUIFunc;
    //与装备UI连接接口
    [SerializeField] private EquipmentBagUIList equipBagUIList;
    public static event Action<EquipmentBagUIList> OnCategoryChanged;
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

        
        Debug.Log("找到装备背包");
    }

    private void EquipRightHand()
    {
        equipmentUIFunc.NextUI("BagEquipment");
        Debug.Log("装备右手");
        equipBagUIList.currentEquipCategory = EquipItemCategory.RightHandWeapon;
        equipBagUIList.InitializeSlotPrefabReferences();
    }

    private void EquipLeftHand()
    {
        equipmentUIFunc.NextUI("BagEquipment");
        Debug.Log("装备左手");
        equipBagUIList.currentEquipCategory = EquipItemCategory.LeftHandWeapon;
        equipBagUIList.InitializeSlotPrefabReferences();
    }

    private void EquipHead()
    {
        equipmentUIFunc.NextUI("BagEquipment");
        Debug.Log("装备头");
        equipBagUIList.currentEquipCategory = EquipItemCategory.HeadEquipment;
        equipBagUIList.InitializeSlotPrefabReferences();
    }

    private void EquipBody()
    {
        equipmentUIFunc.NextUI("BagEquipment");
        Debug.Log("装备身体");
        equipBagUIList.currentEquipCategory = EquipItemCategory.BodyEquipment;
        equipBagUIList.InitializeSlotPrefabReferences();
    }

    private void EquipKernel()
    {
        equipmentUIFunc.NextUI("BagEquipment");
        Debug.Log("装备核心");
        equipBagUIList.currentEquipCategory = EquipItemCategory.KernelEquipment;
        equipBagUIList.InitializeSlotPrefabReferences();
    }
}
