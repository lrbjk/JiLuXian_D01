using System;
using System.Collections.Generic;

namespace ns.Value
{
    public class ConfigurablePiecewiseFunction
    {
        // 存储分段规则：条件谓词 + 计算函数
        private readonly List<Func<float, bool>> _conditions = new();
        private readonly List<Func<float, float>> _functions = new();

        /// <summary>
        /// 添加新的分段规则
        /// </summary>
        /// <param name="condition">区间条件（例如 x => x >= 0 && x < 10）</param>
        /// <param name="func">该区间对应的计算函数</param>
        public void AddRule(Func<float, bool> condition, Func<float, float> func)
        {
            _conditions.Add(condition);
            _functions.Add(func);
        }

        /// <summary>
        /// 清空现有规则
        /// </summary>
        public void ClearRules()
        {
            _conditions.Clear();
            _functions.Clear();
        }

        /// <summary>
        /// 计算分段函数值
        /// </summary>
        /// <param name="input">输入值</param>
        /// <returns>计算结果</returns>
        /// <exception cref="InvalidOperationException">未匹配任何规则时抛出异常</exception>
        public float Evaluate(float input)
        {
            for (int i = 0; i < _conditions.Count; i++)
            {
                if (_conditions[i](input))
                {
                    return _functions[i](input);
                }
            }
            throw new InvalidOperationException($"输入值 {input} 未匹配任何分段规则");
        }

        // ================== 辅助方法（用于从配置生成规则）==================

        /// <summary>
        /// 从分段点配置生成规则（左闭右开区间）
        /// </summary>
        /// <param name="breakpoints">有序分段点（从小到大）</param>
        /// <param name="funcs">每个区间对应的函数（数量=分段点数+1）</param>
        public void ConfigureFromBreakpoints(
            float[] breakpoints,
            Func<float, float>[] funcs)
        {
            if (funcs.Length != breakpoints.Length + 1)
                throw new ArgumentException("函数数量必须比分段点多1");

            ClearRules();

            // 添加 (-∞, 第一分段点) 区间
            AddRule(x => x < breakpoints[0], funcs[0]);

            // 添加中间区间 [breakpoints[i], breakpoints[i+1])
            for (int i = 0; i < breakpoints.Length - 1; i++)
            {
                float current = breakpoints[i];
                float next = breakpoints[i + 1];
                AddRule(x => x >= current && x < next, funcs[i + 1]);
            }

            // 添加 [最后分段点, +∞) 区间
            AddRule(x => x >= breakpoints[^1], funcs[^1]);
        }

        /// <summary>
        /// 创建常量函数
        /// </summary>
        public static Func<float, float> CreateConstant(float value)
            => _ => value;

        /// <summary>
        /// 创建线性函数
        /// </summary>
        public static Func<float, float> CreateLinear(float slope, float intercept)
            => x => slope * x + intercept;

        /// <summary>
        /// 创建多项式函数
        /// </summary>
        public static Func<float, float> CreatePolynomial(params float[] coefficients)
        {
            return x =>
            {
                float result = 0;
                for (int i = 0; i < coefficients.Length; i++)
                {
                    result += coefficients[i] * MathF.Pow(x, i);
                }
                return result;
            };
        }
    }
}
