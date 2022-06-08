using System;
using System.Collections.Generic;
using System.Text;

namespace XeroTaxCalculator
{
    class Invoice
    {
        public decimal amount { get; }
        //ISO 3166-1 alpha-2 country code
        public string countryCode { get; }

        public Invoice(decimal amount, string countryCode)
        {
            this.amount = amount;
            this.countryCode = countryCode;
        }
    }
}
