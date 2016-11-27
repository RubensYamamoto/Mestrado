using MECAI.Priceables;

namespace MECAI.Volatilities
{
    public class VolStrike
    {
        public VolStrike(double strike)
        {
            this.Strike = strike;
        }

        public VolQuote VolCallQuote { get; set; }
        public VolQuote VolPutQuote { get; set; }
        public double Strike { get; private set; }

        private void UpdateCall(VolQuote volQuote)
        {
            this.VolCallQuote = volQuote;
        }

        private void UpdatePut(VolQuote volQuote)
        {
            this.VolPutQuote = volQuote;
        }

        public void Update(OptionType type, VolQuote volQuote)
        {
            if (type == OptionType.Call)
                this.UpdateCall(volQuote);
            else
                this.UpdatePut(volQuote);
        }
        
        public double GetVolatility()
        {
            double volCall = this.VolCallQuote.GetVolatility();
            double volPut = this.VolPutQuote.GetVolatility();

            double volatility = 0.00;


            return volatility;
        }
    }
}
