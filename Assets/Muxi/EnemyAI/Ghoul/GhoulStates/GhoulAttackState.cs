using AI.FSM.Framework;
using Common;
using EnemyAIBase;
using ns.Movtion;
using UnityEngine;

namespace AI.FSM
{
    /// <summary>
    /// Ghoul攻击状态
    /// </summary>
    public class GhoulAttackState : MovtionState
    {
        protected GhoulFSMBase ghoulFsmBase;
        protected EnemyInfo enemyInfo;
        protected Ghoul ghoul;
        private bool attackFinished = false;
        private Transform target;
        
        // 位置保持
        private Vector3 attackPosition; // 攻击开始时的位置

        // 简化的基于时间的攻击控制
        private float attackStartTime;

        public override void Init()
        {
            StateID = FSMStateID.GhoulAttack;
        }

        protected override MovtionInfo InitMovtionInfo(FSMBase fSMBase)
        {
            ghoulFsmBase = fSMBase as GhoulFSMBase;
            this.enemyInfo = fSMBase.GetComponent<EnemyInfo>();  // 修复：给类字段赋值
            ghoul = fSMBase.GetComponent<Ghoul>();

            // 检查必要组件
            if (fSMBase.movtionManager == null)
            {
                Debug.LogError("GhoulAttackState: movtionManager为null！");
                return null;
            }

            if (this.enemyInfo == null)
            {
                Debug.LogError("GhoulAttackState: EnemyInfo组件未找到！");
                return null;
            }

            Debug.Log($"GhoulAttackState: 尝试获取AttackMovtionID: {this.enemyInfo.AttackMovtionID}");

            // 获取Ghoul的攻击动作信息
            var movtionInfo = fSMBase.movtionManager.GetMovtionInfo(this.enemyInfo.AttackMovtionID);

            if (movtionInfo == null)
            {
                Debug.LogError($"GhoulAttackState: 找不到AttackMovtionID {this.enemyInfo.AttackMovtionID} 对应的MovtionInfo！");
                return null;
            }

            Debug.Log($"GhoulAttackState: 成功获取MovtionInfo: {movtionInfo.name}");
            return movtionInfo;
        }

        public override void EnterState(FSMBase fSMBase)
        {
            base.EnterState(fSMBase);

            // 获取攻击目标
            target = ghoul.GetTarget();
            attackFinished = false;

            // 停止NavMeshAgent移动，避免攻击时继续靠近
            var navAgent = fSMBase.GetComponent<UnityEngine.AI.NavMeshAgent>();
            if (navAgent != null && navAgent.isActiveAndEnabled)
            {
                navAgent.ResetPath();
                navAgent.velocity = Vector3.zero;
            }

            // 面向目标
            if (target != null)
            {
                Vector3 directionToTarget = (target.position - fSMBase.transform.position).normalized;
                directionToTarget.y = 0;
                if (directionToTarget != Vector3.zero)
                {
                    fSMBase.transform.rotation = Quaternion.LookRotation(directionToTarget);
                }
            }

            // 更新攻击动作ID
            enemyInfo.CurrentMovtionID = movtionInfo.MovtionID;

            // 记录攻击开始位置
            attackPosition = fSMBase.transform.position;

            // 临时禁用Root Motion，防止攻击动画导致位移
            if (ghoulFsmBase?.animator != null)
            {
                ghoulFsmBase.animator.applyRootMotion = false;
                Debug.Log("[GhoulAttackState] 禁用Root Motion");
            }

            // 更新攻击时间（同步到所有相关触发器）
            TargetInAttackRangeTrigger.UpdateLastAttackTime();
            TooCloseToTargetTrigger.UpdateLastAttackTime();
            TargetInSightTrigger.UpdateLastAttackTime();

            Debug.Log($"Ghoul进入攻击状态，锁定位置: {attackPosition}");
        }

        // 动作事件处理方法（修复方法签名）
        public void OnPreMovtionEnd(object sender, Common.AnimationEventArgs args)
        {
            Debug.Log("Ghoul攻击前摇结束");
        }

