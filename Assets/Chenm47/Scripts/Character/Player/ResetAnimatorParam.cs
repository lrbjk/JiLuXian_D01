using AI.FSM;
using AI.FSM.Framework;
using UnityEngine;

public class ResetAnimatorParam : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}
    public string ParamName = "IsInteracting";
    public bool ParamValue = false;
    [Tooltip("是否需要在后摇阶段才触发重置参数")]
    public bool ShouldMovtionRecoveryTrigger = true;
    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!ShouldMovtionRecoveryTrigger)
        {
            animator.SetBool(ParamName, ParamValue);
            Debug.Log(Time.frameCount + "Animator退出状态，重置参数：" + ParamName + " 为 " + ParamValue);
            return;
        }
        //仅当处于后摇阶段动画结束
        var isInMovtionRecoveryTrigger =
             animator.GetComponentInParent<FSMBase>().characterInfo.IsInMovtionRecoveryFlag;
        if (isInMovtionRecoveryTrigger)
        {
            animator.SetBool(ParamName, ParamValue);
            Debug.Log(Time.frameCount + "Animator退出状态且处于后摇阶段，重置参数：" + ParamName + " 为 " + ParamValue);
        }
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
