using ns.Character;
using ns.Item.Weapons;
using System.Collections.Generic;
using UnityEngine;

namespace ns.Value
{

    public class WeaponLVHandler
    {
        private Dictionary<WeaponType, Dictionary<int, float>> map;

        WeaponLVHandler()
        {
            //读取配置
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

    public struct WeaponCharacterProperty
    {
        public CharacterPropertyType cpt;
        public WeaponPropertyLV wpt;
    }

    public class WeaponCharacterPropertyHandler
    {
        private Dictionary<WeaponPropertyLV, float> map;

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
        public float BaseAtk;
    }

}
