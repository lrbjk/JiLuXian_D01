using System;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Serialization;

namespace LookingGlass
{
    public class VirtualCamera : MonoBehaviour
    {

        [SerializeField] private Camera MainCamera;
    
        private Vector3 Offset;
        private Camera Camera;
        [SerializeField] private float m_RenderTextureScale = 1;
        private RenderTexture currentRenderTexture;

        private void Start()
        {
            Camera = GetComponent<Camera>();
            CreateRenderTexture();
        }

        private void CreateRenderTexture()
        {
            if (currentRenderTexture != null)
            {
                currentRenderTexture.Release();
                DestroyImmediate(currentRenderTexture);
            }

            int width = (int)(Camera.scaledPixelWidth * m_RenderTextureScale);
            int height = (int)(Camera.scaledPixelHeight * m_RenderTextureScale);
            
            currentRenderTexture = new RenderTexture(width, height, 24);
            currentRenderTexture.format = RenderTextureFormat.ARGB32;
            Camera.targetTexture = currentRenderTexture;
        }

        private void OnDestroy()
        {
            if (currentRenderTexture != null)
            {
                Camera.targetTexture = null; 
                currentRenderTexture.Release();
                DestroyImmediate(currentRenderTexture);
            }
        }

        public void ToggleOffset()
        {
            Offset = -Offset;
        }

        public void SetOffset(Vector3 offset)
        {   
            Debug.Log(offset);
            Offset = offset;
        }

        public Vector3 GetOffset()
        {
            return Offset;
        }

        public void UpdateWithOffset()
        {
            Transform mainCamTransform = MainCamera.transform;

            transform.SetPositionAndRotation(mainCamTransform.position + Offset, mainCamTransform.rotation);
        
            Camera.fieldOfView = MainCamera.fieldOfView;
        }

        
    }
}
