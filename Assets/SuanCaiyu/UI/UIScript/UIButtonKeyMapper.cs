using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System.Linq;

public class UIButtonKeyMapper : MonoBehaviour
{
    [System.Serializable]
    public class ButtonKeyPair
    {
        public Button targetButton;
        public KeyCode keyCode;
        [Tooltip("冷却时间（秒）")]
        public float cooldown = 0.5f;
        [HideInInspector] public float lastPressTime;
    }

    [Header("按键映射设置")]
    public List<ButtonKeyPair> buttonKeyMappings = new List<ButtonKeyPair>();

    [Header("UI层级管理")]
    [Tooltip("允许响应按键的UI层级")]
    public List<string> allowedUILayers = new List<string> { "UI" };
    [Tooltip("是否检查父Canvas激活状态")]
    public bool checkCanvasActive = true;

    private void Update()
    {
        foreach (var mapping in buttonKeyMappings)
        {
            if (Input.GetKeyDown(mapping.keyCode) &&
                Time.time - mapping.lastPressTime >= mapping.cooldown)
            {
                if (IsInteractionAllowed(mapping.targetButton))
                {
                    mapping.targetButton.onClick.Invoke();
                    mapping.lastPressTime = Time.time;
                }
            }
        }
    }

    /// <summary>
    /// 检查按钮是否允许交互
    /// </summary>
    private bool IsInteractionAllowed(Button button)
    {
        if (button == null || !button.interactable)
            return false;

        // 检查父Canvas状态
        if (checkCanvasActive)
        {
            Canvas canvas = button.GetComponentInParent<Canvas>();
            if (canvas != null && !canvas.isActiveAndEnabled)
                return false;
        }

        // 检查UI层级
        if (allowedUILayers.Count > 0)
        {
            bool layerAllowed = false;
            foreach (var layer in allowedUILayers)
            {
                if (LayerMask.LayerToName(button.gameObject.layer) == layer)
                {
                    layerAllowed = true;
                    break;
                }
            }
            if (!layerAllowed) return false;
        }

        return true;
    }

    // 编辑器辅助方法
    public void AddNewMapping() => buttonKeyMappings.Add(new ButtonKeyPair());
    public void RemoveMapping(int index) => buttonKeyMappings.RemoveAt(index);

#if UNITY_EDITOR
    private void OnValidate()
    {
        // 自动设置新建映射的默认值
        foreach (var mapping in buttonKeyMappings)
        {
            if (mapping.cooldown < 0) mapping.cooldown = 0;
        }
    }
#endif
}