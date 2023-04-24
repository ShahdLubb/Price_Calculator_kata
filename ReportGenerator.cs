using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Price_Calculator_kata.PriceCalculator;

namespace Price_Calculator_kata
{
    public  class ReportGenerator
    {
        public DiscountService MyDiscountService { get; }
        public CostService MycostService { get; }
        public ProductRepository products { get; }
        public CapService MyCapService { get; }
        private StringBuilder report;
        public ReportGenerator(DiscountService discountService, CostService costService, CapService myCapService, ProductRepository products)
        {
            report=new StringBuilder();
            this.MyDiscountService = discountService;
            this.MycostService = costService;
            this.products = products;
            MyCapService = myCapService;
            discountService.DiscountAdded += DiscountAddedEventHandler;

        }
        private void ReportPriceDetails(Product product, DiscountCombinationMethod discountCombinationMethod)
        {
            AppendProductInfo(product);
            AppendTaxInfo(product);
            Product ProductAfterDiscounts=new Product(product.Name,product.UPC,product.Price);
            double beforeTaxDiscountAmount = AppendBeforeTaxDiscounts(ProductAfterDiscounts, discountCombinationMethod);
            double beforeTaxPrice = product.Price - beforeTaxDiscountAmount;
            double taxAmount = product.TaxCalculator.CalculateTaxAmount(beforeTaxPrice);
            double afterTaxDiscountAmount = AppendAfterTaxDiscounts(ProductAfterDiscounts, beforeTaxPrice, discountCombinationMethod);
            AppendTaxAmount(taxAmount);
            double totalCosts = AppendCosts(product.Price);
            double DiscountAmount = AppendDiscountAmount(beforeTaxDiscountAmount + afterTaxDiscountAmount, product);
            double totalPrice = Math.Round(product.Price + taxAmount + totalCosts - DiscountAmount, 2);
            AppendTotalPrice(totalPrice);
        }

        private void AppendProductInfo(Product product)
        {
            report.AppendLine(product.ToString());
        }

        private void AppendTaxInfo(Product product)
        {
            if (product.TaxCalculator is null)
            {
                throw new TaxNotAppliedException();
            }
            report.AppendLine(product.TaxCalculator.ToString());
        }

        private double AppendBeforeTaxDiscounts(Product product, DiscountCombinationMethod discountCombinationMethod)
        {
            double beforeTaxDiscountAmount = 0.0;
            foreach (IDiscountCalculator discount in MyDiscountService.GetBeforeTaxDiscounts())
            {
                beforeTaxDiscountAmount += discount.CalculateDiscountAmount(product);
                if(discountCombinationMethod.Equals(DiscountCombinationMethod.Multiplicative)) 
                    product.Price= product.Price -beforeTaxDiscountAmount;
                report.AppendLine(discount.ToString() + ",");
            }
            return beforeTaxDiscountAmount;
        }

        private double AppendAfterTaxDiscounts(Product product, double beforeTaxPrice, DiscountCombinationMethod discountCombinationMethod)
        {
            double afterTaxDiscountAmount = 0.0;
            product.Price = beforeTaxPrice;
            foreach (IDiscountCalculator discount in MyDiscountService.GetAfterTaxDiscounts())
            {
                afterTaxDiscountAmount += discount.CalculateDiscountAmount(product);
                if (discountCombinationMethod.Equals(DiscountCombinationMethod.Multiplicative))
                    product.Price = product.Price - afterTaxDiscountAmount;
                report.AppendLine(discount.ToString() + ",");
            }
            
            return afterTaxDiscountAmount;
        }

        private void AppendTaxAmount(double taxAmount)
        {
            report.AppendLine($"Tax=${taxAmount}");
        }

        private double AppendCosts(double productPrice)
        {
            double totalCosts = 0;
            foreach (ICost cost in MycostService.GetAll())
            {
                double costAmount = cost.GetCostAmount(productPrice);
                report.AppendLine($"{cost.ToString()} ===> ${costAmount}");
                totalCosts += Math.Round(costAmount, 2);
            }
            return totalCosts;
        }

        private double AppendDiscountAmount(double discountAmount,Product product)
        {
            discountAmount = AppendCap(discountAmount,product);
            report.AppendLine($"Discount Amount=${Math.Round(discountAmount, 2)}");
            return discountAmount;
        }
        private double AppendCap(double discountAmount, Product product)
        {
            if (MyCapService is null) return discountAmount;
            discountAmount = MyCapService.ApplyCap(discountAmount, product);
            report.AppendLine($"Cap={MyCapService.ToString()}");
            return discountAmount;
        }

        private void AppendTotalPrice(double totalPrice)
        {
            report.AppendLine($"Price=${totalPrice}");
        }

        public void ReportPriceDetailsForAllProducts(DiscountCombinationMethod discountCombinationMethod)
        {
            report.Clear(); 
            foreach (Product product in products.GetAll())
            {
                ReportPriceDetails(product, discountCombinationMethod);
            }
            Console.WriteLine(report.ToString());
            Console.WriteLine("*******************************");
        }

        private void DiscountAddedEventHandler(object sender, EventArgs e)
        {
            Console.WriteLine("Additive:");
            ReportPriceDetailsForAllProducts(DiscountCombinationMethod.Additive);
            Console.WriteLine("Multiplicative:");
            ReportPriceDetailsForAllProducts(DiscountCombinationMethod.Multiplicative);
        }
    }
}
