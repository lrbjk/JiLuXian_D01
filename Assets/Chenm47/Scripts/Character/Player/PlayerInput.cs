using UnityEngine;


namespace ns.Character.Player
{
    /// <summary>
    /// 描述：
    /// </summary>
    public class PlayerInput : MonoBehaviour
    {
        public float HorizontalMove { get; protected set; }

        public float VerticalMove { get; protected set; }
        /// <summary>移动输入的强度，范围0-1</summary>
        public float Movement { get; protected set; }
        /// <summary> 按移动键按下的时间 </summary>
        public float MovementPressedTimer { get; protected set; }

        /// <summary> 是否长按移动键 </summary>
        public bool MovementHoldTrigger { get; protected set; }

        public float HorizontalCamera { get; protected set; }

        public float VerticalCamera { get; protected set; }

        public bool RollDown { get; protected set; }

        public bool RollHold { get; protected set; }
        /// <summary>翻滚键按下的时间</summary>
        public float RollPressedTimer { get; protected set; }
        public bool RollUp { get; protected set; }
        /// <summary>翻滚键是否为长按</summary>
        public bool RollHoldTrigger { get; protected set; }

        public bool Attack { get; protected set; }
        public bool Jump { get; protected set; }

        public bool LockView { get; protected set; }
        /// <summary>
        /// 是否处于锁定视角状态
        /// </summary>
        public bool LockViewTrigger { get; protected set; }

        public float SwitchLockedTarget { get; protected set; }

        #region 按键配置
        [Tooltip("按下移动键的时间，超过这个时间就会被视为长按")]
        [SerializeField]
        float MovementPressdTime = 0.5f;
        [Tooltip("翻滚按下的时间，超过这个时间就会被视为长按")]
        [SerializeField]
        float RollPressedTime = 0.5f;
        #endregion
        private void Update()
        {
            //获取输入
            MovementInput();

            HorizontalCamera = HorizontalCameraAixInput();
            VerticalCamera = VerticalCameraAixInput();

            RollInput();

            Attack = AttackInput();
            Jump = JumpInput();

            LockViewInput();

            SwitchLockedTarget = SwitchLockedTargetInput();

        }
        //常用输入
        /// <summary>
        /// 移动输入
        /// </summary>
        /// <returns></returns>
        private void MovementInput()
        {
            HorizontalMove = HorizontalMoveAixInput();
            VerticalMove = VerticalMoveAixInput();
            Movement = Mathf.Clamp01(Mathf.Abs(HorizontalMove)
                + Mathf.Abs(VerticalMove));

            if (Movement > 0)
                MovementPressedTimer += Time.deltaTime;
            else
                MovementPressedTimer = 0;
            if (MovementPressedTimer >= MovementPressdTime)
                MovementHoldTrigger = true; //长按
            else
                MovementHoldTrigger = false; //短按
        }
        protected virtual float HorizontalMoveAixInput()
        {
            return Input.GetAxis("Horizontal");
        }
        protected virtual float VerticalMoveAixInput()
        {
            return Input.GetAxis("Vertical");
        }

        /// <summary>
        /// 相机转向输入
        /// </summary>
        /// <returns></returns>
        protected virtual float HorizontalCameraAixInput()
        {
            return Input.GetAxis("Mouse X");
        }
        protected virtual float VerticalCameraAixInput()
        {
            return Input.GetAxis("Mouse Y");
        }

        /// <summary>
        /// 翻滚输入
        /// </summary>
        /// <returns></returns>
        protected virtual void RollInput()
        {
            RollDown = RollInputDown();
            RollHold = RollInputHold();
            RollUp = RollInputUp();
            if (RollHold || RollUp)
                RollPressedTimer += Time.deltaTime;
            else
                RollPressedTimer = 0;

            if ((RollHold || RollUp) && RollPressedTimer >= RollPressedTime)
                RollHoldTrigger = true;
            else
                RollHoldTrigger = false;
        }
        protected virtual bool RollInputDown()
        {
            return Input.GetKeyDown(KeyCode.LeftShift);
        }
        protected virtual bool RollInputHold()
        {
            return Input.GetKey(KeyCode.LeftShift);
        }
        protected virtual bool RollInputUp()
        {
            return Input.GetKeyUp(KeyCode.LeftShift);
        }

        /// <summary>
        /// 普攻输入
        /// </summary>
        /// <returns></returns>
        protected virtual bool AttackInput()
        {
            return Input.GetMouseButtonDown(0);
        }

        /// <summary>
        /// 跳跃输入
        /// </summary>
        /// <returns></returns>
        protected virtual bool JumpInput()
        {
            return Input.GetKeyDown(KeyCode.Space);
        }

        /// <summary>
        /// 锁定视角输入
        /// </summary>
        /// <returns></returns>
        private void LockViewInput()
        {
            LockView = LockViewKeyInput();
            if (LockView)
            {
                LockViewTrigger = !LockViewTrigger;
                if (LockViewTrigger)
                {
                    //检测锁定目标
                    var targets = PlayerAction.Instance.GetLockTargets();
                    if (targets.Length == 0)
                    {
                        LockViewTrigger = false;
                    }
                }
            }
        }
        protected virtual bool LockViewKeyInput()
        {
            return Input.GetKeyDown(KeyCode.F);
        }

        /// <summary>
        /// 切换锁定目标输入
        /// </summary>
        /// <returns></returns>
        protected virtual float SwitchLockedTargetInput()
        {
            return Input.GetAxisRaw("Mouse ScrollWheel");
        }

    }
}
