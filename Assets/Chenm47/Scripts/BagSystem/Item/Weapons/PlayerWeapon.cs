using ns.Character;
using ns.Character.Player;
using ns.Value;
using System;
using Common;
using ns.ItemInfos;

namespace ns.Weapons
{
    [Serializable]
    /// <summary>
    /// 描述：
    /// </summary>
    public class PlayerWeapon : Weapon
    {
        private PlayerInfo playerInfo;
        private PlayerCPFactorPiecewise playerCPFactorPiecewise;

        public PlayerWeapon(PlayerInfo palyerInfo, WeaponInfo wInfo)
        {
            lVHandler = new WeaponLVHandler();
            WPCPHandler = new WeaponCharacterPropertyHandler();
            this.playerInfo = palyerInfo;
            WInfo = wInfo;
            playerCPFactorPiecewise = new PlayerCPFactorPiecewise();
        }

        public override float GetFinalPhysicalATK()
        {
            //武器基础物理攻击力=
            //((武器基础面板（武器攻击力）*武器等级影响)
            //+（人物属性面板 *（武器对应属性加成倍率注：不规则曲线）)=>wpcp
            var weaponValues = WInfo.WeaponValue;
            float res = weaponValues.BasePhysicalAtk;
            res = res * lVHandler.GetLVFactor(WInfo.wType, WInfo.CurrentLV);

            float wpcp = 0;
            foreach (var property in WInfo.WCPCurrentProperties)
            {
                float cpValue = playerInfo.CharacterProperties.Find(cp => cp.propertyType == property.cpt).value;
                //分段获得玩家能力值加成系数
                float cpfactor = playerCPFactorPiecewise.GetCPFactor(cpValue);

                wpcp += (cpfactor * WPCPHandler.GetWeaponPropertyLVFactor(property.wpt));
            }
            res += wpcp;
            return res;
        }

        public override ResistanceType[] GetAllSpecialResistanceTypes()
        {
            return WInfo.WeaponValue.WeaponSpecialBaseAtks.ToArray().
                Select(
                atk => atk.specialResistanceType
                );
        }

        public override float GetSpecialATK(ResistanceType resistanceType)
        {
            //武器基础属性攻击力=
            //((武器基础面板（武器攻击力）*武器等级影响)
            //+（人物属性面板 *（武器对应属性加成倍率注：不规则曲线）)=>wpcp
            var weaponValues = WInfo.WeaponValue;
            float res = weaponValues.WeaponSpecialBaseAtks.
                Find(atk => atk.specialResistanceType == resistanceType).Value;//获取属性基础攻击力
            res = res * lVHandler.GetLVFactor(WInfo.wType, WInfo.CurrentLV);

            float wpcp = 0;
            foreach (var property in WInfo.WCPCurrentProperties)
            {
                float cpValue = playerInfo.CharacterProperties.Find(cp => cp.propertyType == property.cpt).value;
                //分段获得玩家能力值加成系数
                float cpfactor = playerCPFactorPiecewise.GetCPFactor(cpValue);

                wpcp += (cpfactor * WPCPHandler.GetWeaponPropertyLVFactor(property.wpt));
            }
            res += wpcp;
            return res;
        }
    }
}
