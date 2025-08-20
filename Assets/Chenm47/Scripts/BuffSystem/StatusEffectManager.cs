using ns.Character.Player;
using System.Collections.Generic;
using UnityEngine;

namespace ns.BuffSystem
{
    /// <summary>
    /// 描述：
    /// </summary>
    public class StatusEffectManager : MonoBehaviour
    {
        private class ActiveEffect
        {
            public StatusEffect Effect;
            public float TimeRemaining;

            public ActiveEffect(StatusEffect effect)
            {
                Effect = effect;
                TimeRemaining = effect.Duration;
            }
        }

        private PlayerInfo stats;
        private List<ActiveEffect> activeEffects = new List<ActiveEffect>();

        private void Awake()
        {
            stats = GetComponent<PlayerInfo>();
        }

        private void Update()
        {
            float deltaTime = Time.deltaTime;

            for (int i = activeEffects.Count - 1; i >= 0; i--)
            {
                var ae = activeEffects[i];
                ae.TimeRemaining -= deltaTime;
                ae.Effect.OnUpdate(stats, deltaTime);

                if (ae.TimeRemaining <= 0)
                {
                    ae.Effect.OnRemove(stats);
                    activeEffects.RemoveAt(i);
                }
            }
        }

        public void AddEffect(StatusEffect effect)
        {
            if (!effect.IsStackable)
            {
                // 如果不可叠加，先移除同类效果
                RemoveEffect(effect.GetType());
            }

            var ae = new ActiveEffect(effect);
            ae.Effect.OnApply(stats);
            activeEffects.Add(ae);
        }

        public void RemoveEffect(System.Type type)
        {
            for (int i = activeEffects.Count - 1; i >= 0; i--)
            {
                if (activeEffects[i].Effect.GetType() == type)
                {
                    activeEffects[i].Effect.OnRemove(stats);
                    activeEffects.RemoveAt(i);
                }
            }
        }
    }
}
