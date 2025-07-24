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

        public bool ApplyAnimaMotionY { get; set; } = false;
        public bool ApplyAnimaMotionAll { get; set; } = false;

        public Vector3 BeforeApplySpeed { get; set; } = Vector3.zero;

        private void Start()
        {
            rb = GetComponentInParent<Rigidbody>(true);
            animator = GetComponent<Animator>();
        }
        private void OnAnimatorMove()
        {
            //施加刚体运动
            if (ApplyAnimaMotionAll)
                rb.velocity = animator.velocity;
            else if (ApplyAnimaMotionY)
            {
                //其他轴速度保持
                //Debug.Log("rb:" + rb.velocity.ToString() + "animator" + animator.velocity);
                Vector3 v = new Vector3(BeforeApplySpeed.x, animator.velocity.y, BeforeApplySpeed.z);
                rb.velocity = v;

            }
        }
    }
}
