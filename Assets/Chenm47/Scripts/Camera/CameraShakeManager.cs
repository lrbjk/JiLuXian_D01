using Cinemachine;
using System.Collections;
using UnityEngine;

namespace ns.Camera.Shake
{
    /// <summary>
    /// 描述：
    /// </summary>
    public class CameraShakeManager : MonoSingleton<CameraShakeManager>
    {
        protected override void Init()
        {
            base.Init();
            vcam = GetComponent<CinemachineVirtualCamera>();
            noise = vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }

        private CinemachineVirtualCamera vcam;
        private CinemachineBasicMultiChannelPerlin noise;

        private Coroutine currentShake;

        public void RequestCustomShake(ShakeRequest request)
        {
            //Debug.LogWarning("镜头摇晃开始");
            if (currentShake != null) StopCoroutine(currentShake);
            currentShake = StartCoroutine(DoShake(request));
        }

        private IEnumerator DoShake(ShakeRequest request)
        {
            float timer = 0f;
            noise.m_NoiseProfile = request.ShakeNoiseSetting;
            while (timer < request.Duration)
            {
                float t = timer / request.Duration;
                float strength = request.decayCurve.Evaluate(t);

                noise.m_AmplitudeGain = request.AmplitudeGain * strength;
                noise.m_FrequencyGain = request.FrequencyGain;

                timer += Time.deltaTime;
                yield return null;
            }

            noise.m_AmplitudeGain = 0f;
            noise.m_FrequencyGain = 0f;
            currentShake = null;
        }

    }
}
