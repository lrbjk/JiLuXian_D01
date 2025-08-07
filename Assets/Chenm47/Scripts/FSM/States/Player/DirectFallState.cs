using AI.FSM.Framework;
using ns.Character.Player;

namespace AI.FSM
{
    /// <summary>
    /// 描述：
    /// </summary>
    public class DirectFallState : FallState
    {
        public override void Init()
        {
            StateID = FSMStateID.DirectFall;
        }

        public override void EnterState(FSMBase fSMBase)
        {
            base.EnterState(fSMBase);
            fSMBase.animator.CrossFade("DirectFall", 0.1f);
            //给一个向前的力
            PlayerAction playerAction = (fSMBase as PlayerFSMBase).playerAction;
            playerAction.playerMotor3D.AddFallPuchForce();
        }

    }
}
