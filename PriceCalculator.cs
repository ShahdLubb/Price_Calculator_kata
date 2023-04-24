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
            Product ProductAfterDiscounts = new Product(product.Name, product.UPC, product.Price,product.currency);
            Money beforeTaxDiscountAmount = CalculateBeforeTaxDiscounts(ProductAfterDiscounts, discountCombinationMethod);
            Money beforeTaxPrice = new Money(product.Price.ValueHigherPrecision - beforeTaxDiscountAmount.ValueHigherPrecision);
            Money taxAmount = product.TaxCalculator.CalculateTaxAmount(beforeTaxPrice);
            Money afterTaxDiscountAmount = CalculateAfterTaxDiscounts(ProductAfterDiscounts, beforeTaxPrice.ValueHigherPrecision, discountCombinationMethod);
            Money totalCosts = CalculateCosts(product);
            Money BeforCapDisountAmount = new Money(beforeTaxDiscountAmount.ValueHigherPrecision + afterTaxDiscountAmount.ValueHigherPrecision);
            Money DiscountAmount = CalculateDiscountAmount(BeforCapDisountAmount, product);
            Money totalPrice = GetTotalPrice(product.Price, taxAmount, totalCosts, DiscountAmount);
            return totalPrice.Value;
        }
        

        private void CheckTax(Product product)
        {
            if (product.TaxCalculator is null)
            {
                throw new TaxNotAppliedException();
            }
            
        }

        private Money CalculateBeforeTaxDiscounts(Product product, DiscountCombinationMethod discountCombinationMethod)
        {
            Money beforeTaxDiscountAmount = new Money(0);
            foreach (IDiscountCalculator discount in MyDiscountService.GetBeforeTaxDiscounts())
            {
                double value= discount.CalculateDiscountAmount(product).ValueHigherPrecision;
                beforeTaxDiscountAmount = new Money(beforeTaxDiscountAmount.ValueHigherPrecision +value);
                if (discountCombinationMethod.Equals(DiscountCombinationMethod.Multiplicative))
                    product.Price.ValueHigherPrecision = product.Price.ValueHigherPrecision - beforeTaxDiscountAmount.ValueHigherPrecision;
            }
            return beforeTaxDiscountAmount;
        }

        private Money CalculateAfterTaxDiscounts(Product product, double beforeTaxPrice , DiscountCombinationMethod discountCombinationMethod)
        {
            Money afterTaxDiscountAmount = new Money(0);
            product.Price = new Money(beforeTaxPrice);
            foreach (IDiscountCalculator discount in MyDiscountService.GetAfterTaxDiscounts())
            {
                double value = discount.CalculateDiscountAmount(product).ValueHigherPrecision;
                afterTaxDiscountAmount = new Money(afterTaxDiscountAmount.ValueHigherPrecision + value);
                if (discountCombinationMethod.Equals(DiscountCombinationMethod.Multiplicative))
                    product.Price.ValueHigherPrecision = product.Price.ValueHigherPrecision - afterTaxDiscountAmount.ValueHigherPrecision;

            }
            
            return afterTaxDiscountAmount;
        }

       
        private Money CalculateCosts(Product product)
        {
            Money totalCosts = new Money(0);
            foreach (ICost cost in MycostService.GetAll())
            {
                double costAmount = cost.GetCostAmount(product).ValueHigherPrecision;
                totalCosts = new Money(totalCosts.ValueHigherPrecision+costAmount);
            }
            return totalCosts;
        }
        private Money CalculateDiscountAmount(Money DiscountAmount, Product product)
        {
            return  MyCapService is null ? DiscountAmount: MyCapService.ApplyCap(DiscountAmount, product); 

        }

        private Money GetTotalPrice(Money Price,Money taxAmount,Money totalCosts,Money DiscountAmount) {
            double value = Price.ValueHigherPrecision;
            value += taxAmount.ValueHigherPrecision;
            value+= totalCosts.ValueHigherPrecision;
            value-=DiscountAmount.ValueHigherPrecision;
            return new Money(value);

        }

    }
}
