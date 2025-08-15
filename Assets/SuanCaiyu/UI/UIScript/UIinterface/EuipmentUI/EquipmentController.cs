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

    public Button consumerEquipButton_1;
    public Button consumerEquipButton_2;
    public Button consumerEquipButton_3;
    public Button consumerEquipButton_4;
    public Button consumerEquipButton_5;
    public Button consumerEquipButton_6;
    public Button consumerEquipButton_7;
    public Button consumerEquipButton_8;

    public Button exitButton;

    //当前选中的道具栏索引
    public int currentConsumerSelectorIdx = 0;

    //道具栏图标列表，方便管理
    public List<Image> ConsumerSpriteList = new List<Image>();

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
    [SerializeField] private GameObject consumerSelector;
    private void Start()
    {
        rightHandButton.onClick.AddListener(EquipRightHand);
        leftHandButton.onClick.AddListener(EquipLeftHand);
        headEquipButton.onClick.AddListener(EquipHead);
        bodyEquipButton.onClick.AddListener(EquipBody);
        KernelEquipButton.onClick.AddListener(EquipKernel);

        consumerEquipButton_1.onClick.AddListener(EquipConsumer_1);
        consumerEquipButton_2.onClick.AddListener(EquipConsumer_2);
        consumerEquipButton_3.onClick.AddListener(EquipConsumer_3);
        consumerEquipButton_4.onClick.AddListener(EquipConsumer_4);
        consumerEquipButton_5.onClick.AddListener(EquipConsumer_5);
        consumerEquipButton_6.onClick.AddListener(EquipConsumer_6);
        consumerEquipButton_7.onClick.AddListener(EquipConsumer_7);
        consumerEquipButton_8.onClick.AddListener(EquipConsumer_8);

        exitButton.onClick.AddListener(OnExit);
        exitButton.onClick.AddListener(equipmentUIFunc.LastUI);


        equipmentUIFunc = UIManager.Instance.GetUILayerManager("EquipmentUI") as EquipmentUIFunc;

        //提前加载好插槽列表
        equipmentSelector = equipmentUIFunc.equipmentSelector;
        equipmentSelector.UpdateEquipmentSelectList();

        //提前创建好格子，里面要获取插槽，在插槽船舰之后调用
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
        equipBagUIList.UpdateEquipmentBag();

        righthandSelector.SetActive(true);
    }

    private void EquipLeftHand()
    {
        equipmentUIFunc.NextUI("BagEquipment");
        Debug.Log("装备左手");
        equipBagUIList.currentEquipCategory = EquipItemCategory.LeftHandWeapon;
        equipBagUIList.UpdateEquipmentBag();

        lefthandSelector.SetActive(true);
    }

    private void EquipHead()
    {
        equipmentUIFunc.NextUI("BagEquipment");
        Debug.Log("装备头");
        equipBagUIList.currentEquipCategory = EquipItemCategory.HeadEquipment;
        equipBagUIList.UpdateEquipmentBag();

        headSelector.SetActive(true);

    }

    private void EquipBody()
    {
        equipmentUIFunc.NextUI("BagEquipment");
        Debug.Log("装备身体");
        equipBagUIList.currentEquipCategory = EquipItemCategory.BodyEquipment;
        equipBagUIList.UpdateEquipmentBag();

        bodySelector.SetActive(true);
    }

    private void EquipKernel()
    {
        equipmentUIFunc.NextUI("BagEquipment");
        Debug.Log("装备核心");
        equipBagUIList.currentEquipCategory = EquipItemCategory.KernelEquipment;
        equipBagUIList.UpdateEquipmentBag();

        kernelSelector.SetActive(true);
    }

    private void EquipConsumer_1()
    {
        equipmentUIFunc.NextUI("BagEquipment");
        Debug.Log("装备道具1");
        equipBagUIList.currentEquipCategory = EquipItemCategory.Consumer;
        equipBagUIList.UpdateEquipmentBag();
        equipmentSelector.consumerEquipmentList[0].gameObject.SetActive(true);

        currentConsumerSelectorIdx = 0;
    }

    private void EquipConsumer_2()
    {
        equipmentUIFunc.NextUI("BagEquipment");
        Debug.Log("装备道具2");
        equipBagUIList.currentEquipCategory = EquipItemCategory.Consumer;
        equipBagUIList.UpdateEquipmentBag();
        equipmentSelector.consumerEquipmentList[1].gameObject.SetActive(true);
        currentConsumerSelectorIdx = 1;
    }

    private void EquipConsumer_3()
    {
        equipmentUIFunc.NextUI("BagEquipment");
        Debug.Log("装备道具3");
        equipBagUIList.currentEquipCategory = EquipItemCategory.Consumer;
        equipBagUIList.UpdateEquipmentBag();
        equipmentSelector.consumerEquipmentList[2].gameObject.SetActive(true);
        currentConsumerSelectorIdx = 2;
    }

    private void EquipConsumer_4()
    {
        equipmentUIFunc.NextUI("BagEquipment");
        Debug.Log("装备道具4");
        equipBagUIList.currentEquipCategory = EquipItemCategory.Consumer;
        equipBagUIList.UpdateEquipmentBag();
        equipmentSelector.consumerEquipmentList[3].gameObject.SetActive(true);
        currentConsumerSelectorIdx = 3;
    }

    private void EquipConsumer_5()
    {
        equipmentUIFunc.NextUI("BagEquipment");
        Debug.Log("装备道具5");
        equipBagUIList.currentEquipCategory = EquipItemCategory.Consumer;
        equipBagUIList.UpdateEquipmentBag();
        equipmentSelector.consumerEquipmentList[4].gameObject.SetActive(true);
        currentConsumerSelectorIdx = 4;
    }

    private void EquipConsumer_6()
    {
        equipmentUIFunc.NextUI("BagEquipment");
        Debug.Log("装备道具6");
        equipBagUIList.currentEquipCategory = EquipItemCategory.Consumer;
        equipBagUIList.UpdateEquipmentBag();
        equipmentSelector.consumerEquipmentList[5].gameObject.SetActive(true);
        currentConsumerSelectorIdx = 5;
    }

    private void EquipConsumer_7()
    {
        equipmentUIFunc.NextUI("BagEquipment");
        Debug.Log("装备道具7");
        equipBagUIList.currentEquipCategory = EquipItemCategory.Consumer;
        equipBagUIList.UpdateEquipmentBag();
        equipmentSelector.consumerEquipmentList[6].gameObject.SetActive(true);
        currentConsumerSelectorIdx = 6;
    }

    private void EquipConsumer_8()
    {
        equipmentUIFunc.NextUI("BagEquipment");
        Debug.Log("装备道具8");
        equipBagUIList.currentEquipCategory = EquipItemCategory.Consumer;
        equipBagUIList.UpdateEquipmentBag();
        equipmentSelector.consumerEquipmentList[7].gameObject.SetActive(true);
        currentConsumerSelectorIdx = 7;
    }

    /// <summary>
    /// 选项槽不可见
    /// </summary>
    private void OnExit()
    {
        righthandSelector.SetActive(false);
        lefthandSelector.SetActive(false);
        headSelector.SetActive(false);
         bodySelector.SetActive(false);
         kernelSelector.SetActive(false);

        for(int i = 0; i < 8; i++)
        {
            equipmentSelector.consumerEquipmentList[i].gameObject.SetActive(false);
        }
    }
}
