using Common.Attributes;
using UnityEditor;
using UnityEngine;

namespace Common.MyEditor
{
    [CustomPropertyDrawer(typeof(CustomLabelAttribute))]
    /// <summary>
    /// 描述：定义对带有 `CustomLabelAttribute` 特性的字段的面板内容的绘制行为。
    /// </summary>
    public class CustomLabelDrawer : PropertyDrawer
    {
        private GUIContent _label = null;
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (_label == null)
            {
                string name = (attribute as CustomLabelAttribute).name;
                _label = new GUIContent(name);
            }
            EditorGUI.PropertyField(position, property, _label);
        }
    }
}