        public void OnMovtionStart(object sender, Common.AnimationEventArgs args)
        {
            Debug.Log("Ghoul攻击开始");
            // 在这里可以启用攻击碰撞体或处理攻击逻辑
            PerformAttack();
        }

        public void OnMovtionEnd(object sender, Common.AnimationEventArgs args)
        {
            Debug.Log("Ghoul攻击结束");
            attackFinished = true;
        }

        public void OnMovtionRecovery(object sender, Common.AnimationEventArgs args)
        {
            Debug.Log("Ghoul攻击后摇开始");
            fSMBase.characterInfo.IsInMovtionRecoveryFlag = true;
        }

        private void PerformAttack()
        {
            if (target == null) return;

            // 检查攻击范围
            float attackRange = ghoul.attackRange;
            float distanceToTarget = Vector3.Distance(ghoulFsmBase.transform.position, target.position);

            if (distanceToTarget <= attackRange)
            {
                //// 执行攻击逻辑
                //Debug.Log($"Ghoul攻击目标，伤害值: {movtionInfo.MovtionAtkValue}");

                // 这里可以添加实际的伤害计算和应用
                // 例如：target.GetComponent<CharacterInfo>().TakeDamage(movtionInfo.MovtionAtkValue);
            }
        }

        public override void ActionState(FSMBase fSMBase)
        {
            base.ActionState(fSMBase);

            // 强制保持位置，防止Root Motion导致的移动
            MaintainPosition(fSMBase);

            // 更新Animator参数（攻击时停止移动）
            UpdateAnimatorParameters();

            // 在攻击过程中保持面向目标
            if (target != null && !fSMBase.characterInfo.IsInMovtionRecoveryFlag)
            {
                Vector3 directionToTarget = (target.position - fSMBase.transform.position).normalized;
                directionToTarget.y = 0;
                if (directionToTarget != Vector3.zero)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
                    fSMBase.transform.rotation = Quaternion.Slerp(
                        fSMBase.transform.rotation,
                        targetRotation,
                        Time.deltaTime * 5f
                    );
                }
            }

            // 添加调试信息检查攻击状态
            if (Time.frameCount % 60 == 0) // 每秒输出一次
            {
                Debug.Log($"[GhoulAttackState] AttackFinished: {attackFinished}, Distance: {(target ? Vector3.Distance(fSMBase.transform.position, target.position) : 0):F2}");
            }
        }

        private void UpdateAnimatorParameters()
        {
            if (ghoulFsmBase?.animator == null) return;

            // 攻击时停止移动参数
            ghoulFsmBase.animator.SetFloat("Speed", 0f);

            // 只有在参数存在时才设置
            if (HasAnimatorParameter("MoveX"))
            {
                ghoulFsmBase.animator.SetFloat("MoveX", 0f);
            }
            if (HasAnimatorParameter("MoveY"))
            {
                ghoulFsmBase.animator.SetFloat("MoveY", 0f);
            }
        }

        private bool HasAnimatorParameter(string paramName)
        {
            if (ghoulFsmBase?.animator == null) return false;

            foreach (AnimatorControllerParameter param in ghoulFsmBase.animator.parameters)
            {
                if (param.name == paramName)
                    return true;
            }
            return false;
        }

        public override void ExitState(FSMBase fSMBase)
        {
            base.ExitState(fSMBase);

            // 恢复Root Motion
            if (ghoulFsmBase?.animator != null)
            {
                ghoulFsmBase.animator.applyRootMotion = true;
                Debug.Log("[GhoulAttackState] 恢复Root Motion");
            }

            // 清理攻击状态
            attackFinished = false;
            target = null;

            Debug.Log("Ghoul退出攻击状态");
        }

        // 检查攻击是否完成的公共方法
        public bool IsAttackFinished()
        {
            return attackFinished;
        }

