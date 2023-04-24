using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Price_Calculator_kata
{
    public class CapService
    {
        public Money Amount { get; }
        private bool isPercentage;
        public Currency currency;
        public CapService(double amount, bool isPercentage, Currency currency)
        {
            Amount = new Money(amount);
            this.isPercentage = isPercentage;
            this.currency = currency;
        }

        public Money ApplyCap(Money DiscountAmount,Product product)
        {
            Money capVal ;
            if (isPercentage) capVal = new Money(product.Price.ValueHigherPrecision * (Amount.ValueHigherPrecision / 100));
            else { 
                capVal = Amount;
                if (!product.currency.Code.Equals(this.currency.Code))
                {
                    capVal = currency.ConvertToBase(capVal);
                    capVal = product.currency.ConvertFromBase(capVal);
                }
            }

            if (DiscountAmount.ValueHigherPrecision > capVal.ValueHigherPrecision) return capVal;
            else return DiscountAmount;

           
        }


        public override string ToString()
        {
            if (isPercentage)
                return $"%{Amount.ToString()} of price";
            else
                return $"{Amount.ToString()} {currency.ToString()}";

        }
        public  string ToStringInProductCurrency(Product product)
        {
            if (isPercentage)
                return $"%{Amount.ToString()} of price";
            else
            {
                Money capVal = Amount;
                if (!product.currency.Code.Equals(this.currency.Code))
                {
                    capVal = currency.ConvertToBase(capVal);
                    capVal = product.currency.ConvertFromBase(capVal);
                }
                return $"{capVal.ToString()} {product.currency.ToString()}";
            }
                

        }


    }
}
