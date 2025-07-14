using System;
using UnityEngine;


namespace ns.Skill
{
    /// <summary>
    /// 描述：技能释放器，用于技能施加释放后的算法
    /// </summary>
    public abstract class SkillDeployer : MonoBehaviour
    {
        private SkillData skillData;

        public SkillData SkillData
        {
            get
            {
                return skillData;
            }
            set
            {
                skillData = value;
                InitDeployer();
            }
        }

        private ISelector selector;
        private IEffect[] effects;
        //初始化释放器
        private void InitDeployer()
        {
            //创建算法对象
            //创建选区
            selector = DeployerConfigFactory.CreateSelector(SkillData);
            //创建影响效果
            effects = DeployerConfigFactory.CreateEffects(SkillData);
        }

        /// <summary>
        /// 获取选区选中对象
        /// </summary>
        protected void CalculateTargets()
        {
            skillData.attackTargets = selector.GetTargets(skillData);
            //测试**************
            //foreach (var item in skillData.attackTargets)
            //{
            //    print(item.name);
            //}
        }

        /// <summary>
        /// 影响对象
        /// </summary>
        protected void ImpactTargers()
        {
            for (int i = 0; i < effects.Length; i++)
            {
                effects[i].Execute(this);
            }
        }

        public abstract void DeploySkill();

    }
}
