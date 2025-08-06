using System.IO;
using UnityEditor;

namespace Common.MyEditor
{
    /// <summary>
    /// 描述：生成res文件目录config类
    /// </summary>
    public class GenerateResConfig : Editor
    {
        [MenuItem("Tools/Resources/Generate Resource Config")]
        public static void Generate()
        {
            //生成配置文件
            string[] prefabPaths = GetPaths("prefab", "prefab");            //获取预制体路径
            string[] pngPaths = GetPaths("texture2d", "png");            //获取png图片(texture2d)路径
            //string[] spriteatlasPaths = GetPaths("spriteatlas", "spriteatlas");            //获取spriteAtlas路径
            string[] SOPaths = GetPaths("ScriptableObject", "asset");            //获取spriteAtlas路径
            //3.写入文件
            File.WriteAllLines("Assets/StreamingAssets/ConfigMap.txt", prefabPaths);
            File.AppendAllLines("Assets/StreamingAssets/ConfigMap.txt", pngPaths);
            //File.AppendAllLines("Assets/StreamingAssets/ConfigMap.txt", spriteatlasPaths);
            File.AppendAllLines("Assets/StreamingAssets/ConfigMap.txt", SOPaths);
            AssetDatabase.Refresh();
        }
        /// <summary>
        /// 获取resource文件夹下指定类型的所有文件路径(格式：flienName=path)
        /// </summary>
        /// <param name="filetype"></param>
        /// <param name="fileExtension">文件后缀名称</param>
        private static string[] GetPaths(string filetype, string fileExtension)
        {
            //1.查找res目录下所有预制件路径
            string[] paths = AssetDatabase.FindAssets($"t:{filetype}", new string[] { "Assets/Resources" });
            //GUID
            for (int i = 0; i < paths.Length; i++)
            {
                paths[i] = AssetDatabase.GUIDToAssetPath(paths[i]);
                //Assets/Resources/Skill/BaseMeleeAttackSkill.prefab
                //2.生成对应关系 名称=路径
                string fileName = Path.GetFileNameWithoutExtension(paths[i]);
                string pathName = paths[i].Replace("Assets/Resources/", string.Empty).Replace($".{fileExtension}", string.Empty);
                paths[i] = fileName + "=" + pathName;
            }
            return paths;
        }
    }
}
