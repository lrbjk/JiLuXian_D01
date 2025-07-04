using UnityEngine;

namespace Common.Attributes
{
    /// <summary>
    /// 描述：自定义显示字段名称
    /// </summary>
    public class CustomLabelAttribute : PropertyAttribute
    {
        public string name;
        public CustomLabelAttribute(string name) { this.name = name; }
    }
}