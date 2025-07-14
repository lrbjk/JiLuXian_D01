namespace ns.Skill
{
    /// <summary>
    /// 描述：定义规则，选区类命名："ns.Skill."+skillData.impactType[i]+"Effect"，且继承接口IEffect
    /// </summary>
    public interface IEffect
    {
        void Execute(SkillDeployer skillDeployer);
    }
}
