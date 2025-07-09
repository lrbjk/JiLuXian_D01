using System.Collections.Generic;

namespace AI.FSM
{
    /// <summary>
    /// 描述：配置文件工厂，保证一份文件只需要读取一次
    /// </summary>
    public class AIConfigurationReaderFactory
    {
        private static Dictionary<string, AIConfigurationReader> cache;

        static AIConfigurationReaderFactory()
        {
            cache = new Dictionary<string, AIConfigurationReader>();
        }

        public static AIConfigurationReader GetStatesMap(string fileName)
        {
            if (fileName == null)
                return null;
            if (!cache.ContainsKey(fileName))
            {
                cache.Add(fileName, new AIConfigurationReader(fileName));
            }
            return cache[fileName];
        }

    }
}
