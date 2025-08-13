using GlassSystem;
using UnityEngine;

namespace GlassSystem
{
    public class SimpleRaycast : MonoBehaviour
    {
        public float impactForce = 3f;
        public int Retry = 3;
        
        [Header("鼠标灵敏度设置")]
    public float mouseSensitivity = 900f;
    
    [Header("垂直旋转限制")]
    public float maxLookAngle = 80f;
    public float minLookAngle = -80f;
    
    private float xRotation = 0f;
    private float yRotation = 0f;
    private Vector3 targetRotation;
        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            xRotation = this.transform.localEulerAngles.x;
            yRotation = this.transform.localEulerAngles.y;
        }

        void Update()
        {   
           
        
            // 只有在鼠标被锁定时才处理鼠标输入
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                HandleMouseInput();
            }
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                var raycastDirection = transform.TransformDirection(Vector3.forward);
                if (Physics.Raycast(transform.position, raycastDirection, out hit, Mathf.Infinity))
                {
                    Debug.DrawRay(transform.position, raycastDirection * hit.distance, Color.yellow, 10);
                    var fracturable = hit.collider.gameObject.GetComponent<Fracturable>();
                    if (fracturable is not null)
                    {
                        int failBreak = 0;
                        while (true)
                            try
                            {
                                fracturable.TriggerFracture(hit.point, raycastDirection * impactForce);
                                return;
                            }
                            catch (GlassFractureException e)
                            {
                                if (++failBreak >= Retry)
                                    throw;
                                Debug.LogWarning($"Failed to break fracturable (retry {failBreak}): {e}");
                            }
                    }
                }
            }
        }
        void HandleMouseInput()
        {
            // 获取鼠标移动输入
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        
            // 计算旋转角度
            yRotation += mouseX;
            xRotation -= mouseY; // 减号是为了让鼠标向上移动时摄像机向上看
        
            // 限制垂直旋转角度，防止翻转
            xRotation = Mathf.Clamp(xRotation, minLookAngle, maxLookAngle);
        
            // 应用旋转
            
                // 直接旋转
                transform.rotation = Quaternion.Euler(xRotation, yRotation, 0f);
          
        }
    }
    
}
