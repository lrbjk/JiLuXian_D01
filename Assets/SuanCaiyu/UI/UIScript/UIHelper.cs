using System;
using System.Collections;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Helper
{
    /// <summary>
    /// 描述：UI助手，提供一些常用的UI效果
    /// </summary>
    public static class UIHelper
    {
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
            for (float i = 0; i < time; i += Time.unscaledDeltaTime)
            {
                canvasGroup.alpha += speed * Time.unscaledDeltaTime;
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
            downDo?.Invoke();
        }
        /// <summary>
        /// 单个Img的UI简单淡入淡出
        /// </summary>
        /// <param name="canvasGroup">UI组</param>
        /// <param name="to">期望的淡入的最终值，默认淡入</param>
        /// <param name="time">消耗时间</param>
        public static IEnumerator FadeUI(Image img, float to = 1, float time = 0.4f)
        {
            if (to > 0) img.gameObject.SetActive(true);
            float speed = (to - img.color.a) / time;
            Color color = img.color;
            for (float i = 0; i < time; i += Time.unscaledDeltaTime)
            {
                color.a += speed * Time.unscaledDeltaTime;
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
        /// <param name="to">期望的淡入的最终值，默认淡入</param>
        /// <param name="time">消耗时间</param>
        public static IEnumerator FadeUI(Image img, Action downDo, float to = 1, float time = 0.4f)
        {
            yield return FadeUI(img, to, time);
            downDo?.Invoke();
        }
        /// <summary>
        /// UI闪烁效果
        /// </summary>
        /// <param name="img"></param>
        /// <param name="outime">淡出时间</param>
        /// <param name="intime">淡入时间</param>
        /// <returns></returns>
        public static IEnumerator FlickerUI(Image img, float outime = 0.4f, float intime = 0.4f)
        {
            while (true)
            {
                //先淡出
                yield return FadeUI(img, 0, outime);
                //再淡入
                yield return FadeUI(img, 1, intime);
            }
        }
        /// <summary>
        /// 含有富文本的字符缓慢显示
        /// </summary>
        /// <param name="text">text组件</param>
        /// <param name="textString">显示的文字</param>
        /// <param name="deltaTtime">每个字符出现间隔</param>
        /// <returns></returns>
        public static IEnumerator ShowText(TextMeshProUGUI text, string textString, float deltaTtime = 0.1f)
        {
            text.text = null;
            for (int i = 0; i < textString.Length; i++)
            {
                char c = textString[i];
                if (c != '<')
                {
                    text.text += c;
                    yield return new WaitForSecondsRealtime(deltaTtime);
                }
                else
                {
                    StringBuilder tag = new StringBuilder();
                    tag.Append(c);
                    //为标签，完全读入一个标签再加入
                    while ((c = textString[++i]) != '>')
                        tag.Append(c);
                    tag.Append('>');
                    text.text += tag.ToString();
                }
            }
        }
    }
}
