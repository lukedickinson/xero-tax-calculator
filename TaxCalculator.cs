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


        public Dictionary<decimal, decimal> CalculateGST(List<Invoice> orders, string countryName)
        {
            const string URL = "http://127.0.0.1:8000/v1/tax/";

            var responseCache = new List<RestSharp.RestResponse<ApiResponse>>();

            var result = new Dictionary<decimal, decimal>();
            foreach (Invoice i in orders)
            {
                ApiResponse data = null;
                foreach (RestSharp.RestResponse<ApiResponse> r in responseCache)
                {
                    if (r.ResponseUri.AbsolutePath.Substring(8,2) == i.countryCode)
                    {
                        data = r.Data;
                        break;
                    }
                }

                if (data == null)
                {
                    var client = new RestClient(URL + i.countryCode);

                    var response = client.Execute<ApiResponse>(new RestRequest());

                    responseCache.Add(response);

                    data = response.Data;
                }

                if (data.countryName == countryName)
                {
                    decimal taxInAmount = GetGstFromInclusiveAmount(data.gstRate, i.amount);
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
