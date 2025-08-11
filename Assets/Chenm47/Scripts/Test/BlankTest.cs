using AI.FSM;
using ns.BagSystem;
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
        private void OnGUI()
        {
            //if (GUILayout.Button("获得核心1"))
            //{
            //    InventoryManager.Instance.AddItem(new BagSystem.Freamwork.Item(ItemInfoManager.GetItemInfo("核心1")));
            //}

            //if (GUILayout.Button("查看当前所有物品"))
            //{
            //    foreach (var item in InventoryManager.Instance.GetAllItems())
            //    {
            //        print(item.itemInfo.ItemName + " x" + item.CurrentCount);
            //    }
            //}

            if (GUILayout.Button("播放"))
            {
                animator.CrossFade("Roll", 0.1f, -1, 0);
                //animator.Play("Roll", 0, 0);
            }

        }
    }
}
