using Unity.VisualScripting;
using UnityEngine;

namespace ns.Character.Player
{
    /// <summary>
    /// 描述：
    /// </summary>
    public class PlayerMotor3D : MonoBehaviour
    {
        [SerializeField]
        private float RotateSpeed = 10f; // 移动转向速度
        private PlayerInfo playerInfo;
        private Rigidbody rb;
        private Transform groundRayPoint;
        [SerializeField]
        private float Radius = 0.15f;
        public LayerMask GroundLayer;
        public float GroundDistance = 0.16f;
        public float FallPushForce = 1f;
        public float MaxSlopeAngle = 30f;
        public float GroundAngle;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            playerInfo = GetComponent<PlayerInfo>();
            groundRayPoint = transform.Find("GroundRayPoint");
        }

        private void Update()
        {
            //每帧地面检测
            playerInfo.IsOnGround = CheckIsOnGround(out playerInfo.GroundHit);
        }

        public bool GroundCastHit(out RaycastHit hitInfo)
        {
            ////射线检测
            //bool isHit = Physics.Raycast
            //     (groundRayPoint.position, Vector3.down, out var res, playerInfo.GroundDistance, playerInfo.GroundLayer);
            //射线检测
            bool isHit = Physics.SphereCast(groundRayPoint.position, Radius, Vector3.down, out hitInfo,
                GroundDistance, GroundLayer);
            //Debug.DrawRay(groundRayPoint.position, Vector3.down * playerInfo.GroundDistance, Color.red);

            return isHit;
        }

        //private void OnDrawGizmos()
        //{
        //    Gizmos.color = Color.blue;
        //    Gizmos.DrawSphere(groundRayPoint.position, Radius);
        //    Gizmos.DrawSphere(groundRayPoint.position + Vector3.down * GroundDistance, Radius);
        //}

        private bool CheckIsOnGround(out RaycastHit hit)
        {
            //if (hit && rb.velocity.y <= 0)//防止还未起跳已经检测为地面
            if (GroundCastHit(out hit))//防止还未起跳已经检测为地面
            {
                //刷新跳跃次数
                playerInfo.CurrentJumpCount = 0;
                return true;
            }
            return false;
        }

        public void LookAtVector(Vector3 dir, float rotateSpeed = 10f)
        {
            if (dir == Vector3.zero) return;
            //转向
            Quaternion tr = Quaternion.LookRotation(dir);
            if (Quaternion.Angle(transform.rotation, tr) < 0.01f) return; //如果角度差小于0.01度则不转向
            //transform.rotation = Quaternion.Slerp(transform.rotation, tr, RotateSpeed * Time.deltaTime);
            rb.MoveRotation(Quaternion.Slerp(transform.rotation, tr, rotateSpeed * Time.fixedDeltaTime));
        }

        public void Move(Vector3 dir, float MoveSpeed)
        {
            LookAtVector(dir);

            //判断hit是否有打击
            if (playerInfo.IsOnGround)
            {
                dir = Vector3.ProjectOnPlane(dir, playerInfo.GroundHit.normal);
                dir.Normalize();
                //限制角度
                float angle = Vector3.Angle(dir, transform.forward);
                GroundAngle = angle;
                if (angle > MaxSlopeAngle)
                    return;//不移动
            }

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

        public void AddFallPuchForce()
        {
            AddForce(transform.forward * FallPushForce, ForceMode.VelocityChange);
        }

        public void AddForce(Vector3 force, ForceMode forceMode = ForceMode.Force)
        {
            rb.AddForce(force, forceMode);
        }

    }
}
