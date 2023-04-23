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
        public ReportGenerator(DiscountService discountService, CostService costService, ProductRepository products)
        {
            this.MyDiscountService = discountService;
            this.MycostService = costService;
            this.products = products;
            discountService.DiscountAdded += DiscountAddedEventHandler;
        }
        public string ReportPriceDetails(Product product)
        {
            StringBuilder report = new StringBuilder();
            report.AppendLine(product.ToString());
            if (product.TaxCalculator is null) throw new TaxNotAppliedException();
            report.AppendLine(product.TaxCalculator.ToString());
            double BeforTaxDiscountAmount = 0.0;
            foreach (IDiscountCalculator Discount in MyDiscountService.GetBeforeTaxDiscounts())
            {
                BeforTaxDiscountAmount += Discount.CalculateDiscountAmount(product);
                report.AppendLine(Discount.ToString() + ",");
            }
            double BeforeTaxPrice = product.Price - BeforTaxDiscountAmount;
            double TaxAmount = product.TaxCalculator.CalculateTaxAmount(BeforeTaxPrice);
            double AfterTaxDiscountAmount = 0.0;
            Product temp = new Product(product.Name, product.UPC, BeforeTaxPrice);
            foreach (IDiscountCalculator Discount in MyDiscountService.GetAfterTaxDiscounts())
            {
                AfterTaxDiscountAmount += Discount.CalculateDiscountAmount(temp);
                report.AppendLine(Discount.ToString() + ",");
            }
            temp = null;
            report.AppendLine($"Tax=${TaxAmount}");
            double TotalCosts = 0;
            foreach (ICost cost in MycostService.GetAll())
            {
                double costAmount= cost.GetCostAmount(product.Price);
                report.AppendLine($"{cost.ToString()} ===> ${costAmount}");
                TotalCosts += Math.Round(costAmount,2);
            }
            double TotalPrice = Math.Round(product.Price + TaxAmount+ TotalCosts - AfterTaxDiscountAmount - BeforTaxDiscountAmount, 2);
            report.AppendLine($"Discount Amount=${Math.Round(AfterTaxDiscountAmount + BeforTaxDiscountAmount, 2)}");
            report.AppendLine($"Price=${TotalPrice}");
            return report.ToString();
        }
        public void ReportPriceDetailsForAllProducts()
        {
            foreach(Product product in products.GetAll())
            {
                Console.WriteLine(ReportPriceDetails(product));
            }
            Console.WriteLine("*******************************");
        }
        private void DiscountAddedEventHandler(object sender, EventArgs e)
        {
            ReportPriceDetailsForAllProducts();
        }
    }
}
