using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Price_Calculator_kata
{
    public interface IDiscountCalculator
    {
        public double CalculateDiscountAmount(Product product);
    }
}
