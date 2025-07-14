using ns.Character;
using Common;
using System.Collections.Generic;
using UnityEngine;

namespace ns.Skill
{
    /// <summary>
    /// 描述：扇形选区
    /// </summary>
    public class SectorSelector : ISelector
    {
        public Transform[] GetTargets(SkillData skillData)
        {
            //扇形选区
            GameObject[] targets;
            Transform ownerTF = skillData.owner.transform;
            List<Transform> targetsList = new List<Transform>();
            foreach (var tag in skillData.attackTargetTags)
            {
                targets = GameObject.FindGameObjectsWithTag(tag);

                targetsList.AddRange(targets.Select(go => go.transform));
            }

            targetsList = targetsList.FindAll(t =>
            //t.GetComponent<CharacterStatus>().HP > 0 &&
            Vector3.Distance(t.position, ownerTF.position) <= skillData.attackDistance &&
            Vector3.Angle(ownerTF.forward, (t.position - ownerTF.position)) <= skillData.attackAngle / 2
            );

            //单攻群攻
            Transform[] res = targetsList.ToArray();
            if (skillData.attackType == SkillAttackType.Group)
                return res;
            if (res.Length == 0)
                return null;
            Transform min = res.GetMin(t => Vector3.Distance(ownerTF.position, t.position));
            return new Transform[] { min };
        }
    }
}
