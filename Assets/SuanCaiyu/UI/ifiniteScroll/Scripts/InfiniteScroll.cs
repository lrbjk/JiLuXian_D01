using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfiniteScroll : MonoBehaviour
{
    private ScrollRect scrollRect;
    public ScrollRect SR;
    public RectTransform viewPort_rtf;
    public RectTransform content_rtf;
    public HorizontalLayoutGroup HLG;
    public RectTransform[] ItemList;

    [SerializeField] private float moveDistance = 300f; // 每次移动的距离
    [SerializeField] private float moveSpeed = 1500f;   // 移动速度
    private Vector3 targetPosition;
    private bool isMovingLeft = false;
    private bool isMovingRight = false;

    //private bool isUpdated;
    //private Vector2 OldVelocity;

    void Start()
    {
        scrollRect = GetComponent<ScrollRect>();
        if (scrollRect == null)
        {
            Debug.LogError("ScrollRect component not found!");
            return;
        }

        // 禁止鼠标拖拽，但仍可通过代码控制滚动
        scrollRect.enabled = false; // 禁用整个 ScrollRect 交互

        targetPosition = content_rtf.localPosition;
        //isUpdated = false;
        //OldVelocity = Vector2.zero;//即静止不滚动

        //Mathf.CeilToInt(a)获得≥a的最小整数
        //前后各需要补充多少个
        int AddNum = Mathf.CeilToInt(viewPort_rtf.rect.width / (ItemList[0].rect.width + HLG.spacing));
        //右边补充(ABC...)
        for (int i = 0; i < AddNum; i++)
        {
            RectTransform rtf = Instantiate(ItemList[i % ItemList.Length], content_rtf);
            rtf.SetAsLastSibling();
        }
        //左边补充(ZYX...)
        for (int i = 0; i < AddNum; i++)
        {
            //倒数循环
            int j = ItemList.Length - 1 - i;
            while (j < 0)
            {
                j += ItemList.Length;
            }
            RectTransform rtf = Instantiate(ItemList[j], content_rtf);
            rtf.SetAsFirstSibling();
        }

        //默认显示原始组(x后退左边补充宽度)
        content_rtf.localPosition = new Vector3(-(ItemList[0].rect.width + HLG.spacing)*AddNum + (viewPort_rtf.rect.width- ItemList[0].rect.width)/2,
                                                content_rtf.localPosition.y, 
                                                content_rtf.localPosition.z);
    }


    void Update()
    {
        //if (isUpdated)
        //{   
        //    isUpdated = false;
        //    SR.velocity = OldVelocity;
        //}
        //左边补充用完时跳转用原始组
        if (content_rtf.localPosition.x > 0)
        {
            //强制刷新，避免计算所需数值有误
            Canvas.ForceUpdateCanvases();
            //OldVelocity = SR.velocity;
            targetPosition -= new Vector3(ItemList.Length * (ItemList[0].rect.width + HLG.spacing), 0, 0);
            content_rtf.localPosition -= new Vector3(ItemList.Length * (ItemList[0].rect.width + HLG.spacing), 0, 0);
            //isUpdated = true;
        }
        //右边补充用完时跳转用原始组
        if (content_rtf.localPosition.x < -(ItemList[0].rect.width + HLG.spacing) * ItemList.Length)
        {
            Canvas.ForceUpdateCanvases();
            //OldVelocity = SR.velocity;
            targetPosition += new Vector3(ItemList.Length * (ItemList[0].rect.width + HLG.spacing), 0, 0);
            content_rtf.localPosition += new Vector3(ItemList.Length * (ItemList[0].rect.width + HLG.spacing), 0, 0);
            //isUpdated = true;
        }

        if (isMovingLeft)
        {
            Debug.Log("movingleft");
            // 平滑移动到目标位置
            content_rtf.localPosition = Vector3.MoveTowards(content_rtf.localPosition, targetPosition, moveSpeed * Time.deltaTime);

            // 如果已经到达目标位置，停止移动
            if (content_rtf.localPosition == targetPosition)
            {
                isMovingLeft = false;
            }
        }

        if (isMovingRight)
        {
            Debug.Log("movingright");
            // 平滑移动到目标位置
            content_rtf.localPosition = Vector3.MoveTowards(content_rtf.localPosition, targetPosition, moveSpeed * Time.deltaTime);

            // 如果已经到达目标位置，停止移动
            if (content_rtf.localPosition == targetPosition)
            {
                isMovingRight = false;
            }
        }

    }

    public void MovingLeft()
    {
        
      
        if (!isMovingLeft)
        {
            // 计算新的目标位置（向左移动）
            targetPosition = content_rtf.localPosition + Vector3.left * moveDistance;
            isMovingLeft = true;
        }
    }

    public void MovingRight()
    {


        if (!isMovingRight)
        {
            // 计算新的目标位置（向左移动）
            targetPosition = content_rtf.localPosition + Vector3.right * moveDistance;
            isMovingRight = true;
        }
    }

}
