using AI.FSM;
using ns.BagSystem;
using ns.Character.Player;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
        public Rigidbody rb;
        public CapsuleCollider capsuleCollider;
        public LayerMask obstacleMask;

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

        private Vector3 targetPosition;
        private Vector3 verticalVelocity; // 竖直方向速度

        private void Awake()
        {
            targetPosition = transform.position;
        }

        //private void Update()
        //{
        //    verticalVelocity +=
        //}

        Vector3 start;
        Vector3 end;
        private void FixedUpdate()
        {
            //targetPosition += verticalVelocity * Time.fixedDeltaTime; // 应用重力影响
            rb.velocity += Physics.gravity * Time.fixedDeltaTime;// 模拟重力加速度

            ////障碍检测
            //float capsuleRadius = capsuleCollider.radius;
            //float capsuleHeight = capsuleCollider.height;
            //start = transform.position - Vector3.up * (capsuleHeight * 0.5f - capsuleRadius);
            //end = transform.position + Vector3.up * (capsuleHeight * 0.5f - capsuleRadius);
            //Vector3 moveDir = targetPosition - rb.position;
            //float moveDist = moveDir.magnitude;
            //// 检测移动路径是否会撞到障碍
            //if (Physics.CapsuleCast(start, end, capsuleRadius, moveDir, out RaycastHit hit, moveDist, obstacleMask))
            //{
            //    // 会撞，移动到碰撞点前capsule半径里
            //    //float safeDist = hit.distance - 0.01f;
            //    //targetPosition += moveDir * Mathf.Max(safeDist, 0f);
            //    targetPosition = Vector3.Project((hit.point - rb.position), moveDir) - moveDir * capsuleRadius;
            //    Debug.Log("目标位置" + targetPosition);
            //}


            //rb.MovePosition(targetPosition); // 移动刚体到目标位置
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(start, capsuleCollider.radius);
            Gizmos.DrawSphere(end, capsuleCollider.radius);
        }

    }
}
