using UnityEngine;


namespace ns.Character.Player
{
    /// <summary>
    /// 描述：
    /// </summary>
    public class PlayerAnimationHandler : MonoBehaviour
    {
        private Animator anim;

        private void Start()
        {
            anim = GetComponentInChildren<Animator>(true);
        }

        public void PlayTargetAnimation(string targetAnima, bool isInteracting)
        {
            anim.applyRootMotion = isInteracting;
            anim.SetBool("IsInteracting", isInteracting);
            anim.CrossFade(targetAnima, 0.2f);
        }

    }
}
