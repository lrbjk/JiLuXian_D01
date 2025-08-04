using ns.Character;
using ns.Value;
using UnityEngine;
using CharacterInfo = ns.Character.CharacterInfo;

namespace EnemyAIBase
{
    public class EnemyInfo : CharacterInfo
    {
        [Header("Enemy Basic Info")]
        public int idk = 10;

        [Header("Enemy Movement")]
        [Tooltip("敌人移动速度")]
        public float MoveSpeed = 1f;

        [Tooltip("敌人旋转速度")]
        public float RotationSpeed = 120f;

        [Header("Enemy Combat")]
        [Tooltip("攻击动作ID")]
        public int AttackMovtionID = 1001;

        [Tooltip("受击动作ID")]
        public int HitReactionMovtionID = 1002;

        [Tooltip("死亡动作ID")]
        public int DeathMovtionID = 1003;

        [Header("Enemy Stats")]
        [Tooltip("基础攻击力")]
        public float BaseAttackPower = 20f;

        [Tooltip("基础防御力")]
        public int BaseDefense = 5;

        [Tooltip("基础削韧值")]
        public float BaseReducedPoiseValue = 15f;

        [Tooltip("基础动作韧性")]
        public float BaseMovtionPoiseValue = 20f;

        [Header("Enemy AI")]
        [Tooltip("视野范围")]
        public float SightRange = 10f;

        [Tooltip("攻击范围")]
        public float AttackRange = 2f;

        [Tooltip("巡逻半径")]
        public float PatrolRadius = 5f;

        [Tooltip("追击速度倍数")]
        public float ChaseSpeedMultiplier = 1.5f;

        public override float GetBaseMovtionPoise()
        {
            return BaseMovtionPoiseValue;
        }

        public override float GetBaseReducedPoise()
        {
            // 敌人的削韧值直接使用配置值，不需要考虑武器
            return BaseReducedPoiseValue;
        }

        public override int GetDEF()
        {
            return BaseDefense;
        }

        public override int GetResistance(ResistanceType resistanceType)
        {
            // 敌人对所有伤害类型都有基础抗性，可以根据需要调整
            return 5;
        }

        public override ResistanceType[] GetWeaponAllSpecialResistanceTypes()
        {
            throw new System.NotImplementedException();
        }

        public override float GetWeaponExecutionCoefficient()
        {
            return 0f; // 敌人无法处决
        }

        public override float GetWeaponPhysicalATK()
        {
            // 敌人的攻击力直接使用配置值
            return BaseAttackPower;
        }

        /// <summary>
        /// 重写TakeDamage方法，添加敌人特有的受击逻辑
        /// </summary>
        /// <param name="damageContext"></param>
        public override void TakeDamage(DamageContext damageContext)
        {
            Debug.Log($"=== EnemyInfo.TakeDamage 开始 ===");
            Debug.Log($"受击前状态 - HP: {HP}, IsDamaged: {IsDamaged}, IsInvincible: {IsInvincible}, IsInArmorFlag: {IsInArmorFlag}");

            // 检查是否能够受击
            if (IsInvincible)
            {
                Debug.Log("敌人处于无敌状态，忽略伤害");
                return;
            }

            if (IsDied)
            {
                Debug.Log("敌人已死亡，忽略伤害");
                return;
            }

         
            int damageValue = DamageCalculator.CalculateDamage(damageContext.AttackerInfo, this);
            Debug.Log($"攻击方伤害: {damageValue}");

            // 血量扣除
            HP -= damageValue;
            if (HP < 0) HP = 0; // 确保血量不为负数

            Debug.Log($"受击后状态 - HP: {HP}");

            // 判断是否死亡
            if (HP <= 0 && !IsDied)
            {
                // 设置死亡动作ID
                CurrentMovtionID = DeathMovtionID;
                // 标记死亡状态
                IsDied = true;

                Debug.Log($"敌人死亡: HP={HP}, 切换到死亡动作ID={DeathMovtionID}");
            }
            else if (HP > 0)
            {
                // 设置受击动作ID
                CurrentMovtionID = HitReactionMovtionID;
                // 标记受击状态，触发状态机转换
                IsDamaged = true;

                Debug.Log($"敌人受击: HP={HP}, 切换到受击动作ID={HitReactionMovtionID}");
            }

            Debug.Log($"=== EnemyInfo.TakeDamage 结束 ===");
        }

        public override float GetWeaponSpecialResistanceAtk(ResistanceType resistanceType)
        {
            throw new System.NotImplementedException();
        }
    }
}