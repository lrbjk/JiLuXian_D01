using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Common.Helper
{
    /// <summary>
    /// 描述：UI助手
    /// </summary>
    public static class UIHelper
    {

        public static IEnumerator MoveUI(Transform transform, Vector3 target, float speed)
        {
            while ((target - transform.position).sqrMagnitude > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
                yield return null;
            }
            transform.position = target;
        }

        /// <summary>
        /// 整体cnavasGroup的UI简单淡入淡出
        /// </summary>
        /// <param name="canvasGroup">UI组</param>
        /// <param name="to">期望的淡入的最终值，默认淡入</param>
        /// <param name="time">消耗时间</param>
        public static IEnumerator FadeUI(CanvasGroup canvasGroup, float to = 1, float time = 0.4f)
        {
            if (to > 0) canvasGroup.gameObject.SetActive(true);
            float speed = (to - canvasGroup.alpha) / time;
            for (float i = 0; i < time; i += Time.deltaTime)
            {
                canvasGroup.alpha += speed * Time.deltaTime;
                yield return null;
            }
            canvasGroup.alpha = to;
            if (to == 0) canvasGroup.gameObject.SetActive(false);
        }
        /// <summary>
        /// 整体cnavasGroup的UI简单淡入淡出，并在完成后执行downDo
        /// </summary>
        /// <param name="canvasGroup">UI组</param>
        /// <param name="downDo">当完成时执行的操作</param>
        /// <param name="to">期望的淡入的最终值，默认淡入</param>
        /// <param name="time">消耗时间</param>
        public static IEnumerator FadeUI(CanvasGroup canvasGroup, Action downDo, float to = 1, float time = 0.4f)
        {
            yield return FadeUI(canvasGroup, to, time);
            downDo();
        }
        /// <summary>
        /// 单个Img的UI简单淡入淡出
        /// </summary>
        /// <param name="canvasGroup">UI组</param>
        /// <param name="to">期望的淡入的最终值，默认淡出</param>
        /// <param name="time">消耗时间</param>
        public static IEnumerator FadeUI(Image img, float to = 1, float time = 0.4f)
        {
            if (to > 0) img.gameObject.SetActive(true);
            float speed = (to - img.color.a) / time;
            Color color = img.color;
            for (float i = 0; i < time; i += Time.deltaTime)
            {
                color.a += speed * Time.deltaTime;
                img.color = color;
                yield return null;
            }
            color.a = to;
            img.color = color;
            if (to == 0) img.gameObject.SetActive(false);
        }
        /// <summary>
        /// 单个Img的UI简单淡入淡出，并在完成后执行downDo
        /// </summary>
        /// <param name="img">UI的img对象</param>
        /// <param name="downDo">当完成时执行的操作</param>
        /// <param name="to">期望的淡入的最终值，默认淡出</param>
        /// <param name="time">消耗时间</param>
        public static IEnumerator FadeUI(Image img, Action downDo, float to = 1, float time = 0.4f)
        {
            yield return FadeUI(img, to, time);
            downDo();
        }
    }
}
