using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Common
{
    /// <summary>
    /// 描述：资源管理器
    /// </summary>
    public class ResourceManager
    {
        private static Dictionary<string, string> configMap;
        static ResourceManager()
        {
            configMap = new Dictionary<string, string>();
            //加载文件
            string fileContent = ConfigurationReader.GetConfigFile("ConfigMap.txt");
            //解析文件(string--> Dictionary<string,string>)
            ConfigurationReader.LoadConfigFile(fileContent, BuildMap);
        }
        public static void BuildMap(string line)
        {
            //Blank=Prefabs/SavedGos/Blank
            //BaseMeleeAttackSkill = Skill / BaseMeleeAttackSkill
            string[] strings = line.Split('=');
            string n = strings[0];
            string path = strings[1];
            configMap.Add(n, path);
        }

        public static T Load<T>(string fileName) where T : Object
        {
            //prefabName -->Path
            string path = configMap[fileName];
            //读取配置文件
            return Resources.Load<T>(path);
        }
    }
}
