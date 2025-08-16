using Common.UI;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace Common.UI
{
    // 背包插槽脚本
    public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
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
        //public Image SelectedImage;

        //状态信息
        [Header("图片信息")]
        public int BagIndex; //当前索引
        //public bool ableToEquip = false;//格子有物品可以装备
        //public bool isSelected = false;//当前物品是否可以被装备

        [SerializeField] private EquipmentUIFunc equipmentUIFunc;
        //[SerializeField] private MainUIFunc mainUIFunc;
        //[SerializeField] private EquipmentController equipmentController;


        //是否装备，注意切换出去的时候会被刷新掉
        // public bool isVeiwed = false;//是否显示
        // 不再需要存储BagList引用，通过父对象获取
        private EquipmentBagUIList bagUIList;
        private EquipmentSelector equipmentSelector;

        private void Start()
        {
            equipmentUIFunc = UIManager.Instance.GetUILayerManager("EquipmentUI") as EquipmentUIFunc;
            //mainUIFunc = UIManager.Instance.GetUILayerManager("MainUI") as MainUIFunc;
            bagUIList = equipmentUIFunc.equipBagUIList;
            equipmentSelector = equipmentUIFunc.equipmentSelector;
            //equipmentController = equipmentUIFunc.equipmentController;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (equipmentUIFunc != null && Displayimage != null && description != null)
            {
                equipmentUIFunc.DescriptionImage.sprite =Displayimage.sprite;
                equipmentUIFunc.DescriptionText.text = description.text;

                HighLightImage.gameObject.SetActive(true);
                Debug.Log("鼠标进入插槽");
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            HighLightImage.gameObject.SetActive(false);
            Debug.Log("鼠标离开插槽");
            // 这里添加离开时的逻辑
        }
    }

}