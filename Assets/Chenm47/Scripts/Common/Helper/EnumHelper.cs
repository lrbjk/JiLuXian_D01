using System.Collections.Generic;
using System;

namespace Helper
{
    /// <summary>
    /// 描述：
    /// </summary>
    public static class EnumHelper
    {
        /// <summary>
        /// 获取任意多选枚举变量包含的全部枚举名称列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selectedValues"></param>
        /// <returns></returns>
        public static string[] GetIncludedEnumNames<T>(this T selectedValues) where T : Enum
        {
            var resultList = new List<string>();

            foreach (T value in Enum.GetValues(typeof(T)))
            {
                // 跳过 None 或不包含的值
                if (Convert.ToInt64(value) == 0) continue;

                // 检查是否包含当前的枚举值
                if (selectedValues.HasFlag(value))
                {
                    resultList.Add(value.ToString());
                }
            }
            return resultList.ToArray();
        }
    }
}
