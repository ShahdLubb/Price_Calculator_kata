﻿namespace Price_Calculator_kata
{
    public class PriceCalculator
    {
        TaxServices MyTaxService;
        DiscountService MyDiscountService;
        public PriceCalculator(TaxServices MyTaxService, DiscountService MyDiscountService)
        {
            this.MyTaxService = MyTaxService;
            this.MyDiscountService = MyDiscountService;

        }

        public double CalculateTotalPrice(Product product)
        {
            double taxAmount = CalculateTaxAmount(product);
            double DiscountAmount = CalculateDiscountAmount(product);
            double totalPrice = Math.Round(product.Price + taxAmount - DiscountAmount, 2);
            return totalPrice;
        }
        private void CheckTax()
        {
            if (MyTaxService is null)
            {
                throw new TaxNotAppliedException();
            }

        }

        public double CalculateTaxAmount(Product product)
        {
            ITaxCalculator TaxCalculator = MyTaxService.getFlatRateTaxCalculator();
            double TaxAmount = TaxCalculator.CalculateTaxAmount(product.Price);
            return TaxAmount;
        }
        private double CalculateDiscountAmount(Product product)
        {
            double DiscountAmount = 0;
            foreach (IDiscountCalculator discount in MyDiscountService.GetDiscounts())
            {
                DiscountAmount += discount.CalculateDiscountAmount(product);
            }
            return DiscountAmount;
        }


    }
}
