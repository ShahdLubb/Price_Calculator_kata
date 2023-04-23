using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Price_Calculator_kata
{
    public class RelativeDiscountCalculator : IDiscountCalculator
    {
        private  double _Discount = 0.0;
        private bool _IsBeforeTax = false;
        public  double Discount
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
            _IsBeforeTax = false;
        }
        public RelativeDiscountCalculator(double discount, bool isBeforeTax)
        {
            Discount = discount;
            _IsBeforeTax= isBeforeTax;
        }
        public virtual double CalculateDiscountAmount(Product product)
        {
            double DiscountedPrice =  product.Price * (_Discount / 100.0);
            return DiscountedPrice;
            
        }
        public bool isBeforeTax()
        {
            return _IsBeforeTax;
        }
        public override string ToString()
        {
            if (isBeforeTax())
            {
                return $"Befor-Tax-Relative-Discount= %{Discount} ";
            }
            else return $"After-Tax-Relative-Discount= %{Discount} ";
        }

        
    }
}
