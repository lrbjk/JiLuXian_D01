using UnityEngine;
using CharacterInfo = ns.Character.CharacterInfo;

namespace ns.Value
{
    /// <summary>
    /// 描述：伤害计算器（静态服务类）
    /// </summary>
    public static class DamageCalculator
    {
        public static float CalculatePhysicalDamage(
            CharacterInfo attacker,
            CharacterInfo defender)
        {
            #region 物理伤害部分
            // (1)获取武器基础物理攻击力
            float weaponPhysicalATK = attacker.GetWeaponPhysicalATK();

            //1.1.Buff后武器基础物理攻击力
            //float weaponPostBuffPhysicalATK=(weaponPhysicalATK+ WeaponBuff)*(1 + 应用BodyBuff(乘算))
            float weaponPostBuffPhysicalATK = weaponPhysicalATK;

            // (2)物理部分伤害
            var attackerMovtionInfo = attacker.MovtionManager.GetMovtionInfo(attacker.CurrentMovtionID);
            float physicalDamage = (weaponPostBuffPhysicalATK * GlobalConstants.PhysicalDamageCorrectionFactor)
                * attackerMovtionInfo.ActionMultiplier;
            #endregion

            #region 其他属性伤害部分
            float otherDamages = 0;
            #endregion

            //基本伤害总值
            float allBaseDamages = physicalDamage + otherDamages;

            // (3.1)防御力计算
            float DEF = defender.GetDEF();
            //防御力减伤率 = 防御力 /(A*基本伤害总值 + B*防御者防御力)
            float defenseRate = DEF / (GlobalConstants.ReducedDamageRateDamageFactor * allBaseDamages
                + GlobalConstants.DamageReductionRateDefenseFactor * DEF);

            ////3.1.1.Buff后防御力减伤率,
            ////防御力减伤率+DamageAbsorptionBuff(百分比加算，可叠加)+DamageAbsorptionDebuff(负值、减益效果、可叠加)
            //defenseRate = DamageAbsorptionBuff + DamageAbsorptionDebuff;

            defenseRate = Mathf.Min(defenseRate, GlobalConstants.DefenseRateCeiling);

            //（3.2）防御后伤害 = 基本伤害总值 x (1 - 防御力减伤率)
            float postDefenseDamage = allBaseDamages * (1 - Mathf.Min(defenseRate, 0.9f));

            // 抗性计算
            //（4.1.1）属性伤害占比 = 该属性部分伤害 / 基本伤害总值
            //该属性分配伤害 = 防御后伤害 x 属性伤害占比
            float attributePhysicalDamage = (physicalDamage / allBaseDamages) * postDefenseDamage;

            //((4.1.2)属性抗性减伤率 = 防御者该属性抗性 / (防御者该属性抗性 + C) (或类似的收益递减公式)，劈砍等物理属性也算)
            var prt = attackerMovtionInfo.PhysicalResistanceType;            //物理属性类型
            float resistanceValue = defender.GetResistance(prt);
            float reductionRate = resistanceValue / (resistanceValue + GlobalConstants.AttributeReductionRateAdjustmentFactor);

            //（4.2）该属性最终伤害 = 该属性分配伤害 x (1 - 该属性抗性减伤率)
            float finalAttributePhysicalDamage = attributePhysicalDamage * (1 - reductionRate);

            //......其他属性伤害计算同理，暂不写
            float otherAttributePhysicalDamage = 0;

            //(5) 伤害总值 =所有属性最终伤害之和* 临界状态效果系数(核心决定)
            float allAttributesDamages = finalAttributePhysicalDamage + otherAttributePhysicalDamage;
            float allDamages = allAttributesDamages * attacker.GetCriticalStateEffectCoefficient();

            //（6.1.1）反击系数 = 1.0 + 反击伤害加成率 (例如，1.1 - 1.4，浮动值）
            float defenderCoefficient = 1f;
            if (defender.IsInPreMovtionFlag || defender.IsInMovtionRecoveryFlag)
            {
                defenderCoefficient = Random.Range(GlobalConstants.DefenderFactorFloor, GlobalConstants.DefenderFactorCeiling);
            }
            //(6.1.2) 常规伤害=伤害总值*反击系数*伤害浮动系数(随机数)
            float ruleDamage = allDamages * defenderCoefficient *
                Random.Range(GlobalConstants.DamageFloatFactorFloor, GlobalConstants.DamageFloatFactorCeiling);

            //(6.2) 处决伤害=Buff后武器基础物理攻击力 * 武器处决系数 * 处决动作倍率
            float ExecutionDamage = 0;
            if (attackerMovtionInfo.ExecutionMultiplier != 0f)
            {
                ExecutionDamage = attackerMovtionInfo.ExecutionMultiplier *
                    weaponPostBuffPhysicalATK * attacker.GetWeaponExecutionCoefficient();
            }

            //(6.3) 最终造成伤害=常规伤害+处决伤害
            float finalDamage = ruleDamage + ExecutionDamage;
            return finalDamage;
        }
    }
}
