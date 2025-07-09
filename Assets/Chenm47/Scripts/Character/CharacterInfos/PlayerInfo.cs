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
        public float GroundDistance = 0.05f;
        [Header("下落平台恢复时间")]
        public float DownStairRecoverTime = 0.25f;
        public LayerMask GroundLayer;
        public int MaxJumpCount = 2;
        [Header("最大锁定距离")]
        public float MaxLockDistance = 2f;
        public LayerMask EnemyLayer;

        /*为状态机以及动画事件提供*/
        [HideInInspector]
        public PlayerAnimaParams AnimaParams = new PlayerAnimaParams();
        [HideInInspector]
        public int CurrentJumpCount = 0;
        [HideInInspector]
        public bool IsDownStair = false;
    }
}
