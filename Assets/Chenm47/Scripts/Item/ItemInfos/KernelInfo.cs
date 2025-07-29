using System;
using System.Collections.Generic;
using UnityEngine;


namespace ns.Item.Equipment
{
    [Serializable]
    public class CriticalStateEffectCoefficientPairs
    {
        public CriticalStateType type;
        public float value;
    }

    public enum CriticalStateType
    {
        充盈,
        空虚
    }

    [CreateAssetMenu(menuName = "Item/Kernel")]
    /// <summary>
    /// 描述：核心物品信息
    /// </summary>
    public class KernelInfo : ItemInfo
    {
        public List<CriticalStateEffectCoefficientPairs> criticalStateEffectCoefficientPairs;

        public float GetCriticalStateEffectCoefficient(CriticalStateType t)
        {
            //暂时这样
            return criticalStateEffectCoefficientPairs.Find(p => p.type == t).value;
        }
    }
}
