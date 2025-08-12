using TMPro;
using UnityEngine;
namespace ns.Character.Player
{
    /// <summary>
    /// 描述：
    /// </summary>
    public class PlayerMotor3D : MonoBehaviour
    {
        //public bool IsDebug = false;

        [SerializeField]
        private float RotateSpeed = 10f; // 移动转向速度
        private PlayerInfo playerInfo;
        private Rigidbody rb;
        private float checkIsExitGroundTimer = 0;
        public float checkIsExitGroundMaxTime = 0.1f;

        //供物理系统使用
        private Quaternion targetRotation;
        private Vector3 targetPosition;
        //private Vector3 verticalVelocity; // 竖直方向速度

        public float GroundSphereRadius = 0.05f;
        public LayerMask GroundLayer;
        public float GroundDistance = 0.16f;
        public float FallPushForce = 1f;
        public float MaxSlopeAngle = 30f;
        public float GroundAngle;


        //障碍物检测
        public float startUpOffest = 0.05f;
        public float forwardCheckDistance = 0.2f;
        public float stepHeight = 0.3f;//台阶高度
        public float stepSmooth = 0.01f;
        public float stepDownVelocity = 3f;
        public LayerMask ObstacleLayer;

        private Vector3 footPos;

        private void Awake()
        {
            rb = GetComponentInParent<Rigidbody>();
            playerInfo = GetComponentInParent<PlayerInfo>();
            targetRotation = transform.rotation;
            //targetPosition = transform.position;
            CapsuleCollider capsuleCollider = GetComponentInParent<CapsuleCollider>();

        }

        private void Start()
        {
            animator = GetComponent<Animator>();
        }

        private void Update()
        {
            //每帧地面检测
            playerInfo.IsOnGround = GroundCastHit();
            //当连续一段时间不在地面才真正离开地面
            if (!playerInfo.IsOnGround)
            {
                checkIsExitGroundTimer += Time.deltaTime;
                if (checkIsExitGroundTimer >= checkIsExitGroundMaxTime)
                    playerInfo.IsOnGround = false;
                else
                {
                    playerInfo.IsOnGround = true;
                }
            }
            else
            {
                checkIsExitGroundTimer = 0;
            }
        }


        private void FixedUpdate()
        {
            //if (IsDebug)
            //{
            //    Debug.Log(Time.frameCount + "Fixed" + targetRotation.eulerAngles);
            //}
            rb.MoveRotation(targetRotation);
        }

        private void OnDrawGizmos()
        {
            //Gizmos.color = Color.red;
            //Gizmos.DrawSphere(transform.position + forwardCheckDistance * transform.forward, GroundSphereRadius);//地面球
            Gizmos.color = Color.blue;
            Vector3 dir = transform.forward;

            Gizmos.DrawLine(transform.position + Vector3.up * startUpOffest, transform.position + Vector3.up * startUpOffest + transform.forward * forwardCheckDistance);// 低位射线
            Gizmos.DrawLine(transform.position + Vector3.up * stepHeight,
               transform.position + Vector3.up * stepHeight + transform.forward * forwardCheckDistance);// 高位射线

            Gizmos.color = Color.gray;
            Gizmos.DrawLine(transform.position - Vector3.up * startUpOffest + transform.forward * forwardCheckDistance,//下台阶检测
                transform.position - Vector3.up * startUpOffest + transform.forward * forwardCheckDistance +
                Vector3.down * stepHeight);

        }

        public bool GroundCastHit()
        {
            Collider[] colliders = new Collider[4];
            int v = Physics.OverlapSphereNonAlloc(transform.position + forwardCheckDistance * transform.forward, GroundSphereRadius, colliders, GroundLayer);

            if (v > 0)
            {
                return true;
            }
            return false;
        }

        public void LookAtVector(Vector3 dir, float rotateSpeed = 10f)
        {
            if (dir == Vector3.zero) return;
            //转向
            Quaternion tr = Quaternion.LookRotation(dir);
            if (Quaternion.Angle(transform.rotation, tr) < 0.01f)
            {
                targetRotation = tr;
                return;
            } //如果角度差小于0.01度则不转向
            //transform.rotation = Quaternion.Slerp(transform.rotation, tr, RotateSpeed * Time.deltaTime);
            targetRotation = Quaternion.Slerp(transform.rotation, tr, rotateSpeed * Time.deltaTime);
        }

        public void LookAtVentorNow(Vector3 dir)
        {
            Quaternion tr = Quaternion.LookRotation(dir);
            targetRotation = tr;
            Debug.Log("LookAt" + targetRotation);
            rb.rotation = tr;
        }

