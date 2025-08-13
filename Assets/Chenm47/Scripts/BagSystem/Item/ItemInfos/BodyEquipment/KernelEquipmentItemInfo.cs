using ns.Character.Player;
using ns.ItemInfos;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*

*/
namespace ns.ItemInfos
{
    [CreateAssetMenu(menuName = "Item/Equipment/Kernel")]
    public class KernelEquipmentItemInfo : EquipmentInfo
    {
        [Header("临界点切换值")]
        public List<CriticalStateValuePairs> SwitchCriticalPoint;
        [Header("临界状态系数")]
        public List<CriticalStateValuePairs> criticalStateEffectCoefficientPairs;

        public float GetCriticalStateEffectCoefficient(CriticalStateType t)
        {
            //暂时这样
            return criticalStateEffectCoefficientPairs.Find(p => p.type == t).value;
        }

        protected override void InitializeDefaults()
        {
            base.InitializeDefaults();
            iType = ItemType.KernelEquipment;
        }
    }
}
