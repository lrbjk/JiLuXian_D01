using ns.Character;
using UnityEngine;

namespace ns.Skill
{
    /// <summary>
    /// 描述：减少蓝量
    /// </summary>
    public class CostSPEffect : IEffect
    {
        public void Execute(SkillDeployer skillDeployer)
        {
            //var status = skillDeployer.SkillData.owner.GetComponent<CharacterStatus>();
            //status.SP -= skillDeployer.SkillData.costSP;
            Debug.Log("CostSP");
        }
    }
}
