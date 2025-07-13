using Common;
using ns.Character.Player;
using UnityEngine;
using UnityEngine.Windows;

namespace ns.Camera
{
    /// <summary>
    /// 描述：
    /// </summary>
    public class CameraHandler : MonoBehaviour
    {
        public Transform TargetTF;
        public Transform CameraTF;
        public Transform CameraPivotTF;
        public float FollowSpeed = 1f;
        public float LookSpeed = 1f;
        public float PivotSpeed = 1f;
        public float MinPivot = -35;
        public float MaxPivot = 35;
        [SerializeField]
        PlayerInput input;
        [SerializeField]
        PlayerInfo playerInfo;


        private Vector3 cameraFallowVelocity = Vector3.zero;
        private float pitchAngle = 0f, lookAngle = 0f;
        private void Start()
        {
            defaultDistance = -CameraTF.localPosition.z;
        }

        private void LateUpdate()
        {
            FollowTarget();
            HandleCameraRotation(input.HorizontalCamera, input.VerticalCamera);
            HandleCamerLockOn();
            HandleCameraCollisions(Time.deltaTime);
        }

        public void FollowTarget()
        {
            Vector3 targetPos = TargetTF.position;
            transform.position = Vector3.SmoothDamp(
                transform.position, targetPos, ref cameraFallowVelocity, Time.deltaTime / FollowSpeed);
        }

        public void HandleCameraRotation(float xInput, float yInput)
        {
            if (!input.LockViewTrigger)
            {
                lookAngle += (xInput * LookSpeed);//不需要*delta因为input已经包含时间影响
                pitchAngle -= (yInput * PivotSpeed);
                pitchAngle = Mathf.Clamp(pitchAngle, MinPivot, MaxPivot); //限制俯仰角度
                                                                          //设置绕y轴旋转
                Vector3 rotation = Vector3.zero;
                rotation.y = lookAngle;
                Quaternion targetRotation = Quaternion.Euler(rotation);
                transform.rotation = targetRotation;
                //设置绕x轴旋转
                rotation = new Vector3(pitchAngle, 0, 0);
                targetRotation = Quaternion.Euler(rotation);
                CameraPivotTF.localRotation = targetRotation;
            }
        }

        public float CameraCollisionOffSet = 0.2f;
        public float MinimumCollisionOffset = 0.2f;
        public float CameraCollisionRadius = 0.1f;
        public LayerMask CameraCollisonLayers;

        private Vector3 cameraTransformPosition = Vector3.zero;
        private float targetDistance;
        private float defaultDistance;
        private void HandleCameraCollisions(float delta)
        {
            targetDistance = defaultDistance;
            RaycastHit hit;
            Vector3 direction = CameraTF.position - CameraPivotTF.position;
            direction.Normalize();
            Debug.DrawRay(CameraPivotTF.position, direction * targetDistance, Color.red);
            if (Physics.SphereCast
            (CameraPivotTF.position, CameraCollisionRadius,
            direction, out hit, targetDistance, CameraCollisonLayers))
            {
                float dis = Vector3.Distance(CameraPivotTF.position, hit.point);
                //print("dis:" + dis);
                targetDistance = (dis - CameraCollisionOffSet);
                //print("targetDistance:" + targetDistance);
            }
            if (targetDistance < MinimumCollisionOffset)
            {
                targetDistance = MinimumCollisionOffset;
            }

            if (Mathf.Abs(CameraTF.localPosition.z + targetDistance) < 0.01f)
                cameraTransformPosition.z = -targetDistance;
            else
                cameraTransformPosition.z =
                    Mathf.Lerp(CameraTF.localPosition.z, -targetDistance, delta * 10f);//每秒插值速度
            CameraTF.localPosition = cameraTransformPosition;
        }

        private void HandleCamerLockOn()
        {
            if (input.LockView)
            {
                if (!input.LockViewTrigger)
                {
                    //没有锁定目标
                    //回正镜头
                    print("回正镜头");
                    return;
                }
            }
            if (input.LockViewTrigger)
            {
                //锁定目标
                var targetPos = playerInfo.LockedTargetTF.position;
                Vector3 cameraPosition = CameraTF.position;

                //计算从相机到目标点的理想方向
                Vector3 toTarget = targetPos - cameraPosition;
                Vector3 desiredDirection = toTarget.normalized;

                // 计算水平旋转（绕Y轴）
                Vector3 horizontalDir = toTarget;
                horizontalDir.y = 0; // 投影到XZ平面
                if (horizontalDir.sqrMagnitude > 0.001f)
                {
                    Quaternion targetYRotation = Quaternion.LookRotation(horizontalDir);
                    lookAngle = targetYRotation.eulerAngles.y;
                    transform.rotation = Quaternion.Slerp(
                        transform.rotation,
                        targetYRotation,
                        Time.deltaTime * LookSpeed
                    );
                }

                //计算相机应该的旋转（直接指向目标）
                Quaternion targetCameraRotation = Quaternion.LookRotation(desiredDirection);

                //将目标旋转转换到CameraPivot空间
                Quaternion relativeRotation
                    = Quaternion.Inverse(transform.rotation) * targetCameraRotation;
                Vector3 euler = relativeRotation.eulerAngles;
                pitchAngle = euler.x;
                if (pitchAngle > 180f) pitchAngle -= 360f;
                pitchAngle = Mathf.Clamp(pitchAngle, MinPivot, MaxPivot);

                //应用俯仰旋转
                Quaternion targetPivotRotation = Quaternion.Euler(pitchAngle, 0, 0);
                CameraPivotTF.localRotation = Quaternion.Slerp(
                    CameraPivotTF.localRotation,
                    targetPivotRotation,
                    Time.deltaTime * PivotSpeed
                );
                ////Debug
                //Vector3 actualForward = CameraTF.forward;
                //float angleError = Vector3.Angle(actualForward, desiredDirection);
                //Debug.DrawRay(cameraPosition, actualForward * toTarget.magnitude, Color.green);
                //Debug.DrawLine(cameraPosition, targetPos, Color.red);
            }
        }

    }
}
