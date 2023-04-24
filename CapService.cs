using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Price_Calculator_kata
{
    public class CapService
    {
        public double Amount { get; }
        private bool isPercentage;
        public CapService(double amount, bool isPercentage)
        {
            Amount = amount;
            this.isPercentage = isPercentage;
        }

        public double ApplyCap(double DiscountAmount,Product product)
        {
            double capVal = 0;
            if (isPercentage) capVal = product.Price * (Amount / 100);
            else capVal = Amount;

            if (DiscountAmount > capVal) return capVal;
            else return DiscountAmount;
        }

        public override string ToString()
        {
            if (isPercentage)
                return $"%{Amount} of price";
            else
                return $"${Amount}";

        }
    }
}
