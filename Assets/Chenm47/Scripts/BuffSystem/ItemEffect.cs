using UnityEngine;

namespace ns.BuffSystem
{
    /// <summary>
    /// 描述：
    /// </summary>
    public abstract class ItemEffect : ScriptableObject, IEffect
    {
        public abstract void ApplyEffect(GameObject user);
    }
}
