using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Price_Calculator_kata
{
    public class TaxServices
    {
        
        public  ITaxCalculator getFlatRateTaxCalculator()
        {
            return  new FlatRateTaxCalculator();
        }
       
        
    }
}
