using Common.UI;
using ns.Character.Player;
using System;
using System.Collections.Generic;

namespace Common.Helper
{
    /// <summary>
    /// 描述：数值计算相关的助手类
    /// </summary>
    public static class ValueHelper
    {
        /// <summary>
        /// 考虑当前量与上下限后的改变量
        /// </summary>
        /// <param name="currentValue"></param>
        /// <param name="delta"></param>
        /// <param name="floor"></param>
        /// <param name="ceil"></param>
        /// <returns></returns>
        public static int SmoothDeltaByFloor(int currentValue, int delta, int floor, int ceil)
        {
            if (delta < 0)
            {
                delta = currentValue + delta < floor ? -currentValue : delta;
            }
            else
            {
                delta = currentValue + delta > ceil ? ceil - currentValue : delta;
            }
            return delta;
        }
        public static float SmoothDeltaByFloor(float currentValue, float delta, float floor, float ceil)
        {
            if (delta < 0)
            {
                delta = currentValue + delta < floor ? -currentValue : delta;
            }
            else
            {
                delta = currentValue + delta > ceil ? ceil - currentValue : delta;
            }
            return delta;
        }

        public static int DeltaHandleVale(int currentValue, int delta, int floor, int ceil)
        {
            currentValue += delta;
            if (delta > 0)
                currentValue = Math.Min(currentValue, ceil);
            else
                currentValue = Math.Max(currentValue, floor);
            return currentValue;
        }

    }
}
