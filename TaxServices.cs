using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Price_Calculator_kata
{
    public class TaxServices
    {
        
        private static ITaxCalculator getFlatRateTaxCalculator()
        {
            return  FlatRateTaxCalculator.Instance;
        }
        public static void ApplyFlatRateTax(Product product)
        {
            product.TaxCalculator = getFlatRateTaxCalculator();
        }
        public static void ApplyTax(Product product, ITaxCalculator TaxCalculator)
        {
            product.TaxCalculator = TaxCalculator;
        }
    }
}
