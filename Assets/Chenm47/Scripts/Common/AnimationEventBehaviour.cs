using UnityEngine;

namespace Common
{
    /// <summary>
    /// 描述：动画事件行为类、特定时机触发事件
    /// </summary>
    public class AnimationEventBehaviour : MonoBehaviour
    {
        private Animator animator;

        private void Start()
        {
            animator = GetComponent<Animator>();
        }
        /// <summary>
        /// 由Unity调用
        /// </summary>
        /// <param name="animaparam"></param>
        private void OnCancelAnim(string animaparam)
        {
            animator.SetBool(animaparam, false);
        }
    }
}
