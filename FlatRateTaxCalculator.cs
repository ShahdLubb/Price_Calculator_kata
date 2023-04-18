using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Price_Calculator_kata
{
    public class FlatRateTaxCalculator : ITaxCalculator
    {
        public static double TaxPercentageDefault = 20.0;
        
        public double CalculateTaxAmount(double Price)
        {
            double taxAmount = Price * (TaxPercentageDefault / 100.0);
            return taxAmount;
        }

    }
}
