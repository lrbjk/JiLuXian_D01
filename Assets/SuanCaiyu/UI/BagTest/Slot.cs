using Common.UI;
using ns.BagSystem.Freamwork;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace Common.UI
{
    // ������۽ű�
    public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        //����������Ϣ
        [Header("����������Ϣ")]
        public string nametext;
        public string descriptiontext;
        public string effectText;

        public int maxStorage;
        public int maxHold;

        public int currentCount;

        public string CType;

        //存储目前的item
        public Item item;


        //ͼƬ��Ϣ
        [Header("ͼƬ��Ϣ")]
        public Sprite EmptyImage;
        public Image Displayimage;
        public Image HighLightImage;
        //public Image SelectedImage;

        //״̬��Ϣ
        [Header("ͼƬ��Ϣ")]
        public int BagIndex; //��ǰ����
        //public bool ableToEquip = false;//��������Ʒ����װ��
        //public bool isSelected = false;//��ǰ��Ʒ�Ƿ���Ա�װ��

        [SerializeField] private EquipmentUIFunc equipmentUIFunc;
        //[SerializeField] private MainUIFunc mainUIFunc;
        //[SerializeField] private EquipmentController equipmentController;


        //�Ƿ�װ����ע���л���ȥ��ʱ��ᱻˢ�µ�
        // public bool isVeiwed = false;//�Ƿ���ʾ
        // ������Ҫ�洢BagList���ã�ͨ���������ȡ
        NormalDescription description;
        private BagList bagList;
        //private EquipmentSelector equipmentSelector;

        private void Start()
        {
            equipmentUIFunc = UIManager.Instance.GetUILayerManager("EquipmentUI") as EquipmentUIFunc;
            //mainUIFunc = UIManager.Instance.GetUILayerManager("MainUI") as MainUIFunc;            
            bagList = equipmentUIFunc.bagList;
            description = equipmentUIFunc.normalDescription;
            //equipmentSelector = equipmentUIFunc.equipmentSelector;
            //equipmentController = equipmentUIFunc.equipmentController;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (equipmentUIFunc != null && Displayimage != null && descriptiontext != null)
            {
                description.NameText.text = nametext;

                equipmentUIFunc.DescriptionImage.sprite =Displayimage.sprite;

                HighLightImage.gameObject.SetActive(true);

                if (bagList.currentCategory == BagList.ItemCategory.Consumable)
                {
                    description.NumberText_1.text = maxHold.ToString();
                    description.NumberText_2.text = maxStorage.ToString();
                    description.EffectText.text = effectText;
                    description.NumberText_3.text = CType.ToString();
                    description.DescriptionText.text = descriptiontext;
                }
                else if (bagList.currentCategory == BagList.ItemCategory.None)
                {
                    description.NumberText_1.text = maxHold.ToString();
                    description.NumberText_2.text = maxStorage.ToString();
                    description.NumberText_3.text = CType.ToString();
                    description.EffectText.text = effectText;
                    description.DescriptionText.text = descriptiontext;

                }
                else if(bagList.currentCategory == BagList.ItemCategory.Material || bagList.currentCategory == BagList.ItemCategory.Currency ||
                    bagList.currentCategory == BagList.ItemCategory.Key)
                {
                    description.NumberText_1.text = "-";
                    description.NumberText_2.text = maxStorage.ToString();
                    description.EffectText.text = effectText;
                    description.DescriptionText.text = descriptiontext;
                }
                else if(bagList.currentCategory == BagList.ItemCategory.Spell)
                {

                }
                else if(bagList.currentCategory == BagList.ItemCategory.RightHandWeapon || bagList.currentCategory == BagList.ItemCategory.LeftHandWeapon)
                {

                }


                    Debug.Log("��������");
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            HighLightImage.gameObject.SetActive(false);
            Debug.Log("����뿪���");
            // ���������뿪ʱ���߼�
        }
    }

}