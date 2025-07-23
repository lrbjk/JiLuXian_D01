using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class SmoothNormalTools : EditorWindow
{
    // 工具参数
    private GameObject targetModel;
    private float smoothAngle = 60f;
    private bool overwriteUV2 = true;

    // 添加菜单项
    [MenuItem("Tools/Smooth Normal Tool")]
    public static void ShowWindow()
    {
        GetWindow<SmoothNormalTools>("Smooth Normal Tool");
    }

    private void OnGUI()
    {
        GUILayout.Label("Smooth Normal Settings", EditorStyles.boldLabel);

        // 模型选择
        targetModel = (GameObject)EditorGUILayout.ObjectField(
            "Target Model", 
            targetModel, 
            typeof(GameObject), 
            true
        );

        // 平滑角度设置
        smoothAngle = EditorGUILayout.Slider("Smooth Angle", smoothAngle, 0f, 180f);

        // 覆盖选项
        overwriteUV2 = EditorGUILayout.Toggle("Overwrite UV2", overwriteUV2);

        // 处理按钮
        if (GUILayout.Button("Process Smooth Normals"))
        {
            if (targetModel != null)
            {
                ProcessModel();
            }
            else
            {
                Debug.LogError("Please assign a target model");
            }
        }
    }

    private void ProcessModel()
    {
        // 获取所有MeshFilter
        MeshFilter[] meshFilters = targetModel.GetComponentsInChildren<MeshFilter>();
        
        foreach (MeshFilter mf in meshFilters)
        {
            Mesh mesh = mf.sharedMesh;
            
            // 验证Mesh可读性
            if (!mesh.isReadable)
            {
                Debug.LogError($"Mesh {mesh.name} is not readable! Enable Read/Write in import settings.");
                continue;
            }

            // 计算平滑法线
            Vector3[] smoothNormals = CalculateSmoothNormals(mesh);

            // 转换法线到UV2
            ConvertNormalsToUV2(mesh, smoothNormals, overwriteUV2);

            // 标记修改
            EditorUtility.SetDirty(mesh);
        }
        
        AssetDatabase.SaveAssets();
        Debug.Log("Smooth normal processing completed!");
    }

    private Vector3[] CalculateSmoothNormals(Mesh mesh)
    {
        Vector3[] normals = mesh.normals;
        Vector3[] vertices = mesh.vertices;
        int[] triangles = mesh.triangles;

        // 创建顶点邻接字典
        Dictionary<int, List<int>> vertexTriangles = new Dictionary<int, List<int>>();
        for (int i = 0; i < vertices.Length; i++)
        {
            vertexTriangles[i] = new List<int>();
        }

        // 记录每个顶点所属的三角形
        for (int i = 0; i < triangles.Length; i += 3)
        {
            vertexTriangles[triangles[i]].Add(i / 3);
            vertexTriangles[triangles[i + 1]].Add(i / 3);
            vertexTriangles[triangles[i + 2]].Add(i / 3);
        }

        Vector3[] smoothNormals = new Vector3[vertices.Length];
        float cosThreshold = Mathf.Cos(smoothAngle * Mathf.Deg2Rad);

        for (int i = 0; i < vertices.Length; i++)
        {
            List<int> adjacentTriangles = vertexTriangles[i];
            List<Vector3> accumulatedNormals = new List<Vector3>();

            // 收集符合角度条件的法线
            foreach (int triIndex in adjacentTriangles)
            {
                Vector3 triNormal = normals[triangles[triIndex * 3]];
                
                // 角度判断
                if (Vector3.Dot(normals[i], triNormal) >= cosThreshold)
                {
                    accumulatedNormals.Add(triNormal);
                }
            }

            // 计算平均法线
            Vector3 finalNormal = Vector3.zero;
            foreach (Vector3 n in accumulatedNormals)
            {
                finalNormal += n.normalized;
            }
            finalNormal = finalNormal.normalized;

            smoothNormals[i] = finalNormal;
        }

        return smoothNormals;
    }

    private void ConvertNormalsToUV2(Mesh mesh, Vector3[] smoothNormals, bool overwrite)
    {
        Vector2[] uv2 = overwrite ? new Vector2[mesh.vertexCount] : mesh.uv2;

        // 转换法线到UV2（使用球面坐标编码）
        for (int i = 0; i < smoothNormals.Length; i++)
        {
            Vector3 n = smoothNormals[i];
            
            // 球面坐标编码（将法线转换为UV）
            float u = (Mathf.Atan2(n.x, n.z) + Mathf.PI) / (2 * Mathf.PI);
            float v = Mathf.Asin(n.y) / Mathf.PI + 0.5f;
            
            uv2[i] = new Vector2(u, v);
        }

        mesh.uv2 = uv2;
    }
}