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
        public Text text;
        public Text description;
        public Image image;
        public int number;
        [SerializeField]private EquipmentUIFunc equipmentUIFunc;
        [SerializeField]private MainUIFunc mainUIFunc;


        public bool isSelected = false;//是否装备
       // public bool isVeiwed = false;//是否显示
        // 不再需要存储BagList引用，通过父对象获取
        private EquipmentBagUIList bagUIList;
        private EquipmentSelector equipmentSelector;

        
        private void Start()
        {
            equipmentUIFunc = UIManager.Instance.GetUILayerManager("EquipmentUI") as EquipmentUIFunc;
            mainUIFunc = UIManager.Instance.GetUILayerManager("MainUI") as MainUIFunc;

            equipmentSelector = equipmentUIFunc.equipmentSelector;
        }
        void ScrollCellIndex(int idx)
        {
            // 从父对象获取BagList组件
            if (bagUIList == null)
            {
                bagUIList = GetComponentInParent<EquipmentBagUIList>();
                if (bagUIList == null)
                {
                    Debug.LogError("无法找到父对象上的BagList组件");
                    return;
                }
            }

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

            // 更新UI
            if (text != null)
            {
                text.text = item.text_1.text; 
                transform.name = item.text_1.text;
            }

            if (image != null && item.image_1 != null) 
            {
                image.sprite = item.image_1.sprite;
            }

            // 更新其他属性
            if (description != null)
            {
                //description.text = item.text.text; // 假设BagItem有description属性
            }

            number = idx; // 存储当前索引
                          //Debug.Log(number);
        }

        void EquipSelected()
        {
            //选中装备右手武器
            if (bagUIList.currentEquipCategory == EquipmentBagUIList.EquipItemCategory.RightHandWeapon)
            {

                for(int i = 0;i<2; i++)
                {
                    if (equipmentSelector.rightHandWeaponList[i].isEquiped == false && !isSelected)
                    {
                        equipmentSelector.rightHandWeaponList[i].equipImage.sprite = image.sprite;
                        equipmentSelector.rightHandWeaponList[i].isEquiped = true;

                        isSelected = true;

                        mainUIFunc.equipmentViewManager.UpdatRightHandView(i);
                        equipmentUIFunc.RightHandImage.sprite = equipmentSelector.rightHandWeaponList[0].equipImage.sprite;
                        Debug.Log("装备成功！");
                        return;
                    }
                    else
                    {
                        Debug.Log("已达到装备上限");
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
                        equipmentSelector.leftHandWeaponList[i].equipImage.sprite = image.sprite;
                        equipmentSelector.leftHandWeaponList[i].isEquiped = true;

                        isSelected = true;

                        mainUIFunc.equipmentViewManager.UpdatLeftHandView();
                        equipmentUIFunc.LeftHandImage.sprite = equipmentSelector.leftHandWeaponList[0].equipImage.sprite;
                        Debug.Log("装备成功！");
                        return;
                    }
                    else
                    {
                        Debug.Log("已达到装备上限");
                    }
                }

               
            }
        }
        void ScrollCellReturn()
        {
            Debug.Log("回收触发");
            // 可以在这里重置插槽状态
            if (text != null) text.text = "";
            if (description != null) description.text = "";
            if (image != null) image.sprite = null;
            number = -1;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (equipmentUIFunc != null && image != null && text != null)
            {
                equipmentUIFunc.EquipDescriptionImage.sprite = image.sprite;
                equipmentUIFunc.EquipDescriptionText.text = text.text;
                Debug.Log("鼠标进入插槽");
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            Debug.Log("鼠标离开插槽");
            // 这里添加离开时的逻辑
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            EquipSelected();
            Debug.Log("鼠标点击插槽");
        }
    }

}