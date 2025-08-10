using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using Common.Helper;
using System.Collections;


public class MoneyText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private TextMeshProUGUI changedMonyText;
    [SerializeField]private int currentDisplayValue; // 当前显示的值
    private int targetValue;       // 目标值
    private Tween countTween;      // 存储动画引用

    [SerializeField]private FadeUI fadeText;
    

    // 初始化
    private void Start()
    {
        currentDisplayValue = 0;
        UpdateText();
    }

    
    // 设置金钱值（带动画）
    public void SetMoney(int newValue, float duration = 0.5f)
    {
        // 如果已有动画在进行，先杀死它
        if (countTween != null && countTween.IsActive())
        {
            countTween.Kill();
        }

        targetValue = newValue;

        // 使用DOTween.To实现数字滚动
        countTween = DOTween.To(() => currentDisplayValue, x => {
            currentDisplayValue = x;
            UpdateText();
        }, targetValue, duration)
        .SetEase(Ease.OutQuad);
    }

    //增加瓶数
    public void AddBottle(int amount)
    {
        SetMoney(targetValue + amount, 0.5f);
    }

    //减少瓶数
    public void SubBottle(int amount)
    {
        SetMoney(targetValue - amount, 0.5f);
    }
    // 增加金钱
    public void AddMoney(int amount )
    {
        StartCoroutine(ChangeMoney_Increase(amount));
    }

    // 减少金钱
    public void SubtractMoney(int amount)
    {
        StartCoroutine (ChangeMoney_Decrease(amount));
        
    }

    // 更新UI文本
    private void UpdateText()
    {
        moneyText.text = currentDisplayValue.ToString(); // N0格式添加千位分隔符
      
    }

    //更新改变金钱的UI文本
    private void IncreaseChangedText(int amount)
    {
        changedMonyText.text = $" +{amount.ToString()}";
    }

    private void DecreaseChangedText(int amount)
    {
        changedMonyText.text = $" -{amount.ToString()}";
    }

    /// <summary>
    /// 先显示增加或者减少的钱数，再更新UI文本
    /// </summary>
    /// <param name="amount"></param>
    /// <returns></returns>
    private IEnumerator ChangeMoney_Increase(int amount)
    {
        IncreaseChangedText(amount);
        fadeText.FadeIn();    
        yield return new WaitForSeconds(0.8f);
        SetMoney(targetValue + amount, 0.5f);
        fadeText.FadeOut();
    }

    private IEnumerator ChangeMoney_Decrease(int amount)
    {
        DecreaseChangedText(amount);
        fadeText.FadeIn();
        yield return new WaitForSeconds(0.8f);
        SetMoney(targetValue - amount, 0.5f);
        fadeText.FadeOut();
    }
}