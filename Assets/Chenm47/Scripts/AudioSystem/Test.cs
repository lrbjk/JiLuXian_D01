using AK;
using Audio.Sys;
using Audio.WwiseAudio;
using UnityEngine;

/*

*/
namespace ns
{
    /// <summary>
    /// 描述：
    /// </summary>
    public class Test : MonoBehaviour
    {
        public float value = 50;

        private void OnGUI()
        {
            if (GUILayout.Button("Play"))
            {
                //AkSoundEngine.LoadBank(BANKS.INIT);
                //AkSoundEngine.LoadBank(BANKS.TESTSOUNDBANK);
                //AkSoundEngine.PostEvent(EVENTS.TESTEVENT, gameObject);
                WwiseAudioSystem audioSystem = AudioSystem.Instance as WwiseAudioSystem;
                audioSystem.PlayAudio(EVENTS.TESTEVENT, gameObject);
            }

            if (GUILayout.Button("Load"))
            {
                //AkSoundEngine.LoadBank(BANKS.INIT);
                //AkSoundEngine.LoadBank(BANKS.TESTSOUNDBANK);
                //AkSoundEngine.PostEvent(EVENTS.TESTEVENT, gameObject);
                WwiseAudioSystem audioSystem = AudioSystem.Instance as WwiseAudioSystem;
                audioSystem.LoadBank(BANKS.TESTSOUNDBANK);
            }
            if (GUILayout.Button("LoadAsync"))
            {
                WwiseAudioSystem audioSystem = AudioSystem.Instance as WwiseAudioSystem;
                float currentTime = Time.time;
                audioSystem.LoadBankAsync(BANKS.TESTSOUNDBANK, (_, _, _, _) =>
                {
                    Debug.Log($"LoadAsync bank:{BANKS.TESTSOUNDBANK} cost time:{Time.time - currentTime}");
                });
            }
            if (GUILayout.Button("Unload"))
            {
                WwiseAudioSystem audioSystem = AudioSystem.Instance as WwiseAudioSystem;
                float currentTime = Time.time;
                audioSystem.UnloadBank(BANKS.TESTSOUNDBANK);
            }
            if (GUILayout.Button("UnloadAsync"))
            {
                WwiseAudioSystem audioSystem = AudioSystem.Instance as WwiseAudioSystem;
                float currentTime = Time.time;
                audioSystem.UnloadBankAsync(BANKS.TESTSOUNDBANK, (_, _, _, _) =>
                {
                    Debug.Log($"UnloadAsync bank:{BANKS.TESTSOUNDBANK} cost time:{Time.time - currentTime}");
                });
            }
            if (GUILayout.Button("UnloadAll"))
            {
                WwiseAudioSystem audioSystem = AudioSystem.Instance as WwiseAudioSystem;
                audioSystem.UnloadAllBanks();
            }
            if (GUILayout.Button("UnloadAllAsync"))
            {
                WwiseAudioSystem audioSystem = AudioSystem.Instance as WwiseAudioSystem;
                float currentTime = Time.time;
                audioSystem.UnloadAllBanksAsync(() =>
                {
                    Debug.Log($"cost time:{Time.time - currentTime}");
                });
            }

            if (GUILayout.Button("reduce"))
            {
                //AkSoundEngine.LoadBank(BANKS.INIT);
                //AkSoundEngine.LoadBank(BANKS.TESTSOUNDBANK);
                //AkSoundEngine.PostEvent(EVENTS.TESTEVENT, gameObject);
                WwiseAudioSystem audioSystem = AudioSystem.Instance as WwiseAudioSystem;
                //audioSystem.PlayAudio(BANKS.TESTSOUNDBANK, gameObject);
                value -= 10;
                if (value < 0) value = 0;
                audioSystem.SetAudioVolume(Audio.Sys.AudioType.Sfx, value);
            }
            if (GUILayout.Button("Add"))
            {
                //AkSoundEngine.LoadBank(BANKS.INIT);
                //AkSoundEngine.LoadBank(BANKS.TESTSOUNDBANK);
                //AkSoundEngine.PostEvent(EVENTS.TESTEVENT, gameObject);
                WwiseAudioSystem audioSystem = AudioSystem.Instance as WwiseAudioSystem;
                //audioSystem.PlayAudio(BANKS.TESTSOUNDBANK, gameObject);
                value += 10;
                if (value > 100) value = 100;
                audioSystem.SetAudioVolume(Audio.Sys.AudioType.Sfx, value);
            }

        }
    }
}
