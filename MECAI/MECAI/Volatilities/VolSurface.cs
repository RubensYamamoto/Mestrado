using System;
using System.Collections.Generic;
using MECAI.Priceables;

namespace MECAI.Volatilities
{
    public class VolSurface
    {
        public VolSurface()
        { }

        private IDictionary<DateTime, VolExpiry> volExpiries;
        public IDictionary<DateTime, VolExpiry> VolExpiries
        {
            get
            {
                if (this.volExpiries == null)
                {
                    this.volExpiries = new SortedDictionary<DateTime, VolExpiry>();
                }

                return this.volExpiries;
            }
        }

        public void Update(DateTime expiryDate, double strike, OptionType type, VolQuote volQuote)
        {
            VolExpiry existingExpiry = null;
            if (this.VolExpiries.TryGetValue(expiryDate, out existingExpiry))
            {
                existingExpiry.Update(strike, type, volQuote);
            }
            else
            {
                VolExpiry volExpiry = new VolExpiry(expiryDate);
                volExpiry.Update(strike, type, volQuote);
                this.VolExpiries[expiryDate] = volExpiry;
            }
        }
    }
}
