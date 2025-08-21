using Cinemachine;
using System;
using UnityEngine;

namespace ns.Camera.Shake
{
    public enum ShakeType
    {
        Default,
        PlayerHit,      // 玩家受击
        EnemyHit,       // 敌人受击
        BossAttack,     // Boss攻击（范围大）
        Explosion       // 特殊爆炸
    }
    [Serializable]
    /// <summary>
    /// 描述：
    /// </summary>
    public class ShakeRequest
    {
        public NoiseSettings ShakeNoiseSetting;
        public Vector3 PivotOffset;
        public float AmplitudeGain;
        public float FrequencyGain;
        public float Duration;
        public AnimationCurve decayCurve;

        public ShakeRequest(float amp, float freq, float dur, AnimationCurve curve = null)
        {
            AmplitudeGain = amp;
            FrequencyGain = freq;
            Duration = dur;
            decayCurve = curve ?? AnimationCurve.Linear(0, 1, 1, 0); // 默认线性衰减
        }
    }
}
