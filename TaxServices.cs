using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Price_Calculator_kata
{
    public class TaxServices
    {
        
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
