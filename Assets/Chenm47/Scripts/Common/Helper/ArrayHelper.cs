using System;
using UnityEngine;

namespace Common
{
    /// <summary>
    /// 描述：
    /// </summary>
    public static class ArrayHelper
    {

        public static T Find<T>(this T[] array, Func<T, bool> predicate)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (predicate(array[i]))
                    return array[i];
            }
            return default(T);
        }

        /// <summary>
        /// 根据Q数组中Q对象，映射为T对象数组
        /// </summary>
        /// <typeparam name="T">目标对象数组类型</typeparam>
        /// <typeparam name="Q">待选择数组类型</typeparam>
        /// <param name="array">待选择数组</param>
        /// <param name="condition">条件</param>
        /// <returns>返回T的数组</returns>
        public static T[] Select<Q, T>(this Q[] array, Func<Q, T> condition)
        {
            T[] res = new T[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                res[i] = condition(array[i]);
            }
            return res;
        }

        public static void ConvertSelf<T>(this T[] array, Func<T, T> converter)
        {
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = converter(array[i]);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="condition">判断依据</param>
        /// <returns></returns>
        public static T GetMin<T>(this T[] array, Func<T, float> condition)
        {
            T min = array[0];
            for (int i = 1; i < array.Length; i++)
            {
                if (condition(array[i]) < condition(min))//A(array[i])是否比B(min)小？
                {
                    min = array[i];
                }
            }
            return min;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="condition">参数1对象是否比参数2对象小？</param>
        /// <returns></returns>
        public static T GetMin<T>(this T[] array, Func<T, T, bool> condition)
        {
            T min = array[0];
            for (int i = 1; i < array.Length; i++)
            {
                if (condition(array[i], min))//A(array[i])是否比B(min)小？
                {
                    min = array[i];
                }
            }
            return min;
        }


    }
}
