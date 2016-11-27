using System;
using System.Collections.Generic;
using MECAI.Priceables;

namespace MECAI.Volatilities
{
    public class VolExpiry
    {
        public VolExpiry(DateTime date)
        {
            this.Date = date;
        }

        public DateTime Date { get; private set; }

        private IDictionary<double, VolStrike> volStrikes;
        public IDictionary<double, VolStrike> VolStrikes
        {
            get
            {
                if (this.volStrikes == null)
                {
                    this.volStrikes = new SortedDictionary<double, VolStrike>();
                }

                return this.volStrikes;
            }
        }

        public void Update(double strike, OptionType type, VolQuote volQuote)
        {
            VolStrike existingStrike = null;
            if (this.VolStrikes.TryGetValue(strike, out existingStrike))
            {
                existingStrike.Update(type, volQuote);
            }
            else
            {
                VolStrike volStrike = new VolStrike(strike);
                volStrike.Update(type, volQuote);
                this.VolStrikes[strike] = volStrike;
            }
        }
    }
}
