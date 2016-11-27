using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MECAI.Interfaces;
using MECAI.MarketData;
using MECAI.Models;
using MECAI.Volatilities;
using MECAI.Priceables;
using System.Globalization;

namespace VolatilityViewer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SortedDictionary<DateTime, RiskFreeRate> RiskFreeRates { get; set; }
        OrderBook UnderlyingOrderBook { get; set; }
        List<OptionMarketData> OptionsMarketData { get; set; }
        VolSurface VolSurface { get; set; }

        private void LoadData()
        {
            string riskFreeRatesFile = @"C:\Users\Rubens\Google Drive\Mestrado\Projeto_Pesquisa\vols3\cdi_20161124_154509.csv";
            this.RiskFreeRates = this.GetRiskFreeRates(riskFreeRatesFile);

            string underlyingMarketDataFile = @"C:\Users\Rubens\Google Drive\Mestrado\Projeto_Pesquisa\vols3\underlying_20161124_154509.csv";
            this.UnderlyingOrderBook = this.GetUnderlyingMarketData(underlyingMarketDataFile);

            string optionsMarketDataFile = @"C:\Users\Rubens\Google Drive\Mestrado\Projeto_Pesquisa\vols3\options_20161124_154509.csv";
            this.OptionsMarketData = this.GetOptionsMarketData(optionsMarketDataFile);

            this.VolSurface = this.GetVolSurface(this.RiskFreeRates, this.UnderlyingOrderBook, this.OptionsMarketData);
        }

        private void buttonLoadData_Click(object sender, EventArgs e)
        {
            this.LoadData();

            this.comboBoxMaturities.Items.Clear();

            foreach (DateTime maturity in this.RiskFreeRates.Keys)
            {
                this.comboBoxMaturities.Items.Add(maturity);
            }
        }

        private void BuildChart(DateTime expiry)
        {
            VolExpiry volExpiry = null;

            double strikeMin = 9999.00;
            double strikeMax = 0.00;
            double volMin = 9999.00;
            double volMax = 0.00;

            if (this.VolSurface.VolExpiries.TryGetValue(expiry, out volExpiry))
            {
                foreach (VolStrike volStrike in volExpiry.VolStrikes.Values)
                {
                    if (volStrike.VolCallQuote != null)
                    {
                        VolQuote volCallQuote = volStrike.VolCallQuote;

                        if (this.IsValid(volCallQuote.BidImpliedVolatility))
                        {
                            double strike = volStrike.Strike;
                            strikeMin = Math.Min(strikeMin, strike);
                            strikeMax = Math.Max(strikeMax, strike);

                            double vol = volCallQuote.BidImpliedVolatility * 100;
                            volMin = Math.Min(volMin, vol);
                            volMax = Math.Max(volMax, vol);
                            chart1.Series["call_bid"].Points.AddXY(strike, vol);
                        }

                        if (this.IsValid(volCallQuote.AskImpliedVolatility))
                        {
                            double strike = volStrike.Strike;
                            strikeMin = Math.Min(strikeMin, strike);
                            strikeMax = Math.Max(strikeMax, strike);

                            double vol = volCallQuote.AskImpliedVolatility * 100;
                            volMin = Math.Min(volMin, vol);
                            volMax = Math.Max(volMax, vol);
                            chart1.Series["call_ask"].Points.AddXY(strike, vol);
                        }
                    }

                    if (volStrike.VolPutQuote != null)
                    {
                        VolQuote volPutQuote = volStrike.VolPutQuote;

                        if (this.IsValid(volPutQuote.BidImpliedVolatility))
                        {
                            double strike = volStrike.Strike;
                            strikeMin = Math.Min(strikeMin, strike);
                            strikeMax = Math.Max(strikeMax, strike);

                            double vol = volPutQuote.BidImpliedVolatility * 100;
                            volMin = Math.Min(volMin, vol);
                            volMax = Math.Max(volMax, vol);
                            chart1.Series["put_bid"].Points.AddXY(strike, vol);
                        }

                        if (this.IsValid(volPutQuote.AskImpliedVolatility))
                        {
                            double strike = volStrike.Strike;
                            strikeMin = Math.Min(strikeMin, strike);
                            strikeMax = Math.Max(strikeMax, strike);

                            double vol = volPutQuote.AskImpliedVolatility * 100;
                            volMin = Math.Min(volMin, vol);
                            volMax = Math.Max(volMax, vol);
                            chart1.Series["put_ask"].Points.AddXY(strike, vol);
                        }
                    }
                }
            }

            chart1.ChartAreas[0].AxisX.Minimum = Math.Floor(strikeMin);
            chart1.ChartAreas[0].AxisX.Maximum = Math.Ceiling(strikeMax);
            chart1.ChartAreas[0].AxisX.Interval = 1;

            chart1.ChartAreas[0].AxisY.Minimum = Math.Floor(volMin);
            chart1.ChartAreas[0].AxisY.Maximum = Math.Ceiling(volMax);
            chart1.ChartAreas[0].AxisY.Interval = 5;
        }

        private bool IsValid(double vol)
        {
            bool isValid = false;

            if ((vol > 0.10) && (vol < 3.00))
                isValid = true;

            return isValid;
        }

        private VolSurface GetVolSurface(SortedDictionary<DateTime, RiskFreeRate> riskFreeRates, OrderBook underlyingOrderBook, List<OptionMarketData> optionsMarketData)
        {
            double businessDaysInTheYear = 252;
            VolSurface volSurface = new VolSurface();
            double spot = underlyingOrderBook.GetPriceMid();

            foreach (OptionMarketData option in optionsMarketData)
            {
                RiskFreeRate riskFreeRate = null;
                riskFreeRates.TryGetValue(option.OptionInfo.ExpiryDate, out riskFreeRate);

                double timeToMaturity = riskFreeRate.BusinessDays / businessDaysInTheYear;

                double bid = (option.OrderBook.Bid != null) ? option.OrderBook.Bid.Value : 0.00;
                double bidSize = (option.OrderBook.BidSize != null) ? option.OrderBook.BidSize.Value : 0.00; ;
                double ask = (option.OrderBook.Ask != null) ? option.OrderBook.Ask.Value : 0.00; ;
                double askSize = (option.OrderBook.AskSize != null) ? option.OrderBook.AskSize.Value : 0.00; ;

                BlackScholes bs = new BlackScholes();
                double bidImpliedVolatility = 0.00;
                double askImpliedVolatility = 0.00;

                if (bid > 0.00 && bidSize > 0.00) bidImpliedVolatility = bs.GetImpliedVolatility(option.OptionInfo.Type, bid, spot, option.OptionInfo.Strike, riskFreeRate.Rate, timeToMaturity);
                if (ask > 0.00 && askSize > 0.00) askImpliedVolatility = bs.GetImpliedVolatility(option.OptionInfo.Type, ask, spot, option.OptionInfo.Strike, riskFreeRate.Rate, timeToMaturity);

                if (!(bidImpliedVolatility > 0.0000)) bidImpliedVolatility = 0.00;
                if (!(askImpliedVolatility > 0.0000)) askImpliedVolatility = 0.00;

                VolQuote volQuote = new VolQuote(bidSize, askSize, bidImpliedVolatility, askImpliedVolatility);
                volSurface.Update(option.OptionInfo.ExpiryDate, option.OptionInfo.Strike, option.OptionInfo.Type, volQuote);
            }
            
            return volSurface;
        }

        private SortedDictionary<DateTime, RiskFreeRate> GetRiskFreeRates(string filePath)
        {
            SortedDictionary<DateTime, RiskFreeRate> riskFreeRates = new SortedDictionary<DateTime, RiskFreeRate>();
            string line;

            System.IO.StreamReader file = new System.IO.StreamReader(filePath);
            while ((line = file.ReadLine()) != null)
            {
                string[] tokens = line.Split(';');

                DateTime maturity = DateTime.ParseExact(tokens[0], "yyyyMMdd", null);
                double days = Double.Parse(tokens[1], CultureInfo.InvariantCulture);
                double businessDays = Double.Parse(tokens[2], CultureInfo.InvariantCulture);
                double rate = Double.Parse(tokens[3], CultureInfo.InvariantCulture);

                RiskFreeRate riskFreeRate = new RiskFreeRate(maturity, days, businessDays, rate);
                riskFreeRates.Add(maturity, riskFreeRate);
            }

            file.Close();

            return riskFreeRates;
        }

        private List<OptionMarketData> GetOptionsMarketData(string filePath)
        {
            List<OptionMarketData> resultList = new List<OptionMarketData>();
            string line;

            System.IO.StreamReader file = new System.IO.StreamReader(filePath);
            while ((line = file.ReadLine()) != null)
            {
                string[] tokens = line.Split(';');

                string symbol = tokens[0];
                OptionType optionType = (tokens[1].Equals("C")) ? OptionType.Call : OptionType.Put;
                double strike = Double.Parse(tokens[2], CultureInfo.InvariantCulture);
                DateTime maturity = DateTime.ParseExact(tokens[3], "yyyyMMdd", null);
                OptionInfo optionInfo = new OptionInfo(symbol, string.Empty, optionType, strike, maturity);

                double? bid = null;
                double? bidSize = null;
                double? ask = null;
                double? askSize = null;

                if (tokens[4] != string.Empty) bid = Double.Parse(tokens[4], CultureInfo.InvariantCulture);
                if (tokens[5] != string.Empty) bidSize = Double.Parse(tokens[5], CultureInfo.InvariantCulture);
                if (tokens[6] != string.Empty) ask = Double.Parse(tokens[6], CultureInfo.InvariantCulture);
                if (tokens[7] != string.Empty) askSize = Double.Parse(tokens[7], CultureInfo.InvariantCulture);

                OrderBook orderBook = new OrderBook(symbol, bid, ask, bidSize, askSize);

                OptionMarketData marketData = new OptionMarketData(optionInfo, orderBook);

                resultList.Add(marketData);
            }

            file.Close();

            return resultList;
        }

        private OrderBook GetUnderlyingMarketData(string filePath)
        {
            OrderBook marketData = null;
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

                marketData = new OrderBook(name, bid, ask, bidSize, askSize);
            }

            file.Close();

            return marketData;
        }

        private void comboBoxMaturities_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.comboBoxMaturities.Items.Count == 0)
                return;

            DateTime maturity = (DateTime) this.comboBoxMaturities.SelectedItem;

            foreach (var series in chart1.Series)
            {
                series.Points.Clear();
            }

            this.BuildChart(maturity);
        }
    }
}
