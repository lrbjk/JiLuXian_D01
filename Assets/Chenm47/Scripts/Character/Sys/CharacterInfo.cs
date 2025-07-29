using ns.Movtion;
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
        打击,
        斩击,
        突刺,
        魔力,
    }


    public class CharacterProperty
    {
        public CharacterPropertyType propertyType;
        public int value;
    }


    [RequireComponent(typeof(CharacterMovtionManager))]
    /// <summary>
    /// 描述：角色信息基类
    /// </summary>
    public abstract class CharacterInfo : MonoBehaviour
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

        private void Start()
        {
            MovtionManager = GetComponent<CharacterMovtionManager>();
        }

        /// <summary>当前血量 </summary>
        public int HP;
        /// <summary>基础韧性值 </summary>
        public int BasePoise;

        /// <summary>当前韧性值 </summary>
        public int Poise;

        /// <summary>
        /// 获取武器基础物理攻击力
        /// </summary>
        /// <returns></returns>
        public abstract float GetWeaponPhysicalATK();
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
        /// 获取临界状态系数
        /// </summary>
        /// <returns></returns>
        public virtual int GetCriticalStateEffectCoefficient()
        {
            return 1;
        }

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
        public int CurrentMovtionID = 0;
        public int ComboMovtionlID = 0;
        public bool IsDamaged = false;
        public bool IsDied = false;

    }
}
