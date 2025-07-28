using ns.Character.Player;
using ns.Value;

namespace ns.Item.Weapons
{
    /// <summary>
    /// 描述：
    /// </summary>
    public class PlayerWeapon : Weapon
    {
        private PlayerInfo playerInfo;
        private ConfigurablePiecewiseFunction playerCPFactorPiecewise;


        public override float GetFinalATK()
        {
            //武器基础物理攻击力=
            //((武器基础面板（武器攻击力）*武器等级影响)
            //+（人物属性面板 *（武器对应属性加成倍率注：不规则曲线）)=>wpcp
            var weaponValues = Info.WeaponValue;
            float res = weaponValues.BaseAtk;
            res = res * lVHandler.GetLVFactor(Info.Type, Info.CurrentLV);

            float wpcp = 0;
            foreach (var property in Info.WCPCurrentProperties)
            {
                float cpValue = playerInfo.CharacterProperties.Find(cp => cp.propertyType == property.cpt).value;
                //分段获得cpfactor
                float cpfactor = playerCPFactorPiecewise.Evaluate(cpValue);

                wpcp += (cpfactor * WPCPHandler.GetWeaponPropertyLVFactor(property.wpt));
            }
            res += wpcp;
            return res;
        }
    }
}
