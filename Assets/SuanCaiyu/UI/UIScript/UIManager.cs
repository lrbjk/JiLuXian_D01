using Helper;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Common.UI
{
    /// <summary>
    /// 描述：管理所有UI组
    /// </summary>
    public class UIManager : MonoSingleton<UIManager>
    {
        [HideInInspector]
        public Camera UICamera;

        /// <summary>
        /// 所有单独UI名称对应的Group
        /// </summary>
        private Dictionary<string, CanvasGroup> uiGroupsDic;
        /// <summary>
        /// 当前活动的UI组
        /// </summary>
        private CanvasGroup currentUIGroup;

        public UILayerManager CurrentUILayerManager
        {
            get
            {
                return currentUIGroup.GetComponent<UILayerManager>();
            }
        }

        protected override void Init()
        {
            uiGroupsDic = new Dictionary<string, CanvasGroup>(8);
            UICamera = transform.Find("UICamera").GetComponent<Camera>();
            //获取所有UI组
            foreach (Transform tf in transform)
            {
                if (tf.name == "UICamera")
                    continue;
                CanvasGroup group = tf.GetComponent<CanvasGroup>();
                if (group == null)
                    group = tf.gameObject.AddComponent<CanvasGroup>();
                if (tf.GetComponent<UILayerManager>() == null)
                    tf.gameObject.AddComponent<UILayerManager>();

                uiGroupsDic.Add(group.gameObject.name, group);
                group.alpha = 0f;
                tf.gameObject.SetActive(false);
            }
            currentUIGroup = uiGroupsDic["MainUI"];//默认当前UI为主UI
            currentUIGroup.alpha = 1f;
            currentUIGroup.gameObject.SetActive(true);
        }

        public UILayerManager GetUILayerManager(string name)
        {
            return uiGroupsDic[name].GetComponent<UILayerManager>();
        }

        /// <summary>
        /// 打开指定名称的UI组，并关闭当前UI组
        /// </summary>
        /// <param name="name"></param>
        public void OpenUIGroup(string name)
        {
            //currentUIGroup.gameObject.SetActive(false);
            //currentUIGroup.interactable = false;
            //StartCoroutine(UIHelper.FadeUI(currentUIGroup, 0));//当前UI消失
            //CurrentUILayerManager.CloseAllMutiUI();//关闭当前UI的其他层级UI

            CloseUIGroupOnly(currentUIGroup.name, 0.4f, null);//关闭当前UI组
            currentUIGroup = uiGroupsDic[name];
            OpenUIGroupOnly(name, 0.4f, null);//打开指定UI组
            //StartCoroutine(UIHelper.FadeUI(currentUIGroup));//打开UI
            //currentUIGroup.interactable = true;
            //currentUIGroup.gameObject.SetActive(true);
        }

        /// <summary>
        /// 仅打开指定名称的UI组
        /// </summary>
        /// <param name="name"></param>
        /// <param name="inTime"></param>
        /// <param name="downDo">打开完成后的行为</param>
        public void OpenUIGroupOnly(string name, float inTime, Action downDo)
        {
            var uiGroup = uiGroupsDic[name];
            StartCoroutine(UIHelper.FadeUI(uiGroup, downDo, 1, inTime));//打开UI
            uiGroup.interactable = true;
            uiGroup.gameObject.SetActive(true);
        }

        /// <summary>
        /// 仅关闭指定名称的UI组
        /// </summary>
        /// <param name="name"></param>
        /// <param name="outTime"></param>
        /// <param name="downDo">关闭完成后的行为</param>
        public void CloseUIGroupOnly(string name, float outTime, Action downDo)
        {
            var uiGroup = uiGroupsDic[name];
            uiGroup.gameObject.SetActive(true);
            uiGroup.interactable = false;
            StartCoroutine(UIHelper.FadeUI(uiGroup, downDo, 0, outTime));//指定UI消失
            uiGroup.GetComponent<UILayerManager>().CloseAllMutiUI();//关闭指定UI的其他层级UI
        }
    }
}

