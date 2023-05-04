using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Price_Calculator_kata
{
    public class FlatRateTaxCalculator : ITaxCalculator
    {
      
        public double CalculateTaxAmount(double Price)
        {
            double TaxPercentageDefault = PriceCalculatorConfigurations.FlatRateTax;
            double taxAmount = Price * (TaxPercentageDefault / 100.0);
            return taxAmount;
        }

    }
}
