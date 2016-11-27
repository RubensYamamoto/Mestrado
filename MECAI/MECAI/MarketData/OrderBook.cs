
namespace MECAI.MarketData
{
    public class OrderBook
    {
        string name;
        double? bid;
        double? ask;
        double? bidSize;
        double? askSize;

        public OrderBook(string name, double? bid, double? ask, double? bidSize, double? askSize)
        {
            this.name = name;
            this.bid = bid;
            this.ask = ask;
            this.bidSize = bidSize;
            this.askSize = askSize;
        }

        public string Name
        {
            get
            {
                return this.name;
            }
        }

        public double? Bid
        {
            get
            {
                return this.bid;
            }
        }

        public double? Ask
        {
            get
            {
                return this.ask;
            }
        }

        public double? BidSize
        {
            get
            {
                return this.bidSize;
            }
        }

        public double? AskSize
        {
            get
            {
                return this.askSize;
            }
        }

        public double GetPriceMid()
        {
            double bid = (this.Bid != null) ? (double)this.Bid : 0.00;
            double ask = (this.Ask != null) ? (double)this.Ask : 0.00;

            double average = (double)(bid + ask) / 2;

            return average;
        }

        public double GetPriceAverage()
        {
            double bid = (this.Bid != null) ? (double)this.Bid : 0.00;
            double bidSize = (this.BidSize != null) ? (double)this.BidSize : 0.00;
            double ask = (this.Ask != null) ? (double)this.Ask : 0.00;
            double askSize = (this.AskSize != null) ? (double)this.AskSize : 0.00;

            double average = (bid * bidSize + ask * askSize) / (bidSize + askSize);

            return average;
        }
    }
}
