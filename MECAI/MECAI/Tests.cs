using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using MECAI.Interfaces;
using MECAI.MarketData;
using MECAI.Models;
using MECAI.Priceables;
using MECAI.Volatilities;

namespace MECAI.Tests
{
    public class StrikeVol
    {
        public StrikeVol(double strike, double volatility)
        {
            this.Strike = strike;
            this.Volatility = volatility;
        }

        public double Strike { get; private set; }
        public double Volatility { get; private set; }
    }

    public class Test
    {
        #region Private methods

        private Dictionary<string, OptionInfo> ListAllAvailableOptions(string filePath)
        {
            Dictionary<string, OptionInfo> resultList = new Dictionary<string, OptionInfo>();
            string line;

            System.IO.StreamReader file = new System.IO.StreamReader(filePath);
            while ((line = file.ReadLine()) != null)
            {
                string[] tokens = line.Split(';');

                string underlying = tokens[0];
                DateTime expiryDate = DateTime.ParseExact(tokens[1], "yyyy-MM-dd", null);
                OptionType optionType = (tokens[2].Equals("Call")) ? OptionType.Call : OptionType.Put;
                double strike = Double.Parse(tokens[3], CultureInfo.InvariantCulture);
                string name = tokens[4];

                OptionInfo option = new OptionInfo(name, underlying, optionType, strike, expiryDate);
                resultList.Add(name, option);
            }

            file.Close();

            return resultList;
        }

        private Dictionary<string, OrderBook> GetMarketData(string filePath)
        {
            Dictionary<string, OrderBook> optionsMarketData = new Dictionary<string, OrderBook>();
            string line;

            System.IO.StreamReader file = new System.IO.StreamReader(filePath);
            while ((line = file.ReadLine()) != null)
            {
                string[] tokens = line.Split(';');

                string name = tokens[0];

                double? bid = null;
                double? bidSize = null;
                double? ask = null;
                double? askSize = null;

                if (tokens[1] != string.Empty) bid = Double.Parse(tokens[1], CultureInfo.InvariantCulture);
                if (tokens[2] != string.Empty) bidSize = Double.Parse(tokens[2], CultureInfo.InvariantCulture);
                if (tokens[3] != string.Empty) ask = Double.Parse(tokens[3], CultureInfo.InvariantCulture);
                if (tokens[4] != string.Empty) askSize = Double.Parse(tokens[4], CultureInfo.InvariantCulture);

                OrderBook orderBook = new OrderBook(name, bid, ask, bidSize, askSize);
                optionsMarketData.Add(name, orderBook);
            }

            file.Close();

            return optionsMarketData;
        }

        //private OptionInfo PopulateOptionInfo(OptionType type, double strike, double spot, double volatility, double riskFreeRate, double ttm)
        //{
        //    EquityOptionStaticData staticData = new EquityOptionStaticData(type, strike, spot, volatility, riskFreeRate, ttm);
        //    IPriceable priceable = staticData.BuildPriceable();
        //    double price = priceable.Price();

        //    OptionInfo info = new OptionInfo(staticData);
        //    info.Price = price;

        //    return info;
        //}

        #endregion

        #region Public methods

        //public void TestProb()
        //{
        //    MathNet.Numerics.Distributions.Normal normal = new MathNet.Numerics.Distributions.Normal();
        //    double cdf = normal.CumulativeDistribution(-0.25);

        //    MECAI.Probabilities.Normal myNormal = new MECAI.Probabilities.Normal();
        //    double myCdf = myNormal.CDF(-0.25);
            
        //    int i = 0;
        //}

        public double GetImpliedVolatilitApril()
        {
            OptionType type = OptionType.Call;
            double strike = 12.84;
            double spot = 19.325;
            double price = 1.10;
            //double volatility = 0.47;
            double riskFreeRate = 0.132343187; // discrete = 0.1415, continuos = ln(1+r) = 0.132343187
            double ttm = (double)11 / 252;

            BlackScholes bs = new BlackScholes();
            double volGuess = 0.30;
            double relTolerance = 0.01;
            int maxAttempts = 10;

            double impliedVol = bs.GetImpliedVolatility(type, price, spot, strike, riskFreeRate, ttm, volGuess, relTolerance, maxAttempts);

            return impliedVol;
        }

        public double GetImpliedVolatilitMay()
        {
            OptionType type = OptionType.Call;
            double strike = 26.72;
            double spot = 27.00;
            double price = 2.17;
            //double volatility = 0.50;
            double riskFreeRate = 0.132343187; // discrete = 0.1415, continuos = ln(1+r) = 0.132343187
            double ttm = (double)30 / 252;

            BlackScholes bs = new BlackScholes();
            double volGuess = 0.30;
            double relTolerance = 0.01;
            int maxAttempts = 10;

            double impliedVol = bs.GetImpliedVolatility(type, price, spot, strike, riskFreeRate, ttm, volGuess, relTolerance, maxAttempts);

            return impliedVol;
        }

