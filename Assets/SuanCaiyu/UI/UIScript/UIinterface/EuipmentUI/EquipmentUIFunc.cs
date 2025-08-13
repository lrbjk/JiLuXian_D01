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
        public Text DescriptionText;
        public Text EquipDescriptionText;
        public Image DescriptionImage;
        public Image EquipDescriptionImage;
        public Image RightHandImage;
        public Image LeftHandImage;

        [Header("选项面板控制")]
         public GameObject righthandSelector;
         public GameObject lefthandSelector;
         public GameObject headSelector;
         public GameObject bodySelector;
         public GameObject kernelSelector;

         public EquipmentSelector equipmentSelector;
         public EquipmentBagUIList equipBagUIList;
        protected override void Start()
        {
            base.Start();

            Transform dt = transform.FindChildByName("DescriptionText");
            DescriptionText = dt.GetComponent<Text>();
            Debug.Log("找到描述文字");

            Transform et = transform.FindChildByName("EquipDescriptionText");
            EquipDescriptionText = et.GetComponent<Text>();
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

            Transform eqb = transform.FindChildByName("EquipmenBagContent");
            equipBagUIList = eqb.GetComponent<EquipmentBagUIList>();
            Debug.Log("找到装备背包列表");

            Transform rightSelector = transform.FindChildByName("RightHandSelector");
            righthandSelector = rightSelector.gameObject;
            Debug.Log("找到右手武器选项");

            Transform leftSelector = transform.FindChildByName("LeftHandSelector");
            lefthandSelector = leftSelector.gameObject;
            Debug.Log("找到左手武器选项");

            Transform hS = transform.FindChildByName("HeadSelector");
            headSelector = hS.gameObject;
            Debug.Log("找到头部装备选项");

            Transform bS = transform.FindChildByName("BodySelector");
            bodySelector = bS.gameObject;
            Debug.Log("找到身体装备选项");

            Transform kernleSelect = transform.FindChildByName("KernelSelector");
            kernelSelector = kernleSelect.gameObject;
            Debug.Log("找到核心装备选项");

            Transform equipSelect = transform.FindChildByName("EquipmentSelector");
            equipmentSelector = equipSelect.GetComponent<EquipmentSelector>();
            Debug.Log("找到装备选择管理");

        }
    }

}
