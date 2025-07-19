using AI.FSM.Framework;
using ns.Character.Player;
using UnityEngine;

/*

*/
namespace AI.FSM
{
    /// <summary>
    /// 描述：
    /// </summary>
    public class FallState : FSMState
    {
        private float fallTimer = 0f;
        public override void Init()
        {
            StateID = FSMStateID.Fall;
        }

        public override void EnterState(FSMBase fSMBase)
        {
            base.EnterState(fSMBase);
            PlayerInfo playerInfo = fSMBase.characterInfo as PlayerInfo;
            fSMBase.animator.Play("Fall");
            playerInfo.FallTimer = 0f;
        }

        public override void ActionState(FSMBase fSMBase)
        {
            base.ActionState(fSMBase);
            PlayerInfo playerInfo = fSMBase.characterInfo as PlayerInfo;
            playerInfo.FallTimer += Time.deltaTime;
        }

    }
}
