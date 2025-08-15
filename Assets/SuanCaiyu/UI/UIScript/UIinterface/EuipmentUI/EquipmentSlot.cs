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

                        equipmentUIFunc.RightHandImage.sprite = equipmentSelector.rightHandWeaponList[0].equipImage.sprite;


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

                        equipmentUIFunc.LeftHandImage.sprite = equipmentSelector.leftHandWeaponList[0].equipImage.sprite;

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
            if (ableToEquip)
            {
                EquipSelected();
            }
        }
    }

}