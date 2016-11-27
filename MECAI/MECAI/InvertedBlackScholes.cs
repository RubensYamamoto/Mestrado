using System;
using MECAI.Priceables;
using MECAI.Probabilities;

namespace MECAI.Models
{
    public class BlackScholes
    {
        public double GetImpliedVolatility(OptionType type, double price, double spot, double strike, double riskFreeRate, double timeToMaturity)
        {
            double impliedVol = 0.00;
            double relTolerance = 0.0001;
            int maxAttempts = 10;
            double[] volGuess = { 0.2000, 0.4000, 0.6000, 0.8000 };
            
            for (int i = 0; i < volGuess.Length; i++)
            {
                impliedVol = this.GetImpliedVolatility(type, price, spot, strike, riskFreeRate, timeToMaturity, volGuess[i], relTolerance, maxAttempts);

                if (impliedVol > 0.00000 && impliedVol < 1000)
                {
                    break;
                }
            }

            return impliedVol;
        }

        // Calculate market implied volatility, by inverting Black-Scholes formula, throught Newton-Raphson method
        public double GetImpliedVolatility(OptionType type, double price, double spot, double strike, double riskFreeRate, double timeToMaturity, double volatilityGuess, double relativeTolerance, int maxAttempts)
        {
            double phi = (type == OptionType.Call) ? 1 : -1;
            double S = spot;
            double K = strike;
            double r = riskFreeRate;
            double ttm = timeToMaturity;
            double currentError = 999999999;
            double previousIV = volatilityGuess;
            double currentIV = previousIV;

            int i = 0;
            while (i < maxAttempts && currentError > relativeTolerance)
            {
                double[] d = this.GetDs(ttm, S, K, r, previousIV);
                double[] firstOrderD = this.GetFirstOrderDs(ttm, S, K, r, previousIV);

                Normal normal = new Normal();
                double cdf1 = normal.CDF(phi * d[0]);
                double cdf2 = normal.CDF(phi * d[1]);
                double f = phi * S * cdf1 - phi * K * Math.Exp(-r * ttm) * cdf2 - price;

                double pdf1 = normal.PDF(phi * d[0]);
                double pdf2 = normal.PDF(phi * d[1]);
                double firstOrderF = S * pdf1 * firstOrderD[0] - K * Math.Exp(-r * ttm) * pdf2 * firstOrderD[1];

                currentIV = previousIV - f / firstOrderF;
                currentError = Math.Abs(currentIV - previousIV) / Math.Abs(previousIV);

                previousIV = currentIV;
                i++;
            }

            return currentIV;
        }

        // Calculate d1 and d2 (Black-Scholes)
        private double[] GetDs(double ttm, double S, double K, double r, double sigma)
        {
            double[] d = new double[2];
            double sigmaSqrtT = sigma * Math.Sqrt(ttm);
            d[0] = (Math.Log(S / K) + (r + 0.5 * sigma * sigma) * ttm) / (sigmaSqrtT);
            d[1] = d[0] - sigmaSqrtT;

            return d;
        }

        // Calculate first order of d1 and d2 (Black-Scholes)
        private double[] GetFirstOrderDs(double ttm, double S, double K, double r, double sigma)
        {
            double[] d = new double[2];
            double sqrtT = Math.Sqrt(ttm);
            double sigmaSigma = sigma * sigma;
            double sigmaSigmaT = sigmaSigma * ttm;

            d[0] = sqrtT * (sigmaSigmaT - (Math.Log(S / K) + (r + 0.5 * sigmaSigma) * ttm)) / (sigmaSigmaT);
            d[1] = d[0] - sqrtT;

            return d;
        }
    }
}
