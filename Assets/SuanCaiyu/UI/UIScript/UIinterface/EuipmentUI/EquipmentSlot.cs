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
        public Text nametext;
        public Text description;


        public Image Displayimage;
        public Image HighLightImage;
        public Image SelectedImage;
        public int number; //当前索引

        public bool ableToEquip = false;//格子有物品可以装备

        [SerializeField] private EquipmentUIFunc equipmentUIFunc;
        [SerializeField] private MainUIFunc mainUIFunc;


        public bool isSelected = false;//是否装备，注意切换出去的时候会被刷新掉
       // public bool isVeiwed = false;//是否显示
        // 不再需要存储BagList引用，通过父对象获取
        private EquipmentBagUIList bagUIList;
        private EquipmentSelector equipmentSelector;

        private void Start()
        {
            equipmentUIFunc = UIManager.Instance.GetUILayerManager("EquipmentUI") as EquipmentUIFunc;
            bagUIList = equipmentUIFunc.equipBagUIList;
            equipmentSelector = equipmentUIFunc.equipmentSelector;
        }
        void ScrollCellIndex(int idx)
        {
            // 从父对象获取BagList组件
            //if (bagUIList == null)
            //{
            //    bagUIList = GetComponentInParent<EquipmentBagUIList>();
            //    if (bagUIList == null)
            //    {
            //        Debug.LogError("无法找到父对象上的BagList组件");
            //        return;
            //    }
            //}
            equipmentUIFunc = UIManager.Instance.GetUILayerManager("EquipmentUI") as EquipmentUIFunc;
            bagUIList = equipmentUIFunc.equipBagUIList;
            equipmentSelector = equipmentUIFunc.equipmentSelector;

            // 检查索引是否有效
            if (idx < 0 || idx >= bagUIList.equipBagItems.Count)
            {
                Debug.LogError($"无效的索引: {idx}, 列表长度: {bagUIList.equipBagItems.Count}");
                return;
            }

            // 获取对应物品
            var item = bagUIList.equipBagItems[idx];
            if (item == null)
            {
                Debug.LogError($"索引 {idx} 处的物品为null");
                return;
            }

            //// 更新UI
            //if (text != null)
            //{
            //    text.text = item.text_1.text; 
            //    transform.name = item.text_1.text;
            //}

            //if (Displayimage != null && item.image_1 != null) 
            //{
            //    Displayimage.sprite = item.image_1.sprite;
            //}

            //// 更新其他属性
            //if (description != null)
            //{
            //    //description.text = item.text.text; // 假设BagItem有description属性
            //}

            //if ( !isAdded && bagUIList.equipmentData.Count < bagUIList.EquipmentNumber)
            //{
            //    Debug.Log(number);
            //    EquipmentSlot newSlot = gameObject.GetComponent<EquipmentSlot>();
            //    bagUIList.equipmentData.Add(newSlot);
            //    //获取固定索引
            //    number = bagUIList.equipmentData.IndexOf(newSlot);
            //    isAdded = false;
            //}

            ////将当前可装备物品存储到数据列表以便后续交互
            ////角标不可见
            //if (isSelected && number < bagUIList.EquipmentNumber)
            //{
            //    SelectedImage.gameObject.SetActive(true);
            //}
            //else
            //{
            //    SelectedImage.gameObject.SetActive(false);
            //}         

            //应对切换时数据列表刷新导致的选中状态刷新,如果当前索引与装备插槽索引相同且
            if (bagUIList.currentEquipCategory == EquipmentBagUIList.EquipItemCategory.RightHandWeapon)
            {
                for (int i = 0; i<2;i++)
                {
                    if( equipmentSelector.rightHandWeaponList[i].isEquiped)
                    {
                        if(number == equipmentSelector.rightHandWeaponList[i].EquipIndex)
                           isSelected = true;
                    }
                    else
                    {
                        isSelected = false;
                    }
                      
                }
            }
            else if (bagUIList.currentEquipCategory == EquipmentBagUIList.EquipItemCategory.LeftHandWeapon)
            {
                for (int i = 0; i < 2; i++)
                {
                    if ( equipmentSelector.leftHandWeaponList[i].isEquiped)
                    {
                        if (number == equipmentSelector.leftHandWeaponList[i].EquipIndex) 
                         isSelected = true;
                    }
                    else
                    {
                        isSelected = false;
                    }

                }
            }
            else if (bagUIList.currentEquipCategory == EquipmentBagUIList.EquipItemCategory.HeadEquipment)
            {
                if( equipmentSelector.headEquipmentList[0].isEquiped)
                {
                    if (number == equipmentSelector.headEquipmentList[0].EquipIndex)
                        isSelected = true;
                }
                else
                {
                    isSelected = false;
                }


            }
            else if (bagUIList.currentEquipCategory == EquipmentBagUIList.EquipItemCategory.BodyEquipment)
            {
                if ( equipmentSelector.bodyEquipmentList[0].isEquiped)
                {
                    if (number == equipmentSelector.bodyEquipmentList[0].EquipIndex)
                    {
                        isSelected = true;
                    }
                }
                else
                {
                    isSelected = false;
                }

            }
            else if (bagUIList.currentEquipCategory == EquipmentBagUIList.EquipItemCategory.KernelEquipment)
            {
                if ( equipmentSelector.kernelEquipmentList[0].isEquiped)
                {
                    if (number == equipmentSelector.kernelEquipmentList[0].EquipIndex)
                    {
                        isSelected = true;
                    }
                }
                else
                {
                    isSelected = false;
                }

            }

            //插槽选中状态不可见
            HighLightImage.gameObject.SetActive(false);

           
        }

        //        void EquipSelected()
        //        {
        //            //选中装备右手武器
        //            if (bagUIList.currentEquipCategory == EquipmentBagUIList.EquipItemCategory.RightHandWeapon)
        //            {

        //                for(int i = 0;i<2; i++)
        //                {
        //                    //判断：装备槽没有被占用且当前武器可以被装备
        //                    if (equipmentSelector.rightHandWeaponList[i].isEquiped == false && !(bagUIList.equipmentData[number].isSelected))//当前被选武器没有被装备
        //                    {
        //                       // 图片获取
        //                        equipmentSelector.rightHandWeaponList[i].equipImage.sprite = Displayimage.sprite;
        //                       //信息获取（后面加） 

        //                        //当前插槽被占用
        //                        equipmentSelector.rightHandWeaponList[i].isEquiped = true;

        //                        //插槽高亮状态消失，武器已经更换
        //                        equipmentSelector.rightHandWeaponList[i].highLightImage.gameObject.SetActive(false);

        //                        //当前武器已被装备
        //                        bagUIList.equipmentData[number].isSelected = true;
        //                        //遍历装备背包，更新装备图标
        //                        for (int j = 0; j < bagUIList.CurrentEquipmentNumber; j++)
        //                        {
        //                            if (bagUIList.equipmentData[j].isSelected)
        //                            {
        //                                bagUIList.equipmentData[j].SelectedImage.gameObject.SetActive(true);
        //                            }
        //                            else
        //                            {
        //                                bagUIList.equipmentData[j].SelectedImage.gameObject.SetActive(false);
        //                            }
        //                        }

        //                        //装备槽获取当前索引
        //                        equipmentSelector.rightHandWeaponList[i].EquipIndex = number;

        //                        //更新主界面显示
        //                        mainUIFunc.equipmentViewManager.UpdatRightHandView();

        //                        equipmentUIFunc.RightHandImage.sprite = equipmentSelector.rightHandWeaponList[0].equipImage.sprite;


        //                        Debug.Log("装备成功！");
        //                        return;
        //                    }
        //                    else if(bagUIList.equipmentData[number].isSelected)
        //                    {
        //                        Debug.Log("当前武器已被装备");
        //                    }
        //                    else
        //                    {
        //                        Debug.Log("已被到装备上限");
        //                    }
        //                }


        //            }

        //            //选中装备左手武器
        //            if (bagUIList.currentEquipCategory == EquipmentBagUIList.EquipItemCategory.LeftHandWeapon)
        //            {

        //                for (int i = 0; i < 2; i++)
        //                {
        //                    if (equipmentSelector.leftHandWeaponList[i].isEquiped == false && !(bagUIList.equipmentData[number].isSelected))
        //                    {
        //                        equipmentSelector.leftHandWeaponList[i].equipImage.sprite = Displayimage.sprite;

        //                        equipmentSelector.leftHandWeaponList[i].isEquiped = true;

        //                        equipmentSelector.leftHandWeaponList[i].highLightImage.gameObject.SetActive(false);

        //                        bagUIList.equipmentData[number].isSelected = true;

        //                        for (int j = 0; j < bagUIList.CurrentEquipmentNumber; j++)
        //                        {
        //                            if (bagUIList.equipmentData[j].isSelected)
        //                            {
        //                                bagUIList.equipmentData[j].SelectedImage.gameObject.SetActive(true);
        //                            }
        //                            else
        //                            {
        //                                bagUIList.equipmentData[j].SelectedImage.gameObject.SetActive(false);
        //                            }
        //                        }

        //                        mainUIFunc.equipmentViewManager.UpdatLeftHandView();

        //                        equipmentUIFunc.LeftHandImage.sprite = equipmentSelector.leftHandWeaponList[0].equipImage.sprite;

        //                        Debug.Log("装备成功！");
        //                        return;
        //                    }
        //                    else if (bagUIList.equipmentData[number].isSelected)
        //                    {
        //                        Debug.Log("当前武器已被装备");
        //                    }
        //                    else
        //                    {
        //                        Debug.Log("已达到装备上限");
        //                    }
        //                }


        //            }
        //        }


        public void OnPointerEnter(PointerEventData eventData)
        {
            //if (number < bagUIList.EquipmentNumber)
            //{

            //    if (equipmentUIFunc != null && Displayimage != null && text != null)
            //    {
            //        equipmentUIFunc.EquipDescriptionImage.sprite = Displayimage.sprite;
            //        equipmentUIFunc.EquipDescriptionText.text = text.text;

            //        HighLightImage.gameObject.SetActive(true);
            //        Debug.Log("鼠标进入插槽");
            //    }
            //}

        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (number < bagUIList.CurrentEquipmentNumber)
            {
                HighLightImage.gameObject.SetActive(false);
                Debug.Log("鼠标离开插槽");
                // 这里添加离开时的逻辑
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (number < bagUIList.CurrentEquipmentNumber)
            {
                //EquipSelected();
            }
        }
    }

}