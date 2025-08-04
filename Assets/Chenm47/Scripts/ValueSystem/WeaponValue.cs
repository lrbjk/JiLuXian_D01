using ns.Character;
using ns.Item.Weapons;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ns.Value
{

    public class WeaponLVHandler
    {
        private Dictionary<WeaponType, Dictionary<int, float>> map;

        public WeaponLVHandler()
        {
            //读取配置
            //测试---------------
            //每种类型不同等级对应不同系数
            map = new Dictionary<WeaponType, Dictionary<int, float>>(1);
            var swordMap = new Dictionary<int, float>();
            swordMap.Add(0, 0.5f);
            swordMap.Add(1, 0.8f);
            swordMap.Add(2, 1f);
            map.Add(WeaponType.Sword, swordMap);
        }

        public float GetLVFactor(WeaponType type, int lv)
        {
            return map[type][lv];
        }
    }

    public enum WeaponPropertyLV
    {
        A, B, C, D, E
    }

    [Serializable]
    public struct WeaponCharacterProperty
    {
        public CharacterPropertyType cpt;
        public WeaponPropertyLV wpt;
    }
    [Serializable]
    public struct WeaponSpecialBaseAtk
    {
        public ResistanceType specialResistanceType;
        public int Value;
    }

    public class WeaponCharacterPropertyHandler
    {
        private Dictionary<WeaponPropertyLV, float> map;

        public WeaponCharacterPropertyHandler()
        {
            map = new Dictionary<WeaponPropertyLV, float>();
            map.Add(WeaponPropertyLV.A, 1.1f);
            map.Add(WeaponPropertyLV.B, 0.9f);
        }

        public float GetWeaponPropertyLVFactor(WeaponPropertyLV weaponPropertyLV)
        {
            return map[weaponPropertyLV];
        }
    }

    public class WeaponLVMap
    {
        public int LV;
        public float LVFactor;
    }

    [CreateAssetMenu(menuName = "Values/Weapon")]
    /// <summary>
    /// 描述：
    /// </summary>
    public class WeaponValue : ScriptableObject
    {
        public int WeaponItemID;
        public int BasePhysicalAtk;
        [Header("武器属性攻击数值")]
        public List<WeaponSpecialBaseAtk> WeaponSpecialBaseAtks;
        [Header("处决系数")]
        public float ExecutionCoefficient;//处决系数
        [Header("武器削韧值")]
        public float ReducedPoise;//武器削韧值
    }

}
