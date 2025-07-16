using ns.Skill;
using UnityEditor.Animations;
using UnityEngine;


namespace ns.Character.Player
{
    /// <summary>
    /// 描述：
    /// </summary>
    public class PlayerAnimationHandler : MonoBehaviour
    {
        private Animator anim;
        private CharacterSkillManager characterSkillManager;

        private void Start()
        {
            anim = GetComponentInChildren<Animator>(true);
            characterSkillManager = GetComponent<CharacterSkillManager>();
            AnimatorController controller = anim.runtimeAnimatorController as AnimatorController;

            //处理动画事件
            foreach (var skillInfo in characterSkillManager.GetAllSkillInfos())
            {
                foreach (var state in controller.layers[0].stateMachine.states)
                {
                    if (state.state.name == skillInfo.AnimationName)
                    {
                        var clip = state.state.motion as AnimationClip;
                        AddAnimationEvent(clip, skillInfo.PreAttackEndFrame, "PreAttackEndFired");
                        AddAnimationEvent(clip, skillInfo.AttackStartFrame, "AttackStartFired");
                        AddAnimationEvent(clip, skillInfo.AttackEndFrame, "AttackEndFired");
                        AddAnimationEvent(clip, skillInfo.AttackRecoveryFrame, "AttackRecoveryFired");
                    }
                }
            }
        }

        private static void AddAnimationEvent(AnimationClip clip, float frameCount, string functionName)
        {
            float frameRate = clip.frameRate;
            float time = frameCount / frameRate;

            AnimationEvent animEvent = new AnimationEvent();
            animEvent.functionName = functionName;
            animEvent.time = time;

            clip.AddEvent(animEvent);
            Debug.Log($"{functionName}事件添加成功，在第 {frameCount} 帧（{time} 秒）调用 {functionName}");
        }

        public void PlayTargetAnimation(string targetAnima, bool isInteracting)
        {
            Debug.Log($"播放动画：{targetAnima}，是否交互：{isInteracting}");
            anim.applyRootMotion = isInteracting;
            anim.SetBool("IsInteracting", isInteracting);
            anim.CrossFade(targetAnima, 0.2f);
            //anim.Play(targetAnima);//直接播放动画，没有过度
        }

    }
}
