using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class SmoothNormalTools : EditorWindow
{
    // 工具参数
    private GameObject targetModel;

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

            SmoothNormal(mesh);

        }

    }
    
    private struct WeightedNormal
    {
        public Vector3 normal;
        public float weight;
    }
    
    
        private void SmoothNormal(Mesh mesh)
    {
        //创建一个字典，存顶点-顶点所有法线的键值对，顶点由唯一的Vector3来确定
        var normalDict = new Dictionary<Vector3, List<WeightedNormal>>();
        var triangles = mesh.triangles;
        var vertices = mesh.vertices;
        var normals = mesh.normals;
        var tangents = mesh.tangents;
        var smoothNormals = mesh.normals;
        //三角形个数为triangles数组的长度/3
        var n = triangles.Length / 3;
        for (var i = 0; i < n; i++)
        {
            //第i个三角形的三个顶点的索引分别是i*3,i*3+1,i*3=2
            var vertexIndices = new[] {triangles[i * 3], triangles[i * 3 + 1], triangles[i * 3 + 2]};
            for (var j = 0; j < 3; j++)
            {
                var vertexIndex = vertexIndices[j];
                var vertexPosition = vertices[vertexIndex];
                if (!normalDict.ContainsKey(vertexPosition))
                {
                    normalDict.Add(vertexPosition, new List<WeightedNormal>());
                }
                WeightedNormal weightedNormal;
                weightedNormal.normal = normals[vertexIndex];
                //获取当前顶点出发的两条边
                var lineA = Vector3.zero;
                var lineB = Vector3.zero;
                if (j == 0)
                {
                    lineA = vertices[vertexIndices[1]] - vertexPosition;
                    lineB = vertices[vertexIndices[2]] - vertexPosition;
                }
                else if (j == 1)
                {
                    lineA = vertices[vertexIndices[0]] - vertexPosition;
                    lineB = vertices[vertexIndices[2]] - vertexPosition;
                }
                else if (j == 3)
                {
                    lineA = vertices[vertexIndices[0]] - vertexPosition;
                    lineB = vertices[vertexIndices[1]] - vertexPosition;
                }
                //把角度作为权重，记录起来，
                var angle = Vector3.Angle(lineA, lineB) * Mathf.Deg2Rad;
                weightedNormal.weight = angle;
                normalDict[vertexPosition].Add(weightedNormal);
            }
        }
        for (var index = 0; index < vertices.Length; index++)
        {
            var vertex = vertices[index];
            var weightedNormalList = normalDict[vertex];
            var smoothNormal = Vector3.zero;
            float weightSum = 0;
            foreach (var weightedNormal in weightedNormalList)
            {
                weightSum += weightedNormal.weight;
            }
            foreach (var weightedNormal in weightedNormalList)
            {
                smoothNormal = smoothNormal + weightedNormal.normal * weightedNormal.weight / weightSum;
            }
            smoothNormal = smoothNormal.normalized;
            smoothNormals[index] = smoothNormal;
            //构建三个正交向量，作为切线空间的坐标轴
            var normal = normals[index];
            var tangent = tangents[index];
            var biTangent = (Vector3.Cross(normal, tangent) * tangent.w).normalized;
            //构建TBN矩阵
            var tbn = new Matrix4x4(tangent, biTangent, normal, Vector3.zero);
            //相当于smoothNormal重新投影到三个坐标轴上，获取在切线空间下的法线。
            smoothNormals[index] = tbn.transpose.MultiplyVector(smoothNormal).normalized;
        }
        mesh.SetUVs(7, smoothNormals);
        Debug.Log("平滑法线成功");
    }
    
}