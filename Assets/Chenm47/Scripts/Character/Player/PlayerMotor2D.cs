using UnityEngine;

namespace ns.Character.Player
{
    /// <summary>
    /// 描述：2D角色马达，控制玩家的运动
    /// </summary>
    public class PlayerMotor2D : MonoBehaviour
    {
        private Rigidbody2D rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void LookAtVector(Vector3 dir)
        {
            if (dir == Vector3.zero) return;
            //水平转向即可
            if (dir.x < 0)
                transform.rotation = Quaternion.LookRotation(Vector3.back);
            else if (dir.x > 0)
                transform.rotation = Quaternion.LookRotation(Vector3.forward);
        }

        public void Move(Vector3 dir, float MoveSpeed)
        {
            LookAtVector(dir);
            //向右方向移动
            //transform.position = (transform.position + transform.right * MoveSpeed * Time.deltaTime);
            rb.velocity = dir.normalized * MoveSpeed;
        }
        public void MoveKeepVy(Vector3 dir, float MoveSpeed)
        {
            LookAtVector(dir);
            //向右方向移动
            Vector3 v = dir.normalized * MoveSpeed;
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
            rb.velocity = new Vector2(rb.velocity.x, JumpSpeed);
        }

    }
}
