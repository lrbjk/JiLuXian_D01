using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;


namespace Common.UI
{
    public class Emotionalbar : MonoBehaviour
    {
        [Header("Slider Reference")]
        [SerializeField] private Slider pointerSlider;
        [SerializeField] private Image lowImage;
        [SerializeField] private Image highImage;
        [SerializeField] private Image normalImage;
        [SerializeField] private Image mark1;
        [SerializeField] private Image mark2;
        [SerializeField] private Image decreseImage;

        [Header("State Thresholds")]
        [SerializeField] private float lowThreshold = 0f;
        [SerializeField] private float highThreshold = 0;

        [SerializeField] private float currentAmount;
        [SerializeField] private float maxAmount;

        [SerializeField] private float fullAmount;

        public Ease easeType = Ease.OutQuad;
        // 当前指针状态
        public enum PointerState
        {
            Low,        // 低谷状态 (< 0.25)
            Normal,     // 普通状态 (0- 0.75)
            High        // 亢奋状态 (> 0.75)
        }

        private PointerState currentState;

        private void Start()
        {
            // 初始化Slider范围
            pointerSlider.minValue = 0f;
            pointerSlider.maxValue = 1f;
            pointerSlider.value = 0.5f;
            currentAmount = 0.5f;
            maxAmount = 1f;
            fullAmount = 100f;
            // 初始状态
            currentState = PointerState.Normal;

            lowThreshold = 0.2f;
            highThreshold = 0.9f;

            RestImage();
            DecreaseMax(0);
        }

        private void Update()
        {
            // 检测状态变化
            // CheckStateTransition();
        }

        #region 公有方法接口 - 数值控制

        /// <summary>
        /// 增加指针值
        /// </summary>
        /// <param name="amount">增加值</param>
        public void IncreaseValue(float amount)
        {
            currentAmount = Mathf.Clamp(currentAmount + amount / fullAmount, 0f, maxAmount);
            DOTween.To(() => pointerSlider.value, x => pointerSlider.value = x, currentAmount, 0.2f);

        }

        /// <summary>
        /// 减少指针值
        /// </summary>
        /// <param name="amount">减少值</param>
        public void DecreaseValue(float amount)
        {
            currentAmount = Mathf.Clamp(currentAmount - amount / fullAmount, 0f, maxAmount);
            DOTween.To(() => pointerSlider.value, x => pointerSlider.value = x, currentAmount, 0.2f);
        }

        /// <summary>
        /// 设置指针值
        /// </summary>
        /// <param name="value">目标值 (-1到1之间)</param>
        public void SetValue(float value)
        {
            pointerSlider.value = Mathf.Clamp(value / fullAmount, 0f, maxAmount);
        }

        /// <summary>
        /// 重置指针到平衡点(0)
        /// </summary>
        public void ResetToNeutral()
        {
            pointerSlider.value = 0.5f;

        }

        /// <summary>
        /// 更新整个情感条，阈值和最大情感量
        /// </summary>
        /// <param name="newlow"></param>
        /// <param name="newhigh"></param>
        public void ResetEmotionalBar(float newlow, float newhigh, float fullamount)
        {
            lowThreshold = newlow / fullamount;
            highThreshold = newhigh / fullamount;
            fullAmount = fullamount;
            RestImage();
        }

        /// <summary>
        /// 重置背景色块
        /// </summary>
        public void RestImage()
        {
            RectTransform rectTransform = lowImage.GetComponent<RectTransform>();
            RectTransform rectTransform_1 = normalImage.GetComponent<RectTransform>();
            RectTransform rectTransform_2 = highImage.GetComponent<RectTransform>();
            RectTransform rectTransform_3 = mark1.GetComponent<RectTransform>();
            RectTransform rectTransform_4 = mark2.GetComponent<RectTransform>();
            RectTransform rectTransform_5 = decreseImage.GetComponent<RectTransform>();

            rectTransform.sizeDelta = new Vector2(lowThreshold * 950, rectTransform.sizeDelta.y);
            rectTransform_1.sizeDelta = new Vector2((highThreshold - lowThreshold) * 950, rectTransform.sizeDelta.y);
            rectTransform_2.sizeDelta = new Vector2((1f - highThreshold) * 950, rectTransform.sizeDelta.y);
            rectTransform_5.sizeDelta = new Vector2(0f, rectTransform.sizeDelta.y);

            rectTransform_1.anchoredPosition = new Vector2(-475f + rectTransform.sizeDelta.x, 0);
            rectTransform_3.anchoredPosition = rectTransform_1.anchoredPosition;
            rectTransform_4.anchoredPosition = new Vector2(475f - rectTransform_2.sizeDelta.x, 0);
        }

