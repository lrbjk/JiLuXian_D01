using AI.FSM.Framework;
using ns.Camera;
using ns.Character.Player;
using UnityEngine;
using CharacterInfo = ns.Character.CharacterInfo;

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
        public PlayerInput playerInput;
        [HideInInspector]
        public PlayerAction playerAction;
        [HideInInspector]
        public CameraHandler cameraHandler;
        [HideInInspector]
        public PlayerInventory playerInventory;
        #endregion

        protected override void Start()
        {
            //获取组件
            playerInput = GetComponent<PlayerInput>();
            playerAction = GetComponent<PlayerAction>();
            cameraHandler = FindAnyObjectByType<CameraHandler>();
            playerInventory = GetComponent<PlayerInventory>();
            base.Start();
        }
    }
}
