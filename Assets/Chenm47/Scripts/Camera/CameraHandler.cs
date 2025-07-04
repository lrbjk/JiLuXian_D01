using ns.Character.Player;
using UnityEngine;

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
        public PlayerInput Input;

        private Vector3 cameraFallowVelocity = Vector3.zero;
        private float pivotAngle = 0f, lookAngle = 0f;

        private void Start()
        {
            defaultDistance = -CameraTF.localPosition.z;
        }

        private void LateUpdate()
        {
            FollowTarget();
            HandleCameraRotation(Input.HorizontalCamera, Input.VerticalCamera);
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
            lookAngle += (xInput * LookSpeed);//不需要*delta因为input已经包含时间影响
            pivotAngle -= (yInput * PivotSpeed);
            pivotAngle = Mathf.Clamp(pivotAngle, MinPivot, MaxPivot); //限制俯仰角度
            //设置绕轴旋转
            Vector3 rotation = Vector3.zero;
            rotation.y = lookAngle;
            Quaternion targetRotation = Quaternion.Euler(rotation);
            transform.rotation = targetRotation;

            rotation = new Vector3(pivotAngle, 0, 0);
            targetRotation = Quaternion.Euler(rotation);
            CameraPivotTF.localRotation = targetRotation;
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
            if (Physics.SphereCast
            (CameraPivotTF.position, CameraCollisionRadius,
            direction, out hit, targetDistance, CameraCollisonLayers))
            {
                float dis = Vector3.Distance(CameraPivotTF.position, hit.point);
                targetDistance = (dis - CameraCollisionOffSet);
            }
            if (targetDistance < MinimumCollisionOffset)
            {
                targetDistance = MinimumCollisionOffset;
            }
            if ((CameraTF.localPosition.z + targetDistance) < 0.01f)
                cameraTransformPosition.z = -targetDistance;
            else
                cameraTransformPosition.z =
                    Mathf.Lerp(CameraTF.localPosition.z, -targetDistance, delta * 10f);//每秒插值速度
            CameraTF.localPosition = cameraTransformPosition;
        }

    }
}
