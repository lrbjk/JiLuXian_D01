using UnityEngine;
using UnityEngine.SceneManagement;

namespace LookingGlass{
   public class LookingGlassSceneData : MonoBehaviour
   {
      public GameObject mainlight;
      public Material skybox;
      public Cubemap reflcubemap;
      public GameObject root;
      public Scene Scene;

      public bool EnableFog;
      public bool EnablePostProcessing;
      public Transform SpawnTransform;
      public bool SceneActive;

      public int RendererIndex;
      private void Start()
      {
         SetUp();
      }
      private void Awake()
      {
         Scene = gameObject.scene;
         
         //Disable objects that shouldn't be used in a multi scene setup
         foreach (var ob in Scene.GetRootGameObjects())
         {
            if (ob != gameObject && ob != root && SceneActive)
            {
               ob.SetActive(false);
            }
         }
      }
      private void SetUp()
      {
         //Register scene
         LookingGlassManager.RegisterScene(Scene.name, this);
      }
   }
}
