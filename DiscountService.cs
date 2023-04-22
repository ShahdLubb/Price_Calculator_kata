using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Price_Calculator_kata
{
    public class DiscountService
    {
        public static IDiscountCalculator CreateRelativeDiscount(double discountPercentage)
        {
            return new RelativeDiscountCalculator(discountPercentage);
        }

        
        public static void ApplyDiscount(Product product, IDiscountCalculator discountCalculator)
        {
            product.Discounts.Add(discountCalculator);
        }

        public static void RemoveDiscount(Product product, IDiscountCalculator discountCalculator)
        {
            product.Discounts.Remove(discountCalculator);
        }

        public static void RemoveAllDiscount(Product product)
        {
            product.Discounts.Clear();
        }
    }
}
