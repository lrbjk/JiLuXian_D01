using AI.FSM.Framework;
using ns.Item.Weapons;
using ns.Skill;
using UnityEngine;

namespace AI.FSM
{
    /// <summary>
    /// 描述：
    /// </summary>
    public class AttackState : FSMState
    {
        private string animationName;
        private GameObject currentWeaponGO;
        protected PlayerFSMBase playerFSMBase;

        public override void Init()
        {
            StateID = FSMStateID.Attack;
        }

        public override void EnterState(FSMBase fSMBase)
        {
            base.EnterState(fSMBase);
            playerFSMBase = fSMBase as PlayerFSMBase;
            //停止移动
            playerFSMBase.playerAction.StopMove();
            //获取当前武器信息
            //左手？右手？
            bool isLeft = playerFSMBase.playerInput.IsLeftAttackTrigger;
            var lweapon = playerFSMBase.playerInventory.LeftWeapon;
            var rweapon = playerFSMBase.playerInventory.RightWeapon;
            WeaponInfo currentWeponInfo = isLeft ? lweapon : rweapon;
            currentWeaponGO = currentWeponInfo.ModleGO;

            //获取技能信息
            SkillInfo skillInfo = GetSkillInfo(isLeft, currentWeponInfo);
            //更新玩家信息技能ID
            playerFSMBase.playerInfo.LastAttackType = playerFSMBase.playerInput.AtkInputType;
            playerFSMBase.playerInfo.CurrentSkillID = skillInfo.SkillID;
            playerFSMBase.playerInfo.ComboSkillID = skillInfo.ComboSkillID;

            //订阅事件
            playerFSMBase.animationEventBehaviour.OnPreAttackEnd += OnPreAttackEnd;
            playerFSMBase.animationEventBehaviour.OnAttackStart += OnAttackStart;
            playerFSMBase.animationEventBehaviour.OnAttackEnd += OnAttackEnd;
            playerFSMBase.animationEventBehaviour.OnAttackRecovery += OnAttackRecovery;

            //播放相应动画
            animationName = skillInfo.AnimationName;
            Debug.Log("技能名称：" + skillInfo.SkillName + "播放动画状态：" + animationName);
            playerFSMBase.playerAnimationHandler.PlayTargetAnimation(animationName, true);
        }

        protected virtual SkillInfo GetSkillInfo(bool isLeft, WeaponInfo currentWeponInfo)
        {
            //根据手中的武器和输入来决定使用哪个技能
            int skillID = 0;//技能ID
            if (playerFSMBase.playerInput.IsLightAttackTrigger)
            {
                if (isLeft)
                    skillID = currentWeponInfo.LightAtkIDL;
                else
                    skillID = currentWeponInfo.LightAtkIDR;
            }
            else if (playerFSMBase.playerInput.IsHeavyAttackTrigger)
            {
                if (isLeft)
                    skillID = currentWeponInfo.HeavyAtkIDL;
                else
                    skillID = currentWeponInfo.HeavyAtkIDR;
            }
            else if (playerFSMBase.playerInput.IsSkillAttackTrigger)
            {
                skillID = currentWeponInfo.SkillAtkIDL;
            }
            var skillInfo = playerFSMBase.characterSkillManager.GetSkillInfo(skillID);
            return skillInfo;
        }

        public override void ExitState(FSMBase fSMBase)
        {
            base.ExitState(fSMBase);
            //取消订阅
            playerFSMBase.animationEventBehaviour.OnPreAttackEnd -= OnPreAttackEnd;
            playerFSMBase.animationEventBehaviour.OnAttackStart -= OnAttackStart;
            playerFSMBase.animationEventBehaviour.OnAttackEnd -= OnAttackEnd;
            playerFSMBase.animationEventBehaviour.OnAttackRecovery -= OnAttackRecovery;
            Debug.Log("取消订阅");
            //清空临时变量
            currentWeaponGO = null;
            animationName = null;
            //后摇结束
            playerFSMBase.playerInfo.IsInAttackRecoveryFlag = false;
        }

        private void OnPreAttackEnd(object sender, Common.AnimationEventArgs e)
        {
            Debug.Log("动画事件PreAttackEnd");
        }

        private void OnAttackStart(object sender, Common.AnimationEventArgs e)
        {
            Debug.Log("动画事件AttackStart");
            //激活碰撞体
            currentWeaponGO.GetComponentInChildren<WeaponCollderHandle>(true).SetCollider(true);
        }
        private void OnAttackEnd(object sender, Common.AnimationEventArgs e)
        {
            Debug.Log("动画事件AttackEnd");
            //禁用碰撞体
            currentWeaponGO.GetComponentInChildren<WeaponCollderHandle>(true).SetCollider(false);
        }
        private void OnAttackRecovery(object sender, Common.AnimationEventArgs e)
        {
            Debug.Log("动画事件AttackRecovery");
            //后摇开始
            playerFSMBase.playerInfo.IsInAttackRecoveryFlag = true;
        }
    }
}
