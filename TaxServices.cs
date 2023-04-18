using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Price_Calculator_kata
{
    public class TaxServices
    {
        public static Boolean ChangeFlateRateTax( double taxRate)
        {
            if (taxRate > 0 && taxRate < 100)
            {
                FlatRateTaxCalculator.TaxPercentageDefault = taxRate;
                return true;
            }
            else return false;
        }
        public static ITaxCalculator CreateFlatRateTaxCalculator()
        {
            return new FlatRateTaxCalculator();
        }
        public static void ApplyTax(Product product, ITaxCalculator TaxCalculator)
        {
            product.TaxCalculator = TaxCalculator;
        }
    }
}
