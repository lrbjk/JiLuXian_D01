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
        /// <summary>
        /// 攻击输入
        /// </summary>
        public bool IsLeftAttackTrigger { get; protected set; }
        public bool IsLightAttackTrigger { get; protected set; }
        public bool IsHeavyAttackTrigger { get; protected set; }
        public bool IsSkillAttackTrigger { get; protected set; }
        public bool LightAttackL { get; protected set; }
        public bool HeavyAttackL { get; protected set; }
        public bool SkillAttackL { get; protected set; }
        public bool LightAttackR { get; protected set; }
        public bool HeavyAttackR { get; protected set; }
        public bool SkillAttackR { get; protected set; }

        public bool Jump { get; protected set; }

        public bool LockView { get; protected set; }
        /// <summary>
        /// 是否处于锁定视角状态
        /// </summary>
        public bool LockViewTrigger { get; protected set; }
        /// <summary>
        /// 切换锁定目标输入，正数为右目标，负数为左目标
        /// </summary>
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

            AttackInput();

            Jump = JumpInput();

            LockViewInput();
            SwitchLockedTargetInput();

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
        /// 攻击输入
        /// </summary>
        private void AttackInput()
        {
            LightAttackL = LightAttackInputL();
            HeavyAttackL = HeavyAttackInputL();
            LightAttackR = LightAttackInputR();
            HeavyAttackR = HeavyAttackInputR();
            SkillAttackL = SkillAttackInputL();
            SkillAttackR = SkillAttackInputR();
            IsLeftAttackTrigger = LightAttackL || HeavyAttackL || SkillAttackL;
            //输入优先级：轻击 > 重击 > 技能
            IsLightAttackTrigger = LightAttackL || LightAttackR;
            if (IsLightAttackTrigger)
            {
                IsHeavyAttackTrigger = false;
                IsSkillAttackTrigger = false;
                return;
            }
            IsHeavyAttackTrigger = HeavyAttackL || HeavyAttackR;
            if (IsHeavyAttackTrigger)
            {
                IsSkillAttackTrigger = false;
                return;
            }
            IsSkillAttackTrigger = SkillAttackL || SkillAttackR;
        }

        /// <summary>
        /// 轻击L输入
        /// </summary>
        /// <returns></returns>
        protected virtual bool LightAttackInputL()
        {
            return Input.GetKeyDown(KeyCode.J);
        }
        /// <summary>
        /// 重击L输入
        /// </summary>
        /// <returns></returns>
        protected virtual bool HeavyAttackInputL()
        {
            return Input.GetKeyDown(KeyCode.U);
        }

        protected virtual bool SkillAttackInputL()
        {
            return Input.GetKeyDown(KeyCode.M);
        }

        /// <summary>
        /// 轻击R输入
        /// </summary>
        /// <returns></returns>
        protected virtual bool LightAttackInputR()
        {
            return Input.GetKeyDown(KeyCode.K);
        }
        /// <summary>
        /// 重击R输入
        /// </summary>
        /// <returns></returns>
        protected virtual bool HeavyAttackInputR()
        {
            return Input.GetKeyDown(KeyCode.I);
        }

        protected virtual bool SkillAttackInputR()
        {
            return Input.GetKeyDown(KeyCode.Comma);
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
        protected virtual float SwitchLockedKeyInput()
        {
            return -Input.GetAxisRaw("Mouse ScrollWheel");
        }

        private void SwitchLockedTargetInput()
        {
            SwitchLockedTarget = SwitchLockedKeyInput();
            if (SwitchLockedTarget != 0 && LockViewTrigger)
            {
                print("swlt" + SwitchLockedTarget);
                //切换锁定目标
                PlayerAction.Instance.SwitchLockTarget(SwitchLockedTarget);
            }
        }

    }
}
