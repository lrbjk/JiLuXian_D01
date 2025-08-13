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
        public override void Init()
        {
            StateID = FSMStateID.Fall;
        }

        public override void EnterState(FSMBase fSMBase)
        {
            base.EnterState(fSMBase);
            PlayerInfo playerInfo = fSMBase.characterInfo as PlayerInfo;
            playerInfo.FallTimer = 0f;
            playerInfo.IsOnTop = false;
            //启用重力
            PlayerFSMBase.Instance.playerMotor3D.SetRbGravity(true);
        }

        public override void ActionState(FSMBase fSMBase)
        {
            base.ActionState(fSMBase);
            PlayerInfo playerInfo = fSMBase.characterInfo as PlayerInfo;
            playerInfo.FallTimer += Time.deltaTime;
            //Debug.Log("FallTimer" + playerInfo.FallTimer);
            PlayerFSMBase playerFSMBase = (PlayerFSMBase)fSMBase;

            fSMBase.animator.SetFloat("Vy", playerFSMBase.playerAction.GetVelocity().y);
        }

        public override void ExitState(FSMBase fSMBase)
        {
            base.ExitState(fSMBase);
            //禁用重力
            PlayerFSMBase.Instance.playerMotor3D.SetRbGravity(false);
            //将玩家位置更新到地面位置
            PlayerFSMBase.Instance.playerMotor3D.UpdateToGround();
            //if (Physics.Raycast(fSMBase.transform.position, Vector3.down, out RaycastHit hit, PlayerFSMBase.Instance.playerMotor3D.GroundSphereRadius + 0.05f,
            //     PlayerFSMBase.Instance.playerMotor3D.GroundLayer))
            //{
            //    Debug.Log("更新玩家位置到地面");
            //    var pos = PlayerFSMBase.Instance.transform.position;
            //    pos.y = hit.point.y;
            //    //PlayerFSMBase.Instance.transform.position = pos;
            //    PlayerFSMBase.Instance.playerMotor3D.
            //}
        }

    }
}
