using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

namespace LookingGlass
{
    public class LookingGlassManager:SingletonMono<LookingGlassManager>
    {
        private Camera MainCamera;
        private Camera ScreenCamera;
        private Dictionary<string, LookingGlassSceneData> registeredScenes;
        private LookingGlassSceneData screenScene;
        private LookingGlassSceneData currentScene;
        private LookingGlassController Controller;
        private Transform spawnTransform;
        private LookingGlassSceneLoader Loader;

        protected override void Awake()
        {
            base.Awake(); 
            setup();
        }

        public void setup()
        {
            registeredScenes = new Dictionary<string, LookingGlassSceneData>();
            
            MainCamera = GameObject.FindGameObjectWithTag("MainCamera")?.GetComponent<Camera>();
            if (MainCamera == null)
            {
                Debug.LogError("LookingGlassManager: MainCamera not found");
            }
            ScreenCamera = GameObject.FindGameObjectWithTag("Screen Camera")?.GetComponent<Camera>();
            if (ScreenCamera == null)
            {
                Debug.LogError("LookingGlassManager: ScreenCamera not found");
            }
            
            
        }
        public static RenderTexture  GetScreenRT()
        {
            return Instance.ScreenCamera.activeTexture;
        }
        public static void RegisterScene(string name, LookingGlassSceneData metaData)
        {
            // 确保单例实例存在
            if (Instance == null)
            {
                Debug.LogError("LookingGlassManager instance not found. Make sure there's a LookingGlassManager in the scene.");
                return;
            }
            
            // 确保字典已初始化
            if (Instance.registeredScenes == null)
            {
                Instance.setup();
            }
            
            // 确保单例实例存在
            if (Instance == null)
            {
                Debug.LogError("LookingGlassManager instance not found. Make sure there's a LookingGlassManager in the scene.");
                return;
            }
            
            // 确保字典已初始化
            if (Instance.registeredScenes == null)
            {
                Instance.setup();
            }
            
            // 确保单例实例存在
            if (Instance == null)
            {
                Debug.LogError("LookingGlassManager instance not found. Make sure there's a LookingGlassManager in the scene.");
                return;
            }
            
            // 确保字典已初始化
            if (Instance.registeredScenes == null)
            {
                Instance.setup();
            }
            
            Instance.registeredScenes.Add(name, metaData);

            if (Instance.currentScene == null) 
            {
                Instance.currentScene = metaData;
            }
        }
        
        public static void EnableScene(LookingGlassSceneLoader sceneLoader)
        {
            LookingGlassSceneData sceneMetaData = Instance.registeredScenes[sceneLoader.SceneName];

            if (sceneMetaData == null)
            {
                throw new Exception("Trying to enable unregistered scene");
            }

            Debug.Log("Enabling this scene: " + sceneMetaData.Scene.name);

            Instance.Loader = sceneLoader;
            Instance.screenScene = sceneMetaData;

            LightProbes.TetrahedralizeAsync();

            
            sceneMetaData.root.SetActive(true);
            

      
            if (sceneMetaData.SpawnTransform != null)
            {
                Instance.ScreenCamera.GetComponent<VirtualCamera>().SetOffset(
                    sceneMetaData.SpawnTransform.position - Instance.Loader.ReferencePoint.position);
            }


            if (sceneLoader.screenController != null)
            {
                sceneLoader.screenController.enableLookingGlass();
            }
        
          
            int index = sceneMetaData.RendererIndex > 1 ? sceneMetaData.RendererIndex : 1;
            Instance.ScreenCamera.GetComponent<UniversalAdditionalCameraData>().SetRenderer(index);
        
            Instance.ScreenCamera.GetComponent<Camera>().enabled = true;
            
        }

        public static void DisableScene(LookingGlassSceneLoader sceneLoader){}
        
        void OnBeginCameraRendering(ScriptableRenderContext context, Camera camera)
        {
            bool isMainCamera = camera.CompareTag("MainCamera");

            if (!isMainCamera && screenScene == null)
            {
                return;
            }
        
            //Toggle main light
            if (camera.cameraType == CameraType.SceneView)
            {
                ToggleMainLight(currentScene, true);
                ToggleMainLight(screenScene, false);
            }
            else
            {
                ToggleMainLight(currentScene, isMainCamera);
                ToggleMainLight(screenScene, !isMainCamera);
            }

            //Setup render settings
            LookingGlassSceneData sceneToRender = isMainCamera || camera.cameraType == CameraType.SceneView ? currentScene : screenScene;
           
            // RenderSettings.skybox = sceneToRender.skybox;
            // if (sceneToRender.reflection != null)
            // {
            //     RenderSettings.customReflectionTexture = sceneToRender.reflection;
            // }

            if (!isMainCamera && camera.cameraType == CameraType.Game)
            {
                camera.GetComponent<VirtualCamera>().UpdateWithOffset();
            }
        }

        private void ToggleMainLight(LookingGlassSceneData scene, bool value)
        {
            if (scene != null && scene.mainlight != null)
            {
                scene.mainlight.SetActive(value);
            }
        }
        
        
        private void OnEnable()
        {
            RenderPipelineManager.beginCameraRendering += OnBeginCameraRendering;
          
        }

        private void OnDisable()
        {
            RenderPipelineManager.beginCameraRendering -= OnBeginCameraRendering;
      
        }
    }
}
