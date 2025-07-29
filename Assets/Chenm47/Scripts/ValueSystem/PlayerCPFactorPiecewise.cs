using System;

namespace ns.Value
{
    /// <summary>
    /// 描述：玩家能力值系数分段函数
    /// </summary>
    public class PlayerCPFactorPiecewise
    {
        private ConfigurablePiecewiseFunction piecewiseFunction;

        public PlayerCPFactorPiecewise()
        {
            piecewiseFunction = new ConfigurablePiecewiseFunction();
            //初始化分段函数
            var breakpoints = new float[] { 0, 10, 20 }; // 分段点测试用
            var functions = new Func<float, float>[] {
            ConfigurablePiecewiseFunction.CreateConstant(1),    // x < 0
            ConfigurablePiecewiseFunction.CreateConstant(1.1f), // 0 ≤ x < 10
           ConfigurablePiecewiseFunction.CreateConstant(1.2f), // 10 ≤ x < 20
            ConfigurablePiecewiseFunction.CreateConstant(1.3f)  // x ≥ 20
        };
            piecewiseFunction.ConfigureFromBreakpoints(breakpoints, functions);
        }

        public float GetCPFactor(float cpValue)
        {
            return piecewiseFunction.Evaluate(cpValue);
        }

    }
}
