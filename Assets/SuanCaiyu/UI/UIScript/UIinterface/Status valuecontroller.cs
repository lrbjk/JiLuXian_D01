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
        statusValueTime = 0.4f;


        // 添加点击事件监听
        if (targetButton1 != null && targetButton2 != null)
        {
            targetButton1.onClick.AddListener(StartCoroutineOnClick_increse);
            targetButton2.onClick.AddListener(StartCoroutineOnClick_decrese);
            targetButton3.onClick.AddListener(StartCoroutionOnClick_increseMax);
            targetButton4.onClick.AddListener (StartCoroutionOnClick_decreseMax);
        }
        else
        {
            Debug.LogError("未找到Button组件", this);
        }

    }

    /// <summary>
    /// 外部直接调用的接口
    /// </summary>
    private void StartCoroutineOnClick_increse()
    {
        StartCoroutine(Increase(100f));
    }

    private void StartCoroutineOnClick_decrese()
    {
        StartCoroutine(Decrease(100f));
    }

    private void StartCoroutionOnClick_increseMax()
    {
        StartCoroutine(IncreseMax(100f));
    }

    private void StartCoroutionOnClick_decreseMax()
    {
        StartCoroutine(DecreseMax(100f));
    }




    /// <summary>
    /// 实血直接增加状态值
    /// </summary>
    /// <param name="amount"></param>
    public void IncreaseValue(float amount)
    {
        RectTransform rectTransform = statusValueimage.GetComponent<RectTransform>();

        float targetWidth = Mathf.Clamp(rectTransform.rect.width + amount, 0f, maxValue);

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
        float targetWidth = Mathf.Clamp(rectTransform.rect.width - amount, 0f, maxValue);

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

    public void DecreasemMax(float amount)
    {
        //更新最大上限
        maxValue = maxValue - amount;

        RectTransform rectTransform = statusValueimage.GetComponent<RectTransform>();

        float targetWidth = Mathf.Clamp(maxValue, 0f, maxValue);

        rectTransform.DOSizeDelta(new Vector2(targetWidth, rectTransform.sizeDelta.y), statusValueTime)
           .SetEase(easeType);
       
    }

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


    #region 公有方法，数值控制
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

    public IEnumerator IncreseMax(float amount)
    {
        IncreasemMax(amount);
        yield return new WaitForSeconds(0.1f);

        IncreseValueToMax(amount);
    }

    public IEnumerator DecreseMax(float amount)
    {
        DecreasemMax(amount);
        yield return new WaitForSeconds(0.1f);

        DecreseValueToMax(amount);
    }
    #endregion


}