        /// <summary>
        /// 减少上限百分比
        /// </summary>
        /// <param name="amount"></param>
        public void DecreaseMax(float amount)
        {
            maxAmount = maxAmount - amount;

            if (maxAmount <= 0)
            {
                maxAmount = 0;
            }
            else
            {
                maxAmount = maxAmount;
            }

            RectTransform rectTransform = decreseImage.GetComponent<RectTransform>();
            float targetWidth = (1 - maxAmount) * 950;

            rectTransform.DOSizeDelta(new Vector2(targetWidth, rectTransform.sizeDelta.y), 0.1f)
               .SetEase(easeType);

            if (currentAmount > maxAmount)
            {
                DecreaseValue(currentAmount - maxAmount);
            }

        }

        /// <summary>
        /// 增加上限百分比
        /// </summary>
        /// <param name="amount"></param>
        public void IncreseMax(float amount)
        {
            maxAmount = maxAmount + amount;
            if (maxAmount >= 1)
            {
                maxAmount = 1;
            }
            else
            {
                maxAmount = maxAmount;
            }
            RectTransform rectTransform = decreseImage.GetComponent<RectTransform>();

            float targetWidth = (1 - maxAmount) * 950f;

            rectTransform.DOSizeDelta(new Vector2(targetWidth, rectTransform.sizeDelta.y), 0.1f)
               .SetEase(easeType);

        }

        /// <summary>
        /// 增加最大情感量到某个值
        /// </summary>
        /// <param name="amount"></param>
        public void IncreseFullAmount(float amount)
        {
            fullAmount = amount;
        }
        #endregion

        #region 公有方法接口 - 状态检测

        /// <summary>
        /// 获取当前指针状态
        /// </summary>
        public PointerState GetCurrentState()
        {
            return currentState;
        }

        /// <summary>
        /// 检测是否处于低谷状态
        /// </summary>
        public bool IsInLowState()
        {
            return pointerSlider.value < lowThreshold;
        }

        /// <summary>
        /// 检测是否处于亢奋状态
        /// </summary>
        public bool IsInHighState()
        {
            return pointerSlider.value > highThreshold;
        }

        /// <summary>
        /// 检测是否处于普通状态
        /// </summary>
        public bool IsInNormalState()
        {
            return !IsInLowState() && !IsInHighState();
        }

        #endregion

        #region 私有方法

        // 检测状态变化并触发事件
        //private void CheckStateTransition()
        //{
        //    float currentValue = pointerSlider.value;
        //    PointerState newState;

        //    if (currentValue < lowThreshold)
        //    {
        //        newState = PointerState.Low;
        //    }
        //    else if (currentValue > highThreshold)
        //    {
        //        newState = PointerState.High;
        //    }
        //    else
        //    {
        //        newState = PointerState.Normal;
        //    }

        //    // 如果状态发生变化
        //    if (newState != currentState)
        //    {
        //        PointerState previousState = currentState;
        //        currentState = newState;
        //        OnStateChanged(previousState, currentState);
        //    }
        //}

        // 状态变化回调
        //private void OnStateChanged(PointerState oldState, PointerState newState)
        //{
        //    Debug.Log($"状态变化: {oldState} -> {newState}");

        //    // 这里可以添加状态变化时的具体逻辑
        //    switch (newState)
        //    {
        //        case PointerState.Low:
        //            HandleLowStateEnter();
        //            break;
        //        case PointerState.Normal:
        //            HandleNormalStateEnter();
        //            break;
        //        case PointerState.High:
        //            HandleHighStateEnter();
        //            break;
        //    }
        //}

        //private void HandleLowStateEnter()
        //{
        //    // 低谷状态进入逻辑
        //    Debug.Log("进入低谷状态");
        //}

        //private void HandleNormalStateEnter()
        //{
        //    // 普通状态进入逻辑
        //    Debug.Log("进入普通状态");
        //}

        //private void HandleHighStateEnter()
        //{
        //    // 亢奋状态进入逻辑
        //    Debug.Log("进入亢奋状态");
        //}

        #endregion
    }
}