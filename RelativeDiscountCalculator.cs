using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Price_Calculator_kata
{
    internal class RelativeDiscountCalculator : IDiscountCalculator
    {
        private static double _Discount = 0.0;
        public static double Discount
        {
            get => _Discount;
            set
            {
                if (value >= 0 && value < 100.0)
                {
                    _Discount = Math.Round(value, 2);
                }
            }
        }
        public RelativeDiscountCalculator(double discount)
        {
            Discount = discount;
        }
        public double CalculateDiscountAmount(Product product)
        {
            double DiscountedPrice =  product.Price * (_Discount / 100.0);
            return DiscountedPrice;
            
        }

        
    }
}
