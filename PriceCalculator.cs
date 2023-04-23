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
        CostService MycostService;
        ProductRepository products;
        public PriceCalculator(DiscountService discountService, CostService costService, ProductRepository products) {
            this.MyDiscountService = discountService;
            this.MycostService = costService;  
            this.products= products;
         }

        public  double CalculateTotalPrice( Product product)
        {
            CheckTax(product);
            double beforeTaxDiscountAmount = CalculateBeforeTaxDiscounts(product);
            double beforeTaxPrice = product.Price - beforeTaxDiscountAmount;
            double taxAmount = product.TaxCalculator.CalculateTaxAmount(beforeTaxPrice);
            double afterTaxDiscountAmount = CalculateAfterTaxDiscounts(product, beforeTaxPrice);
            double totalCosts = CalculateCosts(product.Price);
            double totalPrice = Math.Round(beforeTaxPrice + taxAmount + totalCosts - afterTaxDiscountAmount, 2);
            return totalPrice;
        }
        

        private void CheckTax(Product product)
        {
            if (product.TaxCalculator is null)
            {
                throw new TaxNotAppliedException();
            }
            
        }

        private double CalculateBeforeTaxDiscounts(Product product)
        {
            double beforeTaxDiscountAmount = 0.0;
            foreach (IDiscountCalculator discount in MyDiscountService.GetBeforeTaxDiscounts())
            {
                beforeTaxDiscountAmount += discount.CalculateDiscountAmount(product);
               
            }
            return beforeTaxDiscountAmount;
        }

        private double CalculateAfterTaxDiscounts(Product product, double beforeTaxPrice)
        {
            double afterTaxDiscountAmount = 0.0;
            Product temp = new Product(product.Name, product.UPC, beforeTaxPrice);
            foreach (IDiscountCalculator discount in MyDiscountService.GetAfterTaxDiscounts())
            {
                afterTaxDiscountAmount += discount.CalculateDiscountAmount(temp);
                
            }
            temp = null;
            return afterTaxDiscountAmount;
        }

       
        private double CalculateCosts(double productPrice)
        {
            double totalCosts = 0;
            foreach (ICost cost in MycostService.GetAll())
            {
                double costAmount = cost.GetCostAmount(productPrice);
                totalCosts += Math.Round(costAmount, 2);
            }
            return totalCosts;
        }

    }
}
