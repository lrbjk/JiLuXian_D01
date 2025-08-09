namespace ns.Character
{
    /// <summary>
    /// 描述：角色参数
    /// </summary>
    public class CharacterParam
    {
        /*
         角色参数
        ------------------------
        角色参数ID
        角色名称
        角色参数类型
        基础最大血量
        基础普通攻击伤害
        基础移动速度
         */
        /// <summary>角色参数ID</summary>
        public int CharacterParamID;
        /// <summary>角色名称</summary>
        public string CharacterName;
        /// <summary>角色参数类型</summary>
        public CharacterParamType CharacterParam_Type;
    }
}
