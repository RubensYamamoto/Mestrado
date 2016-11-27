using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VolatilityViewer
{
    public class RiskFreeRate
    {
        public RiskFreeRate(DateTime maturity, double days, double businessDays, double rate)
        {
            this.Maturity = maturity;
            this.Days = days;
            this.BusinessDays = businessDays;
            this.Rate = rate;
        }

        public DateTime Maturity { get; private set; }

        public double Days { get; private set; }

        public double BusinessDays { get; private set; }

        public double Rate { get; private set; }
    }
}