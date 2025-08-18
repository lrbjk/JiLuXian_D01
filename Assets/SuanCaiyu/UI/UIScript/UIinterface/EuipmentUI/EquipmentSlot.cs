using Common.UI;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace Common.UI
{
    // 背包插槽脚本
    public class  EquipmentSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler ,IPointerClickHandler
    {
        //基本文字信息
        [Header("基本文字信息")]
        public Text nametext;
        public Text description;


        //图片信息
        [Header("图片信息")]
        public Sprite EmptyImage;
        public Image Displayimage;
        public Image HighLightImage;
        public Image SelectedImage;

        //状态信息
        [Header("图片信息")]
        public int BagIndex; //当前索引
        public bool ableToEquip = false;//格子有物品可以装备
        public bool isSelected = false;//当前物品是否可以被装备

        [SerializeField] private EquipmentUIFunc equipmentUIFunc;
        [SerializeField] private MainUIFunc mainUIFunc;
        [SerializeField] private EquipmentController equipmentController;


        //是否装备，注意切换出去的时候会被刷新掉
        // public bool isVeiwed = false;//是否显示
        // 不再需要存储BagList引用，通过父对象获取
        private EquipmentBagUIList bagUIList;
        private EquipmentSelector equipmentSelector;

        private void Start()
        {
            equipmentUIFunc = UIManager.Instance.GetUILayerManager("EquipmentUI") as EquipmentUIFunc;
            mainUIFunc = UIManager.Instance.GetUILayerManager("MainUI") as MainUIFunc;
            bagUIList = equipmentUIFunc.equipBagUIList;
            equipmentSelector = equipmentUIFunc.equipmentSelector;
            equipmentController = equipmentUIFunc.equipmentController;
        }
       

        void EquipSelected()
        {
            //选中装备右手武器
            if (bagUIList.currentEquipCategory == EquipmentBagUIList.EquipItemCategory.RightHandWeapon)
            {

                for (int i = 0; i < 2; i++)
                {
                    //判断：装备槽没有被占用且当前武器可以被装备
                    if (equipmentSelector.rightHandWeaponList[i].isEquiped == false && !isSelected)//当前被选武器没有被装备
                    {
                        // 图片获取
                        equipmentSelector.rightHandWeaponList[i].equipImage.sprite = Displayimage.sprite;
                        //信息获取（后面加） 

                        //当前插槽被占用
                        equipmentSelector.rightHandWeaponList[i].isEquiped = true;

                        //插槽高亮状态消失，武器已经更换
                        equipmentSelector.rightHandWeaponList[i].highLightImage.gameObject.SetActive(false);

                        //当前武器已被装备
                        isSelected = true;

                        //装备槽获取当前装备背包索引
                        equipmentSelector.rightHandWeaponList[i].EquipIndex = BagIndex;

                        //更新装备图标，刷新背包
                        bagUIList.UpdateEquipmentBag();

                        //更新主界面显示
                        mainUIFunc.equipmentViewManager.UpdatRightHandView();

                        if (mainUIFunc.rightWeapon.equipmentImages.Count != 0)
                        {
                            equipmentUIFunc.RightHandImage.sprite = mainUIFunc.rightWeapon.DisplayImage.sprite;
                        }

                        Debug.Log("装备成功！");
                        return;
                    }
                    else if (isSelected)
                    {
                        Debug.Log("当前右手武器已被装备");
                    }
                    else
                    {
                        Debug.Log("已被到装备上限");
                    }
                }
            }


            //选中装备左手武器
            if (bagUIList.currentEquipCategory == EquipmentBagUIList.EquipItemCategory.LeftHandWeapon)
            {

                for (int i = 0; i < 2; i++)
                {
                    if (equipmentSelector.leftHandWeaponList[i].isEquiped == false && !isSelected)
                    {
                        equipmentSelector.leftHandWeaponList[i].equipImage.sprite = Displayimage.sprite;

                        equipmentSelector.leftHandWeaponList[i].isEquiped = true;

                        equipmentSelector.leftHandWeaponList[i].highLightImage.gameObject.SetActive(false);

                        isSelected = true;

                        equipmentSelector.leftHandWeaponList[i].EquipIndex = BagIndex;

                        //更新装备图标，刷新背包
                        bagUIList.UpdateEquipmentBag();

                        mainUIFunc.equipmentViewManager.UpdatLeftHandView();


                        if (mainUIFunc.leftWeapon.equipmentImages.Count != 0)
                        {
                            equipmentUIFunc.LeftHandImage.sprite = mainUIFunc.leftWeapon.DisplayImage.sprite;
                        }


                        Debug.Log("装备成功！");
                        return;
                    }
                    else if (isSelected)
                    {
                        Debug.Log("当前左手武器已被装备");
                    }
                    else
                    {
                        Debug.Log("已达到装备上限");
                    }
                }


            }

            ///选中装备头部装备
            if (bagUIList.currentEquipCategory == EquipmentBagUIList.EquipItemCategory.HeadEquipment)
            {

                    if (equipmentSelector.headEquipmentList[0].isEquiped == false && !isSelected)
                    {
                        equipmentSelector.headEquipmentList[0].equipImage.sprite = Displayimage.sprite;

                        equipmentSelector.headEquipmentList[0].isEquiped = true;

                        equipmentSelector.headEquipmentList[0].highLightImage.gameObject.SetActive(false);

                        isSelected = true;

                        equipmentSelector.headEquipmentList[0].EquipIndex = BagIndex;

                    //更新装备图标，刷新背包
                        bagUIList.UpdateEquipmentBag();

                        equipmentUIFunc.HeadImage.sprite = equipmentSelector.headEquipmentList[0].equipImage.sprite;

                        Debug.Log("装备成功！");
                        return;
                    }
                    else if (isSelected)
                    {
                        Debug.Log("当前头盔已被装备");
                    }
                    else
                    {
                        Debug.Log("已达到装备上限");
                    }
             }


            ///选中装备身体装备
            if (bagUIList.currentEquipCategory == EquipmentBagUIList.EquipItemCategory.BodyEquipment)
            {

                if (equipmentSelector.bodyEquipmentList[0].isEquiped == false && !isSelected)
                {
                    equipmentSelector.bodyEquipmentList[0].equipImage.sprite = Displayimage.sprite;

                    equipmentSelector.bodyEquipmentList[0].isEquiped = true;

                    equipmentSelector.bodyEquipmentList[0].highLightImage.gameObject.SetActive(false);

                    isSelected = true;

                    equipmentSelector.bodyEquipmentList[0].EquipIndex = BagIndex;

                    //更新装备图标，刷新背包
                    bagUIList.UpdateEquipmentBag();

                    equipmentUIFunc.BodyImage.sprite = equipmentSelector.bodyEquipmentList[0].equipImage.sprite;

                    Debug.Log("装备成功！");
                    return;
                }
                else if (isSelected)
                {
                    Debug.Log("当前服装已被装备");
                }
                else
                {
                    Debug.Log("已达到装备上限");
                }
            }

            ///选中装备核心装备
            if (bagUIList.currentEquipCategory == EquipmentBagUIList.EquipItemCategory.KernelEquipment)
            {

                if (equipmentSelector.kernelEquipmentList[0].isEquiped == false && !isSelected)
                {
                    equipmentSelector.kernelEquipmentList[0].equipImage.sprite = Displayimage.sprite;

                    equipmentSelector.kernelEquipmentList[0].isEquiped = true;

                    equipmentSelector.kernelEquipmentList[0].highLightImage.gameObject.SetActive(false);

                    isSelected = true;

                    equipmentSelector.kernelEquipmentList[0].EquipIndex = BagIndex;

                    //更新装备图标，刷新背包
                    bagUIList.UpdateEquipmentBag();

                    equipmentUIFunc.KernelImg.sprite = equipmentSelector.kernelEquipmentList[0].equipImage.sprite;

                    mainUIFunc.KernelImage.sprite = equipmentSelector.kernelEquipmentList[0].equipImage.sprite;

                    Debug.Log("装备成功！");
                    return;
                }
                else if (isSelected)
                {
                    Debug.Log("当前核心已被装备");
                }
                else
                {
                    Debug.Log("已达到装备上限");
                }
            }

            //选中装备消耗品
            if (bagUIList.currentEquipCategory == EquipmentBagUIList.EquipItemCategory.Consumer)
            {
                int index =  equipmentController.currentConsumerSelectorIdx;

                if (equipmentSelector.consumerEquipmentList[index].isEquiped == false && !isSelected)
                {
                    equipmentSelector.consumerEquipmentList[index].equipImage.sprite = Displayimage.sprite;

                    equipmentSelector.consumerEquipmentList[index].isEquiped = true;

                    equipmentSelector.consumerEquipmentList[index].highLightImage.gameObject.SetActive(false);

                    isSelected = true;

                    equipmentSelector.consumerEquipmentList[index].EquipIndex = BagIndex;

                    //更新装备图标，刷新背包
                    bagUIList.UpdateEquipmentBag();

                    mainUIFunc.equipmentViewManager.UpdateConsumerView();

                    equipmentController.ConsumerSpriteList[index].sprite = Displayimage.sprite;

                    Debug.Log("装备成功！");
                    return;
                }
                else if (isSelected)
                {
                    Debug.Log("当前道具已被装备");
                }
                else
                {
                    Debug.Log("已达到装备上限");
                }

            }

        }


        void UnEquipSelected()
        {
            //选中卸下右手武器
            if (bagUIList.currentEquipCategory == EquipmentBagUIList.EquipItemCategory.RightHandWeapon)
            {

                for (int i = 0; i < 2; i++)
                {
                    //判断：当前武器对应索引是否在装备插槽中
                    if (equipmentSelector.rightHandWeaponList[i].EquipIndex == BagIndex && equipmentSelector.rightHandWeaponList[i].isEquiped && isSelected)
                    {

                        // 图片获取
                        equipmentSelector.rightHandWeaponList[i].equipImage.sprite = equipmentSelector.rightHandWeaponList[i].emptyImage;
                        //信息获取（后面加） 

                        //当前插槽被置空
                        equipmentSelector.rightHandWeaponList[i].isEquiped = false;

                        //当前武器取消装备
                        isSelected = false;

                        //装备槽当前装备背包索引初始化
                        equipmentSelector.rightHandWeaponList[i].EquipIndex = -1;

                        //更新装备图标，刷新背包
                        bagUIList.UpdateEquipmentBag();

                        //更新主界面显示
                        mainUIFunc.equipmentViewManager.UpdatRightHandView();

                        if (mainUIFunc.rightWeapon.equipmentImages.Count != 0)
                        {
                            equipmentUIFunc.RightHandImage.sprite = mainUIFunc.rightWeapon.DisplayImage.sprite;
                        }
                        else if (mainUIFunc.rightWeapon.equipmentImages.Count == 0)
                        {
                            equipmentUIFunc.RightHandImage.sprite = EmptyImage;
                            mainUIFunc.rightWeapon.DisplayImage.sprite = EmptyImage;
                        }

                        Debug.Log("取消装备成功！");
                        return;
                    }
                    else if (!isSelected)
                    {
                        Debug.Log("当前右手武器未被装备");
                    }
                    else
                    {
                        Debug.Log("装备槽中没有此武器！");
                    }
                }
            }


            //选中卸下左手武器
            if (bagUIList.currentEquipCategory == EquipmentBagUIList.EquipItemCategory.LeftHandWeapon)
            {

                for (int i = 0; i < 2; i++)
                {
                    //判断：当前武器对应索引是否在装备插槽中
                    if (equipmentSelector.leftHandWeaponList[i].EquipIndex == BagIndex && equipmentSelector.leftHandWeaponList[i].isEquiped && isSelected)
                    {

                        // 图片获取
                        equipmentSelector.leftHandWeaponList[i].equipImage.sprite = equipmentSelector.leftHandWeaponList[i].emptyImage;

                        //当前插槽被置空
                        equipmentSelector.leftHandWeaponList[i].isEquiped = false;

                        //当前武器取消装备
                        isSelected = false;

                        //装备槽当前装备背包索引初始化
                        equipmentSelector.leftHandWeaponList[i].EquipIndex = -1;

                        //更新装备图标，刷新背包
                        bagUIList.UpdateEquipmentBag();

                        //更新主界面显示
                        mainUIFunc.equipmentViewManager.UpdatLeftHandView();

                        if (mainUIFunc.leftWeapon.equipmentImages.Count != 0)
                        {
                            equipmentUIFunc.LeftHandImage.sprite = mainUIFunc.leftWeapon.DisplayImage.sprite;
                        }
                        else if (mainUIFunc.leftWeapon.equipmentImages.Count == 0)
                        {
                            equipmentUIFunc.LeftHandImage.sprite = EmptyImage;
                            mainUIFunc.leftWeapon.DisplayImage.sprite = EmptyImage;
                        }

                        Debug.Log("取消装备成功！");
                        return;
                    }
                    else if (!isSelected)
                    {
                        Debug.Log("当前左手武器未被装备");
                    }
                    else
                    {
                        Debug.Log("装备槽中没有此武器！");
                    }
                }
            }


            //选中卸下头部装备
            if (bagUIList.currentEquipCategory == EquipmentBagUIList.EquipItemCategory.HeadEquipment)
            {

                //判断：当前武器对应索引是否在装备插槽中
                if (equipmentSelector.headEquipmentList[0].EquipIndex == BagIndex && equipmentSelector.headEquipmentList[0].isEquiped && isSelected)
                {

                    // 图片获取
                    equipmentSelector.headEquipmentList[0].equipImage.sprite = equipmentSelector.headEquipmentList[0].emptyImage;

                    //当前插槽被置空
                    equipmentSelector.headEquipmentList[0].isEquiped = false;

                    //当前武器取消装备
                    isSelected = false;

                    //装备槽当前装备背包索引初始化
                    equipmentSelector.headEquipmentList[0].EquipIndex = -1;

                    //更新装备图标，刷新背包
                    bagUIList.UpdateEquipmentBag();

                    equipmentUIFunc.HeadImage.sprite = EmptyImage;

                    Debug.Log("取消装备成功！");
                    return;
                }
                else if (!isSelected)
                {
                    Debug.Log("当前头盔未被装备");
                }
                else
                {
                    Debug.Log("装备槽中没有此头盔！");
                }
            }

            //选中卸下身体装备
            if (bagUIList.currentEquipCategory == EquipmentBagUIList.EquipItemCategory.BodyEquipment)
            {

                //判断：当前武器对应索引是否在装备插槽中
                if (equipmentSelector.bodyEquipmentList[0].EquipIndex == BagIndex && equipmentSelector.bodyEquipmentList[0].isEquiped && isSelected)
                {

                    // 图片获取
                    equipmentSelector.bodyEquipmentList[0].equipImage.sprite = equipmentSelector.bodyEquipmentList[0].emptyImage;

                    //当前插槽被置空
                    equipmentSelector.bodyEquipmentList[0].isEquiped = false;

                    //当前武器取消装备
                    isSelected = false;

                    //装备槽当前装备背包索引初始化
                    equipmentSelector.bodyEquipmentList[0].EquipIndex = -1;

                    //更新装备图标，刷新背包
                    bagUIList.UpdateEquipmentBag();

                    equipmentUIFunc.BodyImage.sprite = EmptyImage;

                    Debug.Log("取消装备成功！");
                    return;
                }
                else if (!isSelected)
                {
                    Debug.Log("当前服装未被装备");
                }
                else
                {
                    Debug.Log("装备槽中没有此服装！");
                }
            }

            //选中卸下核心装备
            if (bagUIList.currentEquipCategory == EquipmentBagUIList.EquipItemCategory.KernelEquipment)
            {

                //判断：当前武器对应索引是否在装备插槽中
                if (equipmentSelector.kernelEquipmentList[0].EquipIndex == BagIndex && equipmentSelector.kernelEquipmentList[0].isEquiped && isSelected)
                {

                    // 图片获取
                    equipmentSelector.kernelEquipmentList[0].equipImage.sprite = equipmentSelector.kernelEquipmentList[0].emptyImage;

                    //当前插槽被置空
                    equipmentSelector.kernelEquipmentList[0].isEquiped = false;

                    //当前武器取消装备
                    isSelected = false;

                    //装备槽当前装备背包索引初始化
                    equipmentSelector.kernelEquipmentList[0].EquipIndex = -1;

                    //更新装备图标，刷新背包
                    bagUIList.UpdateEquipmentBag();

                    equipmentUIFunc.KernelImg.sprite = EmptyImage;
                    mainUIFunc.KernelImage.sprite = EmptyImage;

                    Debug.Log("取消装备成功！");
                    return;
                }
                else if (!isSelected)
                {
                    Debug.Log("当前核心未被装备");
                }
                else
                {
                    Debug.Log("装备槽中没有此核心！");
                }
            }

            //取消装备消耗品
            if (bagUIList.currentEquipCategory == EquipmentBagUIList.EquipItemCategory.Consumer)
            {
                int index = equipmentController.currentConsumerSelectorIdx;

                //只有当前装备槽中的物品才可以被卸下
                if (equipmentSelector.consumerEquipmentList[index].isEquiped == true && isSelected && (equipmentSelector.consumerEquipmentList[index].EquipIndex == BagIndex))
                {
                    equipmentSelector.consumerEquipmentList[index].equipImage.sprite = EmptyImage;

                    equipmentSelector.consumerEquipmentList[index].isEquiped = false;

                    isSelected = false;

                    equipmentSelector.consumerEquipmentList[index].EquipIndex = -1;

                    //更新装备图标，刷新背包
                    bagUIList.UpdateEquipmentBag();

                    mainUIFunc.equipmentViewManager.UpdateConsumerView();

                    equipmentController.ConsumerSpriteList[index].sprite = EmptyImage;

                     if (mainUIFunc.downWeapon.equipmentImages.Count == 0)
                     { 
                        mainUIFunc.downWeapon.DisplayImage.sprite = EmptyImage;
                        mainUIFunc.downWeapon.NextImage.sprite = EmptyImage;
                     }
                    else if(mainUIFunc.downWeapon.equipmentImages.Count == 1)
                    {
                        mainUIFunc.downWeapon.DisplayImage.sprite = mainUIFunc.downWeapon.equipmentImages[0];
                        mainUIFunc.downWeapon.NextImage.sprite = EmptyImage;
                    }

                    Debug.Log("取消装备成功！");
                    return;
                }
                else if (!isSelected)
                {
                    Debug.Log("当前道具没有被装备");
                }
                else if(equipmentSelector.consumerEquipmentList[index].EquipIndex != BagIndex && isSelected )
                {
                    Debug.Log("选中的不是该装备槽中的道具");
                }
                else
                {
                    Debug.Log("已达到装备上限");
                }
            }
        }




        public void OnPointerEnter(PointerEventData eventData)
        {
            if (ableToEquip)
            {

                if (equipmentUIFunc != null && Displayimage != null && description != null)
                {
                    equipmentUIFunc.EquipDescriptionImage.sprite = Displayimage.sprite;
                    equipmentUIFunc.EquipDescriptionText.text = description.text;

                    Debug.Log("鼠标进入插槽");
                }
                HighLightImage.gameObject.SetActive(true);
            }

        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (ableToEquip)
            {
                HighLightImage.gameObject.SetActive(false);
                Debug.Log("鼠标离开插槽");
                // 这里添加离开时的逻辑
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                if (ableToEquip)
                {
                    EquipSelected();
                }
                Debug.Log("左键点击");
                // 左键点击逻辑
            }
            else if (eventData.button == PointerEventData.InputButton.Right)
            {
                if (ableToEquip)
                {
                    UnEquipSelected();
                }
                Debug.Log("右键点击");
                // 右键点击逻辑
            }
            else if (eventData.button == PointerEventData.InputButton.Middle)
            {
                Debug.Log("中键点击");
                // 中键点击逻辑
            }
        }
    }

}