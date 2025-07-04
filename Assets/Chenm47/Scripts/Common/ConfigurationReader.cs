using System;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

namespace Common
{
    /// <summary>
    /// 描述：StreamAsset文件夹下配置读取器
    /// </summary>
    public class ConfigurationReader
    {
        /// <summary>
        /// 获取StreamingAssets文件夹下的文件
        /// </summary>
        /// <param name="fileFullName"></param>
        /// <returns></returns>
        public static string GetConfigFile(string fileFullName)
        {
            string url;

            #region 获取文件路径url
#if UNITY_EDITOR || UNITY_STANDALONE
            url = "file://" + Application.dataPath + "/StreamingAssets/" + fileFullName;
#elif UNITY_ANDROID
            url= "jar:file://" + Application.dataPath + "!/assets/" + fileName;
#elif UNITY_IOS
            url="file://" + Application.dataPath + "/Raw/" + fileName;
#endif
            #endregion

            UnityWebRequest www = UnityWebRequest.Get(url);
            www.SendWebRequest();
            while (true)
            {
                if (www.downloadHandler.isDone)
                    return www.downloadHandler.text;
            }
        }

        /// <summary>
        /// 加载配置文件
        /// </summary>
        /// <param name="fileContent">配置文件内容</param>
        /// <param name="handle">行处理逻辑</param>
        public static void LoadConfigFile(string fileContent, Action<string> handle)
        {
            using (StringReader sr = new StringReader(fileContent))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    handle(line);
                }
            }
        }

    }
}
