using UnityEngine;
using UnityEngine.UI;

public class SimpleKeyToButton : MonoBehaviour
{
    [Header("按键设置")]
    public KeyCode triggerKey = KeyCode.Tab;

    [Header("按钮引用")]
    public Button targetButton;

    private void Update()
    {
        // 检测按键按下且按钮可交互
        if (Input.GetKeyDown(triggerKey))
        {
            if (targetButton != null && targetButton.interactable)
            {
                // 直接触发点击事件
                targetButton.onClick.Invoke();
            }
        }
    }

    // 编辑器辅助：重置时自动获取按钮组件
    private void Reset()
    {
        if (targetButton == null)
        {
            targetButton = GetComponent<Button>();
        }
    }
}