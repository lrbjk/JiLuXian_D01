using Helper;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common.UI
{
    /// <summary>
    /// 描述：管理单个UI组的层级
    /// </summary>
    public class UILayerManager : MonoBehaviour
    {
        /// <summary>
        /// 所有层级UI组的group
        /// </summary>
        private List<CanvasGroup> UILayerGroups;
        private int currentIndex = 0;
        /// <summary>
        /// 当前层级下活动的UIPanel物体栈
        /// </summary>
        private Stack<GameObject> currentUIPanel;

        protected virtual void Awake() { }

        protected virtual void Start()
        {
            UILayerGroups = new List<CanvasGroup>(3);
            foreach (Transform tf in transform)
            {
                var group = tf.GetComponent<CanvasGroup>();//获取层级UIGrp
                if (group == null)
                    group = tf.gameObject.AddComponent<CanvasGroup>();
                UILayerGroups.Add(group);
                group.alpha = 0f;
                tf.gameObject.SetActive(false);
            }
            currentUIPanel = new Stack<GameObject>(3);
            //一级UI默认开启
            UILayerGroups[0].alpha = 1f;
            UILayerGroups[0].gameObject.SetActive(true);
        }

        /// <summary>
        /// 进入下一级UI中指定的UIPanel
        /// </summary>
        /// <param name="name">名称</param>
        public void NextUI(string name)
        {
            if (currentIndex < UILayerGroups.Count)
            {
                //当前UI不可交互
                UILayerGroups[currentIndex++].interactable = false;
                //打开下一级UI
                CanvasGroup nextGroup = UILayerGroups[currentIndex];
                StartCoroutine(UIHelper.FadeUI(nextGroup));
                //下级UI开启交互
                nextGroup.interactable = true;
                currentUIPanel.Push(nextGroup.transform.Find(name).gameObject);
                currentUIPanel.Peek().SetActive(true);
            }
            else
                //报错
                print("不存在名称为" + name + "的下一级UI");
        }
        /// <summary>
        /// 延迟进入下一级UI中指定的UIPanel
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="delayTime">延迟出现时间</param>
        /// <param name="fadeTime">淡入时间</param>
        public void NextUI(string name, float delayTime, float fadeTime)
        {
            StartCoroutine(DelyNextUI(name, delayTime, fadeTime));
        }

        private IEnumerator DelyNextUI(string name, float delayTime, float fadeTime)
        {
            yield return new WaitForSeconds(delayTime);
            NextUI(name);
        }

        /// <summary>
        /// 返回到上一级UI
        /// </summary>
        public void LastUI()
        {
            if (currentIndex > 0)
            {
                CanvasGroup currentGroup = UILayerGroups[currentIndex];
                //当前UI不可交互
                currentGroup.interactable = false;
                //淡出当前UILayerGroup,当前活动UIPalne在完成淡出后关闭
                StartCoroutine(UIHelper.FadeUI(UILayerGroups[currentIndex--],
                    () => currentUIPanel.Pop().SetActive(false), 0));
                //上一级UI恢复交互
                UILayerGroups[currentIndex].interactable = true;
            }
            else
                print("不存在上一级UI");
        }
        /// <summary>
        /// 关闭所有层级UI并开启一级UI的交互
        /// </summary>
        public void CloseAllMutiUI()
        {
            currentUIPanel.Clear();
            UILayerGroups[0].interactable = true;
            currentIndex = 0;
            for (int i = 1; i < UILayerGroups.Count; i++)
            {
                UILayerGroups[i].interactable = false;
                UILayerGroups[i].alpha = 0;
                UILayerGroups[i].gameObject.SetActive(false);
            }

        }

    }
}
