using UnityEngine;

namespace ns.Character
{
    /// <summary>
    /// 描述：角色信息基类
    /// </summary>
    public class CharacterInfo : MonoBehaviour
    {
        /*
        角色信息
        ------------------------
        角色参数ID
        当前血量
        最大血量加成
        普通攻击伤害加成值
        受到伤害增加值
        速度变化比例
        buff持续时间变化值
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

        /// <summary>当前血量 </summary>
        public int HP;
        /// <summary>最大血量加成 </summary>
        public int MaxHPDelta;
        /// <summary>计算变化后的最大血量</summary>
        public int MaxHPAftDelta { get { return MaxHPDelta + CharacterParam.BaseMaxHP; } }
        /// <summary>普通攻击伤害加成值 </summary>
        public int ExSimpleATK;
        /// <summary>受到伤害增加值 </summary>
        public int ExDamaged;
        /// <summary>速度变化比例</summary>
        public float SpeedRatio;
        /// <summary>buff持续时间变化值 </summary>
        public float BuffTimeDelta;

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
