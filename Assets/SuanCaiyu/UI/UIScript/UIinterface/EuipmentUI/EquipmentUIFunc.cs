using Common.Helper;
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
        public EquipDescription  equipDescription;


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


        [Header("选项面板控制")]
         public GameObject righthandSelector;
         public GameObject lefthandSelector;
         public GameObject headSelector;
         public GameObject bodySelector;
         public GameObject kernelSelector;

         public EquipmentSelector equipmentSelector;
         public EquipmentBagUIList equipBagUIList;
         public EquipmentController equipmentController;
         public BagList bagList;
        protected override void Start()
        {
            base.Start();

            Transform dt = transform.FindChildByName("DescriptionLayer");
            normalDescription = dt.GetComponent<NormalDescription>();
            Debug.Log("找到描述文字");

            Transform et = transform.FindChildByName("EquipDescriptionLayer");
            equipDescription = et.GetComponent<EquipDescription>();
            Debug.Log("找到装备描述文字");

            Transform di = transform.FindChildByName("DescriptionImage");
            DescriptionImage = di.GetComponent<Image>();
            Debug.Log("找到描述图片");

            Transform ei = transform.FindChildByName("EquipDescriptionImage");
            EquipDescriptionImage = ei.GetComponent<Image>();
            Debug.Log("找到装备描述图片");

            Transform rightImg = transform.FindChildByName("RightHandEquipImg ");
            RightHandImage = rightImg.GetComponent<Image>();
            Debug.Log("找到右手装备展示图片");

            Transform leftImg = transform.FindChildByName("LeftHandEquipImg");
            LeftHandImage = leftImg.GetComponent<Image>();
            Debug.Log("找到左手装备展示图片");

            Transform headImg = transform.FindChildByName("HeadEquipImg");
            HeadImage = headImg.GetComponent<Image>();
            Debug.Log("找到头部装备展示图片");

            Transform bodyImg = transform.FindChildByName("BodyEquipImg ");
            BodyImage = bodyImg.GetComponent<Image>();
            Debug.Log("找到身体装备展示图片");

            Transform krenel = transform.FindChildByName("KernelEquipImg");
            KernelImg = krenel.GetComponent<Image>();
            Debug.Log("找到核心装备展示图片");

            Transform consumer_1 = transform.FindChildByName("Consumer_1");
            Consumer_1Img = consumer_1.GetComponent<Image>();
            Debug.Log("找到道具1展示图片");

            Transform consumer_2 = transform.FindChildByName("Consumer_2");
            Consumer_2Img = consumer_2.GetComponent<Image>();
            Debug.Log("找到道具2展示图片");

            Transform consumer_3 = transform.FindChildByName("Consumer_3");
            Consumer_3Img = consumer_3.GetComponent<Image>();
            Debug.Log("找到道具3展示图片");

            Transform consumer_4 = transform.FindChildByName("Consumer_4");
            Consumer_4Img = consumer_4.GetComponent<Image>();
            Debug.Log("找到道具4展示图片");

            Transform consumer_5 = transform.FindChildByName("Consumer_5");
            Consumer_5Img = consumer_5.GetComponent<Image>();
            Debug.Log("找到道具5展示图片");

            Transform consumer_6 = transform.FindChildByName("Consumer_6");
            Consumer_6Img = consumer_6.GetComponent<Image>();
            Debug.Log("找到道具6展示图片");

            Transform consumer_7 = transform.FindChildByName("Consumer_7");
            Consumer_7Img = consumer_7.GetComponent<Image>();
            Debug.Log("找到道具7展示图片");

            Transform consumer_8 = transform.FindChildByName("Consumer_8");
            Consumer_8Img = consumer_8.GetComponent<Image>();
            Debug.Log("找到道具8展示图片");

            Transform eqb = transform.FindChildByName("EquipmenBagContent");
            equipBagUIList = eqb.GetComponent<EquipmentBagUIList>();
            Debug.Log("找到装备背包列表");

            Transform rightSelector = transform.FindChildByName("RightHandSelector");
            righthandSelector = rightSelector.gameObject;
            Debug.Log("找到右手武器选项槽");

            Transform leftSelector = transform.FindChildByName("LeftHandSelector");
            lefthandSelector = leftSelector.gameObject;
            Debug.Log("找到左手武器选项槽");

            Transform hS = transform.FindChildByName("HeadSelector");
            headSelector = hS.gameObject;
            Debug.Log("找到头部装备选项槽");

            Transform bS = transform.FindChildByName("BodySelector");
            bodySelector = bS.gameObject;
            Debug.Log("找到身体装备选项槽");

            Transform kernleSelect = transform.FindChildByName("KernelSelector");
            kernelSelector = kernleSelect.gameObject;
            Debug.Log("找到核心装备选项槽");

            Transform equipSelect = transform.FindChildByName("EquipmentSelector");
            equipmentSelector = equipSelect.GetComponent<EquipmentSelector>();
            Debug.Log("找到装备选择管理");

            Transform equipControl = transform.FindChildByName("EquipmentBar");
            equipmentController = equipControl.GetComponent<EquipmentController>();
            Debug.Log("找到装备交互控制管理");

            Transform bl = transform.FindChildByName("BagContent");
            bagList = bl.GetComponent<BagList>();
            bagList.CreatBagList();
            bagList.UpdateBag();
            Debug.Log("找到并创建背包格子！");

            normalDescription.UpdateNormalDescription();
        }
    }

}
