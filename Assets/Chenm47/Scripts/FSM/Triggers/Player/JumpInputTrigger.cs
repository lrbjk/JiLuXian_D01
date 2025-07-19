using AI.FSM.Framework;
using ns.Character.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*

*/
namespace AI.FSM
{
    /// <summary>
    /// 描述：
    /// </summary>
    public class JumpInputTrigger : OnGroundTrigger
    {
        public override bool HandleTrigger(FSMBase fSMBase)
        {
            // 检查是否有跳跃输入
            PlayerFSMBase playerFSMBase = fSMBase as PlayerFSMBase;
            PlayerInput playerInput = playerFSMBase.playerInput;
            //Debug.Log("input" + playerInput.Jump + "地面" + base.HandleTrigger(fSMBase));
            return playerInput.Jump &&
                base.HandleTrigger(fSMBase);//在地面
        }

        public override void Init()
        {
            triggerID = FSMTriggerID.JumpInput;
        }
    }
}
