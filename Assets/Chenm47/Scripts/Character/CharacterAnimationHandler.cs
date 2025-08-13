using ns.Movtion;
using UnityEditor.Animations;
using UnityEngine;

namespace ns.Character
{
    /// <summary>
    /// 描述：
    /// </summary>
    public class CharacterAnimationHandler : MonoBehaviour
    {
        protected Animator anim;
        protected CharacterMovtionManager characterMovtionManager;

        private void Awake()
        {
            anim = GetComponentInChildren<Animator>(true);
        }
        private void Start()
        {
            characterMovtionManager = GetComponent<CharacterMovtionManager>();
            AnimatorController controller = anim.runtimeAnimatorController as AnimatorController;

            //处理动画事件
            foreach (var movtionInfo in characterMovtionManager.GetAllMovtionInfos())
            {
                foreach (var state in controller.layers[0].stateMachine.states)
                {
                    if (state.state.name == movtionInfo.AnimationName)
                    {
                        switch (state.state.motion)
                        {
                            case AnimationClip clip:
                                // 处理单个动画片段
                                foreach (var movtionEvent in movtionInfo.MovtionEvents)
                                {
                                    AddAnimationEvent(clip, movtionEvent.AnimationFrame, movtionEvent.EventType.ToString() + "Fired");
                                }
                                break;
                            case BlendTree blendTree:
                                // 处理混合树中的所有子运动
                                foreach (var child in blendTree.children)
                                {
                                    foreach (var movtionEvent in movtionInfo.MovtionEvents)
                                    {
                                        AddAnimationEvent(child.motion as AnimationClip, movtionEvent.AnimationFrame, movtionEvent.EventType.ToString() + "Fired");
                                    }
                                }
                                break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 播放目标动画
        /// </summary>
        /// <param name="targetAnima">目标动画状态名称</param>
        /// <param name="isInteracting">是否交互</param>
        /// <param name="normalizedTransitionDuration">过度时间</param>
        public virtual void PlayTargetAnimation(string targetAnima, bool isInteracting, float normalizedTransitionDuration)
        {
            Debug.Log($"播放动画：{targetAnima}，是否交互：{isInteracting}");//本质是用于标记动画是否完结
            anim.SetBool("IsInteracting", isInteracting);
            anim.CrossFade(targetAnima, normalizedTransitionDuration);
            //anim.CrossFadeInFixedTime(targetAnima, normalizedTransitionDuration);
            //anim.Play(targetAnima);//直接播放动画，没有过度
        }
        /// <summary>
        /// 播放目标动画，使用固定时间过度
        /// </summary>
        /// <param name="targetAnima">目标动画状态名称</param>
        /// <param name="isInteracting">是否交互</param>
        /// <param name="normalizedTransitionDuration">过度时间</param>
        public virtual void PlayTargetAnimationFixed(string targetAnima, bool isInteracting, float normalizedTransitionDuration)
        {
            Debug.Log($"播放动画：{targetAnima}，是否交互：{isInteracting}");//本质是用于标记动画是否完结
            anim.SetBool("IsInteracting", isInteracting);
            //anim.CrossFade(targetAnima, normalizedTransitionDuration);
            anim.CrossFadeInFixedTime(targetAnima, normalizedTransitionDuration);
            //anim.Play(targetAnima);//直接播放动画，没有过度
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

    }
}
