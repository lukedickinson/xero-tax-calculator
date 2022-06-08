using System;
using System.Collections.Generic;
using RestSharp;

namespace XeroTaxCalculator
{
    class TaxCalculator
    {
        private class ApiResponse
        {
            public string countryName { get; set; }
            public decimal gstRate { get; set; }
            public string currency { get; set; }
            public char currencySymbol { get; set; }
        }

        public Dictionary<decimal, decimal> CalculateGST(List<Invoice> orders, string countryCode)
        {
            const string URL = "http://127.0.0.1:8000/v1/tax/";
            var client = new RestClient(URL + countryCode);
            var response = client.Execute<ApiResponse>(new RestRequest());

            var responseData = response.Data;
            var result = new Dictionary<decimal, decimal>();

            foreach (Invoice i in orders)
            {
                if (i.countryCode == countryCode)
                {
                    decimal taxInAmount = GetGstFromInclusiveAmount(responseData.gstRate, i.amount);
                    result.Add(taxInAmount, i.amount);
                }
            }

            return result;
        }

        private decimal GetGstFromInclusiveAmount(decimal gstRate, decimal amountIncludingGST)
        {
            decimal amountExcludingGST = amountIncludingGST / (1 + gstRate);
            decimal tax = amountIncludingGST - amountExcludingGST;
            return Math.Round(tax, 2, MidpointRounding.AwayFromZero);
        }
    }
}
