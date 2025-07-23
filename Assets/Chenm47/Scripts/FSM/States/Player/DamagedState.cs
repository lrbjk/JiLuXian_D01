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
        }

        protected override MovtionInfo InitMovtionInfo(FSMBase fSMBase)
        {
            return fSMBase.movtionManager.GetMovtionInfo(fSMBase.characterInfo.CurrentMovtionID);
        }
    }
}
