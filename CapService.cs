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
        public Currency currency;
        public CapService(double amount, bool isPercentage, Currency currency)
        {
            Amount = amount;
            this.isPercentage = isPercentage;
            this.currency = currency;
        }

        public double ApplyCap(double DiscountAmount,Product product)
        {
            double capVal = 0;
            if (isPercentage) capVal = product.Price * (Amount / 100);
            else { 
                capVal = Amount;
                if (!product.currency.Code.Equals(this.currency.Code))
                {
                    capVal = currency.ConvertToBase(capVal);
                    capVal = product.currency.ConvertFromBase(capVal);
                }
            }

            if (DiscountAmount > capVal) return capVal;
            else return DiscountAmount;

           
        }


        public override string ToString()
        {
            if (isPercentage)
                return $"%{Amount} of price";
            else
                return $"{Amount} {currency.ToString()}";

        }
        public  string ToStringInProductCurrency(Product product)
        {
            if (isPercentage)
                return $"%{Amount} of price";
            else
            {
                double capVal = Amount;
                if (!product.currency.Code.Equals(this.currency.Code))
                {
                    capVal = currency.ConvertToBase(capVal);
                    capVal = product.currency.ConvertFromBase(capVal);
                }
                return $"{capVal} {product.currency.ToString()}";
            }
                

        }


    }
}
