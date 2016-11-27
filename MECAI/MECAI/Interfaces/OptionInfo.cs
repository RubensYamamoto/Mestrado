using System;
using MECAI.Priceables;

namespace MECAI.Interfaces
{
    public class OptionInfo
    {
        public OptionInfo(string name, string underlying, OptionType type, double strike, DateTime expiryDate)
        {
            this.Name = name;
            this.Underlying = underlying;
            this.Type = type;
            this.Strike = strike;
            this.ExpiryDate = expiryDate;
        }

        public string Name { get; private set; }
        public string Underlying { get; private set; }
        public OptionType Type { private set; get; }
        public double Strike { private set; get; }
        public DateTime ExpiryDate { private set; get; }
    }
}
