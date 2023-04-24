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
            Product ProductAfterDiscounts=new Product(product.Name,product.UPC,product.Price,product.currency);
            Money beforeTaxDiscountAmount = AppendBeforeTaxDiscounts(ProductAfterDiscounts, discountCombinationMethod);
            Money beforeTaxPrice = new Money(product.Price.ValueHigherPrecision - beforeTaxDiscountAmount.ValueHigherPrecision);
            Money taxAmount = product.TaxCalculator.CalculateTaxAmount(beforeTaxPrice);
            Money afterTaxDiscountAmount = AppendAfterTaxDiscounts(ProductAfterDiscounts, beforeTaxPrice.ValueHigherPrecision, discountCombinationMethod);
            AppendTaxAmount(taxAmount, product);
            Money totalCosts = AppendCosts(product);
            Money BeforCapDisountAmount = new Money(beforeTaxDiscountAmount.ValueHigherPrecision + afterTaxDiscountAmount.ValueHigherPrecision);
            Money DiscountAmount = AppendDiscountAmount(BeforCapDisountAmount, product);
            Money totalPrice = GetTotalPrice(product.Price, taxAmount, totalCosts, DiscountAmount);
            AppendTotalPrice(totalPrice.Value,product);
            
        }

        private void AppendProductInfo(Product product)
        {
            report.AppendLine(product.ToString());
        }
        private void AppendProductCurrency(Product product)
        {
            report.Append(product.currency.ToString());
        }
        private void AppendTaxInfo(Product product)
        {
            if (product.TaxCalculator is null)
            {
                throw new TaxNotAppliedException();
            }
            report.AppendLine(product.TaxCalculator.ToString());
        }

        private Money AppendBeforeTaxDiscounts(Product product, DiscountCombinationMethod discountCombinationMethod)
        {
           Money beforeTaxDiscountAmount = new Money(0);
           foreach (IDiscountCalculator discount in MyDiscountService.GetBeforeTaxDiscounts())
                {
                    double value = discount.CalculateDiscountAmount(product).ValueHigherPrecision;
                    beforeTaxDiscountAmount = new Money(beforeTaxDiscountAmount.ValueHigherPrecision + value);
                    if (discountCombinationMethod.Equals(DiscountCombinationMethod.Multiplicative))
                        product.Price.ValueHigherPrecision = product.Price.ValueHigherPrecision - beforeTaxDiscountAmount.ValueHigherPrecision;
                    report.AppendLine(discount.ToString() + ",");
                }
           return beforeTaxDiscountAmount;
        }

        private Money AppendAfterTaxDiscounts(Product product, double beforeTaxPrice, DiscountCombinationMethod discountCombinationMethod)
        {
            Money afterTaxDiscountAmount = new Money(0);
            product.Price = new Money(beforeTaxPrice);
            foreach (IDiscountCalculator discount in MyDiscountService.GetAfterTaxDiscounts())
            {
                double value = discount.CalculateDiscountAmount(product).ValueHigherPrecision;
                afterTaxDiscountAmount = new Money(afterTaxDiscountAmount.ValueHigherPrecision + value);
                if (discountCombinationMethod.Equals(DiscountCombinationMethod.Multiplicative))
                    product.Price.ValueHigherPrecision = product.Price.ValueHigherPrecision - afterTaxDiscountAmount.ValueHigherPrecision;
                report.AppendLine(discount.ToString() + ",");
            }

            return afterTaxDiscountAmount;
        }

        private void AppendTaxAmount(Money taxAmount, Product product)
        {
            report.Append($"Tax={taxAmount.ToString()} ");
            AppendProductCurrency(product);
            report.AppendLine();
        }

        private Money AppendCosts(Product product)
        {
            Money totalCosts = new Money(0);
            foreach (ICost cost in MycostService.GetAll())
            {
                Money costAmount = cost.GetCostAmount(product);
                report.AppendLine($"{cost.ToString()} ===> {costAmount.ToString()} {product.currency.ToString()}");
                totalCosts = new Money(totalCosts.ValueHigherPrecision + costAmount.ValueHigherPrecision);
            }
            return totalCosts;
           
        }

        private Money AppendDiscountAmount(Money discountAmount,Product product)
        {
            discountAmount = AppendCap(discountAmount,product);
            report.AppendLine($"Discount Amount={discountAmount.ToString()} {product.currency.ToString()}");
            return discountAmount;
        }
        private Money AppendCap(Money discountAmount, Product product)
        {
            if (MyCapService is null) return discountAmount;
            discountAmount = MyCapService.ApplyCap(discountAmount, product);

            report.AppendLine($"Cap={MyCapService.ToStringInProductCurrency(product)}");
            return discountAmount;
        }

        private void AppendTotalPrice(double totalPrice, Product product)
        {
            report.Append($"Price= {totalPrice} ");
            AppendProductCurrency(product);
            report.AppendLine();
        }
        private Money GetTotalPrice(Money Price, Money taxAmount, Money totalCosts, Money DiscountAmount)
        {
            double value = Price.ValueHigherPrecision;
            value += taxAmount.ValueHigherPrecision;
            value += totalCosts.ValueHigherPrecision;
            value -= DiscountAmount.ValueHigherPrecision;
            return new Money(value);

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
