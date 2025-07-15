using Pixeye.Unity;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using UnityEditor;
using UnityEngine;

public class NewGizmos : CustomEditorSelector
{
    [DllImport("User32.dll")]
    public static extern short GetAsyncKeyState(int vkey); // �ⲿ����
    const int VK_MENU = 0x12;
    Object target;
    Event activeEvent;
    List<GizmosSetting> gizmosSettings;

    public void OnEnable(object target, Editor editor)
    {
        this.target = (Object)target;
        gizmosSettings = new List<GizmosSetting>();
        var fields = target.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.GetField | BindingFlags.Public).ToList();
        var GizmosSetting_ = fields.FindAll(x => x.FieldType == typeof(GizmosSetting));
        var GizmosSetting_List = fields.FindAll(x => x.FieldType == typeof(List<GizmosSetting>));

        foreach (var a in GizmosSetting_)
        {
            if ((GizmosSetting)a.GetValue(target) != null)
                gizmosSettings.Add((GizmosSetting)a.GetValue(target));
        }

        foreach (var a in GizmosSetting_List)
        {
            foreach (var b in (List<GizmosSetting>)a.GetValue(target))
            {
                if (b != null) gizmosSettings.Add(b);
            }
        }
    }

    public void OnSceneGUI(object target, Editor editor)
    {
        //Debug.Log("OnSceneGUI called");
        activeEvent = Event.current;
        foreach (var gizmosSetting in gizmosSettings)
        {
            DrawGizmosFunc(gizmosSetting);
        }
    }

    private void DrawGizmosFunc(GizmosSetting gizmosSetting)
    {
        if (gizmosSetting.openGizmos)
        {
            Handles.color = gizmosSetting.gizmosColor;
            if (gizmosSetting.GizmosType == DrawGizmosType.cube)
            {
                // 保存当前的变换矩阵
                Matrix4x4 savedMatrix = Handles.matrix;

                // 创建一个新的变换矩阵，其中包括旋转
                Matrix4x4 rotationMatrix = Matrix4x4.TRS(gizmosSetting.gizmosCenter, gizmosSetting.point.rotation, Vector3.one);

                // 应用新的变换矩阵
                Handles.matrix = rotationMatrix;

                // 绘制线框立方体
                Handles.DrawWireCube(Vector3.zero, gizmosSetting.size);

                // 恢复之前的变换矩阵
                Handles.matrix = savedMatrix;
            }
            else if (gizmosSetting.GizmosType == DrawGizmosType.sphere)
            {
                Handles.DrawWireDisc(gizmosSetting.gizmosCenter, Vector3.right, gizmosSetting.radius);
                Handles.DrawWireDisc(gizmosSetting.gizmosCenter, Vector3.up, gizmosSetting.radius);
                Handles.DrawWireDisc(gizmosSetting.gizmosCenter, Vector3.forward, gizmosSetting.radius);
            }
            else if (gizmosSetting.GizmosType == DrawGizmosType.capsule)
            {
                Vector3 point0 = gizmosSetting.gizmosCenter - new Vector3(0, gizmosSetting.size.y / 2, 0);
                Vector3 point1 = gizmosSetting.gizmosCenter + new Vector3(0, gizmosSetting.size.y / 2, 0);

                // ���ƽ��������������
                Handles.DrawWireDisc(point0, Vector3.right, gizmosSetting.radius);
                Handles.DrawWireDisc(point0, Vector3.up, gizmosSetting.radius);
                Handles.DrawWireDisc(point0, Vector3.forward, gizmosSetting.radius);

                Handles.DrawWireDisc(point1, Vector3.right, gizmosSetting.radius);
                Handles.DrawWireDisc(point1, Vector3.up, gizmosSetting.radius);
                Handles.DrawWireDisc(point1, Vector3.forward, gizmosSetting.radius);

                // ���������������������������
                Handles.DrawLine(point0 - new Vector3(gizmosSetting.radius, 0, 0), point1 - new Vector3(gizmosSetting.radius, 0, 0));
                Handles.DrawLine(point0 + new Vector3(gizmosSetting.radius, 0, 0), point1 + new Vector3(gizmosSetting.radius, 0, 0));
                Handles.DrawLine(point0 - new Vector3(0, gizmosSetting.radius, 0), point1 - new Vector3(0, gizmosSetting.radius, 0));
                Handles.DrawLine(point0 + new Vector3(0, gizmosSetting.radius, 0), point1 + new Vector3(0, gizmosSetting.radius, 0));
                Handles.DrawLine(point0 - new Vector3(0, 0, gizmosSetting.radius), point1 - new Vector3(0, 0, gizmosSetting.radius));
                Handles.DrawLine(point0 + new Vector3(0, 0, gizmosSetting.radius), point1 + new Vector3(0, 0, gizmosSetting.radius));
            }


        }
        if (gizmosSetting.editorMode) EditorModeFunc(gizmosSetting);
    }

    /// <summary>
    /// scene���϶���С
    /// </summary>
    private void EditorModeFunc(GizmosSetting setting)
    {
        Handles.color = setting.gizmosColor;
        if (setting.GizmosType == DrawGizmosType.cube)
        {
            HandlePartFunc(new Vector3(setting.size.x / 2, 0, 0), setting, DrawGizmosType.cube);
            HandlePartFunc(new Vector3(-setting.size.x / 2, 0, 0), setting, DrawGizmosType.cube);
            HandlePartFunc(new Vector3(0, setting.size.y / 2, 0), setting, DrawGizmosType.cube);
            HandlePartFunc(new Vector3(0, -setting.size.y / 2, 0), setting, DrawGizmosType.cube);
            HandlePartFunc(new Vector3(0, 0, setting.size.z / 2), setting, DrawGizmosType.cube);
            HandlePartFunc(new Vector3(0, 0, -setting.size.z / 2), setting, DrawGizmosType.cube);
            HandlePartFunc(Vector3.zero, setting, DrawGizmosType.cube);
        }
        else if (setting.GizmosType == DrawGizmosType.sphere)
        {
            HandlePartFunc(Vector3.zero, setting, DrawGizmosType.sphere);
        }
        else if (setting.GizmosType == DrawGizmosType.capsule)
        {
            HandlePartFunc(new Vector3(0, setting.size.y / 2, 0), setting, DrawGizmosType.capsule);
            HandlePartFunc(new Vector3(0, -setting.size.y / 2, 0), setting, DrawGizmosType.capsule);
            HandlePartFunc(Vector3.zero, setting, DrawGizmosType.capsule);
        }
    }

    private Vector3 NewCubeHandle(Vector3 position)
    {
        var fmh_121_49_638881397631120808 = Quaternion.identity; return Handles.FreeMoveHandle(position, 0.05f * HandleUtility.GetHandleSize(position), Vector3.one * 0.05f * HandleUtility.GetHandleSize(position), Handles.CubeHandleCap);
    }

    private void HandlePartFunc(Vector3 delta_position, GizmosSetting setting, DrawGizmosType type)
    {
        if (delta_position == Vector3.zero)
        {
            setting.offSet = NewCubeHandle(setting.gizmosCenter) - setting.point.position;
            return;
        }

        Vector3 mid_value;
        mid_value = NewCubeHandle(setting.gizmosCenter + delta_position) - (setting.gizmosCenter + delta_position);

        if (GetAsyncKeyState(VK_MENU) != 0)
        {
            switch (type)
            {
                case DrawGizmosType.cube:
                    if (Mathf.Abs(mid_value.x) > setting.size.x - 0.04) mid_value.x = Mathf.Sign(mid_value.x) * (setting.size.x - 0.05f);
                    if (Mathf.Abs(mid_value.y) > setting.size.y - 0.04) mid_value.y = Mathf.Sign(mid_value.y) * (setting.size.y - 0.05f);
                    if (Mathf.Abs(mid_value.z) > setting.size.z - 0.04) mid_value.z = Mathf.Sign(mid_value.z) * (setting.size.z - 0.05f);
                    if (delta_position.x > 0) setting.size.x += mid_value.x * 2;
                    else if (delta_position.x < 0) setting.size.x -= mid_value.x * 2;
                    if (delta_position.y > 0) setting.size.y += mid_value.y * 2;
                    else if (delta_position.y < 0) setting.size.y -= mid_value.y * 2;
                    if (delta_position.z > 0) setting.size.z += mid_value.z * 2;
                    else if (delta_position.z < 0) setting.size.z -= mid_value.z * 2;
                    break;
                case DrawGizmosType.sphere:
                    if (Mathf.Abs(mid_value.x) > setting.radius - 0.04) mid_value.x = Mathf.Sign(mid_value.x) * (setting.radius - 0.05f);
                    if (Mathf.Abs(mid_value.y) > setting.radius - 0.04) mid_value.y = Mathf.Sign(mid_value.y) * (setting.radius - 0.05f);
                    if (Mathf.Abs(mid_value.z) > setting.radius - 0.04) mid_value.z = Mathf.Sign(mid_value.z) * (setting.radius - 0.05f);
                    setting.radius += Mathf.Sqrt(mid_value.x * mid_value.x + mid_value.y * mid_value.y + mid_value.z * mid_value.z);
                    if (setting.radius < 0.05f) setting.radius = 0.05f;
                    break;
                case DrawGizmosType.capsule:
                    if (Mathf.Abs(mid_value.x) > setting.radius - 0.04) mid_value.x = Mathf.Sign(mid_value.x) * (setting.radius - 0.05f);
                    if (Mathf.Abs(mid_value.y) > setting.radius - 0.04) mid_value.y = Mathf.Sign(mid_value.y) * (setting.radius - 0.05f);
                    if (Mathf.Abs(mid_value.z) > setting.radius - 0.04) mid_value.z = Mathf.Sign(mid_value.z) * (setting.radius - 0.05f);
                    if (delta_position.y > 0) setting.size.y += mid_value.y * 2;
                    else if (delta_position.y < 0) setting.size.y -= mid_value.y * 2;
                    setting.radius += Mathf.Sqrt(mid_value.x * mid_value.x + mid_value.y * mid_value.y + mid_value.z * mid_value.z);
                    if (setting.radius < 0.05f) setting.radius = 0.05f;
                    break;
            }
        }
        else
        {
            switch (type)
            {
                case DrawGizmosType.cube:
                    if (Mathf.Abs(mid_value.x) > setting.size.x - 0.04) mid_value.x = Mathf.Sign(mid_value.x) * (setting.size.x - 0.05f);
                    if (Mathf.Abs(mid_value.y) > setting.size.y - 0.04) mid_value.y = Mathf.Sign(mid_value.y) * (setting.size.y - 0.05f);
                    if (Mathf.Abs(mid_value.z) > setting.size.z - 0.04) mid_value.z = Mathf.Sign(mid_value.z) * (setting.size.z - 0.05f);
                    if (delta_position.x > 0)
                    {
                        setting.size.x += mid_value.x * 2;
                        setting.offSet.x += mid_value.x;
                    }
                    else if (delta_position.x < 0)
                    {
                        setting.size.x -= mid_value.x * 2;
                        setting.offSet.x -= mid_value.x;
                    }
                    if (delta_position.y > 0)
                    {
                        setting.size.y += mid_value.y * 2;
                        setting.offSet.y += mid_value.y;
                    }
                    else if (delta_position.y < 0)
                    {
                        setting.size.y -= mid_value.y * 2;
                        setting.offSet.y -= mid_value.y;
                    }
                    if (delta_position.z > 0)
                    {
                        setting.size.z += mid_value.z * 2;
                        setting.offSet.z += mid_value.z;
                    }
                    else if (delta_position.z < 0)
                    {
                        setting.size.z -= mid_value.z * 2;
                        setting.offSet.z -= mid_value.z;
                    }
                    break;
                case DrawGizmosType.sphere:
                    if (Mathf.Abs(mid_value.x) > setting.radius - 0.04) mid_value.x = Mathf.Sign(mid_value.x) * (setting.radius - 0.05f);
                    if (Mathf.Abs(mid_value.y) > setting.radius - 0.04) mid_value.y = Mathf.Sign(mid_value.y) * (setting.radius - 0.05f);
                    if (Mathf.Abs(mid_value.z) > setting.radius - 0.04) mid_value.z = Mathf.Sign(mid_value.z) * (setting.radius - 0.05f);
                    setting.radius += Mathf.Sqrt(mid_value.x * mid_value.x + mid_value.y * mid_value.y + mid_value.z * mid_value.z);
                    setting.offSet += mid_value;
                    if (setting.radius < 0.05f) setting.radius = 0.05f;
                    break;
                case DrawGizmosType.capsule:
                    if (Mathf.Abs(mid_value.x) > setting.radius - 0.04) mid_value.x = Mathf.Sign(mid_value.x) * (setting.radius - 0.05f);
                    if (Mathf.Abs(mid_value.y) > setting.radius - 0.04) mid_value.y = Mathf.Sign(mid_value.y) * (setting.radius - 0.05f);
                    if (Mathf.Abs(mid_value.z) > setting.radius - 0.04) mid_value.z = Mathf.Sign(mid_value.z) * (setting.radius - 0.05f);
                    if (delta_position.y > 0)
                    {
                        setting.size.y += mid_value.y * 2;
                        setting.offSet.y += mid_value.y;
                    }
                    else if (delta_position.y < 0)
                    {
                        setting.size.y -= mid_value.y * 2;
                        setting.offSet.y -= mid_value.y;
                    }
                    setting.radius += Mathf.Sqrt(mid_value.x * mid_value.x + mid_value.y * mid_value.y + mid_value.z * mid_value.z);
                    setting.offSet += mid_value;
                    if (setting.radius < 0.05f) setting.radius = 0.05f;
                    break;
            }
        }
        EditorUtility.SetDirty(target);
    }

    public bool IsMatch(object target, Editor editor)
    {
        return gizmosSettings.Count > 0;
    }

    public void OnInspectorGUI(object target, Editor editor)
    {
        //foreach (var gizmosSetting in gizmosSettings)
        //{
        //    EditorGUILayout.BeginVertical("box");
        //    gizmosSetting.editorMode = EditorGUILayout.Toggle("Editor Mode", gizmosSetting.editorMode);
        //    gizmosSetting.openGizmos = EditorGUILayout.Toggle("Open Gizmos", gizmosSetting.openGizmos);
        //    gizmosSetting.GizmosType = (DrawGizmosType)EditorGUILayout.EnumPopup("Gizmos Type", gizmosSetting.GizmosType);
        //    gizmosSetting.point = (Transform)EditorGUILayout.ObjectField("Point", gizmosSetting.point, typeof(Transform), true);
        //    gizmosSetting.radius = EditorGUILayout.FloatField("Radius", gizmosSetting.radius);
        //    gizmosSetting.size = EditorGUILayout.Vector3Field("Size", gizmosSetting.size);
        //    gizmosSetting.offSet = EditorGUILayout.Vector3Field("Offset", gizmosSetting.offSet);
        //    gizmosSetting.gizmosColor = EditorGUILayout.ColorField("Gizmos Color", gizmosSetting.gizmosColor);
        //    EditorGUILayout.EndVertical();
        //}
    }

    public void OnDisable(object target, Editor editor)
    {
    }

    public bool RequiresConstantRepaint(object target, Editor editor)
    {
        return EditorFramework.needToRepaint;
    }
}
