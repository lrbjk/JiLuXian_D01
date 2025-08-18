using AI.FSM.Framework;
using ns.Movtion;
using UnityEngine;

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
            //fSMBase.characterInfo.IsDied=false;
            PlayerFSMBase.Instance.playerMotor3D.StopMove();
            fSMBase.characterInfo.IsDied = false;
            Debug.Log("Die");
        }

        protected override MovtionInfo InitMovtionInfo(FSMBase fSMBase)
        {
            return fSMBase.movtionManager.GetMovtionInfo(fSMBase.characterInfo.DiedMovtionID);
        }
    }
}
