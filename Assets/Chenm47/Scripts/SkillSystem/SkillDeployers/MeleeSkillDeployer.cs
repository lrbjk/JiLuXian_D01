namespace ns.Skill
{
    /// <summary>
    /// 描述：
    /// </summary>
    public class MeleeSkillDeployer : SkillDeployer
    {
        /// <summary>
        /// 释放技能
        /// </summary>
        public override void DeploySkill()
        {
            CalculateTargets();//执行选区算法
            ImpactTargers();//影响选中对象
        }
    }
}
