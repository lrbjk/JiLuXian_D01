using UnityEngine;

namespace ns.Skill
{
    /// <summary>
    /// 描述：定义规则，选区类命名："ns.Skill."+skillData.selectorType+"Selector"，且继承接口ISelector
    /// </summary>
    public interface ISelector
    {
        Transform[] GetTargets(SkillData skillData);
    }
}
