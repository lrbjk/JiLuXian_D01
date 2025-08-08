using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Statusvaluecontroller : MonoBehaviour
{
    [Header("Slider Reference")]
    [SerializeField] private Image statusValueimage;
    [SerializeField] private Image statusValueBack;
    [SerializeField] private Image backGround;

    [SerializeField] private float statusValueTime;

    [SerializeField] private float maxValue;
    [SerializeField] private float targetWidth;

    public Ease easeType = Ease.OutQuad;

    /// <summary>
    /// 仅供测试
    /// </summary>
    [Header("按钮配置")]
    [SerializeField] private Button targetButton1;
    [SerializeField] private Button targetButton2;
    [SerializeField] private Button targetButton3;
    [SerializeField] private Button targetButton4;

    private void Start()
    {
        //初始化当前宽度
        maxValue = statusValueimage.GetComponent<RectTransform>().rect.width;
        //初始化时间间隔
        statusValueTime = 0.4f;
        //初始化目标宽度
        targetWidth = maxValue;


        // 添加点击事件监听
        if (targetButton1 != null && targetButton2 != null)
        {
            targetButton1.onClick.AddListener(() => Increse(100f));
            targetButton2.onClick.AddListener(() => Decrese(100f));
            targetButton3.onClick.AddListener(() => IncreseMaxOnly(100f));
            targetButton4.onClick.AddListener(() => DecreseMax(100f));
        }
        else
        {
            Debug.LogError("未找到Button组件", this);
        }

    }

    /// <summary>
    /// 增加血量
    /// </summary>
    /// <param name="amount"></param>
     private void Increse(float amount)
    {
        StartCoroutine(Increase(amount));
    }

    /// <summary>
    /// 减少血量
    /// </summary>
    /// <param name="amount"></param>
    private void Decrese(float amount)
    {
        StartCoroutine(Decrease(amount));
    }

    /// <summary>
    /// 仅增加血量上限
    /// </summary>
    public void IncreseMaxOnly(float amount)
    {
       
        IncreasemMax(amount);
    }

    /// <summary>
    /// 增加血量上限同时回满血
    /// </summary>
    /// <param name="amount"></param>
    public void IncreseMax_Full(float amount)
    {
        StartCoroutine(IncreseMaxFull(amount));
    }

    /// <summary>
    /// 减少血量上限，不用分情况
    /// </summary>
    /// <param name="amount"></param>
    public void DecreseMax(float amount)
    {
        RectTransform rectTransform = statusValueimage.GetComponent<RectTransform>();
        if (rectTransform.rect.width >= maxValue)
        {
            StartCoroutine(DecreseMax_Full(amount));
        }
        else
        {
            DecreasemMaxOnly(amount);
        }
    }

    //----------------------------------------------------------------------------下面是具体实现

    /// <summary>
    /// 实血直接增加状态值
    /// </summary>
    /// <param name="amount"></param>
    public void IncreaseValue(float amount)
    {
        RectTransform rectTransform = statusValueimage.GetComponent<RectTransform>();

         targetWidth = Mathf.Clamp(targetWidth + amount, 0f, maxValue);

        rectTransform.DOSizeDelta(new Vector2(targetWidth, rectTransform.sizeDelta.y), statusValueTime)
           .SetEase(easeType);

    }

    /// <summary>
    /// 实血直接减少状态值
    /// </summary>
    /// <param name="amount"></param>
    public void DecreaseValue_im(float amount)
    {
        RectTransform rectTransform = statusValueimage.GetComponent<RectTransform>();
        Debug.Log("血量减少！");
         targetWidth = Mathf.Clamp(targetWidth - amount, 0f, maxValue);

        rectTransform.sizeDelta = new Vector2(targetWidth, rectTransform.sizeDelta.y);
    }

    /// <summary>
    /// 虚血逐步改变变量值 
    /// </summary>
    /// <param name="amount"></param>
    /// <returns></returns>
    public void ChangeValue_step()
    {
        RectTransform rectTransform = statusValueimage.GetComponent<RectTransform>();
        RectTransform rectTransform_1 = statusValueBack.GetComponent<RectTransform>();

        float targetWidth = rectTransform.rect.width;
        
        rectTransform_1.DOSizeDelta(new Vector2(targetWidth, rectTransform_1.sizeDelta.y), statusValueTime)
           .SetEase(easeType);


    }

    /// <summary>
    /// 直接增加上限
    /// </summary>
    /// <param name="amount"></param>
    public void IncreasemMax(float amount)
    {
        //更新最大上限
        maxValue = maxValue + amount;
        RectTransform rectTransform = backGround.GetComponent<RectTransform>();
        
        float targetWidth = Mathf.Clamp(maxValue + 10f , 0f, maxValue +  10f);

        rectTransform.DOSizeDelta(new Vector2(targetWidth, rectTransform.sizeDelta.y), statusValueTime)
           .SetEase(easeType);
    }

    /// <summary>
    /// 上限变化时虚血实血逐步变化
    /// </summary>
    public void IncreseValueToMax(float amount)
    {
        RectTransform rectTransform = statusValueimage.GetComponent<RectTransform>();
        RectTransform rectTransform_1 = statusValueBack.GetComponent<RectTransform>();

        float targetWidth = Mathf.Clamp(maxValue, 0f, maxValue);

        rectTransform.DOSizeDelta(new Vector2(targetWidth, rectTransform.sizeDelta.y), statusValueTime)
           .SetEase(easeType);

        rectTransform_1.DOSizeDelta(new Vector2(targetWidth, rectTransform_1.sizeDelta.y), statusValueTime)
           .SetEase(easeType);
        
    }

    /// <summary>
    /// 血量满时减少最大上限
    /// </summary>
    /// <param name="amount"></param>
    public void DecreaseMaxFull(float amount)
    {
        //更新最大上限
        maxValue = maxValue - amount;

        RectTransform rectTransform = statusValueimage.GetComponent<RectTransform>();

        float targetWidth = Mathf.Clamp(maxValue, 0f, maxValue);

        rectTransform.DOSizeDelta(new Vector2(targetWidth, rectTransform.sizeDelta.y), statusValueTime)
           .SetEase(easeType);
       
    }

    /// <summary>
    /// 逐步增加上限至最大值
    /// </summary>
    /// <param name="amount"></param>
    public void DecreseValueToMax(float amount)
    {
        RectTransform rectTransform = backGround.GetComponent<RectTransform>();
        RectTransform rectTransform_1 = statusValueBack.GetComponent<RectTransform>();
        RectTransform rectTransform_2 = statusValueimage.GetComponent<RectTransform>();

        float targetWidth = Mathf.Clamp(maxValue, 0f, maxValue);

        rectTransform.DOSizeDelta(new Vector2(targetWidth + 10, rectTransform.sizeDelta.y), statusValueTime)
           .SetEase(easeType);

        rectTransform_1.DOSizeDelta(new Vector2(targetWidth, rectTransform_1.sizeDelta.y), statusValueTime)
           .SetEase(easeType);

    }

    /// <summary>
    /// 仅仅减少最大上限
    /// </summary>
    /// <param name="amount"></param>
    public void DecreasemMaxOnly(float amount)
    {
        //更新最大上限
        maxValue = maxValue - amount;

        RectTransform rectTransform = backGround.GetComponent<RectTransform>();

        float targetWidth = Mathf.Clamp(maxValue + 10 , 0f, maxValue + 10);

        rectTransform.DOSizeDelta(new Vector2(targetWidth, rectTransform.sizeDelta.y), statusValueTime)
           .SetEase(easeType);

    }

    /// <summary>
    /// 设置当前值，存档点坐火时调用
    /// </summary>
    /// <param name="value"></param>
    public void SetValue(float value)
    {
        RectTransform rectTransform = backGround.GetComponent<RectTransform>();
        RectTransform rectTransform_1 = statusValueBack.GetComponent<RectTransform>();
        RectTransform rectTransform_2 = statusValueimage.GetComponent<RectTransform>();

        rectTransform.sizeDelta = new Vector2(value + 10f , rectTransform.sizeDelta.y);
        rectTransform_1.sizeDelta = new Vector2(value, rectTransform.sizeDelta.y);
        rectTransform_2.sizeDelta = new Vector2(value, rectTransform.sizeDelta.y);
    }

    #region 协程，数值控制
    /// <summary>
    /// 最终的外部调用两个协程
    /// </summary>
    /// <param name="amount"></param>
    /// <returns></returns>
    public IEnumerator Increase(float amount)
    {
        IncreaseValue(amount);
        yield return  new WaitForSeconds(0.2f);

        ChangeValue_step();

    }

     public IEnumerator Decrease(float amount)
    {
        DecreaseValue_im(amount);
        yield return new WaitForSeconds(0.2f);

        ChangeValue_step();

    }

    public IEnumerator IncreseMaxFull(float amount)
    {
        IncreasemMax(amount);
        yield return new WaitForSeconds(0.1f);

        IncreseValueToMax(amount);
    }

    public IEnumerator DecreseMax_Full(float amount)
    {
        DecreaseMaxFull(amount);
        yield return new WaitForSeconds(0.1f);

        DecreseValueToMax(amount);
    }

   
    #endregion


}
