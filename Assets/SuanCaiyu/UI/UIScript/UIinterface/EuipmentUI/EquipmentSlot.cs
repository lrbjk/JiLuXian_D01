using Common.UI;
using ns.BagSystem.Freamwork;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace Common.UI
{
    // ������۽ű�
    public class  EquipmentSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler ,IPointerClickHandler
    {
        //����������Ϣ
        [Header("����������Ϣ")]
        public Text nametext;
        public Text description;

        [Header("ͼƬ��Ϣ")]
        public Sprite EmptyImage;
        public Image Displayimage;
        public Image HighLightImage;
        public Image SelectedImage;

        ///当前数量
        public int currentCount;

        [Header("ͼƬ��Ϣ")]
        public int BagIndex; //��ǰ����
        public bool ableToEquip = false;//��������Ʒ����װ��
        public bool isSelected = false;//��ǰ��Ʒ�Ƿ���Ա�װ��

        [SerializeField] private EquipmentUIFunc equipmentUIFunc;
        [SerializeField] private MainUIFunc mainUIFunc;
        [SerializeField] private EquipmentController equipmentController;

        /// <summary>
        /// 当前对应的Item字段
        /// </summary>
        public Item currentItem;
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
            //ѡ��װ����������
            if (bagUIList.currentEquipCategory == EquipmentBagUIList.EquipItemCategory.RightHandWeapon)
            {

                for (int i = 0; i < 2; i++)
                {
                    //�жϣ�װ����û�б�ռ���ҵ�ǰ�������Ա�װ��
                    if (equipmentSelector.rightHandWeaponList[i].isEquiped == false && !isSelected)//��ǰ��ѡ����û�б�װ��
                    {
                        // ͼƬ��ȡ
                        equipmentSelector.rightHandWeaponList[i].equipImage.sprite = Displayimage.sprite;
                        //��Ϣ��ȡ������ӣ� 

                        //��ǰ��۱�ռ��
                        equipmentSelector.rightHandWeaponList[i].isEquiped = true;

                        //��۸���״̬��ʧ�������Ѿ�����
                        equipmentSelector.rightHandWeaponList[i].highLightImage.gameObject.SetActive(false);

                        //��ǰ�����ѱ�װ��
                        isSelected = true;

                        equipmentSelector.rightHandWeaponList[i].EquipIndex = BagIndex;


                        //将Item信息传递到装备选择列表里
                        equipmentSelector.rightHandWeaponList[i].EquipItem = currentItem;

                        bagUIList.UpdateEquipmentBag();

                        mainUIFunc.equipmentViewManager.UpdatRightHandView();

                        if (mainUIFunc.rightWeapon.equipmentImages.Count != 0)
                        {
                            equipmentUIFunc.RightHandImage.sprite = mainUIFunc.rightWeapon.DisplayImage.sprite;
                        }

                        Debug.Log("װ���ɹ���");
                        return;
                    }
                    else if (isSelected)
                    {
                        Debug.Log("��ǰ���������ѱ�װ��");
                    }
                    else
                    {
                        Debug.Log("�ѱ���װ������");
                    }
                }
            }

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

                        //Item信息列表更新
                        equipmentSelector.leftHandWeaponList[i].EquipItem = currentItem;

                        bagUIList.UpdateEquipmentBag();

                        mainUIFunc.equipmentViewManager.UpdatLeftHandView();


                        if (mainUIFunc.leftWeapon.equipmentImages.Count != 0)
                        {
                            equipmentUIFunc.LeftHandImage.sprite = mainUIFunc.leftWeapon.DisplayImage.sprite;
                        }


                        Debug.Log("װ���ɹ���");
                        return;
                    }
                    else if (isSelected)
                    {
                        Debug.Log("��ǰ���������ѱ�װ��");
                    }
                    else
                    {
                        Debug.Log("�Ѵﵽװ������");
                    }
                }


            }

            ///ѡ��װ��ͷ��װ��
            if (bagUIList.currentEquipCategory == EquipmentBagUIList.EquipItemCategory.HeadEquipment)
            {

                    if (equipmentSelector.headEquipmentList[0].isEquiped == false && !isSelected)
                    {
                        equipmentSelector.headEquipmentList[0].equipImage.sprite = Displayimage.sprite;

                        equipmentSelector.headEquipmentList[0].isEquiped = true;

                        equipmentSelector.headEquipmentList[0].highLightImage.gameObject.SetActive(false);

                        isSelected = true;

                        equipmentSelector.headEquipmentList[0].EquipIndex = BagIndex;
                        
                        //Item信息列表更新
                        equipmentSelector.headEquipItems.Add(currentItem);

                    //����װ��ͼ�꣬ˢ�±���
                    bagUIList.UpdateEquipmentBag();

                        equipmentUIFunc.HeadImage.sprite = equipmentSelector.headEquipmentList[0].equipImage.sprite;

                        Debug.Log("װ���ɹ���");
                        return;
                    }
                    else if (isSelected)
                    {
                        Debug.Log("��ǰͷ���ѱ�װ��");
                    }
                    else
                    {
                        Debug.Log("�Ѵﵽװ������");
                    }
             }


            ///ѡ��װ������װ��
            if (bagUIList.currentEquipCategory == EquipmentBagUIList.EquipItemCategory.BodyEquipment)
            {

                if (equipmentSelector.bodyEquipmentList[0].isEquiped == false && !isSelected)
                {
                    equipmentSelector.bodyEquipmentList[0].equipImage.sprite = Displayimage.sprite;

                    equipmentSelector.bodyEquipmentList[0].isEquiped = true;

                    equipmentSelector.bodyEquipmentList[0].highLightImage.gameObject.SetActive(false);

                    isSelected = true;

                    equipmentSelector.bodyEquipmentList[0].EquipIndex = BagIndex;

                    //Item信息列表更新
                     equipmentSelector.bodyEquipItems.Add(currentItem);

                    //����װ��ͼ�꣬ˢ�±���
                    bagUIList.UpdateEquipmentBag();

                    equipmentUIFunc.BodyImage.sprite = equipmentSelector.bodyEquipmentList[0].equipImage.sprite;

                    Debug.Log("װ���ɹ���");
                    return;
                }
                else if (isSelected)
                {
                    Debug.Log("��ǰ��װ�ѱ�װ��");
                }
                else
                {
                    Debug.Log("�Ѵﵽװ������");
                }
            }

            ///ѡ��װ������װ��
            if (bagUIList.currentEquipCategory == EquipmentBagUIList.EquipItemCategory.KernelEquipment)
            {

                if (equipmentSelector.kernelEquipmentList[0].isEquiped == false && !isSelected)
                {
                    equipmentSelector.kernelEquipmentList[0].equipImage.sprite = Displayimage.sprite;

                    equipmentSelector.kernelEquipmentList[0].isEquiped = true;

                    equipmentSelector.kernelEquipmentList[0].highLightImage.gameObject.SetActive(false);

                    isSelected = true;

                    equipmentSelector.kernelEquipmentList[0].EquipIndex = BagIndex;

                    //Item信息列表更新
                    equipmentSelector.kernelEquipItems.Add(currentItem);

                    //����װ��ͼ�꣬ˢ�±���
                    bagUIList.UpdateEquipmentBag();

                    equipmentUIFunc.KernelImg.sprite = equipmentSelector.kernelEquipmentList[0].equipImage.sprite;

                    mainUIFunc.KernelImage.sprite = equipmentSelector.kernelEquipmentList[0].equipImage.sprite;

                    Debug.Log("װ���ɹ���");
                    return;
                }
                else if (isSelected)
                {
                    Debug.Log("��ǰ�����ѱ�װ��");
                }
                else
                {
                    Debug.Log("�Ѵﵽװ������");
                }
            }

            //ѡ��װ������Ʒ
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

                    equipmentSelector.consumerEquipmentList[index].EquipItem = currentItem;

                    //����װ��ͼ�꣬ˢ�±���
                    bagUIList.UpdateEquipmentBag();

                    mainUIFunc.equipmentViewManager.UpdateConsumerView();

                    equipmentController.ConsumerSpriteList[index].sprite = Displayimage.sprite;

                    Debug.Log("װ���ɹ���");
                    return;
                }
                else if (isSelected)
                {
                    Debug.Log("��ǰ�����ѱ�װ��");
                }
                else
                {
                    Debug.Log("�Ѵﵽװ������");
                }

            }

        }


        void UnEquipSelected()
        {
            //ѡ��ж����������
            if (bagUIList.currentEquipCategory == EquipmentBagUIList.EquipItemCategory.RightHandWeapon)
            {

                for (int i = 0; i < 2; i++)
                {
                    //�жϣ���ǰ������Ӧ�����Ƿ���װ�������
                    if (equipmentSelector.rightHandWeaponList[i].EquipIndex == BagIndex && equipmentSelector.rightHandWeaponList[i].isEquiped && isSelected)
                    {

                        // ͼƬ��ȡ
                        equipmentSelector.rightHandWeaponList[i].equipImage.sprite = equipmentSelector.rightHandWeaponList[i].emptyImage;
                        //��Ϣ��ȡ������ӣ� 

                        //��ǰ��۱��ÿ�
                        equipmentSelector.rightHandWeaponList[i].isEquiped = false;

                        //��ǰ����ȡ��װ��
                        isSelected = false;

                        //װ���۵�ǰװ������������ʼ��
                        equipmentSelector.rightHandWeaponList[i].EquipIndex = -1;

                        //从item列表移除此项对应的Item
                        //Debug.Log("从item列表移除此项对应的Item");
                        //equipmentSelector.rightHandItems.Remove(currentItem);

                        //����װ��ͼ�꣬ˢ�±���
                        bagUIList.UpdateEquipmentBag();

                        //������������ʾ
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

                        Debug.Log("ȡ��װ���ɹ���");
                        return;
                    }
                    else if (!isSelected)
                    {
                        Debug.Log("��ǰ��������δ��װ��");
                    }
                    else
                    {
                        Debug.Log("װ������û�д�������");
                    }
                }
            }


            //ѡ��ж����������
            if (bagUIList.currentEquipCategory == EquipmentBagUIList.EquipItemCategory.LeftHandWeapon)
            {

                for (int i = 0; i < 2; i++)
                {
                    //�жϣ���ǰ������Ӧ�����Ƿ���װ�������
                    if (equipmentSelector.leftHandWeaponList[i].EquipIndex == BagIndex && equipmentSelector.leftHandWeaponList[i].isEquiped && isSelected)
                    {

                        // ͼƬ��ȡ
                        equipmentSelector.leftHandWeaponList[i].equipImage.sprite = equipmentSelector.leftHandWeaponList[i].emptyImage;

                        //��ǰ��۱��ÿ�
                        equipmentSelector.leftHandWeaponList[i].isEquiped = false;

                        //��ǰ����ȡ��װ��
                        isSelected = false;

                        //װ���۵�ǰװ������������ʼ��
                        equipmentSelector.leftHandWeaponList[i].EquipIndex = -1;

                        //����װ��ͼ�꣬ˢ�±���
                        bagUIList.UpdateEquipmentBag();

                        //������������ʾ
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

                        Debug.Log("ȡ��װ���ɹ���");
                        return;
                    }
                    else if (!isSelected)
                    {
                        Debug.Log("��ǰ��������δ��װ��");
                    }
                    else
                    {
                        Debug.Log("װ������û�д�������");
                    }
                }
            }


            //ѡ��ж��ͷ��װ��
            if (bagUIList.currentEquipCategory == EquipmentBagUIList.EquipItemCategory.HeadEquipment)
            {

                //�жϣ���ǰ������Ӧ�����Ƿ���װ�������
                if (equipmentSelector.headEquipmentList[0].EquipIndex == BagIndex && equipmentSelector.headEquipmentList[0].isEquiped && isSelected)
                {

                    // ͼƬ��ȡ
                    equipmentSelector.headEquipmentList[0].equipImage.sprite = equipmentSelector.headEquipmentList[0].emptyImage;

                    //��ǰ��۱��ÿ�
                    equipmentSelector.headEquipmentList[0].isEquiped = false;

                    //��ǰ����ȡ��װ��
                    isSelected = false;

                    //װ���۵�ǰװ������������ʼ��
                    equipmentSelector.headEquipmentList[0].EquipIndex = -1;

                    equipmentSelector.headEquipItems.Remove(currentItem);
                    {
                        
                    }

                    //����װ��ͼ�꣬ˢ�±���
                    bagUIList.UpdateEquipmentBag();

                    equipmentUIFunc.HeadImage.sprite = EmptyImage;

                    Debug.Log("ȡ��װ���ɹ���");
                    return;
                }
                else if (!isSelected)
                {
                    Debug.Log("��ǰͷ��δ��װ��");
                }
                else
                {
                    Debug.Log("װ������û�д�ͷ����");
                }
            }

            //ѡ��ж������װ��
            if (bagUIList.currentEquipCategory == EquipmentBagUIList.EquipItemCategory.BodyEquipment)
            {

                //�жϣ���ǰ������Ӧ�����Ƿ���װ�������
                if (equipmentSelector.bodyEquipmentList[0].EquipIndex == BagIndex && equipmentSelector.bodyEquipmentList[0].isEquiped && isSelected)
                {

                    // ͼƬ��ȡ
                    equipmentSelector.bodyEquipmentList[0].equipImage.sprite = equipmentSelector.bodyEquipmentList[0].emptyImage;

                    //��ǰ��۱��ÿ�
                    equipmentSelector.bodyEquipmentList[0].isEquiped = false;

                    //��ǰ����ȡ��װ��
                    isSelected = false;

                    //װ���۵�ǰװ������������ʼ��
                    equipmentSelector.bodyEquipmentList[0].EquipIndex = -1;

                    equipmentSelector.bodyEquipItems.Remove(currentItem);

                    //����װ��ͼ�꣬ˢ�±���
                    bagUIList.UpdateEquipmentBag();

                    equipmentUIFunc.BodyImage.sprite = EmptyImage;

                    Debug.Log("ȡ��װ���ɹ���");
                    return;
                }
                else if (!isSelected)
                {
                    Debug.Log("��ǰ��װδ��װ��");
                }
                else
                {
                    Debug.Log("װ������û�д˷�װ��");
                }
            }

            //ѡ��ж�º���װ��
            if (bagUIList.currentEquipCategory == EquipmentBagUIList.EquipItemCategory.KernelEquipment)
            {

                //�жϣ���ǰ������Ӧ�����Ƿ���װ�������
                if (equipmentSelector.kernelEquipmentList[0].EquipIndex == BagIndex && equipmentSelector.kernelEquipmentList[0].isEquiped && isSelected)
                {

                    // ͼƬ��ȡ
                    equipmentSelector.kernelEquipmentList[0].equipImage.sprite = equipmentSelector.kernelEquipmentList[0].emptyImage;

                    //��ǰ��۱��ÿ�
                    equipmentSelector.kernelEquipmentList[0].isEquiped = false;

                    //��ǰ����ȡ��װ��
                    isSelected = false;

                    //װ���۵�ǰװ������������ʼ��
                    equipmentSelector.kernelEquipmentList[0].EquipIndex = -1;

                    equipmentSelector.kernelEquipItems.Remove(currentItem);

                    //����װ��ͼ�꣬ˢ�±���
                    bagUIList.UpdateEquipmentBag();

                    equipmentUIFunc.KernelImg.sprite = EmptyImage;
                    mainUIFunc.KernelImage.sprite = EmptyImage;

                    Debug.Log("ȡ��װ���ɹ���");
                    return;
                }
                else if (!isSelected)
                {
                    Debug.Log("��ǰ����δ��װ��");
                }
                else
                {
                    Debug.Log("װ������û�д˺��ģ�");
                }
            }

            //ȡ��װ������Ʒ
            if (bagUIList.currentEquipCategory == EquipmentBagUIList.EquipItemCategory.Consumer)
            {
                int index = equipmentController.currentConsumerSelectorIdx;

                //ֻ�е�ǰװ�����е���Ʒ�ſ��Ա�ж��
                if (equipmentSelector.consumerEquipmentList[index].isEquiped == true && isSelected && (equipmentSelector.consumerEquipmentList[index].EquipIndex == BagIndex))
                {
                    equipmentSelector.consumerEquipmentList[index].equipImage.sprite = EmptyImage;

                    equipmentSelector.consumerEquipmentList[index].isEquiped = false;

                    isSelected = false;

                    equipmentSelector.consumerEquipmentList[index].EquipIndex = -1;

                    //����װ��ͼ�꣬ˢ�±���
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

                    Debug.Log("ȡ��װ���ɹ���");
                    return;
                }
                else if (!isSelected)
                {
                    Debug.Log("��ǰ����û�б�װ��");
                }
                else if(equipmentSelector.consumerEquipmentList[index].EquipIndex != BagIndex && isSelected )
                {
                    Debug.Log("ѡ�еĲ��Ǹ�װ�����еĵ���");
                }
                else
                {
                    Debug.Log("�Ѵﵽװ������");
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
                    //equipmentUIFunc.EquipDescriptionText.text = description.text;

                    Debug.Log("��������");
                }
                HighLightImage.gameObject.SetActive(true);
            }

        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (ableToEquip)
            {
                HighLightImage.gameObject.SetActive(false);
                Debug.Log("����뿪���");
                // ���������뿪ʱ���߼�
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
                Debug.Log("������");
                // �������߼�
            }
            else if (eventData.button == PointerEventData.InputButton.Right)
            {
                if (ableToEquip)
                {
                    UnEquipSelected();
                }
                Debug.Log("�Ҽ����");
                // �Ҽ�����߼�
            }
            else if (eventData.button == PointerEventData.InputButton.Middle)
            {
                Debug.Log("�м����");
                // �м�����߼�
            }
        }
    }

}