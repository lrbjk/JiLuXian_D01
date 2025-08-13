using AI.FSM.Framework;
using ns.Camera;
using ns.Character.Player;
using UnityEngine;

namespace AI.FSM
{
    [RequireComponent(typeof(PlayerInput))]
    [RequireComponent(typeof(PlayerInfo))]
    [RequireComponent(typeof(PlayerAction))]
    [RequireComponent(typeof(PlayerAnimationHandler))]
    /// <summary>
    /// 描述：Awake后的单例
    /// </summary>
    public class PlayerFSMBase : FSMBase
    {
        #region 为状态类提供的成员
        [HideInInspector]
        public PlayerInput playerInput;
        [HideInInspector]
        public PlayerAction playerAction;
        [HideInInspector]
        public CameraHandler cameraHandler;
        public PlayerInfo playerInfo;
        public PlayerMotor3D playerMotor3D;
        #endregion

        public static PlayerFSMBase Instance { get; private set; }
        private void Awake()
        {
            Instance = this;
        }

        protected override void Start()
        {
            //获取组件
            playerInput = GetComponent<PlayerInput>();
            playerAction = GetComponent<PlayerAction>();
            cameraHandler = FindAnyObjectByType<CameraHandler>();
            playerMotor3D = GetComponentInChildren<PlayerMotor3D>();
            base.Start();
            playerInfo = characterInfo as PlayerInfo;
        }
    }
}
