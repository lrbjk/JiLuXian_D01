using UnityEngine;
using UnityEngine.UI;

public class Emotionalbar : MonoBehaviour
{
    [Header("Slider Reference")]
    [SerializeField] private Slider pointerSlider;

    [Header("State Thresholds")]
    [SerializeField] private float lowThreshold = -0.75f;
    [SerializeField] private float highThreshold = 0.75f;

    // 当前指针状态
    public enum PointerState
    {
        Low,        // 低谷状态 (< -0.75)
        Normal,     // 普通状态 (-0.75 ~ 0.75)
        High        // 亢奋状态 (> 0.75)
    }

    private PointerState currentState;

    private void Start()
    {
        // 初始化Slider范围
        pointerSlider.minValue = -1f;
        pointerSlider.maxValue = 1f;
        pointerSlider.value = 0f;

        // 初始状态
        currentState = PointerState.Normal;
    }

    private void Update()
    {
        // 检测状态变化
        CheckStateTransition();
    }

    #region 公有方法接口 - 数值控制

    /// <summary>
    /// 增加指针值
    /// </summary>
    /// <param name="amount">增加值</param>
    public void IncreaseValue(float amount)
    {
        pointerSlider.value = Mathf.Clamp(pointerSlider.value + amount, -1f, 1f);
    }

    /// <summary>
    /// 减少指针值
    /// </summary>
    /// <param name="amount">减少值</param>
    public void DecreaseValue(float amount)
    {
        pointerSlider.value = Mathf.Clamp(pointerSlider.value - amount, -1f, 1f);
    }

    /// <summary>
    /// 设置指针值
    /// </summary>
    /// <param name="value">目标值 (-1到1之间)</param>
    public void SetValue(float value)
    {
        pointerSlider.value = Mathf.Clamp(value, -1f, 1f);
    }

    /// <summary>
    /// 重置指针到平衡点(0)
    /// </summary>
    public void ResetToNeutral()
    {
        pointerSlider.value = 0f;
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
    private void CheckStateTransition()
    {
        float currentValue = pointerSlider.value;
        PointerState newState;

        if (currentValue < lowThreshold)
        {
            newState = PointerState.Low;
        }
        else if (currentValue > highThreshold)
        {
            newState = PointerState.High;
        }
        else
        {
            newState = PointerState.Normal;
        }

        // 如果状态发生变化
        if (newState != currentState)
        {
            PointerState previousState = currentState;
            currentState = newState;
            OnStateChanged(previousState, currentState);
        }
    }

    // 状态变化回调
    private void OnStateChanged(PointerState oldState, PointerState newState)
    {
        Debug.Log($"状态变化: {oldState} -> {newState}");

        // 这里可以添加状态变化时的具体逻辑
        switch (newState)
        {
            case PointerState.Low:
                HandleLowStateEnter();
                break;
            case PointerState.Normal:
                HandleNormalStateEnter();
                break;
            case PointerState.High:
                HandleHighStateEnter();
                break;
        }
    }

    private void HandleLowStateEnter()
    {
        // 低谷状态进入逻辑
        Debug.Log("进入低谷状态");
    }

    private void HandleNormalStateEnter()
    {
        // 普通状态进入逻辑
        Debug.Log("进入普通状态");
    }

    private void HandleHighStateEnter()
    {
        // 亢奋状态进入逻辑
        Debug.Log("进入亢奋状态");
    }

    #endregion
}