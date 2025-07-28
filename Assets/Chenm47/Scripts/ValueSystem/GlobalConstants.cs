namespace ns.Value
{
    /// <summary>
    /// 描述：全局常数
    /// </summary>
    public static class GlobalConstants
    {
        /// <summary>物理攻击力修正系数</summary>
        public static float PhysicalDamageCorrectionFactor;
        /// <summary> 防御减伤率伤害系数A </summary>
        public static float ReducedDamageRateDamageFactor;
        /// <summary> 防御减伤率防御系数B </summary>
        public static float DamageReductionRateDefenseFactor;
        /// <summary> 防御力减伤率上限 </summary>
        public static float DefenseRateCeiling;
        /// <summary> 属性减伤率调整系数C </summary>
        public static float AttributeReductionRateAdjustmentFactor;
        /// <summary> 反击系数随机上限 </summary>
        public static float DefenderFactorCeiling = 1.2f;
        /// <summary> 反击系数随机下限 </summary>
        public static float DefenderFactorFloor = 1.1f;
        /// <summary> 伤害浮动系数随机上限 </summary>
        public static float DamageFloatFactorCeiling = 1.1f;
        /// <summary> 伤害浮动系数随机下限 </summary>
        public static float DamageFloatFactorFloor = 0.9f;
    }
}
