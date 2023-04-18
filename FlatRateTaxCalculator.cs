using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Price_Calculator_kata
{
    internal class FlatRateTaxCalculator : ITaxCalculator
    {
        public static double TaxPercentageDefault = 20.0;
        public double CalculatePriceAfterTax(double Price)
        {
            double taxedPrice = Price * (1.0 + TaxPercentageDefault / 100.0);
            return Math.Round(taxedPrice, 2);
        }

        public double CalculatePriceAfterTax(double Price, double TaxPercentage)
        {
            double taxedPrice = Price * (1.0 + TaxPercentage / 100.0);
            return Math.Round(taxedPrice, 2);
        }
    }
}
