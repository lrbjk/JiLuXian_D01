using Common;
using System.Collections;
using UnityEngine;


namespace ns.Character.Player
{
    /// <summary>
    /// 描述：
    /// </summary>
    public class PlayerAction : MonoSingleton<PlayerAction>
    {
        //通用
        private PlayerMotor3D playerMotor3D;
        private Rigidbody rb;
        private Transform groundRayPoint;
        private PlayerInfo playerInfo;
        public PlayerInfo PlayerInfo { get => playerInfo; }

        protected override void Init()
        {
            base.Init();
            playerInfo = GetComponent<PlayerInfo>();
            rb = GetComponent<Rigidbody>();
            groundRayPoint = transform.Find("GroundRayPoint");
            playerMotor3D = GetComponent<PlayerMotor3D>();
        }

        //通用

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dir">(1,0,0)右,(-1,0,0)左</param>
        /// <param name="speed">z速度</param>
        public void Move(Vector3 dir, float speed)
        {
            playerMotor3D.Move(dir, speed);
        }

        /// <summary>
        /// 保持刚体y速度
        /// </summary>
        /// <param name="dir">(1,0,0)右,(-1,0,0)左</param>
        /// <param name="speed">z速度</param>
        public void MoveKeepVy(Vector3 dir, float speed)
        {
            playerMotor3D.MoveKeepVy(dir, speed);
        }

        private Coroutine moveCoroutine;

        /// <summary>
        /// 从当前位置向指定位置移动speed距离，开启协程直到运动到目标点结束
        /// </summary>
        /// <param name="target"></param>
        /// <param name="speed"></param>
        public Coroutine MoveTo(Vector3 target, float speed)
        {
            moveCoroutine = StartCoroutine(MoveToNextPos(target, speed));
            return moveCoroutine;
        }

        private IEnumerator MoveToNextPos(Vector3 target, float speed)
        {
            while ((transform.position - target).magnitude >= 0.05f)
            {
                Vector3 nextPos = Vector3.MoveTowards(transform.position, target, speed * Time.fixedDeltaTime);
                playerMotor3D.MoveTo(nextPos);
                yield return new WaitForFixedUpdate();
            }
            moveCoroutine = null;
        }

        public void StopMove()
        {
            playerMotor3D.Move(Vector3.zero, 0);
            if (moveCoroutine != null)
            {
                StopCoroutine(moveCoroutine);
                moveCoroutine = null;
            }
        }

        public void Jump()
        {
            playerMotor3D.Jump(playerInfo.JumpSpeed);
        }

        public RaycastHit2D GroundCastHit()
        {
            //射线检测
            var hit = Physics2D.Raycast(groundRayPoint.position, Vector2.down,
                playerInfo.GroundDistance, playerInfo.GroundLayer);
            return hit;
        }

        public bool IsOnGround()
        {
            var hit = GroundCastHit();
            if (hit && rb.velocity.y <= 0)//防止还未起跳已经检测为地面
            {
                //刷新跳跃次数
                playerInfo.CurrentJumpCount = 0;
                return true;
            }
            return false;
        }
        /// <summary>
        /// 发送射线获取是否为下落平台
        /// </summary>
        /// <returns>不是为空</returns>
        public PlatformEffector2D IsDownStair()
        {
            //射线检测
            var hit = GroundCastHit();
            if (hit)//防止还未起跳已经检测为地面
            {
                //获取DownStari
                PlatformEffector2D p = hit.collider.GetComponent<PlatformEffector2D>();
                if (p != null)
                    return p;
            }
            return null;
        }

        public bool IsFall()
        {
            bool res = rb.velocity.y < 0;
            return rb.velocity.y < 0;
        }
    }
}
