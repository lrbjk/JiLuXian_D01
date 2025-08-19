using Common.Helper;
using Common.UI;
using ns.Movtion;
using ns.Value;
using ns.Weapons;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ns.Character.Player
{
    [Serializable]
    public class CriticalStateValuePairs
    {
        public CriticalStateType type;
        public int value;
    }
    [Serializable]
    public class CriticalStateValuePairsFloat
    {
        public CriticalStateType type;
        public float value;
    }

    public enum CriticalStateType
    {
        充盈,
        稳定,
        空虚
    }

    /// <summary>
    /// 描述：玩家角色信息
    /// </summary>
    public class PlayerInfo : CharacterInfo
    {
        /*为调节使用*/
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

        [Header("最大锁定距离")]
        public float MaxLockDistance = 2f;
        public LayerMask EnemyLayer;
        [Header("玩家动作信息ID")]
        [Tooltip("玩家角色的翻滚动作信息ID")]
        public int RollMovtionID;
        public int LockedRollMovtionID;
        public int BackStepMovtionID;
        public int JumpMovtionID;
        public int GunParryMovtionID;

        /*为状态机以及动画事件提供*/
        [HideInInspector]
        public PlayerAnimaParams AnimaParams = new PlayerAnimaParams();
        [HideInInspector]
        public bool IsDownStair = false;
        public AttackInputType LastAttackType = AttackInputType.None;
        public float FallTimer;
        public bool IsOnTop = false;
        public bool IsOnGround;
        public RaycastHit GroundHit;
        public Transform ClimbPosTF;
        public bool IsInUpClimbBox = false;
        public bool IsInDownClimbBox = false;
        public bool IsClimbLiftHandDown = true;

        [Header("角色属性值")]
        /// <summary>角色属性值 </summary>
        public List<CharacterProperty> CharacterProperties;
        [Header("角色抗性表")]
        /// <summary>角色抗性表 </summary>
        public List<CharacterResistanceProperty> CharacterResistanceProperties;
        [Header("角色异常抗性表")]
        /// <summary>角色异常抗性表 </summary>
        public List<CharacterAbnormalResistanceProperty> AbnormalResistanceProperties;

        public float BullteSpeed = 10f;

        private CharacterEquipmentManager equipmentManager;
        private MainUIFunc mainUIFunc;
        protected override void Start()
        {
            base.Start();
            equipmentManager = GetComponent<CharacterEquipmentManager>();
            mainUIFunc = UIManager.Instance.GetUILayerManager("MainUI") as MainUIFunc;
            //设置UI最大血量
            Debug.Log("HP" + HP);
            mainUIFunc.SetPlayerHp(HP);
            //设置转换值
            //设置转换值上下限
            var lst = GetTransitionCriticalPoint();
            mainUIFunc.SetCurrentEmotion(lst[0].value, lst[1].value, lst[2].value);
            //UpdateTransitionValue(Mathf.FloorToInt(GetTransitionCeil() * 0.5f) - TransitionValue);//UI 内部已默认初始化为50%
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
            //如果当前没有动作，直接为0
            if (CurrentMovtionID == 0)
                return 0f;
            //削韧值=武器削韧值*动作倍率
            MovtionInfo movtionInfo = MovtionManager.GetMovtionInfo(CurrentMovtionID);
            return equipmentManager.GetCurrentAtkWeapon().WInfo.WeaponValue.ReducedPoise * movtionInfo.ActionMultiplier;
        }
        public override float GetBaseMovtionPoise()
        {
            //动作韧性 = 削韧值
            return DamageCalculator.CalculatePoiseDamage(this);
        }

        public override List<CriticalStateValuePairs> GetTransitionCriticalPoint()
        {
            return equipmentManager.GetKernelInfo().SwitchCriticalPoint;
        }
        protected override void FlushTransitionUI(int delta)
        {
            base.FlushTransitionUI(delta);
            int maxTV = GetTransitionCeil();
            int amount = ValueHelper.SmoothDelta2Amount(TransitionValue, delta, 0, maxTV);
            if (delta < 0)
            {
                mainUIFunc.DecreaseEmotion(amount);
            }
            else
            {
                mainUIFunc.IncreaseEmotion(amount);
            }
        }

        public override void TakeDamage(DamageContext damageContext)
        {
            base.TakeDamage(damageContext);
        }
        protected override void DamagedTransitionHandle()
        {
            base.DamagedTransitionHandle();
            //受击转换值减少、目前从简处理-5%
            int delta = -Mathf.CeilToInt(GetTransitionCeil() * 0.05f);
            UpdateTransitionValue(delta);
        }
        protected override void FlushHPUI(int damageValue)
        {
            Debug.LogWarning("UI扣除" + Math.Min(HP, damageValue));
            mainUIFunc.DecreasePlayerHp(Math.Min(HP, damageValue));
        }

    }
}
