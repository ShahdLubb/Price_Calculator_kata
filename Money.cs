using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Price_Calculator_kata
{
    public class Money
    {
        private const int HigherPrecision = 4;
        private const int LowerPrecision = 2;

        private  double _value;

        public Money(double value)
        {
            this.ValueHigherPrecision = Math.Round(value, HigherPrecision);
        }

        public double Value => Math.Round(_value, LowerPrecision);
        public double ValueHigherPrecision { get=>_value; set=> _value= Math.Round(value, HigherPrecision); }

        public override string ToString() => Value.ToString("0.00");
    }

}
