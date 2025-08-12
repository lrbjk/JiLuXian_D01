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

        protected override void Start()
        {
            base.Start();

            Transform dt = transform.FindChildByName("DescriptionText");
            DescriptionText = dt.GetComponent<Text>();
            Debug.Log("’“µΩ√Ë ˆŒƒ◊÷");

            Transform et = transform.FindChildByName("EquipDescriptionText");
            EquipDescriptionText = et.GetComponent<Text>();
            Debug.Log("’“µΩ◊∞±∏√Ë ˆŒƒ◊÷");

            Transform di = transform.FindChildByName("DescriptionImage");
            DescriptionImage = di.GetComponent<Image>();
            Debug.Log("’“µΩ√Ë ˆÕº∆¨");

            Transform ei = transform.FindChildByName("EquipDescriptionImage");
            EquipDescriptionImage = ei.GetComponent<Image>();
            Debug.Log("’“µΩ√Ë ˆÕº∆¨");
        }
    }

}
