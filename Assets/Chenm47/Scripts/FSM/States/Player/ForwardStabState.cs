using AI.FSM.Framework;
using UnityEngine;

/*

*/
namespace AI.FSM
{
    /// <summary>
    /// 描述：
    /// </summary>
    public class ForwardStabState : FSMState
    {
        public override void Init()
        {
            StateID = FSMStateID.ForwardStab;
        }
        public override void EnterState(FSMBase fSMBase)
        {
            base.EnterState(fSMBase);
            var playerFSMBase = fSMBase as PlayerFSMBase;
            playerFSMBase.playerAction.StopMove();
            //移动玩家到敌人身前
            var standingPos = fSMBase.characterInfo.BackStabedTarget.ForwardStabedStandingTF.position;
            playerFSMBase.playerAction.MoveDirectly(standingPos);
            //播放玩家正刺动画
            fSMBase.animationHandler.PlayTargetAnimation("ForwardStab", true, 0.01f);
            ////暂时直接播放指定背刺攻击动作    后续可能会改成提供的接口
            //var enemyAnimator = fSMBase.characterInfo.BackStabedTarget.GetComponent<CharacterAnimationHandler>();
            //enemyAnimator.PlayTargetAnimation("BackStabed", true, 0.01f);            //播放敌人背刺受击动画
            //计算伤害
        }

        public override void ActionState(FSMBase fSMBase)
        {
            base.ActionState(fSMBase);
            var playerFSMBase = fSMBase as PlayerFSMBase;
            //旋转玩家
            Vector3 rotationDirection = fSMBase.characterInfo.BackStabedTarget.transform.position - fSMBase.transform.position;
            rotationDirection.y = 0;
            rotationDirection.Normalize();
            Quaternion tr = Quaternion.LookRotation(rotationDirection);
            if (Quaternion.Angle(tr, fSMBase.transform.rotation) > 1f)
            {
                fSMBase.transform.rotation = Quaternion.Slerp(fSMBase.transform.rotation, tr, Time.deltaTime * 100f);
            }
            else
            {
                fSMBase.transform.rotation = tr;
            }
        }
    }
}
