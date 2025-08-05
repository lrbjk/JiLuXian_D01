using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class SelectableImage : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [Header("Component References")]
    [SerializeField] private Image targetImage;
    [SerializeField] private Transform targetTransform;
    [SerializeField] private Image borderImage; // 白色边框图片
    
    [Header("Hover Effects")]
    [SerializeField] private Color hoverColor = new Color(0.95f, 0.95f, 0.95f, 1f);
    [SerializeField] private Vector3 hoverScale = new Vector3(1.1f, 1.1f, 1.1f);
    [SerializeField] private float hoverDuration = 0.15f;
    
    [Header("Selection Effects")]
    [SerializeField] private Color selectedColor = Color.white;
    [SerializeField] private Vector3 selectedScale = new Vector3(1.3f, 1.3f, 1.3f);
    [SerializeField] private Color borderColor = new Color(1f, 1f, 1f, 0.8f); // 白色半透明边框
    [SerializeField] private float selectionDuration = 0.2f;
    
    private Color originalColor;
    private Vector3 originalScale;
    private bool isSelected = false;
    
    private void Awake()
    {
        // 自动获取引用
        if (targetImage == null) targetImage = GetComponent<Image>();
        if (targetTransform == null) targetTransform = transform;
        
        // 初始化状态
        originalColor = targetImage.color;
        originalScale = targetTransform.localScale;
        
        //初始时隐藏边框
        if (borderImage != null)
        {
            borderImage.color = new Color(1f, 1f, 1f, 0f);
        }
    }
    
    // 鼠标悬停效果
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isSelected) return;
        
        targetImage.DOColor(hoverColor, hoverDuration);
        targetTransform.DOScale(hoverScale, hoverDuration).SetEase(Ease.OutQuad);
    }
    
    // 鼠标离开效果
    public void OnPointerExit(PointerEventData eventData)
    {
        if (isSelected) return;
        
        targetImage.DOColor(originalColor, hoverDuration);
        targetTransform.DOScale(originalScale, hoverDuration).SetEase(Ease.InOutQuad);
    }
    
    // 点击处理
    public void OnPointerClick(PointerEventData eventData)
    {
        SelectionManager.Instance.SelectImage(this);
    }
    
    // 设置选中状态
    public void SetSelected(bool selected)
    {
        isSelected = selected;
        
        if (selected)
        {
            // 选中效果
            targetImage.DOColor(selectedColor, selectionDuration);
            targetTransform.DOScale(selectedScale, selectionDuration).SetEase(Ease.OutQuad);
            
            // 显示白色边框（如果后面有的话）
            if (borderImage != null)
            {
                borderImage.DOColor(borderColor, selectionDuration);
            }
         
        }
        else
        {
            // 取消选中效果
            targetImage.DOColor(originalColor, selectionDuration);
            targetTransform.DOScale(originalScale, selectionDuration).SetEase(Ease.InOutQuad);
            
            // 隐藏白色边框
            if (borderImage != null)
            {
                borderImage.DOColor(new Color(1f, 1f, 1f, 0f), selectionDuration);
            }
        }
    }
}