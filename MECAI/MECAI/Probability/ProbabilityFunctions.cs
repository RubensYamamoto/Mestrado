using System;

namespace MECAI.Probabilities
{
    public class Normal
    {
        private static double PI = 3.141592653589793238462643;
        private static double INV_SQRT_2PI = 0.3989422804014327;

        // Cumulative Normal Distribution Function (CDF)
        public double CDF(double x)
        {
            double L, K, w;
            /* constants */
            double a1 = 0.31938153, a2 = -0.356563782, a3 = 1.781477937;
            double a4 = -1.821255978, a5 = 1.330274429;

            L = Math.Abs(x);
            K = (double) 1.0 / (1.0 + 0.2316419 * L);
            w = (double)(1.0 - 1.0 / Math.Sqrt(2 * PI) * Math.Exp(-L * L / 2) * (a1 * K + a2 * K * K + a3 * K * K * K + a4 * K * K * K * K + a5 * K * K * K * K * K));

            if (x < 0.00000)
            {
                w = 1.0 - w;
            }

            return w;
        }

        // Probability Density Function (PDF)
        public double PDF(double x)
        {
            return INV_SQRT_2PI * Math.Exp(-0.5f * x * x);
        }
    }
}
