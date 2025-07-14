using AI.FSM.Framework;
using ns.Item.Weapons;
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

        public override void Init()
        {
            StateID = FSMStateID.Attack;
        }

        public override void EnterState(FSMBase fSMBase)
        {
            base.EnterState(fSMBase);
            var playerFSM = fSMBase as PlayerFSMBase;
            //停止移动
            playerFSM.playerAction.StopMove();
            //获取当前武器信息
            //左手？右手？
            bool isLeft = playerFSM.playerInput.IsLeftAttackTrigger;
            var lweapon = playerFSM.playerInventory.LeftWeapon;
            var rweapon = playerFSM.playerInventory.RightWeapon;
            WeaponInfo currentWeponInfo = isLeft ? lweapon : rweapon;

            //获取技能信息
            int skillID = 0;//技能ID
            if (playerFSM.playerInput.IsLightAttackTrigger)
            {
                if (isLeft)
                    skillID = currentWeponInfo.LightAtkIDL;
                else
                    skillID = currentWeponInfo.LightAtkIDR;
            }
            else if (playerFSM.playerInput.IsHeavyAttackTrigger)
            {
                if (isLeft)
                    skillID = currentWeponInfo.HeavyAtkIDL;
                else
                    skillID = currentWeponInfo.HeavyAtkIDR;
            }
            else if (playerFSM.playerInput.IsSkillAttackTrigger)
            {
                skillID = currentWeponInfo.SkillAtkIDL;
            }

            var skillInfo = playerFSM.characterSkillManager.GetSkillInfo(skillID);
            currentWeaponGO = currentWeponInfo.ModleGO;

            //订阅事件
            playerFSM.animationEventBehaviour.OnPreAttackEnd += OnPreAttackEnd;
            playerFSM.animationEventBehaviour.OnAttackStart += OnAttackStart;
            playerFSM.animationEventBehaviour.OnAttackEnd += OnAttackEnd;
            Debug.Log("订阅事件");
            //播放相应动画
            animationName = skillInfo.AnimationName;
            playerFSM.playerAnimationHandler.PlayTargetAnimation(animationName, true);
        }
        public override void ExitState(FSMBase fSMBase)
        {
            base.ExitState(fSMBase);
            var playerFSM = fSMBase as PlayerFSMBase;
            //取消订阅
            playerFSM.animationEventBehaviour.OnPreAttackEnd -= OnPreAttackEnd;
            playerFSM.animationEventBehaviour.OnAttackStart -= OnAttackStart;
            playerFSM.animationEventBehaviour.OnAttackEnd -= OnAttackEnd;
            Debug.Log("取消订阅");
            //清空临时变量
            currentWeaponGO = null;
            animationName = null;
        }

        private void OnAttackStart(object sender, Common.AnimationEventArgs e)
        {
            Debug.Log(animationName + "AttackStart");
            //激活碰撞体
            currentWeaponGO.GetComponentInChildren<WeaponCollderHandle>(true).SetCollider(true);
        }
        private void OnAttackEnd(object sender, Common.AnimationEventArgs e)
        {
            Debug.Log(animationName + "AttackEnd");
            //禁用碰撞体
            currentWeaponGO.GetComponentInChildren<WeaponCollderHandle>(true).SetCollider(false);
        }
        private void OnPreAttackEnd(object sender, Common.AnimationEventArgs e)
        {
            Debug.Log(animationName + "PreAttackEnd");
        }
    }
}
