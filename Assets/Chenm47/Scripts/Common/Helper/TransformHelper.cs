using UnityEngine;

namespace Common.Helper
{
    /// <summary>
    /// transform助手类
    /// </summary>
    public static class TransformHelper
    {
        /// <summary>
        /// 查找未知层级的父物体中，指定名称的子物体
        /// </summary>
        /// <param name="parentTF">父物体变换组件</param>
        /// <param name="gameObectname">子物体名称</param>
        /// <returns>子物体的变换组件</returns>
        public static Transform FindChildByName(this Transform parentTF, string gameObectname)
        {
            Transform child;
            child = parentTF.Find(gameObectname);
            if (child != null)
            {
                return child;
            }
            for (int i = 0; i < parentTF.childCount; i++)
            {
                child = FindChildByName(parentTF.GetChild(i), gameObectname);
                if (child != null)
                {
                    return child;
                }
            }
            return child;
        }
        /// <summary>
        /// 获取指定rt的世界rect
        /// </summary>
        /// <param name="rectTransform"></param>
        /// <returns></returns>
        public static Rect GetWordRect(RectTransform rectTransform)
        {
            //当前rt的piovt屏幕坐标
            var piovtPos = RectTransformUtility.WorldToScreenPoint(Camera.main, rectTransform.position);
            //当前相对的rect
            Rect r = rectTransform.rect;
            r.x += piovtPos.x;
            r.y += piovtPos.y;
            return r;
        }
    }
}

