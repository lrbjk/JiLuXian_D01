using System;

namespace ns.Skill
{
    /// <summary>
    /// 描述：释放器配置工厂类
    /// </summary>
    public class DeployerConfigFactory
    {
        public static ISelector CreateSelector(SkillData skillData)
        {
            //定义规则，选区类命名："ns.Skill."+skillData.selectorType+"Selector"，且继承接口ISelector
            string className = "ns.Skill." + skillData.selectorType + "Selector";
            return CreatInstace(className) as ISelector;
        }

        public static IEffect[] CreateEffects(SkillData skillData)
        {
            IEffect[] res = new IEffect[skillData.impactType.Length];
            for (int i = 0; i < skillData.impactType.Length; i++)
            {
                string impactClassName = "ns.Skill." + skillData.impactType[i] + "Effect";
                res[i] = CreatInstace(impactClassName) as IEffect;
            }
            return res;
        }

        private static object CreatInstace(string className)
        {
            Type t1 = Type.GetType(className);
            return Activator.CreateInstance(t1);
        }

    }
}
