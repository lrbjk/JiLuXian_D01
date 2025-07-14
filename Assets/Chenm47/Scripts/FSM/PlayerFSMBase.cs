using AI.FSM.Framework;
using Common;
using ns.Camera;
using ns.Character.Player;
using ns.Skill;
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
        [HideInInspector]
        public AnimationEventBehaviour animationEventBehaviour;
        [HideInInspector]
        public PlayerInventory playerInventory;
        [HideInInspector]
        public CharacterSkillManager characterSkillManager;
        #endregion

        protected override void Start()
        {
            //获取组件
            playerInfo = GetComponent<PlayerInfo>();
            playerInput = GetComponent<PlayerInput>();
            playerAction = GetComponent<PlayerAction>();
            animator = GetComponentInChildren<Animator>(true);
            cameraHandler = FindAnyObjectByType<CameraHandler>();
            playerAnimationHandler = GetComponent<PlayerAnimationHandler>();
            animationEventBehaviour = GetComponentInChildren<AnimationEventBehaviour>(true);
            playerInventory = GetComponent<PlayerInventory>();
            characterSkillManager = GetComponent<CharacterSkillManager>();
            base.Start();
        }
    }
}
