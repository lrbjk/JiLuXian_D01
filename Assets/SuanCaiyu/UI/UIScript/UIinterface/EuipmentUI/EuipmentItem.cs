using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Common.UI
{
    public class EuipmentItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        public Image equipImage;
        public Text nameText;
        public Image highLightImage;
        public Text equipDescriptionText;

        //是否已被装备
        public bool isEquiped = false;

        /// <summary>
        /// 当前装备在背包的索引
        /// </summary>
        public int EquipIndex;

        [SerializeField] private EquipmentUIFunc equipmentUIFunc;
        [SerializeField] private EquipmentBagUIList equipmentBagUIList;
        private void Start()
        {
            equipmentUIFunc = UIManager.Instance.GetUILayerManager("EquipmentUI") as EquipmentUIFunc;

            equipmentBagUIList = equipmentUIFunc.equipBagUIList;
        }

        /// <summary>
        /// 点击切换
        /// </summary>
        public void ClickToSwitch()
        {
            if(isEquiped)
            {
                //选中高亮
                highLightImage.gameObject.SetActive(true);    
                
                //插槽占用状态取消
                isEquiped=false;

                //数据列表中被选中项的选中状态取消,卸下武器的效果
                equipmentBagUIList.equipBagItems[EquipIndex].isSelected = false;

                Debug.Log("选中更换武器！");
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            ClickToSwitch();      
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
           
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            
        }
    }
}
