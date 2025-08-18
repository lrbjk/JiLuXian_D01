using AI.FSM.Framework;
using ns.Movtion;

namespace AI.FSM
{
    /// <summary>
    /// 描述：
    /// </summary>
    public class DamagedState : MovtionState
    {
        public override void Init()
        {
            StateID = FSMStateID.Damaged;
        }
        public override void EnterState(FSMBase fSMBase)
        {
            base.EnterState(fSMBase);
            fSMBase.characterInfo.IsDamaged = false;
            //启用RootMovtion
            PlayerFSMBase.Instance.playerMotor3D.ApplyAnimaMotionAll = true;
        }

        public override void ExitState(FSMBase fSMBase)
        {
            base.ExitState(fSMBase);
            PlayerFSMBase.Instance.playerMotor3D.ApplyAnimaMotionAll = false;
        }
    }
}
