using UnityEngine;
using UnityEngine.EventSystems;


namespace Common.UI
{
    /// <summary>
    /// 描述：可拖动UI
    /// </summary>
    public class Drag_UI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        protected RectTransform rectTransform;
        protected Vector3 offset;

        protected virtual void Awake()
        {
            rectTransform = transform as RectTransform;
        }
        /// <summary>
        /// 需要后续继续手动设置设置在最上层
        /// </summary>
        /// <param name="eventData"></param>
        public virtual void OnBeginDrag(PointerEventData eventData)
        {
            //开始拖拽，记录偏差
            offset = rectTransform.position - eventData.pointerPressRaycast.worldPosition;
            //设置在最上层
        }

        public virtual void OnDrag(PointerEventData eventData)
        {
            //拖拽
            if (eventData.button == 0)
            {
                //左键拖拽
                Vector3 mousePos;
                RectTransformUtility.ScreenPointToWorldPointInRectangle(rectTransform, Input.mousePosition, UIManager.Instance.UICamera, out mousePos);//使用UI相机
                transform.position = mousePos + offset;
            }
        }

        public virtual void OnEndDrag(PointerEventData eventData)
        {
            //结束拖拽
            print("结束拖拽");
            //判断与订单UI是否相交
        }
    }
}
