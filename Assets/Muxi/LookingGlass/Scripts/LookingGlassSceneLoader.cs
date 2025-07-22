using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

namespace LookingGlass
{

    public class LookingGlassSceneLoader : MonoBehaviour
    {
        //What scene to load
        public string SceneName;
    
        [SerializeField]
        private Volume CurrentVolume;

        [SerializeField]
        private Volume DestinationVolume;
        

        public LookingGlassController screenController;
        public Transform ReferencePoint;
        public bool SkipLoading;
        private GameObject Root;
        private int LoadedIndex;
    
        public void Start()
        {
            if (!SkipLoading)
            {
                LoadScene();
            }
         
            
        }
        // private IEnumerator WaitAndExecute()
        // {
        //     yield return new WaitForSeconds(3f);
        //
        //     EnableScene();
        //    
        // }
        // private void OnTriggerEnter(Collider other)
        // {
        //     if (other.CompareTag("Player"))
        //     {
        //         EnableScene();
        //     }
        // }

        public void EnableScene()
        {
            LookingGlassManager.EnableScene(this);
        }

        public void DisableScene()
        {
            LookingGlassManager.DisableScene(this);
        }

        private void OnTriggerExit(Collider other)
        {
            DisableScene();
        }

        private void LoadScene()
        {
        
            SceneManager.LoadSceneAsync(SceneName, LoadSceneMode.Additive);

        }

        public void SetVolumeWeights(float weight)
        {
            if (CurrentVolume != null)
            {
                CurrentVolume.weight = weight;
            }

            if (DestinationVolume != null)
            {
                DestinationVolume.weight = 1 - weight;
            }
        }

        public void SetCurrentVolume(Volume volume)
        {
            CurrentVolume = volume;
        }

        public Volume GetDestinationVolume()
        {
            return DestinationVolume;
        }
    }
}
