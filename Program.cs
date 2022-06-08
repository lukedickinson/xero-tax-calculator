﻿using System;
using System.Collections.Generic;

namespace XeroTaxCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            var resultDict = new Dictionary<string, Dictionary<decimal, decimal>>();
            var calculator = new TaxCalculator();
            var orders = new List<Invoice>
            {
                new Invoice(199.99M, "AU"),
                new Invoice(2.49M, "NZ"),
                new Invoice(110.00M, "AU")
            };

            resultDict.Add("AU", calculator.CalculateGST(orders, "AU"));
            resultDict.Add("NZ", calculator.CalculateGST(orders, "NZ"));
        }
    }
}
