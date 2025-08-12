using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
public class DiamondButton : MonoBehaviour
{
    private Image _image;

    void Start()
    {
        _image = GetComponent<Image>();

        // 关键设置：Alpha测试阈值
        _image.alphaHitTestMinimumThreshold = 0.5f; // 推荐初始值

        // 确保Image设置正确
        _image.raycastTarget = true;
        _image.preserveAspect = true;
    }
}
