using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Price_Calculator_kata
{
    public class Currency
    {
        private static HashSet<string> validCodes = new HashSet<string>()
        {
        "USD", "GBP", "JPY", "EUR",
        };
        private string _code;
        public string Code { 
            get=>_code; 
            set {
                if (IsValidISO3(value))
                {
                    _code = value.ToUpper();
                }
                else
                {
                    throw new ArgumentException($"Currency code {value} is not supported.");
                }
            } 
        }
        public double RateToBase { get; set; }

        public Currency(string code, double rate)
        {
            Code = code;
            RateToBase = rate;
        }
        public Money ConvertToBase(Money Amount)
        {
            return new Money(Amount.ValueHigherPrecision/ RateToBase);
        }
        public Money ConvertFromBase(Money Amount)
        {
            return new Money(Amount.ValueHigherPrecision * RateToBase);
        }
        public static bool IsValidISO3(string code)
        {
            return validCodes.Contains(code.ToUpper());
        }

        public override string ToString()
        {
            return $"{Code}";
        }

        public override bool Equals(object? obj)
        {
            if(obj is null) return false;
            if (obj == this) return true;
            Currency currency= obj as Currency;
            return currency.Code.Equals(this.Code) ;
        }
    }
}
