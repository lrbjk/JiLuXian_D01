using UnityEngine;

namespace ns.BuffSystem.Effects
{
    /// <summary>
    /// 描述：
    /// </summary>
    [CreateAssetMenu(menuName = "ItemEffects/HealEffect")]
    public class HealEffect : ItemEffect
    {
        public int HealAmount = 50;

        public override void ApplyEffect(GameObject user)
        {
            var health = user.GetComponent<EffectHandler>();
            if (health != null)
            {
                health.ApplyHeal(HealAmount);
            }
        }
    }
}
