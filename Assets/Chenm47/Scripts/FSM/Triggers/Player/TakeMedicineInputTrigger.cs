using AI.FSM.Framework;
using ns.BagSystem.Freamwork;
using UnityEngine;

namespace AI.FSM
{
    /// <summary>
    /// 描述：
    /// </summary>
    public class TakeMedicineInputTrigger : FSMTrigger
    {
        public override bool HandleTrigger(FSMBase fSMBase)
        {
            if (!PlayerFSMBase.Instance.playerInput.UseItem) return false;

            Item item = PlayerFSMBase.Instance.playerEquipmentManager.GetCurrentItem();

            //Debug.Log(item.itemInfo.ItemName);

            return item.CurrentCount > 0 &&
                item.itemInfo.ItemName == "灰质剂";//暂时这样
        }

        public override void Init()
        {
            triggerID = FSMTriggerID.TakeMedicineInput;
        }
    }
}
