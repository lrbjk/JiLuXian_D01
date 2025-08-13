using AI.FSM.Framework;
using ns.Character.Player;
using ns.ItemInfos;
using ns.Movtion;
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
            playerFSM.characterInfo.IsInMovtionRecoveryFlag = false;
        }
        protected override MovtionInfo GetMovtionInfo(bool isLeft, WeaponInfo currentWeponInfo)
        {
            //直接获取玩家信息中的ComboSkillID
            PlayerInfo playerInfo = playerFSM.characterInfo as PlayerInfo;
            int movtionID = playerInfo.ComboMovtionlID;
            var info = playerFSM.movtionManager.GetMovtionInfo(movtionID);
            return info;
        }

        protected override void PlayAnimation(FSMBase fSMBase)
        {
            //正常过度默认参数(offeset)无法循环播放
            fSMBase.animator.CrossFade(movtionInfo.AnimationName, 0.1f, -1, 0f);
        }

    }
}
