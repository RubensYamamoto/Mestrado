using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MECAI.Interfaces;
using MECAI.MarketData;

namespace VolatilityViewer
{
    public class OptionMarketData
    {
        public OptionMarketData(OptionInfo optionInfo, OrderBook orderBook)
        {
            this.OptionInfo = optionInfo;
            this.OrderBook = orderBook;
        }

        public OptionInfo OptionInfo { get; private set; }
        public OrderBook OrderBook { get; private set; }
    }
}
