using System;

namespace Task1
{
    public static class FunctionsToOptimize
    {
        public static double SineTimesCosine(double x1, double x2)
        {
            return Math.Sin(x1) * Math.Cos(x2);
        }

        public static double SineTimesCosineTimesArguments(double x1, double x2)
        {
            return SineTimesCosine(x1, x2) * x1 - SineTimesCosine(x2, x1) * x2;
        }

        public static double Choose(int n, double x1, double x2)
        {
            return n switch
            {
                1 => SineTimesCosine(x1, x2),
                2 => SineTimesCosineTimesArguments(x1, x2),
                _ => throw new ArgumentException(),
            };

        }
    }
}
