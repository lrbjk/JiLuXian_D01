using Common.Helper;
using Common.UI;
using ns.Character.Player;
using ns.Movtion;
using ns.Value;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ns.Character
{
    public enum CharacterPropertyType
    {
        活力,
        耐力,
        精力,
        力量,
        敏捷,
        智慧,
        感知
    }

    public enum ResistanceType
    {
        普通,
        打击,
        斩击,
        突刺,
        热能,
        电磁,
        共振,
    }

    public enum AbnormalType
    {
        无,
        出血,
        中毒,
        恐慌
    }
    [Serializable]
    public class CharacterProperty
    {
        public CharacterPropertyType propertyType;
        public int value;
    }

    [Serializable]
    public class CharacterResistanceProperty
    {
        public ResistanceType propertyType;
        public int value;
    }
    [Serializable]
    public class CharacterAbnormalResistanceProperty
    {
        public AbnormalType propertyType;
        public int value;
    }

    [RequireComponent(typeof(CharacterMovtionManager))]
    /// <summary>
    /// 描述：角色信息基类
    /// </summary>
    public abstract class CharacterInfo : MonoBehaviour, IDamage
    {
        /*
        角色信息
        ------------------------
        角色参数ID
        当前血量
         */
        /// <summary>角色参数ID </summary>
        public int CharacterParamID;

        private CharacterParam characterParam;
        public CharacterParam CharacterParam
        {
            get
            {
                if (characterParam == null)
                    characterParam = CharacterParamManager.GetCharacterParam(CharacterParamID);
                return characterParam;
            }
        }

        public CharacterMovtionManager MovtionManager;
        /// <summary>当前血量 </summary>
        public int HP;
        [Tooltip("基础韧性上限")]
        /// <summary>基础韧性上限 </summary>
        public int BasePoiseCeling;
        [Tooltip("最大基础韧性上限")]
        public int MaxPoiseCeling;
        [Tooltip("当前韧性值")]
        /// <summary>当前韧性值 </summary>
        public int CurrentBasePoise;
        [Tooltip("霸体期间累积受到的削韧值")]
        /// <summary>累积受到的削韧值 </summary>
        public int AccumulativePoiseDamage;
        /// <summary>是否无敌 </summary>
        public bool IsInvincible = false;

        [Header("转换值相关")]
        [SerializeField]
        private int transitionValue;
        [SerializeField]
        private CriticalStateType currentCriticalStateType;
        /// <summary>切换临界状态前事件 </summary>
        public event Action<CriticalStateType> BeforeChangeCriticalState;           //回调参数均为切换后的状态
        /// <summary>切换临界状态后事件 </summary>
        public event Action<CriticalStateType> AfterChangeCriticalState;
        /// <summary>转换值 </summary>
        public int TransitionValue { get => transitionValue; }
        /// <summary>当前角色临界状态 </summary>
        public CriticalStateType CurrentCriticalStateType { get => currentCriticalStateType; protected set => currentCriticalStateType = value; }
        public void UpdateTransitionValue(int delta)
        {
            FlushTransitionUI(delta);
            transitionValue += delta;
            int ceil = GetTransitionCeil();
            transitionValue = Math.Clamp(transitionValue, 0, ceil);
            //临界阶段处理
            List<CriticalStateValuePairs> switchCriticalPoint = GetTransitionCriticalPoint();
            CriticalStateType? changeToType = null;
            for (int i = 0; i < switchCriticalPoint.Count; i++)
            {
                CriticalStateValuePairs pair = switchCriticalPoint[i];
                if (TransitionValue < pair.value)
                {
                    changeToType = pair.type;
                    break;
                }
            }
            if (changeToType.Value != CurrentCriticalStateType)
            {
                BeforeChangeCriticalState?.Invoke(changeToType.Value);
                CurrentCriticalStateType = changeToType.Value;
                //切换状态、触发事件
                AfterChangeCriticalState?.Invoke(CurrentCriticalStateType);
            }
        }
        public int GetTransitionCeil()
        {
            return GetTransitionCriticalPoint()[^1].value;
        }
        public virtual List<CriticalStateValuePairs> GetTransitionCriticalPoint()
        {
            return new List<CriticalStateValuePairs>(3) { new CriticalStateValuePairs() { type=CriticalStateType.空虚,value=30},
            new CriticalStateValuePairs(){ type=CriticalStateType.稳定,value=70 },
            new CriticalStateValuePairs(){type=CriticalStateType.充盈,value=100} };
        }
        protected virtual void FlushTransitionUI(int delta) { }

        /// <summary>角色被他人锁定的Transform </summary>
        public Transform LockedTF;
        /// <summary>角色被他人背刺时他人站立的Transform </summary>
        public Transform BackStabedStandingTF;
        /// <summary>角色被他人正刺时他人站立的Transform </summary>
        public Transform ForwardStabedStandingTF;
        [HideInInspector]
        /// <summary>角色锁定的他人Transform </summary>
        public Transform LockedTargetTF;
        /// <summary>角色背刺他人目标角色信息 </summary>
        [HideInInspector]
        public CharacterInfo BackStabedTarget;


        //为动作状态机提供的成员
        [Tooltip("是否处于前摇阶段")]
        public bool IsInPreMovtionFlag = false;
        [Tooltip("是否处于后摇阶段")]
        public bool IsInMovtionRecoveryFlag = false;
        [Tooltip("是否处于霸体阶段")]
        public bool IsInArmorFlag = false;
        [Tooltip("是否处于虚弱状态")]
        public bool IsInWeadFlag = false;
        public int CurrentMovtionID = 0;
        public int ComboMovtionlID = 0;
        public bool IsDamaged = false;
        public bool IsDied = false;

        protected virtual void Start()
        {
            MovtionManager = GetComponent<CharacterMovtionManager>();
            LockedTF = transform.Find("LockTF");
            BackStabedStandingTF = transform.FindChildByName("BackStabStandingPoint");
            ForwardStabedStandingTF = transform.FindChildByName("ForwardStabStandingPoint");
        }


        /// <summary>
        /// 获取武器基础物理攻击力
        /// </summary>
        /// <returns></returns>
        public abstract float GetWeaponPhysicalATK();

        /// <summary>
        /// 获取武器基础属性攻击力
        /// </summary>
        /// <returns></returns>
        public abstract float GetWeaponSpecialResistanceAtk(ResistanceType resistanceType);
        /// <summary>
        /// 获取武器拥有的属性伤害类别
        /// </summary>
        /// <returns></returns>
        public abstract ResistanceType[] GetWeaponAllSpecialResistanceTypes();

        /// <summary>
        /// 获取武器处决系数
        /// </summary>
        /// <returns></returns>
        public abstract float GetWeaponExecutionCoefficient();

        /// <summary>
        /// 获取防御力
        /// </summary>
        /// <returns></returns>
        public abstract int GetDEF();
        /// <summary>
        /// 获取抗性
        /// </summary>
        /// <param name="resistanceType"></param>
        /// <returns></returns>
        public abstract int GetResistance(ResistanceType resistanceType);
        /// <summary>
        /// 获取临界状态系数，现阶段默认为1
        /// </summary>
        /// <returns></returns>
        public virtual float GetCriticalStateEffectCoefficient()
        {
            return 1f;
        }
        /// <summary>
        /// 获取基础削韧值
        /// </summary>
        /// <returns></returns>
        public abstract float GetBaseReducedPoise();
        /// <summary>
        /// 获取基础动作韧性值
        /// </summary>
        /// <returns></returns>
        public abstract float GetBaseMovtionPoise();
        /// <summary>
        /// 受伤接口
        /// </summary>
        /// <param name="damageContext"></param>
        public virtual void TakeDamage(DamageContext damageContext)
        {
            //是否无敌
            if (IsInvincible) return;
            //攻击成功回调
            damageContext.AttackSucceed?.Invoke(this);
            MovtionInfo atkMovtionInfo = damageContext.AttackerInfo.MovtionManager.GetMovtionInfo(damageContext.AttackerInfo.CurrentMovtionID);
            //计算伤害
            int damageValue = DamageCalculator.CalculateDamage(damageContext.AttackerInfo, this);
            Debug.Log("攻击方伤害" + damageValue);
            //血量UI 刷新
            FlushHPUI(damageValue);
            //血量扣除
            HP -= damageValue;
            //转换值处理
            DamagedTransitionHandle();
            //是否死亡
            if (HP <= 0)
            {
                //死亡状态
                Debug.Log("死亡");
                IsDied = true;
                CurrentMovtionID = atkMovtionInfo.DeadMovtionID;
                return;
            }
            //计算攻击方削韧值
            int poiseDamageValue = DamageCalculator.CalculatePoiseDamage(damageContext.AttackerInfo);
            Debug.Log("攻击方削韧" + poiseDamageValue);
            //基础韧性扣除
            CurrentBasePoise -= poiseDamageValue;
            //是否虚弱判定
            if (!IsInWeadFlag && CurrentBasePoise <= 0)
            {
                BasePoiseCeling =
                    Mathf.FloorToInt(BasePoiseCeling * GlobalConstants.FrailtyPoiseAmplificationFactor);//重置基础韧性上限
                Debug.Log("虚弱状态");
                IsInWeadFlag = true;
                //虚弱动画
                //何时恢复基础韧性CurrentBasePoise取决于敌人逻辑
                Invoke(nameof(RecoverCurrentBasePoise), 1f);                //目前暂时直接1秒后恢复
                return;
            }

            //计算受击方当前动作的动作韧性值
            int movtionPoise = DamageCalculator.CalculateMovtionPoise(this);
            //是否处于霸体帧
            //是否打断动作
            if (IsInArmorFlag)
            {
                //累积削韧值
                AccumulativePoiseDamage += poiseDamageValue;
                //累积削韧值>动作韧性
                if (AccumulativePoiseDamage >= movtionPoise)
                {
                    AccumulativePoiseDamage = 0;//重置累积
                    Debug.Log("累积削韧值>=动作韧性，打断");
                    //受击动画
                    DamagedStateHandle(damageContext, atkMovtionInfo);
                }
                return;
            }
            else
            {
                AccumulativePoiseDamage = 0;//重置累积
            }
            //直接比较
            if (poiseDamageValue >= movtionPoise)
            {
                Debug.Log("直接削韧值>=动作韧性，打断");
                //受击状态
                DamagedStateHandle(damageContext, atkMovtionInfo);
                return;
            }
        }

        private void DamagedStateHandle(DamageContext damageContext, MovtionInfo atkMovtionInfo)
        {
            IsDamaged = true;
            //判断是哪面命中
            float dotProduct = Vector3.Dot(damageContext.AttackerInfo.transform.forward, transform.forward);
            if (dotProduct <= 0)//在前方
                CurrentMovtionID = atkMovtionInfo.FrontDamagedMovtionID;
            else
                CurrentMovtionID = atkMovtionInfo.BackDamagedMovtionID;
            return;
        }
        private void RecoverCurrentBasePoise()
        {
            Debug.Log("恢复韧性值");
            if (BasePoiseCeling < MaxPoiseCeling)
                BasePoiseCeling = CurrentBasePoise = Mathf.CeilToInt(BasePoiseCeling * 1.1f);
            else
                BasePoiseCeling = CurrentBasePoise = MaxPoiseCeling;
        }
        /// <summary>
        /// 受击转换值处理
        /// </summary>
        protected virtual void DamagedTransitionHandle() { }
        protected abstract void FlushHPUI(int damageValue);
    }
}
