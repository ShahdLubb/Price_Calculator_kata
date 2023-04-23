using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Price_Calculator_kata
{
    public  class ReportGenerator
    {
        DiscountService MyDiscountService;
        CostService MycostService;
        ProductRepository products;
        StringBuilder report;
        public ReportGenerator(DiscountService discountService, CostService costService, ProductRepository products)
        {
            this.MyDiscountService = discountService;
            this.MycostService = costService;
            this.products = products;
            discountService.DiscountAdded += DiscountAddedEventHandler;
        }
        public void ReportPriceDetails(Product product)
        {
            AppendProductInfo(product);
            AppendTaxInfo(product);
            double beforeTaxDiscountAmount = AppendBeforeTaxDiscounts(product);
            double beforeTaxPrice = product.Price - beforeTaxDiscountAmount;
            double taxAmount = product.TaxCalculator.CalculateTaxAmount(beforeTaxPrice);
            double afterTaxDiscountAmount = AppendAfterTaxDiscounts(product, beforeTaxPrice);
            AppendTaxAmount(taxAmount);
            double totalCosts = AppendCosts(product.Price);
            AppendDiscountAmount(beforeTaxDiscountAmount + afterTaxDiscountAmount);
            double totalPrice = Math.Round(beforeTaxPrice + taxAmount + totalCosts - afterTaxDiscountAmount, 2);
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

        private double AppendBeforeTaxDiscounts(Product product)
        {
            double beforeTaxDiscountAmount = 0.0;
            foreach (IDiscountCalculator discount in MyDiscountService.GetBeforeTaxDiscounts())
            {
                beforeTaxDiscountAmount += discount.CalculateDiscountAmount(product);
                report.AppendLine(discount.ToString() + ",");
            }
            return beforeTaxDiscountAmount;
        }

        private double AppendAfterTaxDiscounts(Product product, double beforeTaxPrice)
        {
            double afterTaxDiscountAmount = 0.0;
            Product temp = new Product(product.Name, product.UPC, beforeTaxPrice);
            foreach (IDiscountCalculator discount in MyDiscountService.GetAfterTaxDiscounts())
            {
                afterTaxDiscountAmount += discount.CalculateDiscountAmount(temp);
                report.AppendLine(discount.ToString() + ",");
            }
            temp = null;
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

        private void AppendDiscountAmount(double discountAmount)
        {
            report.AppendLine($"Discount Amount=${Math.Round(discountAmount, 2)}");
        }

        private void AppendTotalPrice(double totalPrice)
        {
            report.AppendLine($"Price=${totalPrice}");
        }

        public void ReportPriceDetailsForAllProducts()
        {
            report = new StringBuilder();
            foreach (Product product in products.GetAll())
            {
                ReportPriceDetails(product);
            }
            Console.WriteLine(report.ToString());
            Console.WriteLine("*******************************");
        }
        private void DiscountAddedEventHandler(object sender, EventArgs e)
        {
            ReportPriceDetailsForAllProducts();
        }
    }
}
