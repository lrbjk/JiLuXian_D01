namespace Common.Helper
{
    /// <summary>
    /// 描述：
    /// </summary>
    public static class JsonHelper
    {
        /// <summary>
        /// 获取普通键值对Json格式："name":"strvalue"或"name":"numvalue"
        /// </summary>
        /// <param name="vName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetSimplePairs(string vName, string value)
        {
            float f;
            if (float.TryParse(value, out f))
                return "\"" + vName + "\"" + ":" + value;
            return "\"" + vName + "\"" + ":" + "\"" + value + "\"";
        }
    }
}
