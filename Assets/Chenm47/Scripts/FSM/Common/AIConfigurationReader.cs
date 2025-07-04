using Common;
using System.Collections.Generic;

namespace AI.FSM
{
    /// <summary>
    /// 描述：ai状态配置文件读取器
    /// </summary>
    public class AIConfigurationReader
    {
        private Dictionary<string, Dictionary<string, string>> statesMap;
        public Dictionary<string, Dictionary<string, string>> StatesMap { get => statesMap; }

        public AIConfigurationReader(string fileFullName)
        {
            var s = ConfigurationReader.GetConfigFile(fileFullName);
            statesMap = new Dictionary<string, Dictionary<string, string>>();
            ConfigurationReader.LoadConfigFile(s, LoadState);
        }

        private string currentState;
        private void LoadState(string line)
        {
            //[idle]
            //nohealth>death
            //
            //[statexxx]
            //.....
            line = line.Trim();
            if (string.IsNullOrEmpty(line)) return;
            if (line.StartsWith('['))
            {
                currentState = line.Trim('[', ']');
                statesMap.Add(currentState, new Dictionary<string, string>());
            }
            else
            {
                var names = line.Split(">");
                statesMap[currentState].Add(names[0], names[1]);
            }

        }
    }
}
