using ns.Character;
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
            throw new System.NotImplementedException();
        }

        public override float GetBaseReducedPoise()
        {
            throw new System.NotImplementedException();
        }

        public override int GetDEF()
        {
            throw new System.NotImplementedException();
        }

        public override int GetResistance(ResistanceType resistanceType)
        {
            throw new System.NotImplementedException();
        }

        public override float GetWeaponExecutionCoefficient()
        {
            throw new System.NotImplementedException();
        }

        public override float GetWeaponPhysicalATK()
        {
            throw new System.NotImplementedException();
        }
    }
}