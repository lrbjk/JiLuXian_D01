using Common;
using Common.Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace ns.Character
{
    /// <summary>
    /// 描述：读取角色参数配置，提供获取角色参数方法
    /// </summary>
    public static class CharacterParamManager
    {
        private static Dictionary<int, CharacterParam> dic;
        static CharacterParamManager()
        {
            //初始化字典
            dic = new Dictionary<int, CharacterParam>();
            //读取配置文件
            string s = ConfigurationReader.GetConfigFile("CharacterParamsConfig.txt");
            ConfigurationReader.LoadConfigFile(s, LoadParamInfo);
        }

        /*
 * 字符串规则
 * 
 * <<                                                          ----表头开始标志
 * sheetName                                       ----表名称
 * name@buffType@...                   ----(变量名称)
 *  >>                                                           ----表结束标志
 * <1>                                                       ----<ID>一个对象开始标志
 * 迅速成长                                             ----对应变量值
 * toolBuff
 * [sun,rain]                                          ----[多选变量值1,多选变量值2]
 * 
 * 
 * ==                                                          ----一个对象结束标志
 * <2>
 * 
 */
        private static bool isSheetStart = false;
        private static int lineIndex = 0;
        private static Type paramType;
        private static List<string> vNames = new List<string>();
        private static StringBuilder jsonstr = new StringBuilder();
        private static void LoadParamInfo(string line)
        {
            //表头处理
            if (line == "<<")
            {
                lineIndex = 0;
                vNames.Clear();
                isSheetStart = true;
                return;
            }
            else if (line == ">>")
            {
                //表头处理结束
                isSheetStart = false;
                lineIndex = 0;
                return;
            }
            if (isSheetStart)
            {//表头处理
                if (lineIndex == 0)
                {
                    //表名称就是对象类名
                    paramType = Type.GetType("Rogue.Character." + line);
                    lineIndex++;
                }
                else if (lineIndex == 1)
                {
                    //变量名称
                    vNames.AddRange(line.Split('@'));
                }
                return;
            }

            //对象处理
            if (line.StartsWith('<') && line.EndsWith('>'))
            {
                //对象处理开始
                //创建CharacterParam对象
                //temp = Activator.CreateInstance(paramType);
                jsonstr.Append("{");
                string id = line[1..^1];
                jsonstr.Append(JsonHelper.GetSimplePairs(vNames[lineIndex], id));
                lineIndex++;
                jsonstr.Append(",");
            }
            else if (line == "==")
            {
                jsonstr.Remove(jsonstr.Length - 1, 1);
                jsonstr.Append("}");
                //单个对象参数处理完成
                //CharacterParam temp = JsonUtility.FromJson(jsonstr.ToString(), paramType) as CharacterParam;
                string s = jsonstr.ToString();
                var temp = JsonConvert.DeserializeObject(s, paramType) as CharacterParam;
                dic.Add(temp.CharacterParamID, temp);
                lineIndex = 0;
                jsonstr.Clear();
            }
            else if (line == string.Empty)
            {
                string pair = $"\"{vNames[lineIndex]}\":null,";
                lineIndex++;
                jsonstr.Append(pair);
            }
            else
            {
                //读取相应参数
                string pair = JsonHelper.GetSimplePairs(vNames[lineIndex], line);
                lineIndex++;
                jsonstr.Append(pair);
                jsonstr.Append(",");
            }
        }

        public static CharacterParam GetCharacterParam(int id)
        {
            return dic[id];
        }
    }
}
