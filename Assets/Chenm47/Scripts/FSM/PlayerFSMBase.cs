using AI.FSM.Framework;
using ns.Camera;
using ns.Character.Player;
using UnityEngine;

namespace AI.FSM
{
    [RequireComponent(typeof(PlayerInput))]
    [RequireComponent(typeof(PlayerInfo))]
    [RequireComponent(typeof(PlayerAction))]
    [RequireComponent(typeof(PlayerMotor3D))]
    [RequireComponent(typeof(PlayerAnimationHandler))]
    [RequireComponent(typeof(Rigidbody))]
    /// <summary>
    /// 描述：
    /// </summary>
    public class PlayerFSMBase : FSMBase
    {
        #region 为状态类提供的成员
        [HideInInspector]
        public PlayerInfo playerInfo;
        [HideInInspector]
        public PlayerInput playerInput;
        [HideInInspector]
        public PlayerAction playerAction;
        [HideInInspector]
        public Animator animator;
        [HideInInspector]
        public CameraHandler cameraHandler;
        [HideInInspector]
        public PlayerAnimationHandler playerAnimationHandler;
        #endregion

        protected override void Start()
        {
            base.Start();
            //获取组件
            playerInfo = GetComponent<PlayerInfo>();
            playerInput = GetComponent<PlayerInput>();
            playerAction = GetComponent<PlayerAction>();
            animator = GetComponentInChildren<Animator>(true);
            cameraHandler = FindAnyObjectByType<CameraHandler>();
            playerAnimationHandler = GetComponent<PlayerAnimationHandler>();
        }
    }
}
