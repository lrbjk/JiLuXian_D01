using UnityEngine;

namespace ns.Skill
{
    /// <summary>
    /// 描述：
    /// </summary>
    public class SkillInfo : ScriptableObject
    {
        [Tooltip("技能ID")]
        /// <summary>技能ID</summary>
        public int SkillID;
        [Tooltip("技能名称")]
        /// <summary>技能名称</summary>
        public string SkillName;
        [Tooltip("连击ID")]
        /// <summary>连击ID</summary>
        public int ComboSkillID;
        [Tooltip("动画名称 ")]
        /// <summary>动画名称 </summary>
        public string AnimationName;
        [Tooltip("前摇时间（结束帧）")]
        /// <summary>前摇时间（结束帧） </summary>
        public int PreAttackEndFrame;
        [Tooltip("打击开始帧 ")]
        /// <summary>打击开始帧 </summary>
        public int AttackStartFrame;
        [Tooltip("打击帧结束帧")]
        /// <summary>打击帧结束帧 </summary>
        public int AttackEndFrame;
        [Tooltip("后摇时间（开始帧）")]
        /// <summary>后摇时间（开始帧） </summary>
        public int AttackRecoveryFrame;
        [Tooltip("蓄力时间 ")]
        /// <summary>蓄力时间 </summary>
        public float HoldingTime;
        [Tooltip("伤害数值")]
        /// <summary>伤害数值</summary>
        public int AtkValue;
    }
}
