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

        public float Movement { get; protected set; }

        public float HorizontalCamera { get; protected set; }

        public float VerticalCamera { get; protected set; }

        public bool Roll { get; protected set; }
        public bool Attack { get; protected set; }
        public bool Jump { get; protected set; }

        private void Update()
        {
            //获取输入
            HorizontalMove = HorizontalMoveAixInput();
            VerticalMove = VerticalMoveAixInput();
            Movement = Mathf.Clamp01(Mathf.Abs(HorizontalMove)
                + Mathf.Abs(VerticalMove));
            HorizontalCamera = HorizontalCameraAixInput();
            VerticalCamera = VerticalCameraAixInput();
            Roll = RollInput();
            Attack = AttackInput();
            Jump = JumpInput();
        }

        //常用输入
        /// <summary>
        /// 移动输入
        /// </summary>
        /// <returns></returns>
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
        protected virtual bool RollInput()
        {
            return Input.GetKeyDown(KeyCode.LeftShift);
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
    }
}
