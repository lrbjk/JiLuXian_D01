using System.Collections;
using UnityEngine;
using Common.UI;
using System.Runtime.CompilerServices;

/// <summary>
/// 商店UI的一些方法
/// </summary>
public class ButtonTimer : MonoBehaviour
{
    public float StampAinmationTime = 0.6f;
    public float ExitAinmationTime = 0.6f;
    public float ChangeColorTime = 0.1f;
    public Animator stampAnimator;
    public Animator PayButtonAnimator;
    
    //public float delayTime_Close = 1.5f;
    //public float delayTime_Stamp = 0.3f;
    //public float delayTime_Exit = 1f;
    private Animator animator;

    private void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
        
    }
    //推出操作
    private void CloseShopUI()
    {
        Debug.Log("关闭商店页面！");
        UIManager.Instance.OpenUIGroup("MainUI");//关闭商店UI
    }

    //盖印章动画
    public IEnumerator PayButton_Stamp()
    {

        Debug.Log("购买成功！");
        stampAnimator.Play("Stamp");
        
        yield return new WaitForSeconds(StampAinmationTime);
    }

    //退出动画
    public IEnumerator PayButton_Exit()
    {
        Debug.Log("退出商店！");
        animator.Play("ExitShop");
        yield return new WaitForSeconds(ExitAinmationTime);
        CloseShopUI();

    }

    //购买按键变灰
    public IEnumerator ChangeColor()
    {
        Debug.Log("金额不足，无法购买！");
        PayButtonAnimator.Play("ChangeColor");
        yield return new WaitForSeconds(ChangeColorTime);
    }

    //购买按键变回原色
    public IEnumerator ColorBack()
    {
        Debug.Log("可以继续购物！");
        PayButtonAnimator.Play("ColorBack");
        yield return new WaitForSeconds(ChangeColorTime);
    }

}
