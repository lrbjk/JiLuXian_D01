using UnityEngine;
using UnityEngine.UI;

public class ToggleAlphaController : MonoBehaviour
{
    [SerializeField] private Toggle toggle;
    [SerializeField] private CanvasGroup canvasGroup;

    private void Start()
    {
        if (toggle == null)
            toggle = GetComponent<Toggle>();

        if (canvasGroup == null)
            canvasGroup = GetComponent<CanvasGroup>();

        // 初始状态同步
        UpdateToggleAlpha(toggle.isOn);

        // 监听 Toggle 状态变化
        toggle.onValueChanged.AddListener(UpdateToggleAlpha);
    }

    private void UpdateToggleAlpha(bool isOn)
    {
        if (canvasGroup == null)
            return;

        canvasGroup.alpha = isOn ? 1f : 0f;
    }

    private void OnDestroy()
    {
        toggle.onValueChanged.RemoveListener(UpdateToggleAlpha);
    }
}