using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Price_Calculator_kata
{
    public class DiscountService
    {
        private readonly ProductRepository _productRepository;

        public DiscountService(ProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public static IDiscountCalculator CreateRelativeDiscount(double discountPercentage)
        {
            return new RelativeDiscountCalculator(discountPercentage);
        }
        public void ApplyDiscountForAllProducts(IDiscountCalculator Discount)
        {
           _productRepository.ApplyDiscountToAll(Discount);
            
        }
    }
}
