using System.Collections;
using System.Collections.Generic;
using ns.BagSystem.Freamwork;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Common.UI
{
    public class EuipmentItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        public Sprite emptyImage;
        public Image equipImage;
        public Text nameText;
        public Image highLightImage;
        public Text equipDescriptionText;

        //�Ƿ��ѱ�װ��
        public bool isEquiped = false;

        /// <summary>
        /// ��ǰװ���ڱ���������
        /// </summary>
        public int EquipIndex;

        public Item EquipItem;

        [SerializeField] private EquipmentUIFunc equipmentUIFunc;
        [SerializeField] private EquipmentBagUIList equipmentBagUIList;
        private void Start()
        {
            equipmentUIFunc = UIManager.Instance.GetUILayerManager("EquipmentUI") as EquipmentUIFunc;

            equipmentBagUIList = equipmentUIFunc.equipBagUIList;

            EquipIndex = -1;
        }

        /// <summary>
        /// ����л�
        /// </summary>
        public void ClickToSwitch()
        {
            if(isEquiped)
            {
                //ѡ�и���
                highLightImage.gameObject.SetActive(true);    
                
                //���ռ��״̬ȡ��
                isEquiped=false;

                //�����б��б�ѡ�����ѡ��״̬ȡ��,ж��������Ч��
                equipmentBagUIList.equipBagItems[EquipIndex].isSelected = false;

                Debug.Log("ѡ�и���������");
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
