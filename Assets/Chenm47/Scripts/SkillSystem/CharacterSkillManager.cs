using Common;
using ns.Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ns.Skill
{
    /// <summary>
    /// 描述：技能管理器
    /// </summary>
    public class CharacterSkillManager : MonoBehaviour
    {
        public SkillData[] skills;

        public SkillInfo[] skillInfos;

        private void Start()
        {
            for (int i = 0; i < skills.Length; i++)
                InitSkill(skills[i]);
        }

        private void InitSkill(SkillData skillData)
        {
            skillData.skillPrefb = ResourceManager.Load<GameObject>(skillData.prefbName);
            skillData.owner = gameObject;
        }

        public void GenerateSkill(SkillData skillData)
        {
            //创建技能预制体
            GameObject go = GameObjectPool.Instance.CreateGameObject(skillData.skillName, skillData.skillPrefb, transform.position, transform.rotation);
            //传递技能数据
            SkillDeployer skillDeployer = go.GetComponent<SkillDeployer>();
            skillDeployer.SkillData = skillData;//内部创建算法对象
            skillDeployer.DeploySkill();//内部执行算法对象
            //销毁技能
            GameObjectPool.Instance.CollectObjectDelay(go, skillData.duringTime);
            ////开启技能冷却
            //StartCoroutine(CoolTimeDown(skillData));
        }

        //private IEnumerator CoolTimeDown(SkillData skillData)
        //{
        //    skillData.coolReaim = skillData.coolTime;
        //    while (skillData.coolReaim > 0)
        //    {
        //        yield return new WaitForSeconds(1f);
        //        skillData.coolReaim -= 1;
        //    }
        //    skillData.coolReaim = 0;
        //}

        /// <summary>
        /// 准备指定ID的合法技能
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SkillData PrepareSkill(int id)
        {
            //根据ID查找技能
            var skillData = skills.Find<SkillData>(data => data.skillID == id);
            //if (skillData != null && skillData.coolReaim <= 0 && skillData.costSP <= GetComponent<CharacterStatus>().SP)
            if (skillData != null)
            {
                return skillData;
            }
            return null;
        }

        public SkillInfo GetSkillInfo(int id)
        {
            //根据ID查找技能信息
            var skillInfo = skillInfos.Find<SkillInfo>(info => info.SkillID == id);
            return skillInfo;
        }

        public IEnumerable<SkillInfo> GetAllSkillInfos()
        {
            foreach (var skill in skillInfos)
            {
                yield return skill;
            }
        }

    }
}
