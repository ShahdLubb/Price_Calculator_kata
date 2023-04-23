using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Price_Calculator_kata
{
    public class DiscountService
    {
        private  List<IDiscountCalculator> _BeforeTaxDiscounts = new List<IDiscountCalculator>();
        private  List<IDiscountCalculator> _AfterTaxDiscounts = new List<IDiscountCalculator>();
        private readonly ProductRepository _productRepository;

        public DiscountService(ProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public static IDiscountCalculator CreateRelativeDiscount(double discountPercentage)
        {
            return new RelativeDiscountCalculator(discountPercentage);
        }
        public static IDiscountCalculator CreateRelativeDiscount(double discountPercentage, bool isBeforeTax)
        {
            return new RelativeDiscountCalculator(discountPercentage, isBeforeTax);
        }

        public static IDiscountCalculator CreateSelectiveRelativeDiscount(double discountPercentage, int UPC)
        {
            return new SelectiveRelativeDiscountCalculator(discountPercentage,UPC);
        }

        public static IDiscountCalculator CreateSelectiveRelativeDiscount(double discountPercentage, int UPC, bool isBeforeTax)
        {
            return new SelectiveRelativeDiscountCalculator(discountPercentage, UPC,isBeforeTax);
        }
        public void ApplyDiscountForAllProducts(IDiscountCalculator Discount)
        {
           if(Discount.isBeforeTax()) _BeforeTaxDiscounts.Add(Discount);
           else _AfterTaxDiscounts.Add(Discount);
           _productRepository.PrintPriceReport(this);
        }

        public List<IDiscountCalculator> GetBeforeTaxDiscounts()
        {
            return _BeforeTaxDiscounts;

        }
        public List<IDiscountCalculator> GetAfterTaxDiscounts()
        {
            return _AfterTaxDiscounts;

        }

    }
}
