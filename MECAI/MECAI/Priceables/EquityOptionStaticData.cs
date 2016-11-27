
namespace MECAI.Priceables
{
    public enum OptionType
    {
        Call,
        Put
    }

    public class EquityOptionStaticData : IStaticData
    {
        public EquityOptionStaticData(OptionType type, double strike, double spot, double volatility, double riskFreeRate, double timeToMaturity)
        {
            this.Type = type;
            this.Strike = strike;
            this.Spot = spot;
            this.Volatility = volatility;
            this.RiskFreeRate = riskFreeRate;
            this.TimeToMaturity = timeToMaturity;
        }

        public OptionType Type { private set; get; }
        public double Strike { private set; get; }
        public double Spot { private set; get; }
        public double Volatility { private set; get; }
        public double RiskFreeRate { private set; get; }
        public double TimeToMaturity { private set; get; }

        public IPriceable BuildPriceable()
        {
            IPriceable priceable = new EquityOption(this);

            return priceable;
        }
    }
}
