using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Price_Calculator_kata.PriceCalculator;

namespace Price_Calculator_kata
{
    public class PriceCalculator
    {
        public DiscountService MyDiscountService { get; }
        public CostService MycostService { get; }
        public ProductRepository products { get; }
        public CapService MyCapService { get; }
        public PriceCalculator(DiscountService discountService, CostService costService,CapService MyCapService, ProductRepository products) {
            this.MyDiscountService = discountService;
            this.MycostService = costService;  
            this.products= products;
            this.MyCapService = MyCapService;
         }
        public enum DiscountCombinationMethod
        {
            Additive,
            Multiplicative
        }
        public  double CalculateTotalPrice( Product product, DiscountCombinationMethod discountCombinationMethod)
        {
            CheckTax(product);
            Product ProductAfterDiscounts = new Product(product.Name, product.UPC, product.Price);
            double beforeTaxDiscountAmount = CalculateBeforeTaxDiscounts(ProductAfterDiscounts, discountCombinationMethod);
            double beforeTaxPrice = product.Price - beforeTaxDiscountAmount;
            double taxAmount = product.TaxCalculator.CalculateTaxAmount(beforeTaxPrice);
            double afterTaxDiscountAmount = CalculateAfterTaxDiscounts(ProductAfterDiscounts, beforeTaxPrice, discountCombinationMethod);
            double totalCosts = CalculateCosts(product.Price);
            double DiscountAmount = CalculateDiscountAmount(beforeTaxDiscountAmount + afterTaxDiscountAmount, product);
            double totalPrice = Math.Round(product.Price + taxAmount + totalCosts - DiscountAmount, 2);
            return totalPrice;
        }
        

        private void CheckTax(Product product)
        {
            if (product.TaxCalculator is null)
            {
                throw new TaxNotAppliedException();
            }
            
        }

        private double CalculateBeforeTaxDiscounts(Product product, DiscountCombinationMethod discountCombinationMethod)
        {
            double beforeTaxDiscountAmount = 0.0;
            foreach (IDiscountCalculator discount in MyDiscountService.GetBeforeTaxDiscounts())
            {
                beforeTaxDiscountAmount += discount.CalculateDiscountAmount(product);
                if (discountCombinationMethod.Equals(DiscountCombinationMethod.Multiplicative))
                    product.Price = product.Price - beforeTaxDiscountAmount;
            }
            return beforeTaxDiscountAmount;
        }

        private double CalculateAfterTaxDiscounts(Product product, double beforeTaxPrice , DiscountCombinationMethod discountCombinationMethod)
        {
            double afterTaxDiscountAmount = 0.0;
            product.Price = beforeTaxPrice;
            foreach (IDiscountCalculator discount in MyDiscountService.GetAfterTaxDiscounts())
            {
                afterTaxDiscountAmount += discount.CalculateDiscountAmount(product);
                if (discountCombinationMethod.Equals(DiscountCombinationMethod.Multiplicative))
                    product.Price = product.Price - afterTaxDiscountAmount;

            }
            
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
        private double CalculateDiscountAmount(double DiscountAmount, Product product)
        {
            return  MyCapService is null ? DiscountAmount: MyCapService.ApplyCap(DiscountAmount, product); 

        }

    }
}
