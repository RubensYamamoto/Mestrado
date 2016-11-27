using System;

namespace MECAI.Volatilities
{
    public class VolQuote
    {
        public VolQuote(double bidSize, double askSize, double bidImpliedVolatility, double askImpliedVolatility)
        {
            this.BidSize = bidSize;
            this.AskSize = askSize;
            this.BidImpliedVolatility = bidImpliedVolatility;
            this.AskImpliedVolatility = askImpliedVolatility;
        }

        public double BidSize { get; private set; }
        public double AskSize { get; private set; }

        public double BidImpliedVolatility { get; private set; }
        public double AskImpliedVolatility { get; private set; }

        public bool IsValid()
        {
            return this.BidImpliedVolatility > 0.00 && this.AskImpliedVolatility > 0.00;
        }

        public double GetVolatility()
        {
            double volatility = 0.00;

            double bidwt = Math.Sqrt(this.BidSize);
            double askwt = Math.Sqrt(this.AskSize);

            if (bidwt > 0.00 && askwt > 0.00)
            {
                volatility = (this.BidImpliedVolatility * askwt + this.AskImpliedVolatility * bidwt) / (bidwt + askwt);
            }
            else
                volatility = 0.50 * (this.BidImpliedVolatility + this.AskImpliedVolatility);

            return volatility;
        }
    }
}
