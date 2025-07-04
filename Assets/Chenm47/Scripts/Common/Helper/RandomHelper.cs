using System;
using System.Collections.Generic;
using UnityEngine;

namespace Common.Helper
{
    /// <summary>
    /// 提供随机数的帮助方法，随机生成指定总和数目的数组（可限制每个数的大小）
    /// </summary>
    public static class RandomHelper
    {
        /// <summary>
        /// 根据权重获取指定枚举随机枚举值
        /// </summary>
        /// <param name="rightLst">权重列表</param>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <returns></returns>
        public static TEnum GetRandomLV<TEnum>((TEnum em, float right)[] rightLst) where TEnum : Enum
        {
            Type t = typeof(TEnum);
            float sum = 0;
            int i = 0;
            for (i = 0; i < rightLst.Length; i++)
            {
                sum += rightLst[i].right;
            }
            float rate = UnityEngine.Random.value;
            for (float currentSum = i = 0; i < rightLst.Length; i++)
            {
                currentSum += rightLst[i].right / sum;
                if (rate < currentSum)
                    return rightLst[i].em;
            }
            return rightLst[i - 1].em;
        }
        /// <summary>
        /// 生成总和一定值的指定数目的数组，并限制每个数的大小为指定值
        /// 若不可能，则将最大值自动调整至合适的数值
        /// </summary>
        /// <param name="sum">所有数的总和</param>
        /// <param name="count">生成数字的数目</param>
        /// <param name="max">每个数的最大值</param>
        /// <returns></returns>
        public static int[] CreatNums(int sum, int count, int max)
        {
            //判断是否可能
            if (count * max < sum)
            {
                string msg = $"不存在和为{sum}个数为{count}最大值为{max}的数";
                max = (int)Mathf.Ceil((float)sum / count);
                msg += $"，已将最大值替换为{max}";
                Debug.Log(msg);
            }

            int[] res = new int[count];
            for (int i = 0; i < count - 1; i++)
            {
                int floor = (sum - max * (count - i - 1));
                if (floor < 0) { floor = 0; }
                int ceiling = sum < max ? sum : max;
                int v = UnityEngine.Random.Range(floor, ceiling + 1);
                res[i] = v;
                sum -= v;
            }
            res[^1] = sum;
            return res;
        }
        /// <summary>
        /// 生成总和一定值的随机数序列
        /// </summary>
        /// <param name="sum"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static int[] CreatNums(int sum, int count)
        {
            List<int> result = new List<int>();
            result.Add(0);
            for (int i = 0; i < count - 1; i++)
            {
                result.Add(UnityEngine.Random.Range(int.MinValue, sum + 1));
            }
            result.Add(sum);
            //正负都要，不排序就行
            //result.Sort();
            for (int i = 0; i < result.Count - 1; i++)
            {
                result[i] = result[i + 1] - result[i];//想生成负数前减后就行
            }
            result.RemoveAt(result.Count - 1);
            return result.ToArray();
        }
    }
}

