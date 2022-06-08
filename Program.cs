using System;
using System.Collections.Generic;

namespace XeroTaxCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            var orders = new List<Invoice>
            {
                new Invoice(199.99M, "AU"),
                new Invoice(2.49M, "NZ"),
                new Invoice(110.00M, "AU")
            };
            var calculator = new TaxCalculator();
            var resultAU = calculator.CalculateGST(orders, "Australia");
            var resultNZ = calculator.CalculateGST(orders, "New Zealand");
        }
    }
}
