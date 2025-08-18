using AI.FSM;
using Common.UI;
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
        MainUIFunc mainUIFunc;
        private void Start()
        {
            mainUIFunc = UIManager.Instance.GetUILayerManager("MainUI") as MainUIFunc;
        }

        private void OnGUI()
        {
            if (GUILayout.Button("增加转换值"))
            {
                mainUIFunc.IncreaseEmotion(30);
            }
            if (GUILayout.Button("减小转换值"))
            {
                mainUIFunc.DecreaseEmotion(10);
            }
        }


        //public Animator animator;
        //public Rigidbody rb;
        //public CapsuleCollider capsuleCollider;
        ////public LayerMask obstacleMask;

        //private void OnGUI()
        //{
        //    //if (GUILayout.Button("获得核心1"))
        //    //{
        //    //    InventoryManager.Instance.AddItem(new BagSystem.Freamwork.Item(ItemInfoManager.GetItemInfo("核心1")));
        //    //}

        //    //if (GUILayout.Button("查看当前所有物品"))
        //    //{
        //    //    foreach (var item in InventoryManager.Instance.GetAllItems())
        //    //    {
        //    //        print(item.itemInfo.ItemName + " x" + item.CurrentCount);
        //    //    }
        //    //}

        //    if (GUILayout.Button("播放"))
        //    {
        //        animator.CrossFade("Roll", 0.1f, -1, 0);
        //        //animator.Play("Roll", 0, 0);
        //    }

        //}

        //private Vector3 targetPosition;
        //private Vector3 verticalVelocity; // 竖直方向速度

        //private void Awake()
        //{
        //    targetPosition = transform.position;
        //}

        ////private void Update()
        ////{
        ////    verticalVelocity +=
        ////}



        //Vector3 start;
        //Vector3 end;

        //private void Update()
        //{
        //    Debug.Log("Update" + Time.frameCount + "transform" + transform.position);
        //    Debug.Log("Update" + Time.frameCount + "rb" + rb.position);//始终相同
        //}

        //private void FixedUpdate()
        //{
        //    Debug.Log("FixedUpdate" + "1tf" + transform.position);
        //    Debug.Log("FixedUpdate" + "1rb" + rb.position);
        //    //rb.velocity = Vector3.down;
        //    //Debug.Log("2tf" + transform.position);
        //    //Debug.Log("2rb" + rb.position);
        //    //rb.MovePosition(rb.position + transform.forward * 1f);
        //    //Debug.Log("3tf" + transform.position);
        //    //Debug.Log("3rb" + rb.position);
        //    ////rb.AddForce(Vector3.left, ForceMode.Impulse);
        //    //rb.AddForce(Vector3.left * 10);
        //    //Debug.Log("4tf" + transform.position);
        //    //Debug.Log("4rb" + rb.position);
        //}
        ////障碍物检测
        //public float startUpOffest = 0.05f;
        //public float forwardCheckDistance = 0.2f;
        //public float stepHeight = 0.3f;//台阶高度
        //public float stepSmooth = 0.01f;
        //public float stepDownVelocity = 3f;
        //public LayerMask ObstacleLayer;

        //private void OnDrawGizmos()
        //{
        //    //Gizmos.color = Color.red;
        //    //Gizmos.DrawSphere(transform.position + forwardCheckDistance * transform.forward, GroundSphereRadius);//地面球
        //    Gizmos.color = Color.blue;
        //    Vector3 dir = transform.forward;

        //    Gizmos.DrawLine(transform.position + Vector3.up * startUpOffest, transform.position + Vector3.up * startUpOffest + transform.forward * forwardCheckDistance);// 低位射线
        //    Gizmos.DrawLine(transform.position + Vector3.up * stepHeight,
        //       transform.position + Vector3.up * stepHeight + transform.forward * forwardCheckDistance);// 高位射线

        //    Gizmos.color = Color.gray;
        //    //Gizmos.DrawLine(transform.position - Vector3.up * startUpOffest + transform.forward * forwardCheckDistance,//下台阶检测
        //    //    transform.position - Vector3.up * startUpOffest + transform.forward * forwardCheckDistance +
        //    //    Vector3.down * stepHeight);
        //    Gizmos.color = Color.red;
        //    Gizmos.DrawLine(transform.position + Vector3.up * stepHeight + transform.forward * forwardCheckDistance,
        //        transform.position + Vector3.up * stepHeight + transform.forward * forwardCheckDistance +
        //        Vector3.down * 0.1f);//前方台阶上表面射线
        //}

        //private void OnAnimatorMove()
        //{
        //    Debug.Log("AnimatorMove" + "1tf" + transform.position);
        //    Debug.Log("AnimatorMove" + "1rb" + rb.position);
        //    rb.velocity = animator.velocity;
        //    //前方障碍物检测
        //    Vector3 forward = transform.forward;

        //    // 低位射线
        //    RaycastHit lowerHit;
        //    bool isObstacle =
        //        Physics.Raycast(transform.position + Vector3.up * startUpOffest, forward, out lowerHit, forwardCheckDistance, ObstacleLayer) //前方
        //                                                                                                                                     //Physics.Raycast(transform.position + Vector3.up * startUpOffest, transform.TransformDirection(new Vector3(-1, 0, 1)), out lowerHit, forwardCheckDistance, ObstacleLayer) ||//左45
        //                                                                                                                                     //Physics.Raycast(transform.position + Vector3.up * startUpOffest, transform.TransformDirection(new Vector3(1, 0, 1)), out lowerHit, forwardCheckDistance, ObstacleLayer)//右45
        //        ;


        //    if (isObstacle)
        //    {
        //        Debug.Log("前方有障碍物" + lowerHit.collider.name);
        //        bool isHighObstacle =
        //            Physics.Raycast(transform.position + Vector3.up * stepHeight, forward, forwardCheckDistance, ObstacleLayer)//前方
        //                                                                                                                       //Physics.Raycast(transform.position + Vector3.up * stepHeight, transform.TransformDirection(new Vector3(-1, 0, 1)), forwardCheckDistance, ObstacleLayer) ||//左45
        //                                                                                                                       //Physics.Raycast(transform.position + Vector3.up * stepHeight, transform.TransformDirection(new Vector3(1, 0, 1)), forwardCheckDistance, ObstacleLayer)//右45
        //            ;
        //        // 高位射线
        //        if (!isHighObstacle)
        //        {
        //            Debug.Log("上台阶");
        //            ////rb.AddForce(Vector3.up * stepDownVelocity, ForceMode.Impulse);
        //            //rb.position += new Vector3(0f, stepSmooth, 0f);
        //            //前方台阶上表面射线
        //            if (Physics.Raycast(transform.position + Vector3.up * (stepHeight + 0.1f) + forward * forwardCheckDistance, Vector3.down, out RaycastHit planeHit, 0.3f, ObstacleLayer))
        //            {
        //                Vector3 newPos = rb.position;
        //                float shouldstepHeight = planeHit.point.y - rb.position.y;
        //                Debug.Log("需要抬升的高度" + shouldstepHeight);
        //                newPos.y = planeHit.point.y;
        //                rb.MovePosition(newPos);
        //                //rb.position = planeHit.point;
        //                Debug.Log("AnimatorMove" + "2tf" + transform.position);
        //                Debug.Log("AnimatorMove" + "2b" + rb.position);
        //                //Debug.DrawLine(planeHit.point, planeHit.normal);
        //            }
        //        }
        //    }

        //    ////targetRotation *= animator.deltaRotation;
        //    ////Debug.Log(Time.time + "OnAnimatorMove");
        //    //if (ApplyAnimaMotionAll)
        //    //{

        //    //    //前方障碍物检测
        //    //    Vector3 forward = transform.forward;

        //    //    // 低位射线
        //    //    RaycastHit lowerHit;
        //    //    bool isObstacle =
        //    //        Physics.Raycast(transform.position + Vector3.up * startUpOffest, forward, out lowerHit, forwardCheckDistance, ObstacleLayer) //前方
        //    //                                                                                                                                     //Physics.Raycast(transform.position + Vector3.up * startUpOffest, transform.TransformDirection(new Vector3(-1, 0, 1)), out lowerHit, forwardCheckDistance, ObstacleLayer) ||//左45
        //    //                                                                                                                                     //Physics.Raycast(transform.position + Vector3.up * startUpOffest, transform.TransformDirection(new Vector3(1, 0, 1)), out lowerHit, forwardCheckDistance, ObstacleLayer)//右45
        //    //        ;


        //    //    if (isObstacle)
        //    //    {
        //    //        Debug.Log("前方有障碍物" + lowerHit.collider.name);
        //    //        bool isHighObstacle =
        //    //            Physics.Raycast(transform.position + Vector3.up * stepHeight, forward, forwardCheckDistance, ObstacleLayer)//前方
        //    //                                                                                                                       //Physics.Raycast(transform.position + Vector3.up * stepHeight, transform.TransformDirection(new Vector3(-1, 0, 1)), forwardCheckDistance, ObstacleLayer) ||//左45
        //    //                                                                                                                       //Physics.Raycast(transform.position + Vector3.up * stepHeight, transform.TransformDirection(new Vector3(1, 0, 1)), forwardCheckDistance, ObstacleLayer)//右45
        //    //            ;
        //    //        // 高位射线
        //    //        if (!isHighObstacle)
        //    //        {
        //    //            Debug.Log("上台阶");
        //    //            ////rb.AddForce(Vector3.up * stepDownVelocity, ForceMode.Impulse);
        //    //            //rb.position += new Vector3(0f, stepSmooth, 0f);
        //    //            //前方台阶上表面射线
        //    //            if (Physics.Raycast(transform.position + Vector3.up * stepHeight + forward * forwardCheckDistance, Vector3.down, out RaycastHit planeHit, 0.1f, ObstacleLayer))
        //    //            {
        //    //                //Vector3 newPos = rb.position;
        //    //                //float shouldstepHeight = planeHit.point.y - rb.position.y;
        //    //                //Debug.Log("需要抬升的高度" + shouldstepHeight);
        //    //                //newPos.y = planeHit.point.y + 0.2f;
        //    //                rb.position = planeHit.point + 0.005f * Vector3.up;
        //    //                //Debug.DrawLine(planeHit.point, planeHit.normal);
        //    //            }
        //    //        }
        //    //    }
        //    //    else
        //    //    {
        //    //        //检测是否下台阶
        //    //        if (!DisableDownStepRay && Physics.Raycast(transform.position - Vector3.up * startUpOffest + transform.forward * forwardCheckDistance
        //    //            , Vector3.down, out RaycastHit lowerStepHit, stepHeight, ObstacleLayer))
        //    //        {
        //    //            if (rb.position.y - lowerStepHit.point.y > 0)
        //    //            {
        //    //                Debug.Log("下台阶" + lowerStepHit.collider.name);
        //    //                //rb.AddForce(Vector3.down * stepDownVelocity, ForceMode.Impulse);
        //    //                rb.position -= new Vector3(0f, stepSmooth, 0f);
        //    //            }
        //    //        }
        //    //    }

        //    //}
        //    //else if (ApplyAnimaMotionY)
        //    //{
        //    //    //其他轴速度保持
        //    //    //Debug.Log("rb:" + rb.velocity.ToString() + "animator" + animator.velocity);
        //    //    Vector3 v = new Vector3(BeforeApplySpeed.x, animator.velocity.y, BeforeApplySpeed.z);
        //    //    rb.velocity = v;
        //    //}


        //    //if (ApplyAnimatRotationY)
        //    //{
        //    //    //应用动画旋转
        //    //    Quaternion deltaRotation = animator.deltaRotation;
        //    //    rb.MoveRotation(rb.rotation * deltaRotation);
        //    //}

        //}

        ////private void OnDrawGizmos()
        ////{
        ////    Gizmos.color = Color.yellow;
        ////    Gizmos.DrawSphere(start, capsuleCollider.radius);
        ////    Gizmos.DrawSphere(end, capsuleCollider.radius);
        ////}

    }
}
