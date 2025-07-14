using ns.Character;
using Common;
using UnityEngine;


namespace ns.Skill
{
    [RequireComponent(typeof(CharacterSkillManager))]
    /// <summary>
    /// 描述：
    /// </summary>
    public class CharacterSkillSystem : MonoBehaviour
    {
        private CharacterSkillManager skillManager;
        private Animator animator;

        private void Start()
        {
            skillManager = GetComponent<CharacterSkillManager>();
            animator = GetComponentInChildren<Animator>();
            //GetComponentInChildren<AnimationEventBehaviour>().attackHandler += DeloySkill;
        }

        SkillData skillData;

        private void DeloySkill()
        {
            //创建、并释放技能
            if (skillData != null)
            {
                skillManager.GenerateSkill(skillData);
            }
        }

        /// <summary>
        /// 为玩家准备
        /// </summary>
        /// <param name="id"></param>
        public void AttackUseSkill(int id, bool isBatter = false)
        {
            //准备技能
            if (skillData != null && isBatter)
                skillData = skillManager.PrepareSkill(skillData.nextSkillID);
            else
                skillData = skillManager.PrepareSkill(id);
            if (skillData == null) return;//该技能不可用
            //播放动画
            animator.SetBool(skillData.animationName, true);
            //生成技能(动画事件中已做)
            //若单攻
            if (skillData.attackType != SkillAttackType.Single) return;
            //查找目标
            var targets = new SectorSelector().GetTargets(skillData);
            if (targets == null) return;
            Transform target = targets[0];
            //朝向目标
            transform.LookAt(target);
        }

        /// <summary>
        /// 随机释放技能
        /// </summary>
        public void UseRandomSkill()
        {
            //先选出可用技能
            //再随机选出技能并释放
        }

    }
}
