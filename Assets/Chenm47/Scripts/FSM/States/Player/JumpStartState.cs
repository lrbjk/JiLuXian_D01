using AI.FSM.Framework;

namespace AI.FSM
{
    /// <summary>
    /// 描述：没有前摇后摇等
    /// </summary>
    public class JumpStartState : FSMState
    {
        public override void Init()
        {
            StateID = FSMStateID.JumpStart;
        }

        public override void EnterState(FSMBase fSMBase)
        {
            base.EnterState(fSMBase);
            PlayerFSMBase playerFSMBase = fSMBase as PlayerFSMBase;
            playerFSMBase.playerAction.Jump();//内部物理引擎处理跳跃运动
            //播放动画
            fSMBase.animationHandler.PlayTargetAnimation("JumpStart", true, 0.01f);
        }

    }
}
