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
    public class DamagedTest : MonoBehaviour
    {
        public PlayerAction playerAction;

        private void OnGUI()
        {
            if (GUILayout.Button("受伤"))
            {
                playerAction.Damaged(10, 9, 10);
            }
        }
    }
}