        // 强制保持攻击位置
        private void MaintainPosition(FSMBase fSMBase)
        {
            // 如果位置偏移太大，强制拉回到攻击位置
            float distanceFromAttackPos = Vector3.Distance(fSMBase.transform.position, attackPosition);

            if (distanceFromAttackPos > 0.5f) // 允许0.5米的偏移
            {
                // 强制设置位置
                fSMBase.transform.position = Vector3.Lerp(
                    fSMBase.transform.position,
                    attackPosition,
                    Time.deltaTime * 10f
                );

                // 同时更新NavMeshAgent位置
                var navAgent = fSMBase.GetComponent<UnityEngine.AI.NavMeshAgent>();
                if (navAgent != null && navAgent.isActiveAndEnabled)
                {
                    navAgent.nextPosition = fSMBase.transform.position;
                }

                Debug.Log($"[GhoulAttackState] 位置偏移过大({distanceFromAttackPos:F2}m)，强制回到攻击位置");
            }
        }
    }
}
//             bool isLeft = playerFSMBase.playerInput.IsLeftAttackTrigger;
//             var lweapon = playerFSMBase.playerInventory.LeftWeapon;
//             var rweapon = playerFSMBase.playerInventory.RightWeapon;
//             currentWeaponGO = currentWeponInfo.ModleGO;
//             //获取技能信息
//             MovtionInfo movtionInfo = GetMovtionInfo(isLeft, currentWeponInfo);
//             return movtionInfo;
//         }
//         protected virtual MovtionInfo GetMovtionInfo(bool isLeft, WeaponInfo currentWeponInfo)
//         {
//             //根据手中的武器和输入来决定使用哪个动作
//             int movtionID = 0;//动作ID
//             if (playerFSMBase.playerInput.IsLightAttackTrigger)
//             {
//                 if (isLeft)
//                     movtionID = currentWeponInfo.LightAtkIDL;
//                 else
//                     movtionID = currentWeponInfo.LightAtkIDR;
//             }
//             else if (playerFSMBase.playerInput.IsHeavyAttackTrigger)
//             {
//                 if (isLeft)
//                     movtionID = currentWeponInfo.HeavyAtkIDL;
//                 else
//                     movtionID = currentWeponInfo.HeavyAtkIDR;
//             }
//             else if (playerFSMBase.playerInput.IsSkillAttackTrigger)
//             {
//                 movtionID = currentWeponInfo.SkillAtkIDL;
//             }
//             var skillInfo = playerFSMBase.movtionManager.GetMovtionInfo(movtionID);
//             return skillInfo;
//         }
//
//         public override void EnterState(FSMBase fSMBase)
//         {
//             base.EnterState(fSMBase);
//             //停止移动
//             playerFSMBase.playerAction.StopMove();
//             PlayerInfo playerInfo = playerFSMBase.characterInfo as PlayerInfo;
//             //更新玩家信息技能ID
//             playerInfo.LastAttackType = playerFSMBase.playerInput.AtkInputType;
//             playerInfo.CurrentMovtionID = movtionInfo.MovtionID;
//             playerInfo.ComboMovtionlID = movtionInfo.ComboMovtionID;
//         }
//
//         public override void ExitState(FSMBase fSMBase)
//         {
//             base.ExitState(fSMBase);
//             //清空临时变量
//             //以防万一禁用碰撞体
//             currentWeaponGO.GetComponentInChildren<WeaponCollderHandle>(true).SetCollider(false);
//             currentWeaponGO = null;
//         }
//
//         protected override void OnMovtionStart(object sender, AnimationEventArgs e)
//         {
//             base.OnMovtionStart(sender, e);
//             //激活碰撞体
//             currentWeaponGO.GetComponentInChildren<WeaponCollderHandle>(true).SetCollider(true);
//         }
//         protected override void OnMovtionEnd(object sender, AnimationEventArgs e)
//         {
//             base.OnMovtionEnd(sender, e);
//             //禁用碰撞体
//             currentWeaponGO.GetComponentInChildren<WeaponCollderHandle>(true).SetCollider(false);
//         }
//     }
// }
