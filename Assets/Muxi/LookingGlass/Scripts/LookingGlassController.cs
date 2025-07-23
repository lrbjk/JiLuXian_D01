using UnityEngine;

namespace LookingGlass
{
    public class LookingGlassController: MonoBehaviour
    {
        private MeshRenderer m_MeshRenderer;
        
        private void Awake()
        {
            m_MeshRenderer = GetComponent<MeshRenderer>();
        }
        
        public void enableLookingGlass()
        {
            if (m_MeshRenderer != null)
            {
                m_MeshRenderer.material.SetTexture("_VirtualTexture", LookingGlassManager.GetScreenRT());
            }
        }
    }

}
