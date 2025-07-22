using AI.FSM.Framework;
using Common;
using ns.Character.Player;
using ns.Item.Weapons;
using ns.Movtion;
using UnityEngine;

namespace AI.FSM
{
    /// <summary>
    /// 描述：
    /// </summary>
    public class AttackState : MovtionState
    {
        private GameObject currentWeaponGO;
        protected PlayerFSMBase playerFSMBase;

        public override void Init()
        {
            StateID = FSMStateID.Attack;
        }
        protected override MovtionInfo InitMovtionInfo(FSMBase fSMBase)
        {
            playerFSMBase = fSMBase as PlayerFSMBase;
            //获取当前武器信息
            WeaponInfo currentWeponInfo = fSMBase.equipmentManager.GetCurrentAtkWeapon(fSMBase);
            //左手？右手？
            bool isLeft = playerFSMBase.playerInput.IsLeftAttackTrigger;
            var lweapon = playerFSMBase.playerInventory.LeftWeapon;
            var rweapon = playerFSMBase.playerInventory.RightWeapon;
            currentWeaponGO = currentWeponInfo.ModleGO;
            //获取技能信息
            MovtionInfo movtionInfo = GetMovtionInfo(isLeft, currentWeponInfo);
            return movtionInfo;
        }
        protected virtual MovtionInfo GetMovtionInfo(bool isLeft, WeaponInfo currentWeponInfo)
        {
            //根据手中的武器和输入来决定使用哪个动作
            int movtionID = 0;//动作ID
            if (playerFSMBase.playerInput.IsLightAttackTrigger)
            {
                if (isLeft)
                    movtionID = currentWeponInfo.LightAtkIDL;
                else
                    movtionID = currentWeponInfo.LightAtkIDR;
            }
            else if (playerFSMBase.playerInput.IsHeavyAttackTrigger)
            {
                if (isLeft)
                    movtionID = currentWeponInfo.HeavyAtkIDL;
                else
                    movtionID = currentWeponInfo.HeavyAtkIDR;
            }
            else if (playerFSMBase.playerInput.IsSkillAttackTrigger)
            {
                movtionID = currentWeponInfo.SkillAtkIDL;
            }
            var skillInfo = playerFSMBase.movtionManager.GetMovtionInfo(movtionID);
            return skillInfo;
        }

        public override void EnterState(FSMBase fSMBase)
        {
            base.EnterState(fSMBase);
            //停止移动
            playerFSMBase.playerAction.StopMove();
            PlayerInfo playerInfo = playerFSMBase.characterInfo as PlayerInfo;
            //更新玩家信息技能ID
            playerInfo.LastAttackType = playerFSMBase.playerInput.AtkInputType;
            playerInfo.CurrentMovtionID = movtionInfo.MovtionID;
            playerInfo.ComboMovtionlID = movtionInfo.ComboMovtionID;
        }

        public override void ExitState(FSMBase fSMBase)
        {
            base.ExitState(fSMBase);
            //清空临时变量
            //以防万一禁用碰撞体
            currentWeaponGO.GetComponentInChildren<WeaponCollderHandle>(true).SetCollider(false);
            currentWeaponGO = null;
        }

        protected override void OnMovtionStart(object sender, AnimationEventArgs e)
        {
            base.OnMovtionStart(sender, e);
            //激活碰撞体
            currentWeaponGO.GetComponentInChildren<WeaponCollderHandle>(true).SetCollider(true);
        }
        protected override void OnMovtionEnd(object sender, AnimationEventArgs e)
        {
            base.OnMovtionEnd(sender, e);
            //禁用碰撞体
            currentWeaponGO.GetComponentInChildren<WeaponCollderHandle>(true).SetCollider(false);
        }
    }
}
