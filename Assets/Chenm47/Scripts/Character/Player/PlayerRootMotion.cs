using AI.FSM;
using UnityEngine;


namespace ns.Character.Player
{
    /// <summary>
    /// 描述：
    /// </summary>
    public class PlayerRootMotion : MonoBehaviour
    {
        private Rigidbody rb;
        private Animator animator;
        private Vector3 rootMotionDelta;        // Root Motion 位移缓存
        public bool ApplyAnimaMotionY { get; set; } = false;
        public bool ApplyAnimaMotionAll { get; set; } = false;

        public bool ApplyAnimatRotationY { get; set; } = false;

        public Vector3 BeforeApplySpeed { get; set; } = Vector3.zero;

        private void Start()
        {
            animator = GetComponent<Animator>();
        }
        //private void Update()
        //{
        //    print("帧数:" + Time.frameCount.ToString() + "Pos:" + transform.position + "Rotation" + transform.rotation.eulerAngles);
        //}
        private void OnAnimatorMove()
        {
            //施加刚体运动
            if (ApplyAnimaMotionAll)
            {
                //rb.velocity = animator.velocity;
                //animator.applyRootMotion = true;
                //animator.v
                //rb.velocity = animator.velocity;
                // 从 Animator 获取 Root Motion 位移
                rootMotionDelta = animator.deltaPosition;

                //// 叠加台阶抬升
                //if (PlayerFSMBase.Instance.playerMotor3D.VerticalOffset > 0f)
                //{
                //    rootMotionDelta.y += PlayerFSMBase.Instance.playerMotor3D.VerticalOffset;
                //}

                // 用 MovePosition 应用 Root Motion + 台阶抬升
                rb.MovePosition(rb.position + rootMotionDelta);
                rb.MoveRotation(rb.rotation * animator.deltaRotation);
            }
            else if (ApplyAnimaMotionY)
            {
                //其他轴速度保持
                //Debug.Log("rb:" + rb.velocity.ToString() + "animator" + animator.velocity);
                Vector3 v = new Vector3(BeforeApplySpeed.x, animator.velocity.y, BeforeApplySpeed.z);
                rb.velocity = v;
            }

            if (ApplyAnimatRotationY)
            {
                //应用动画旋转
                Quaternion deltaRotation = animator.deltaRotation;
                rb.MoveRotation(rb.rotation * deltaRotation);
            }

        }
    }
}