        public void Move(Vector3 dir, float moveSpeed)
        {
            LookAtVector(dir);
            //targetPosition = rb.position
            //   + dir * moveSpeed * Time.fixedDeltaTime;

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
        /// <summary>
        /// 移动到指定位置
        /// </summary>
        /// <param name="target"></param>
        public void MovePositionOnly(Vector3 target)
        {
            rb.MovePosition(target);
        }

        public void StopMove()
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        public void AddFallPuchForce()
        {
            AddForce(transform.forward * FallPushForce, ForceMode.VelocityChange);
        }

        public void AddForce(Vector3 force, ForceMode forceMode = ForceMode.Force)
        {
            rb.AddForce(force, forceMode);
        }

        public void SetRbGravity(bool isGravity)
        {
            rb.useGravity = isGravity;
        }

        #region RootMotion
        private Animator animator;
        public bool ApplyAnimaMotionY { get; set; } = false;
        public bool ApplyAnimaMotionAll { get; set; } = false;
        public bool DisableDownStepRay { get; set; } = false;

        public Vector3 BeforeApplySpeed { get; set; } = Vector3.zero;

        private void OnAnimatorMove()
        {
            if (ApplyAnimaMotionAll)
            {
                rb.velocity = animator.velocity;
                targetRotation *= animator.deltaRotation;

                //前方障碍物检测
                Vector3 forward = transform.forward;

                // 低位射线
                RaycastHit lowerHit;
                bool isObstacle =
                    Physics.Raycast(transform.position + Vector3.up * startUpOffest, forward, out lowerHit, forwardCheckDistance, ObstacleLayer) //前方
                                                                                                                                                 //Physics.Raycast(transform.position + Vector3.up * startUpOffest, transform.TransformDirection(new Vector3(-1, 0, 1)), out lowerHit, forwardCheckDistance, ObstacleLayer) ||//左45
                                                                                                                                                 //Physics.Raycast(transform.position + Vector3.up * startUpOffest, transform.TransformDirection(new Vector3(1, 0, 1)), out lowerHit, forwardCheckDistance, ObstacleLayer)//右45
                    ;


                if (isObstacle)
                {
                    Debug.Log("前方有障碍物" + lowerHit.collider.name);
                    bool isHighObstacle =
                        Physics.Raycast(transform.position + Vector3.up * stepHeight, forward, forwardCheckDistance, ObstacleLayer)//前方
                                                                                                                                   //Physics.Raycast(transform.position + Vector3.up * stepHeight, transform.TransformDirection(new Vector3(-1, 0, 1)), forwardCheckDistance, ObstacleLayer) ||//左45
                                                                                                                                   //Physics.Raycast(transform.position + Vector3.up * stepHeight, transform.TransformDirection(new Vector3(1, 0, 1)), forwardCheckDistance, ObstacleLayer)//右45
                        ;
                    // 高位射线
                    if (!isHighObstacle)
                    {
                        Debug.Log("上台阶");
                        //rb.AddForce(Vector3.up * stepUpVelocity, ForceMode.Impulse);
                        rb.position += new Vector3(0f, stepSmooth, 0f);
                    }
                }
                else
                {
                    //检测是否下台阶
                    if (!DisableDownStepRay && Physics.Raycast(transform.position - Vector3.up * startUpOffest + transform.forward * forwardCheckDistance
                        , Vector3.down, out RaycastHit lowerStepHit, stepHeight, ObstacleLayer))
                    {
                        if (rb.position.y - lowerStepHit.point.y > 0.01f)
                        {
                            Debug.Log("下台阶" + lowerStepHit.collider.name);
                            rb.AddForce(Vector3.down * stepDownVelocity, ForceMode.Impulse);
                            //rb.position -= new Vector3(0f, stepSmooth, 0f);
                        }
                    }
                }

            }
            else if (ApplyAnimaMotionY)
            {
                //其他轴速度保持
                //Debug.Log("rb:" + rb.velocity.ToString() + "animator" + animator.velocity);
                Vector3 v = new Vector3(BeforeApplySpeed.x, animator.velocity.y, BeforeApplySpeed.z);
                rb.velocity = v;
            }


            //if (ApplyAnimatRotationY)
            //{
            //    //应用动画旋转
            //    Quaternion deltaRotation = animator.deltaRotation;
            //    rb.MoveRotation(rb.rotation * deltaRotation);
            //}

        }

        #endregion


    }
}
