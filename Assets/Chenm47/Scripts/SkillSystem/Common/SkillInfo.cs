using UnityEngine;

namespace ns.Skill
{
    [CreateAssetMenu(menuName = "Skill/SkillInfo")]
    /// <summary>
    /// 描述：
    /// </summary>
    public class SkillInfo : ScriptableObject
    {
        /// <summary>技能ID</summary>
        public int SkillID;
        /// <summary>技能名称</summary>
        public string SkillName;
        /// <summary>连击ID</summary>
        public int NextSkillID;
        /// <summary>动画名称 </summary>
        public string AnimationName;
        /// <summary>前摇时间（结束帧） </summary>
        public int PreAttackEndFrame;
        /// <summary>打击开始帧 </summary>
        public int AttackStartFrame;
        /// <summary>打击帧 </summary>
        public int AttackEndFrame;
        /// <summary>后摇时间（开始帧） </summary>
        public int AttackRecoveryFrame;
    }
}
