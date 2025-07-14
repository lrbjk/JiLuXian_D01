using ns.Character;
using System.Collections;
using UnityEngine;

namespace ns.Skill
{
    /// <summary>
    /// 描述：减少敌人血量
    /// </summary>
    public class DamageEffect : IEffect
    {
        public void Execute(SkillDeployer skillDeployer)
        {
            Debug.Log("DamageEffect Execute");
            //var data = skillDeployer.SkillData;
            //if (data.attackTargets == null) return;
            ////间隔扣血
            //skillDeployer.StartCoroutine(StartDamage(data));
        }

        //private IEnumerator StartDamage(SkillData skillData)
        //{
        //    float timer = 0f;
        //    var owenerStatue = skillData.owner.GetComponent<CharacterStatus>();
        //    do
        //    {
        //        //扣血
        //        foreach (var item in skillData.attackTargets)
        //        {
        //            var statu = item.GetComponent<CharacterStatus>();
        //            statu.HP -= owenerStatue.ATK * skillData.attackRatio;
        //        }
        //        yield return new WaitForSeconds(skillData.attackInterval);
        //        timer += skillData.attackInterval;
        //    } while (timer < skillData.duringTime);
        //}

    }
}
