using UnityEngine;

namespace ns.Character.Player
{
    /// <summary>
    /// 描述：
    /// </summary>
    public class PlayerMotor3D : MonoBehaviour
    {
        [SerializeField]
        private float RotateSpeed = 10f; // 转向速度

        private Rigidbody rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void LookAtVector(Vector3 dir)
        {
            if (dir == Vector3.zero) return;
            //转向
            Quaternion tr = Quaternion.LookRotation(dir);
            if (Quaternion.Angle(transform.rotation, tr) < 0.01f) return; //如果角度差小于0.01度则不转向
            //transform.rotation = Quaternion.Slerp(transform.rotation, tr, RotateSpeed * Time.deltaTime);
            rb.MoveRotation(Quaternion.Slerp(transform.rotation, tr, RotateSpeed * Time.fixedDeltaTime));
        }

        public void Move(Vector3 dir, float MoveSpeed)
        {
            LookAtVector(dir);
            rb.velocity = dir * MoveSpeed;
            //transform.Translate(MoveSpeed * Time.deltaTime * dir, Space.World);
        }

        public void LookAndMove(Vector3 lookDir, Vector3 moveDir, float MoveSpeed)
        {
            LookAtVector(lookDir);
            rb.velocity = moveDir * MoveSpeed;
        }

        public void MoveKeepVy(Vector3 dir, float MoveSpeed)
        {
            LookAtVector(dir);
            Vector3 v = dir * MoveSpeed;
            v.y = rb.velocity.y;
            rb.velocity = v;
        }

        /// <summary>
        /// 移动到指定位置
        /// </summary>
        /// <param name="target"></param>
        /// <param name="MoveSpeed"></param>
        public void MoveTo(Vector3 target)
        {
            Vector3 dir = target - transform.position;
            LookAtVector(dir);
            rb.MovePosition(target);
        }

        public void Jump(float JumpSpeed)
        {
            rb.velocity = new Vector3(rb.velocity.x, JumpSpeed, rb.velocity.z);
        }

    }
}
