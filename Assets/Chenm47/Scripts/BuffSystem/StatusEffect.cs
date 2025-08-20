using ns.Character.Player;
using UnityEngine;


namespace ns.BuffSystem
{
    /// <summary>
    /// 描述：
    /// </summary>
    public abstract class StatusEffect : ScriptableObject
    {
        public string EffectName;
        public string Description;
        public float Duration;

        // 进入状态时调用
        public abstract void OnApply(PlayerInfo target);

        // 状态持续时，每帧或每tick调用
        public abstract void OnUpdate(PlayerInfo target, float deltaTime);

        // 状态结束时调用
        public abstract void OnRemove(PlayerInfo target);

        // 是否可叠加
        public virtual bool IsStackable => false;
    }
}
