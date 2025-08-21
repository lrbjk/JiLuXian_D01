using UnityEditor;
using UnityEngine;

namespace ns.Movtion.MovtionEditor
{
    /// <summary>
    /// 描述：
    /// </summary>
    [CustomEditor(typeof(MovtionInfo))]
    public class MovtionInfoEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            SerializedProperty prop = serializedObject.GetIterator();
            bool enterChildren = true;

            while (prop.NextVisible(enterChildren))
            {
                enterChildren = false;

                if (prop.name == "m_Script")   // Unity 内部字段，正常只读
                {
                    GUI.enabled = false;
                    EditorGUILayout.PropertyField(prop, true);
                    GUI.enabled = true;
                    continue;
                }
                if (prop.name == "HitTargetShake")
                    continue;
                if (prop.name == "MovtionShake")
                    continue;
                // 用自定义逻辑绘制 ShakeRequest
                if (prop.name == "EnableHitTargetShake")
                {
                    DrawEnableProp("EnableHitTargetShake", "HitTargetShake");
                    // 跳过 "HitTargetShake" ，避免重复
                    continue;
                }
                if (prop.name == "EnableMovtionShake")
                {
                    DrawEnableProp("EnableMovtionShake", "MovtionShake");
                    continue;
                }

                // 其他字段一律正常绘制
                EditorGUILayout.PropertyField(prop, true);
            }

            serializedObject.ApplyModifiedProperties();
        }

        private void DrawEnableProp(string boolName, string propName)
        {
            SerializedProperty enableConfig = serializedObject.FindProperty(boolName);
            SerializedProperty config = serializedObject.FindProperty(propName);

            EditorGUILayout.PropertyField(enableConfig);

            if (enableConfig.boolValue)
            {
                EditorGUILayout.PropertyField(config, true); // true 递归绘制子字段
            }
            else
            {
                //// 如果关掉开关，则置 null
                //config.Reset();
            }
        }
    }
}
