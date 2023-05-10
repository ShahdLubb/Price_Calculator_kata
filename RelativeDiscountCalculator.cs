﻿namespace Price_Calculator_kata
{
    internal class RelativeDiscountCalculator : IDiscountCalculator
    {
        private double _Discount = 0.0;
        public double Discount
        {
            get => _Discount;
            set
            {
                if (value >= 0 && value < 100.0)
                {
                    _Discount = Math.Round(value, 2);
                }
            }
        }
        public RelativeDiscountCalculator(double discount)
        {
            Discount = discount;
        }
        public double CalculateDiscountAmount(Product product)
        {
            double DiscountedPrice = product.Price * (_Discount / 100.0);
            return DiscountedPrice;

        }

        public override string ToString()
        {
            return $"Relative Discount= %{Discount} ";
        }
    }
}
