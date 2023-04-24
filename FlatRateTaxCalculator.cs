using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Price_Calculator_kata
{
    public class FlatRateTaxCalculator : ITaxCalculator
    {
        private static double _TaxPercentageDefault = 20;
        public static double TaxPercentageDefault {
            get => _TaxPercentageDefault;
            set
            {
                if (value > 0.0 && value < 100.0)
                {
                    _TaxPercentageDefault = value;
                }
            }
        }
        private FlatRateTaxCalculator() { }
        private static FlatRateTaxCalculator? instance = null;
        public static FlatRateTaxCalculator Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new FlatRateTaxCalculator();
                }
                return instance;
            }
        }


        public Money CalculateTaxAmount(Money Price)
        {
            Money taxAmount =new Money( Price.ValueHigherPrecision * (TaxPercentageDefault/ 100.0));
            return taxAmount;
        }

        public override string ToString()
        {
            return $"Flat Rate Tax= %{TaxPercentageDefault}  ";
        }

    }
}
