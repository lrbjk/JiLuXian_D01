using AI.FSM;
using ns.Character.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*

*/
namespace ns.PlayerTest
{
    /// <summary>
    /// 描述：
    /// </summary>
    public class BlankTest : MonoBehaviour
    {
        public Animator animator;
        public PlayerRootMotion rootMotion;

        private void OnGUI()
        {
            if (GUILayout.Button("Play"))
            {
                rootMotion.ApplyAnimaMotionAll = true;
                rootMotion.ApplyAnimatRotationY = true;
                PlayerFSMBase.Instance.playerMotor3D.SetRbGravity(false);
                animator.Play("Test");
            }
        }
    }
}
