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
        private void OnGUI()
        {
            if (GUILayout.Button("获得核心1"))
            {
                InventoryManager.Instance.AddItem(new BagSystem.Freamwork.Item(ItemInfoManager.GetItemInfo("核心1")));
            }

            if (GUILayout.Button("查看当前所有物品"))
            {
                foreach (var item in InventoryManager.Instance.GetAllItems())
                {
                    print(item.itemInfo.ItemName + " x" + item.CurrentCount);
                }
            }

        }
    }
}
