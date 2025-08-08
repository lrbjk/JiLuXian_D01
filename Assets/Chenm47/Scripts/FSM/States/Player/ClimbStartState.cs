using AI.FSM.Framework;
using UnityEngine;

namespace AI.FSM
{
    /// <summary>
    /// 描述：
    /// </summary>
    public class ClimbStartState : FSMState
    {
        public override void Init()
        {
            StateID = FSMStateID.ClimbStart;
        }

        public override void EnterState(FSMBase fSMBase)
        {
            base.EnterState(fSMBase);
            //吸附到梯子点位
            MoveToClimbPoint();
            //开启rootmovtion
            PlayerFSMBase.Instance.playerRootMotion.ApplyAnimatRotationY = true;
            PlayerFSMBase.Instance.playerRootMotion.ApplyAnimaMotionAll = true;
            PlayerFSMBase.Instance.playerMotor3D.SetRbGravity(false);

            fSMBase.animator.SetFloat("Vertical", PlayerFSMBase.Instance.playerInfo.IsInUpClimbBox ? 1f : -1f);

            PlayerFSMBase.Instance.playerInfo.IsClimbLiftHandDown = true;
            //上：左手在下    下：左手在下
            fSMBase.animator.SetFloat("Horizontal", -1);

            //fSMBase.animationHandler.PlayTargetAnimation("ClimbStart", true, 0.1f);
            fSMBase.animationHandler.PlayTargetAnimation("ClimbStart", true, 0f);
        }


        private void MoveToClimbPoint()
        {
            Transform target = PlayerFSMBase.Instance.playerInfo.ClimbPosTF;
            Vector3 targetPos = target.position;
            targetPos.y = PlayerFSMBase.Instance.transform.position.y;
            //PlayerFSMBase.Instance.transform.SetPositionAndRotation(targetPos, Quaternion.LookRotation(target.forward));

            PlayerFSMBase.Instance.playerMotor3D.MovePositionOnly(targetPos);
            PlayerFSMBase.Instance.playerMotor3D.LookAtVentorNow(target.forward);

            Debug.Log(Time.frameCount.ToString() + "吸附到点位" + PlayerFSMBase.Instance.transform.position
                + "rotate" + PlayerFSMBase.Instance.transform.rotation.eulerAngles);
        }

    }
}
