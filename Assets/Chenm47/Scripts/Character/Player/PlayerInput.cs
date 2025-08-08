using System;
using UnityEngine;


namespace ns.Character.Player
{

    public enum AttackInputType
    {
        LeftLight,
        LeftHeavy,
        LeftSkill,
        RightLight,
        RightHeavy,
        RightSkill,
        None
    }

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
        /// <summary>
        /// 翻滚键是否按住
        /// </summary>
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
        public AttackInputType AtkInputType { get; protected set; }
        public bool LightAttackL { get; protected set; }
        public bool HeavyAttackL { get; protected set; }
        public bool SkillAttackL { get; protected set; }
        public bool LightAttackR { get; protected set; }
        public bool HeavyAttackR { get; protected set; }
        public bool SkillAttackR { get; protected set; }

        /// <summary>跳跃输入</summary>
        public bool Jump { get; protected set; }

        /// <summary>交互输入</summary>
        public bool Interacting { get; protected set; }

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
            Jump = JumpInput();
            Interacting = InteractingInput();
            AttackInput();


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
            return Input.GetKeyDown(KeyCode.Space);
        }
        protected virtual bool RollInputHold()
        {
            return Input.GetKey(KeyCode.Space);
        }
        protected virtual bool RollInputUp()
        {
            return Input.GetKeyUp(KeyCode.Space);
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
                if (IsLeftAttackTrigger)
                    AtkInputType = AttackInputType.LeftLight;
                else
                    AtkInputType = AttackInputType.RightLight;
                return;
            }
            IsHeavyAttackTrigger = HeavyAttackL || HeavyAttackR;
            if (IsHeavyAttackTrigger)
            {
                IsSkillAttackTrigger = false;
                if (IsLeftAttackTrigger)
                    AtkInputType = AttackInputType.LeftHeavy;
                else
                    AtkInputType = AttackInputType.RightHeavy;
                return;
            }
            IsSkillAttackTrigger = SkillAttackL || SkillAttackR;
            if (IsSkillAttackTrigger)
            {
                if (IsLeftAttackTrigger)
                    AtkInputType = AttackInputType.LeftSkill;
                else
                    AtkInputType = AttackInputType.RightSkill;
                return;
            }
            AtkInputType = AttackInputType.None;
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

        private float rollUpTimer = 0f;
        [Tooltip("长跑后该时间内若再按下则判断为跳跃")]
        public float JumpDelta = 0.1f;
        /// <summary>
        /// 跳跃输入
        /// </summary>
        /// <returns></returns>
        protected virtual bool JumpInput()
        {
            //长按疾跑后并短时间内再次按下
            if (RollPressedTimer >= RollPressedTime && RollUp)
                //记录放开时刻
                rollUpTimer = Time.time;
            if (RollDown)
            {
                float delta = Time.time - rollUpTimer;
                if (delta < JumpDelta)
                    return true;
            }
            return false;
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

        private bool InteractingInput()
        {
            return Input.GetKeyDown(KeyCode.E);
        }

    }
}
