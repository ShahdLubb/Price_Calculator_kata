﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Price_Calculator_kata
{
    public static  class ReportGenerator
    {
        public static string ReportPriceDetails(this Product product, DiscountService MyDiscountService)
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
            double TotalPrice = Math.Round(product.Price + TaxAmount - AfterTaxDiscountAmount - BeforTaxDiscountAmount, 2);
            report.AppendLine($"Price=${TotalPrice}");
            report.AppendLine($"Discount Amount=${Math.Round(AfterTaxDiscountAmount + BeforTaxDiscountAmount, 2)}");
            return report.ToString();
        }
    }
}