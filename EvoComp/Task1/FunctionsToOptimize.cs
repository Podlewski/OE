using System;

namespace Task1
{
    public static class FunctionsToOptimize
    {
        public static double SineTimesCosine(double x1, double x2)
        {
            return Math.Sin(x1) * Math.Cos(x2);
        }

        public static double SineTimesCosinePlusArguments(double x1, double x2)
        {
            return SineTimesCosine(x1, x2) + x1 + x2;
        }

        public static double SineTimesCosinePlusSquareOfArguments(double x1, double x2)
        {
            return SineTimesCosine(x1, x2) + x1 * x1 + x2 * x2;
        }

        public static double Multiplication(double x1, double x2)
        {
            return x1 * x2;
        }

        public static double Choose(int n, double x1, double x2)
        {
            return n switch
            {
                1 => SineTimesCosine(x1, x2),
                2 => SineTimesCosinePlusArguments(x1, x2),
                3 => SineTimesCosinePlusSquareOfArguments(x1, x2),
                4 => Multiplication(x1, x2),
                _ => throw new ArgumentException(),
            };

        }
    }
}