        public double CallPrice()
        {
            OptionType type = OptionType.Call;
            double strike = 29.14;
            double spot = 27.04;
            double volatility = 0.47;
            double riskFreeRate = 0.1413;
            double ttm = 11/252;

            EquityOptionStaticData staticData = new EquityOptionStaticData(type, strike, spot, volatility, riskFreeRate, ttm);
            IPriceable priceable = staticData.BuildPriceable();
            double price = priceable.Price();
            return price;
        }

        public bool SimpleImpliedVolatilityTest()
        {
            OptionType type = OptionType.Call;
            double strike = 31.21;
            double spot = 31.525;
            double volatility = 0.25116165;
            double riskFreeRate = 0.085209;
            double ttm = (double)35/365;

            EquityOptionStaticData staticData = new EquityOptionStaticData(type, strike, spot, volatility, riskFreeRate, ttm);
            IPriceable priceable = staticData.BuildPriceable();
            double price = priceable.Price();

            BlackScholes bs = new BlackScholes();
            double volGuess = 0.25;
            double relTolerance = 0.00001;
            int maxAttempts = 10;
            price = 1.28;
            double impliedVol = bs.GetImpliedVolatility(type, price, spot, strike, riskFreeRate, ttm, volGuess, relTolerance, maxAttempts);

            double relDiff = Math.Abs(impliedVol - volatility) / volatility;

            if (relDiff <= relTolerance)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //public void GenerateOptionsSample()
        //{
        //    OptionType type = OptionType.Call;
        //    double spot = 100;
        //    double riskFreeRate = 0.10;

        //    // Time to maturity = 1 month
        //    List<OptionInfo> optionInfos1M = new List<OptionInfo>();
        //    double ttm = 1 / 12;

        //    StrikeVol[] strikeVol1M =
        //    {
        //        new StrikeVol(50, 60),
        //        new StrikeVol(60, 45),
        //        new StrikeVol(70, 35),
        //        new StrikeVol(80, 25),
        //        new StrikeVol(90, 20),
        //        new StrikeVol(100, 15),
        //        new StrikeVol(110, 12),
        //        new StrikeVol(120, 15),
        //        new StrikeVol(130, 20),
        //        new StrikeVol(140, 30),
        //        new StrikeVol(150, 40)
        //    };

        //    for (int i = 0; i < strikeVol1M.Length; i++)
        //    {
        //        OptionInfo info = this.PopulateOptionInfo(type, strikeVol1M[i].Strike, spot, strikeVol1M[i].Volatility, riskFreeRate, ttm);
        //        optionInfos1M.Add(info);
        //    }

        //    // Time to maturity = 2 months
        //    List<OptionInfo> optionInfos2M = new List<OptionInfo>();
        //    ttm = 2 / 12;

        //    StrikeVol[] strikeVol2M =
        //    {
        //        new StrikeVol(50, 60),
        //        new StrikeVol(60, 58),
        //        new StrikeVol(70, 56),
        //        new StrikeVol(80, 54),
        //        new StrikeVol(90, 52),
        //        new StrikeVol(100, 50),
        //        new StrikeVol(110, 50),
        //        new StrikeVol(120, 52),
        //        new StrikeVol(130, 54),
        //        new StrikeVol(140, 56),
        //        new StrikeVol(150, 58)
        //    };

        //    for (int i = 0; i < strikeVol2M.Length; i++)
        //    {
        //        OptionInfo info = this.PopulateOptionInfo(type, strikeVol2M[i].Strike, spot, strikeVol2M[i].Volatility, riskFreeRate, ttm);
        //        optionInfos2M.Add(info);
        //    }

        //    // Time to maturity = 3 months
        //    List<OptionInfo> optionInfos3M = new List<OptionInfo>();
        //    ttm = 3 / 12;

        //    StrikeVol[] strikeVol3M =
        //    {
        //        new StrikeVol(100, 45),
        //        new StrikeVol(110, 44),
        //        new StrikeVol(120, 43),
        //        new StrikeVol(130, 42),
        //        new StrikeVol(140, 41),
        //        new StrikeVol(150, 40),
        //        new StrikeVol(160, 39),
        //        new StrikeVol(170, 38),
        //        new StrikeVol(180, 37),
        //        new StrikeVol(190, 36),
        //        new StrikeVol(200, 35)
        //    };

        //    for (int i = 0; i < strikeVol3M.Length; i++)
        //    {
        //        OptionInfo info = this.PopulateOptionInfo(type, strikeVol3M[i].Strike, spot, strikeVol3M[i].Volatility, riskFreeRate, ttm);
        //        optionInfos3M.Add(info);
        //    }
        //}

        public void CalculateImpliedVolatilityToFile(string ammOptionsFile, string underlyingsMarketDataFile, string optionsMarketDataFile, string outputFile, double daysToMaturity, double riskFreeRate, double volGuess)
        {
            double workingDaysInTheYear = 252;
            double relTolerance = 0.0001;
            int maxAttempts = 10;
            double timeToMaturity = daysToMaturity / workingDaysInTheYear;

            Dictionary<string, OptionInfo> allAvailableOptions = this.ListAllAvailableOptions(ammOptionsFile);
            Dictionary<string, OrderBook> underlyingsMarketData = this.GetMarketData(underlyingsMarketDataFile);
            Dictionary<string, OrderBook> optionsMarketData = this.GetMarketData(optionsMarketDataFile);

            StreamWriter streamWriter = new StreamWriter(outputFile);

            foreach (var pair in optionsMarketData)
            {
                OrderBook optionOrderBook = pair.Value;

                OptionInfo option = null;
                if (allAvailableOptions.TryGetValue(optionOrderBook.Name, out option))
                {
                    OrderBook underlyingOrderBook = null;
                    if (underlyingsMarketData.TryGetValue(option.Underlying, out underlyingOrderBook))
                    {
                        double spot = underlyingOrderBook.GetPriceMid();
                        double optionObservedPrice = optionOrderBook.GetPriceAverage();

                        BlackScholes bs = new BlackScholes();
                        double impliedVol = bs.GetImpliedVolatility(option.Type, optionObservedPrice, spot, option.Strike, riskFreeRate, timeToMaturity, volGuess, relTolerance, maxAttempts);

                        if (impliedVol > 0.00000 && impliedVol < 1000)
                        {
                            StringBuilder sb = new StringBuilder();
                            sb.Append(option.Name);
                            sb.Append(";");
                            string optionType = (option.Type == OptionType.Call) ? "Call" : "Put";
                            sb.Append(optionType);
                            sb.Append(";");
                            sb.Append(option.Strike);
                            sb.Append(";");
                            sb.Append(impliedVol.ToString());
                            streamWriter.WriteLine(sb.ToString());
                        }
                    }
                }
            }

            streamWriter.Close();
        }

        public void CalculateImpliedVolatility(string ammOptionsFile, string underlyingsMarketDataFile, string optionsMarketDataFile, double daysToMaturity, double riskFreeRate, ref VolSurface volSurface)
        {
            double workingDaysInTheYear = 252;
            double timeToMaturity = daysToMaturity / workingDaysInTheYear;

            Dictionary<string, OptionInfo> allAvailableOptions = this.ListAllAvailableOptions(ammOptionsFile);
            Dictionary<string, OrderBook> underlyingsMarketData = this.GetMarketData(underlyingsMarketDataFile);
            Dictionary<string, OrderBook> optionsMarketData = this.GetMarketData(optionsMarketDataFile);

            foreach (var pair in optionsMarketData)
            {
                OrderBook optionOrderBook = pair.Value;

                OptionInfo option = null;
                if (allAvailableOptions.TryGetValue(optionOrderBook.Name, out option))
                {
                    OrderBook underlyingOrderBook = null;
                    if (underlyingsMarketData.TryGetValue(option.Underlying, out underlyingOrderBook))
                    {
                        double spot = underlyingOrderBook.GetPriceMid();
                        double bid = (optionOrderBook.Bid != null) ? optionOrderBook.Bid.Value : 0.00;
                        double bidSize = (optionOrderBook.BidSize != null) ? optionOrderBook.BidSize.Value : 0.00; ;
                        double ask = (optionOrderBook.Ask != null) ? optionOrderBook.Ask.Value : 0.00; ;
                        double askSize = (optionOrderBook.AskSize != null) ? optionOrderBook.AskSize.Value : 0.00; ;

                        BlackScholes bs = new BlackScholes();
                        double bidImpliedVolatility = 0.00;
                        double askImpliedVolatility = 0.00;

                        if (bid > 0.00 && bidSize > 0.00) bidImpliedVolatility = bs.GetImpliedVolatility(option.Type, bid, spot, option.Strike, riskFreeRate, timeToMaturity);
                        if (ask > 0.00 && askSize > 0.00) askImpliedVolatility = bs.GetImpliedVolatility(option.Type, ask, spot, option.Strike, riskFreeRate, timeToMaturity);

                        if (!(bidImpliedVolatility > 0.0000)) bidImpliedVolatility = 0.00;
                        if (!(askImpliedVolatility > 0.0000)) askImpliedVolatility = 0.00;

                        VolQuote volQuote = new VolQuote(bidSize, askSize, bidImpliedVolatility, askImpliedVolatility);
                        volSurface.Update(option.ExpiryDate, option.Strike, option.Type, volQuote);
                    }
                }
            }
        }

        #endregion
    }
}
