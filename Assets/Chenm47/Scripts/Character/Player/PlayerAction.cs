using Common;
using ns.Camera;
using System.Collections;
using System.Collections.Generic;
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
        [SerializeField]
        CameraHandler cameraHandler;
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
        public void LookAndMove(Vector3 lookDir, Vector3 moveDir, float moveSpeed)
        {
            playerMotor3D.LookAndMove(lookDir, moveDir, moveSpeed);
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
        /// <summary>
        /// 获取锁定目标
        /// </summary>
        /// <returns></returns>
        public Collider[] GetLockTargets()
        {
            var colliders =
                Physics.OverlapSphere(playerInfo.LockedTF.position, 20f, playerInfo.EnemyLayer);
            List<Collider> targets = new List<Collider>();
            Transform closetTarget = null;
            float tempDistance = float.PositiveInfinity;
            foreach (var collider in colliders)
            {
                Transform targetLockedTF = collider.GetComponent<CharacterInfo>().LockedTF;
                var enemyPos = targetLockedTF.position;
                Vector3 dir = enemyPos - playerInfo.LockedTF.position;
                float distance = dir.magnitude;
                if (distance < tempDistance)
                {
                    tempDistance = distance;
                    closetTarget = targetLockedTF;
                }
                float viewableAngle = Vector3.Angle(dir, cameraHandler.transform.forward);
                if (viewableAngle < 50f && viewableAngle > -50f && distance < playerInfo.MaxLockDistance)
                    targets.Add(collider);
            }
            //设定当前锁定目标为最近
            playerInfo.LockedTargetTF = closetTarget;
            return colliders;
        }

        public void SwitchLockTarget(float switchDir)
        {
            var colliders =
                Physics.OverlapSphere(playerInfo.LockedTargetTF.position, 10f, playerInfo.EnemyLayer);

            float tempClosestDistanceLeft = float.PositiveInfinity;
            float tempClosestDistanceRight = float.PositiveInfinity;

            Transform closestTransformLeft = playerInfo.LockedTargetTF;
            Transform closestTransformRight = playerInfo.LockedTargetTF;

            foreach (var collider in colliders)
            {
                var currentColliderTF = collider.GetComponent<CharacterInfo>().LockedTF;

                if (currentColliderTF == playerInfo.LockedTargetTF)
                {
                    continue;
                }

                Vector3 relativeEnemyPosition = cameraHandler.transform.InverseTransformPoint(currentColliderTF.position);
                print(relativeEnemyPosition);

                float distance = Mathf.Abs(relativeEnemyPosition.x);

                //print(collider.name + "distance" + distance);

                if (relativeEnemyPosition.x < 0f && distance < tempClosestDistanceLeft)
                {
                    //左边    应该是相对于玩家镜头，而不是相对于锁定目标
                    closestTransformLeft = collider.GetComponent<CharacterInfo>().LockedTF;
                    tempClosestDistanceLeft = distance;
                }
                else if (relativeEnemyPosition.x > 0f && distance < tempClosestDistanceRight)
                {
                    //右边
                    closestTransformRight = collider.GetComponent<CharacterInfo>().LockedTF;
                    tempClosestDistanceRight = distance;
                }
            }

            if (switchDir < 0)
            {
                playerInfo.LockedTargetTF = closestTransformLeft;
                print("切换锁定目标到左边: " + closestTransformLeft.parent.name);
            }
            else
            {
                playerInfo.LockedTargetTF = closestTransformRight;
                //print("切换锁定目标到右边: " + closestTransformRight.parent.name);
            }
        }

    }
}
