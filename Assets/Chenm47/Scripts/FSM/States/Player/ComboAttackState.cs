using AI.FSM.Framework;
using ns.Character.Player;
using ns.Item.Weapons;
using ns.Movtion;
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
            playerFSMBase.characterInfo.IsInMovtionRecoveryFlag = false;
        }
        protected override MovtionInfo GetMovtionInfo(bool isLeft, WeaponInfo currentWeponInfo)
        {
            //直接获取玩家信息中的ComboSkillID
            PlayerInfo playerInfo = playerFSMBase.characterInfo as PlayerInfo;
            int movtionID = playerInfo.ComboMovtionlID;
            var info = playerFSMBase.movtionManager.GetMovtionInfo(movtionID);
            return info;
        }

    }
}
