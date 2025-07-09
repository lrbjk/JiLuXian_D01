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

        /// <summary>角色被锁定Transform </summary>
        public Transform LockedTF;
        [HideInInspector]
        /// <summary>角色锁定的Transform </summary>
        public Transform LockedTargetTF;
    }
}
