using Common;
using ns.Skill;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using UnityEditor.Animations;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;


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
            Debug.Log($"PreAttackEndFired事件添加成功，在第 {frameCount} 帧（{time} 秒）调用 {functionName}");
        }

        public void PlayTargetAnimation(string targetAnima, bool isInteracting)
        {
            anim.applyRootMotion = isInteracting;
            anim.SetBool("IsInteracting", isInteracting);
            anim.CrossFade(targetAnima, 0.2f);
        }

    }
}
