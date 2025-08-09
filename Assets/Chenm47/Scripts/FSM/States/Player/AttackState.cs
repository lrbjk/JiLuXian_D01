using AI.FSM.Framework;
using Common;
using ns.Character.Player;
using ns.ItemInfos;
using ns.Movtion;
using ns.Weapons;
using UnityEngine;

namespace AI.FSM
{
    /// <summary>
    /// 描述：
    /// </summary>
    public class AttackState : MovtionState
    {
        private GameObject currentWeaponGO;
        protected PlayerFSMBase playerFSM;

        public override void Init()
        {
            StateID = FSMStateID.Attack;
        }
        protected override MovtionInfo InitMovtionInfo(FSMBase fSMBase)
        {
            playerFSM = fSMBase as PlayerFSMBase;
            //获取当前武器信息
            WeaponInfo currentWeponInfo = fSMBase.equipmentManager.GetCurrentAtkWeapon().WInfo;
            //左手？右手？
            bool isLeft = playerFSM.playerInput.IsLeftAttackTrigger;
            PlayerEquipmentManager em = playerFSM.equipmentManager as PlayerEquipmentManager;
            var lweapon = em.GetHandingWeapon(true);
            var rweapon = em.GetHandingWeapon(false);
            currentWeaponGO = currentWeponInfo.ModleGO;
            //获取技能信息
            MovtionInfo movtionInfo = GetMovtionInfo(isLeft, currentWeponInfo);
            return movtionInfo;
        }
        protected virtual MovtionInfo GetMovtionInfo(bool isLeft, WeaponInfo currentWeponInfo)
        {
            //根据手中的武器和输入来决定使用哪个动作
            int movtionID = 0;//动作ID
            if (playerFSM.playerInput.IsLightAttackTrigger)
            {
                if (isLeft)
                    movtionID = currentWeponInfo.LightAtkIDL;
                else
                    movtionID = currentWeponInfo.LightAtkIDR;
            }
            else if (playerFSM.playerInput.IsHeavyAttackTrigger)
            {
                if (isLeft)
                    movtionID = currentWeponInfo.HeavyAtkIDL;
                else
                    movtionID = currentWeponInfo.HeavyAtkIDR;
            }
            else if (playerFSM.playerInput.IsSkillAttackTrigger)
            {
                movtionID = currentWeponInfo.SkillAtkIDL;
            }
            var skillInfo = playerFSM.movtionManager.GetMovtionInfo(movtionID);
            return skillInfo;
        }

        public override void EnterState(FSMBase fSMBase)
        {
            base.EnterState(fSMBase);
            #region 移动控制
            //停止移动
            playerFSM.playerAction.StopMove();
            playerFSM.playerRootMotion.ApplyAnimaMotionAll = true;
            //启用根运动
            PlayerInfo playerInfo = playerFSM.characterInfo as PlayerInfo;
            #endregion
            //更新玩家信息技能ID
            playerInfo.LastAttackType = playerFSM.playerInput.AtkInputType;
            playerInfo.CurrentMovtionID = movtionInfo.MovtionID;
            playerInfo.ComboMovtionlID = movtionInfo.ComboMovtionID;
        }

        public override void ActionState(FSMBase fSMBase)
        {
            base.ActionState(fSMBase);
            //前摇结束前可以转动
            if (fSMBase.characterInfo.IsInPreMovtionFlag)
                HandleRotate();
        }

        private void HandleRotate()
        {
            if (!playerFSM.playerInput.LockViewTrigger)
            {
                float moveX = playerFSM.playerInput.HorizontalMove;
                float moveY = playerFSM.playerInput.VerticalMove;

                Vector3 moveDir = playerFSM.cameraHandler.transform.right * moveX +
                    playerFSM.cameraHandler.transform.forward * moveY;
                moveDir.y = 0;
                moveDir.Normalize();
                //Debug.Log(movtionInfo.PreMovtionRotateSpeed);
                playerFSM.playerAction.LookDir(moveDir, movtionInfo.PreMovtionRotateSpeed);
            }
            else//锁定视角
            {
                Vector3 lookDir =
                   playerFSM.characterInfo.LockedTargetTF.position - playerFSM.characterInfo.LockedTF.position;
                lookDir.Set(lookDir.x, 0, lookDir.z);
                playerFSM.playerAction.LookDir(lookDir, movtionInfo.PreMovtionRotateSpeed);
            }
        }

        public override void ExitState(FSMBase fSMBase)
        {
            base.ExitState(fSMBase);
            //清空临时变量
            //以防万一禁用碰撞体
            currentWeaponGO.GetComponentInChildren<WeaponCollderHandle>(true).SetCollider(false);
            currentWeaponGO = null;
            playerFSM.playerRootMotion.ApplyAnimaMotionAll = false;
        }

        protected override void OnPreMovtionEnd(object sender, AnimationEventArgs e)
        {
            base.OnPreMovtionEnd(sender, e);
            //此时无法转向

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
