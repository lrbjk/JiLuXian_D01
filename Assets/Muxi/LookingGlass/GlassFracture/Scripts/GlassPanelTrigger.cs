using UnityEngine;

namespace GlassSystem.Scripts
{
    [RequireComponent(typeof(Fracturable))]
    public class GlassPanelTrigger : MonoBehaviour
    {
        [Header("Break Triggers")]
        public bool BreakOnCollision = true;
        
        public bool BreakOnClick = true;
        
        public float MinBreakForce = 1.0f;
        
        [Header("Visual Feedback")]
        public bool ShowBreakEffect = true;
        
        public GameObject BreakEffectPrefab;
        
        private Fracturable _fracturable;
        private Camera _mainCamera;
        
        void Start()
        {
            _fracturable = GetComponent<Fracturable>();
            _mainCamera = Camera.main;
            
            if (_mainCamera == null)
                _mainCamera = FindObjectOfType<Camera>();
        }
        
        void Update()
        {
            if (BreakOnClick && Input.GetMouseButtonDown(0))
            {
                HandleMouseClick();
            }
        }
        
        void OnCollisionEnter(Collision collision)
        {
            if (!BreakOnCollision || _fracturable == null)
                return;
                
            float collisionForce = collision.relativeVelocity.magnitude;
            
            if (collisionForce >= MinBreakForce)
            {
                Vector3 breakPosition = collision.contacts[0].point;
                Vector3 breakDirection = collision.relativeVelocity.normalized;
                
                Break(breakPosition, breakDirection);
            }
        }
        
        void HandleMouseClick()
        {
            if (_mainCamera == null || _fracturable == null)
                return;
                
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            
            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == gameObject)
            {
                Vector3 breakPosition = hit.point;
                Vector3 breakDirection = ray.direction;
                
                Break(breakPosition, breakDirection);
            }
        }
        
        void Break(Vector3 breakPosition, Vector3 breakDirection)
        {
            if (_fracturable == null)
                return;
                
            if (ShowBreakEffect)
            {
                showBreakEffect(breakPosition);
            }
            
            _fracturable.TriggerFracture(breakPosition, breakDirection);
            
            Debug.Log($"Fracturable broken at position: {breakPosition}");
        }
        
        void showBreakEffect(Vector3 position)
        {
            if (BreakEffectPrefab != null)
            {
                GameObject effect = Instantiate(BreakEffectPrefab, position, Quaternion.identity);
                Destroy(effect, 2.0f);
            }
            else
            {
                Debug.DrawRay(position, Vector3.up * 0.5f, Color.red, 1.0f);
                Debug.DrawRay(position, Vector3.right * 0.5f, Color.red, 1.0f);
                Debug.DrawRay(position, Vector3.forward * 0.5f, Color.red, 1.0f);
            }
        }
        
        public void TriggerBreak(Vector3 worldPosition)
        {
            Vector3 direction = (worldPosition - transform.position).normalized;
            Break(worldPosition, direction);
        }
        
        public void Reset()
        {
            var panel = GetComponent<GlassPanel>();
            if (panel != null)
                panel.ResetPanel();
        }
    }
}
