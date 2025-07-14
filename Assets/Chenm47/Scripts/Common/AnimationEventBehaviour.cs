using System;
using UnityEngine;

namespace Common
{

    public class AnimationEventArgs : EventArgs
    {
        public AnimationEventArgs()
        {
        }
    }

    /// <summary>
    /// 描述：动画事件行为类、特定时机触发事件
    /// </summary>
    public class AnimationEventBehaviour : MonoBehaviour
    {
        /// <summary>攻击帧开始事件</summary>
        public event EventHandler<AnimationEventArgs> OnPreAttackEnd;
        public event EventHandler<AnimationEventArgs> OnAttackStart;
        public event EventHandler<AnimationEventArgs> OnAttackEnd;
        public event EventHandler<AnimationEventArgs> OnAttackRecovery;
        private Animator animator;

        private void Start()
        {
            animator = GetComponent<Animator>();

        }

        private void PreAttackEndFired()
        {
            print("前摇结束帧事件触发");
            OnPreAttackEnd?.Invoke(this, new AnimationEventArgs());
        }

        private void AttackStartFired()
        {
            print("攻击开始事件触发");
            OnAttackStart?.Invoke(this, new AnimationEventArgs());
        }

        private void AttackEndFired()
        {
            print("攻击结束事件触发");
            OnAttackEnd?.Invoke(this, new AnimationEventArgs());
        }

        private void AttackRecoveryFired()
        {
            print("后摇开始事件触发");
            OnAttackRecovery?.Invoke(this, new AnimationEventArgs());
        }

    }
}
