using Common.Helper;
using ns.BagSystem.Freamwork;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Common.UI
{
    public class EquipmentUIFunc : UILayerManager
    {
        public NormalDescription normalDescription;
        public EquipDescription equipDescription;


        public Image DescriptionImage;
        public Image EquipDescriptionImage;

        public Image RightHandImage;
        public Image LeftHandImage;
        public Image HeadImage;
        public Image BodyImage;
        public Image KernelImg;
        public Image Consumer_1Img;
        public Image Consumer_2Img;
        public Image Consumer_3Img;
        public Image Consumer_4Img;
        public Image Consumer_5Img;
        public Image Consumer_6Img;
        public Image Consumer_7Img;
        public Image Consumer_8Img;


        [Header("ѡ��������")]
        public GameObject righthandSelector;
        public GameObject lefthandSelector;
        public GameObject headSelector;
        public GameObject bodySelector;
        public GameObject kernelSelector;

        public EquipmentSelector equipmentSelector;
        public EquipmentBagUIList equipBagUIList;
        public EquipmentController equipmentController;
        public BagList bagList;

        private MainUIFunc mainUIFunc;
        protected override void Start()
        {
            base.Start();

            mainUIFunc = UIManager.Instance.GetUILayerManager("MainUI") as MainUIFunc;

            Transform dt = transform.FindChildByName("DescriptionLayer");
            normalDescription = dt.GetComponent<NormalDescription>();
            Debug.Log("�ҵ���������");

            Transform et = transform.FindChildByName("EquipDescriptionLayer");
            equipDescription = et.GetComponent<EquipDescription>();
            Debug.Log("�ҵ�װ����������");

            Transform di = transform.FindChildByName("DescriptionImage");
            DescriptionImage = di.GetComponent<Image>();
            Debug.Log("�ҵ�����ͼƬ");

            Transform ei = transform.FindChildByName("EquipDescriptionImage");
            EquipDescriptionImage = ei.GetComponent<Image>();
            Debug.Log("�ҵ�װ������ͼƬ");

            Transform rightImg = transform.FindChildByName("RightHandEquipImg ");
            RightHandImage = rightImg.GetComponent<Image>();
            Debug.Log("�ҵ�����װ��չʾͼƬ");

            Transform leftImg = transform.FindChildByName("LeftHandEquipImg");
            LeftHandImage = leftImg.GetComponent<Image>();
            Debug.Log("�ҵ�����װ��չʾͼƬ");

            Transform headImg = transform.FindChildByName("HeadEquipImg");
            HeadImage = headImg.GetComponent<Image>();
            Debug.Log("�ҵ�ͷ��װ��չʾͼƬ");

            Transform bodyImg = transform.FindChildByName("BodyEquipImg ");
            BodyImage = bodyImg.GetComponent<Image>();
            Debug.Log("�ҵ�����װ��չʾͼƬ");

            Transform krenel = transform.FindChildByName("KernelEquipImg");
            KernelImg = krenel.GetComponent<Image>();
            Debug.Log("�ҵ�����װ��չʾͼƬ");

            Transform consumer_1 = transform.FindChildByName("Consumer_1");
            Consumer_1Img = consumer_1.GetComponent<Image>();
            Debug.Log("�ҵ�����1չʾͼƬ");

            Transform consumer_2 = transform.FindChildByName("Consumer_2");
            Consumer_2Img = consumer_2.GetComponent<Image>();
            Debug.Log("�ҵ�����2չʾͼƬ");

            Transform consumer_3 = transform.FindChildByName("Consumer_3");
            Consumer_3Img = consumer_3.GetComponent<Image>();
            Debug.Log("�ҵ�����3չʾͼƬ");

            Transform consumer_4 = transform.FindChildByName("Consumer_4");
            Consumer_4Img = consumer_4.GetComponent<Image>();
            Debug.Log("�ҵ�����4չʾͼƬ");

            Transform consumer_5 = transform.FindChildByName("Consumer_5");
            Consumer_5Img = consumer_5.GetComponent<Image>();
            Debug.Log("�ҵ�����5չʾͼƬ");

            Transform consumer_6 = transform.FindChildByName("Consumer_6");
            Consumer_6Img = consumer_6.GetComponent<Image>();
            Debug.Log("�ҵ�����6չʾͼƬ");

            Transform consumer_7 = transform.FindChildByName("Consumer_7");
            Consumer_7Img = consumer_7.GetComponent<Image>();
            Debug.Log("�ҵ�����7չʾͼƬ");

            Transform consumer_8 = transform.FindChildByName("Consumer_8");
            Consumer_8Img = consumer_8.GetComponent<Image>();
            Debug.Log("�ҵ�����8չʾͼƬ");

            Transform eqb = transform.FindChildByName("EquipmenBagContent");
            equipBagUIList = eqb.GetComponent<EquipmentBagUIList>();
            Debug.Log("�ҵ�װ�������б�");

            Transform rightSelector = transform.FindChildByName("RightHandSelector");
            righthandSelector = rightSelector.gameObject;
            Debug.Log("�ҵ���������ѡ���");

            Transform leftSelector = transform.FindChildByName("LeftHandSelector");
            lefthandSelector = leftSelector.gameObject;
            Debug.Log("�ҵ���������ѡ���");

            Transform hS = transform.FindChildByName("HeadSelector");
            headSelector = hS.gameObject;
            Debug.Log("�ҵ�ͷ��װ��ѡ���");

            Transform bS = transform.FindChildByName("BodySelector");
            bodySelector = bS.gameObject;
            Debug.Log("�ҵ�����װ��ѡ���");

            Transform kernleSelect = transform.FindChildByName("KernelSelector");
            kernelSelector = kernleSelect.gameObject;
            Debug.Log("�ҵ�����װ��ѡ���");

            Transform equipSelect = transform.FindChildByName("EquipmentSelector");
            equipmentSelector = equipSelect.GetComponent<EquipmentSelector>();
            Debug.Log("�ҵ�װ��ѡ�����");

            Transform equipControl = transform.FindChildByName("EquipmentBar");
            equipmentController = equipControl.GetComponent<EquipmentController>();
            Debug.Log("�ҵ�װ���������ƹ���");

            Transform bl = transform.FindChildByName("BagContent");
            bagList = bl.GetComponent<BagList>();
            bagList.CreatBagList();
            bagList.UpdateBag();
            Debug.Log("�ҵ��������������ӣ�");

            normalDescription.UpdateNormalDescription();
        }

        #region 外部方法

        /// <summary>
        /// 获取右手武器装备列表
        /// </summary>
        /// <returns></returns>
        public List<Item> GetRightHandItem()
        {
            return equipmentSelector.rightHandItems;
        }

        /// <summary>
        /// 获取右手武器当前的索引
        /// </summary>
        /// <returns></returns>
        public int RightHandCurrentIndex()
        {
            return mainUIFunc.equipmentViewManager.RightHnadCycler.currentIndex;
        }

        /// <summary>
        /// 获取左手武器的装备列表
        /// </summary>
        /// <returns></returns>
        public List<Item> GetLeftHandItem()
        {
            return equipmentSelector.leftHandItems;
        }


        /// <summary>
        /// 获取左手武器当前的索引
        /// </summary>
        /// <returns></returns>
        public int LeftHandCurrentIndex()
        {
            return mainUIFunc.equipmentViewManager.LeftHnadCycler.currentIndex;
        }


        /// <summary>
        /// 获取消耗品装备列表
        /// </summary>
        /// <returns></returns>
        public List<Item> GetConsumableItem()
        {
            return equipmentSelector.consumEquipItems;
        }

        /// <summary>
        /// 获取当前消耗品的索引
        /// </summary>
        /// <returns></returns>
        public int ConsuableCurrentIndex()
        {
            return mainUIFunc.equipmentViewManager.PropCycler.currentIndex;
        }

        /// <summary>
        /// 获取当前的头部装备
        /// </summary>
        /// <returns></returns>
        public Item GetHeadEquipItem()
        {
            return equipmentSelector.headEquipItems[0];
        }


        /// <summary>
        /// 获取当前的身体装备
        /// </summary>
        /// <returns></returns>
        public Item GetBodyEquipItem()
        {
            return equipmentSelector.bodyEquipItems[0];
        }

        /// <summary>
        /// 获取当前的核心装备
        /// </summary>
        /// <returns></returns>
        public Item GetKernelEquipItem()
        {
            return equipmentSelector.kernelEquipItems[0];
        }

        /// <summary>
        /// 更新装备背包
        /// </summary>
        public void UpdateEquipBag()
        {
            equipBagUIList.UpdateEquipmentBag();
        }

        /// <summary>
        /// 更新背包
        /// </summary>
        public void UpdateNormalBag()
        {
            bagList.UpdateBag();
        }

        #endregion

    }

}
