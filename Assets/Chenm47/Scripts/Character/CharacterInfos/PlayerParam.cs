namespace ns.Character
{
    /// <summary>
    /// 描述：
    /// </summary>
    public class PlayerParam : CharacterParam
    {
        /*
         玩家角色参数
        -------------
        基础最大能量
        基础普通攻击单次充能值
        基础自然恢复充能值
        充能刻度
        自然充能回复基础间隔
        手牌上限
         */
        /// <summary>基础最大能量</summary>
        public int BaseMaxMP;
        /// <summary>基础普通攻击单次充能值 </summary>
        public int BasePreMPSimAtk;
        /// <summary>基础自然恢复充能值</summary>
        public int BasePreMPNatural;
        /// <summary>充能刻度</summary>
        public int PreMPConversionValue;
        /// <summary>自然充能回复基础间隔</summary>
        public int BasePreMPNaturalTime;
        /// <summary>基础手牌上限</summary>
        public int BaseMaxHandCardValue;
    }
}
