using System;
using UnityEngine;

namespace ns.Skill
{
    [Serializable]
    /// <summary>
    /// 描述：技能参数
    /// </summary>
    public class SkillData
    {
        /// <summary>技能ID</summary>
        public int skillID;
        /// <summary>技能名称</summary>
        public string skillName;
        /// <summary>技能描述</summary>
        public string skillDescription;
        ///// <summary>冷却时间</summary>
        //public float coolTime;
        ///// <summary>冷却剩余时间</summary>
        //public float coolReaim;
        /// <summary>魔法消耗</summary>
        public int costSP;
        /// <summary>攻击距离</summary>
        public float attackDistance;
        /// <summary>攻击角度</summary>
        public float attackAngle;
        /// <summary>攻击目标tag</summary>
        public string[] attackTargetTags = { "Enemy" };
        [HideInInspector]
        /// <summary>攻击目标对象数组</summary>
        public Transform[] attackTargets;
        /// <summary>技能影响类型</summary>
        public string[] impactType = { "CostSp", "Damage" };
        /// <summary>连击ID</summary>
        public int nextSkillID;
        /// <summary>伤害倍率</summary>
        public float attackRatio;
        /// <summary>持续时间</summary>
        public float duringTime;
        /// <summary>伤害间隔</summary>
        public float attackInterval;
        [HideInInspector]
        /// <summary>技能所属</summary>
        public GameObject owner;
        /// <summary>技能特效预制体名称</summary>
        public string prefbName;
        [HideInInspector]
        /// <summary>技能预制体对象</summary>
        public GameObject skillPrefb;
        /// <summary>动画名称 </summary>
        public string animationName;
        /// <summary>受击特效名称</summary>
        public string hitFxName;
        [HideInInspector]
        /// <summary>受击特效预制体对象</summary>
        public GameObject hitFxPrefeb;
        /// <summary>攻击类型，单攻群攻</summary>
        public SkillAttackType attackType;
        /// <summary>选择类型扇形，矩形</summary>
        public SelectorType selectorType;
    }
}
