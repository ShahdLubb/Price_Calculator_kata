using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Price_Calculator_kata
{
    public class PriceCalculator
    {
        DiscountService MyDiscountService;
        ProductRepository products;
        public PriceCalculator(DiscountService discountService, ProductRepository products) {
            this.MyDiscountService = discountService;
            this.products= products;
         }

        public  double CalculateTotalPrice( Product product)
        {
            if (product.TaxCalculator is null) throw new TaxNotAppliedException();

            double BeforTaxDiscountAmount = 0.0;
            foreach (IDiscountCalculator Discount in MyDiscountService.GetBeforeTaxDiscounts())
            {
                BeforTaxDiscountAmount += Discount.CalculateDiscountAmount(product);
            }
            double BeforeTaxPrice = product.Price - BeforTaxDiscountAmount;
            double TaxAmount = product.TaxCalculator.CalculateTaxAmount(BeforeTaxPrice);
            double AfterTaxDiscountAmount = 0.0;
            Product temp = new Product(product.Name, product.UPC, BeforeTaxPrice);
            foreach (IDiscountCalculator Discount in MyDiscountService.GetAfterTaxDiscounts())
            {
                AfterTaxDiscountAmount += Discount.CalculateDiscountAmount(temp);
            }
            temp = null;
            return Math.Round(product.Price + TaxAmount - BeforTaxDiscountAmount - AfterTaxDiscountAmount, 2);
        }  

    }
}
