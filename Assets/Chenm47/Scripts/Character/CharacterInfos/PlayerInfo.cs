using ns.Item.Equipment;
using ns.Item.Weapons;
using ns.Movtion;
using ns.Value;
using System.Collections.Generic;
using UnityEngine;

namespace ns.Character.Player
{
    /// <summary>
    /// 描述：玩家角色信息
    /// </summary>
    public class PlayerInfo : CharacterInfo
    {
        /*为调节使用*/
        /// <summary>最大心情值</summary>
        [Header("最大心情值")]
        public int MaxMoodValue = 10;
        /// <summary>
        /// 角色基础移动速度
        /// </summary>
        [Header("角色基础移动速度")]
        public float MoveBaseSpeed = 3;
        [Header("角色快速移动速度")]
        public float SprintSpeed = 6;
        [Header("翻滚速度")]
        public float RollSpeed = 10;
        public float BackStepSpeed = 6;
        public float JumpSpeed = 18;

        [Header("下落平台恢复时间")]
        public float DownStairRecoverTime = 0.25f;
        public int MaxJumpCount = 2;
        [Header("最大锁定距离")]
        public float MaxLockDistance = 2f;
        public LayerMask EnemyLayer;
        [Header("玩家动作信息ID")]
        [Tooltip("玩家角色的翻滚动作信息ID")]
        public int RollMovtionID;
        public int BackStepMovtionID;
        public int JumpMovtionID;

        /*为状态机以及动画事件提供*/
        [HideInInspector]
        public PlayerAnimaParams AnimaParams = new PlayerAnimaParams();
        [HideInInspector]
        public int CurrentJumpCount = 0;
        [HideInInspector]
        public bool IsDownStair = false;
        public AttackInputType LastAttackType = AttackInputType.None;
        public float FallTimer;
        public bool IsOnTop = false;
        public bool IsOnGround;
        public RaycastHit GroundHit;

        [Header("角色属性值")]
        /// <summary>角色属性值 </summary>
        public List<CharacterProperty> CharacterProperties;
        [Header("角色抗性表")]
        /// <summary>角色抗性表 </summary>
        public List<CharacterResistanceProperty> CharacterResistanceProperties;
        [Header("角色异常抗性表")]
        /// <summary>角色异常抗性表 </summary>
        public List<CharacterAbnormalResistanceProperty> AbnormalResistanceProperties;
        /// <summary>转换值 </summary>
        public int TransitionValue;
        /// <summary>当前角色临界状态 </summary>
        public CriticalStateType CurrentCriticalStateType;


        private CharacterEquipmentManager equipmentManager;
        protected override void Start()
        {
            base.Start();
            equipmentManager = GetComponent<CharacterEquipmentManager>();
        }

        public override int GetDEF()
        {
            int res = 0;
            foreach (var equipmentInfo in equipmentManager.GetEquipmentInfos())
            {
                res += equipmentInfo.DEF;
            }
            return res;
        }
        public override int GetResistance(ResistanceType resistanceType)
        {
            return CharacterResistanceProperties.Find(p => p.propertyType == resistanceType).value;
        }
        public override float GetCriticalStateEffectCoefficient()
        {
            //该数值会根据玩家装备的“核心”而有所不同
            return equipmentManager.GetKernelInfo().GetCriticalStateEffectCoefficient(CurrentCriticalStateType);
        }

        public override float GetWeaponPhysicalATK()
        {
            Weapon wp = equipmentManager.GetCurrentAtkWeapon();
            wp.GetFinalPhysicalATK();
            return wp.GetSpecialATK(ResistanceType.普通);
        }
        public override float GetWeaponSpecialResistanceAtk(ResistanceType resistanceType)
        {
            return equipmentManager.GetCurrentAtkWeapon().GetSpecialATK(resistanceType);
        }
        public override ResistanceType[] GetWeaponAllSpecialResistanceTypes()
        {
            return equipmentManager.GetCurrentAtkWeapon().GetAllSpecialResistanceTypes();
        }
        public override float GetWeaponExecutionCoefficient()
        {
            //获取武器的处决系数
            return equipmentManager.GetCurrentAtkWeapon().WInfo.WeaponValue.ExecutionCoefficient;
        }

        public override float GetBaseReducedPoise()
        {
            //削韧值=武器削韧值*动作倍率
            MovtionInfo movtionInfo = MovtionManager.GetMovtionInfo(CurrentMovtionID);
            return equipmentManager.GetCurrentAtkWeapon().WInfo.WeaponValue.ReducedPoise * movtionInfo.ActionMultiplier;
        }
        public override float GetBaseMovtionPoise()
        {
            //动作韧性 = 削韧值
            return DamageCalculator.CalculatePoiseDamage(this);
        }

    }
}
