using AI.FSM.Framework;
using ns.Movtion;

namespace AI.FSM
{
    /// <summary>
    /// 描述：
    /// </summary>
    public class DiedState : MovtionState
    {
        public override void Init()
        {
            StateID = FSMStateID.Died;
        }

        public override void EnterState(FSMBase fSMBase)
        {
            base.EnterState(fSMBase);
            fSMBase.characterInfo.IsDied=false;
        }

        protected override MovtionInfo InitMovtionInfo(FSMBase fSMBase)
        {
            return fSMBase.movtionManager.GetMovtionInfo(fSMBase.characterInfo.CurrentMovtionID);
        }
    }
}
