using AI.FSM.Framework;
using Common;
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
        [HideInInspector]
        public PlayerInfo playerInfo;
        [HideInInspector]
        public PlayerMotor3D playerMotor3D;
        [HideInInspector]
        public PlayerIK playerIK;
        [HideInInspector]
        public GameObject BulletPrefab;
        [HideInInspector]
        public Transform BulletCreatPos;
        #endregion

        public static PlayerFSMBase Instance { get; private set; }
        private void Awake()
        {
            Instance = this;
            playerInput = GetComponent<PlayerInput>();
        }

        protected override void Start()
        {
            //获取组件
            //playerInput = GetComponent<PlayerInput>();
            playerAction = GetComponent<PlayerAction>();
            cameraHandler = FindAnyObjectByType<CameraHandler>();
            playerMotor3D = GetComponentInChildren<PlayerMotor3D>();
            playerIK = GetComponentInChildren<PlayerIK>();
            BulletPrefab = ResourceManager.Load<GameObject>("PlayerBullet");
            BulletCreatPos = transform.Find("BulletCreatePos");
            base.Start();
            playerInfo = characterInfo as PlayerInfo;
        }
    }
}
