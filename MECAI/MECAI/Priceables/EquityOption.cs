using System;
using MECAI.Probabilities;

namespace MECAI.Priceables
{
    public class EquityOption : IPriceable
    {
        public EquityOption(EquityOptionStaticData staticData)
        {
            this.StaticData = staticData;
        }

        private EquityOptionStaticData StaticData { set; get; }

        public double Price()
        {
            double price = 0.00;
            double phi = (this.StaticData.Type == OptionType.Call) ? 1 : -1;
            double S = this.StaticData.Spot;
            double K = this.StaticData.Strike;
            double r = this.StaticData.RiskFreeRate;
            double sigma = this.StaticData.Volatility;
            double ttm = this.StaticData.TimeToMaturity;

            if (ttm > 0.00)
            {
                double d1 = (Math.Log(S / K) + (r + sigma * sigma / 2) * ttm) / (sigma * Math.Sqrt(ttm));
                double d2 = d1 - sigma * Math.Sqrt(ttm);

                Normal normal = new Normal();
                double N1 = normal.CDF(phi * d1);
                double N2 = normal.CDF(phi * d2);

                price = phi * N1 * S - phi * N2 * K * Math.Exp(-r * ttm);
            }
            else
            {
                price = Math.Max(phi*(S-K), 0);
            }

            return price;
        }
    }
}
