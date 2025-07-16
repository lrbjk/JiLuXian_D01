using AI.FSM;
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
    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //仅当处于后摇阶段才有可能动画结束
        var isInAtkRecoveryTrigger =
             animator.GetComponentInParent<PlayerFSMBase>().playerInfo.IsInAttackRecoveryFlag;
        if (isInAtkRecoveryTrigger)
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
