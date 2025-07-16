using AI.FSM.Framework;
using ns.Item.Weapons;
using ns.Skill;
using UnityEngine;

namespace AI.FSM
{
    /// <summary>
    /// 描述：连续攻击状态
    /// </summary>
    public class ComboAttackState : AttackState
    {
        public override void Init()
        {
            StateID = FSMStateID.ComboAttack;
        }

        public override void EnterState(FSMBase fSMBase)
        {
            Debug.Log(Time.frameCount + "进入ComboAttackState状态");
            base.EnterState(fSMBase);
            //后摇结束
            playerFSMBase.playerInfo.IsInAttackRecoveryFlag = false;
        }

        protected override SkillInfo GetSkillInfo(bool isLeft, WeaponInfo currentWeponInfo)
        {
            //直接获取玩家信息中的ComboSkillID，并更新
            int skillID = playerFSMBase.playerInfo.ComboSkillID;
            var skillInfo = playerFSMBase.characterSkillManager.GetSkillInfo(skillID);
            return skillInfo;
        }

    }
}
