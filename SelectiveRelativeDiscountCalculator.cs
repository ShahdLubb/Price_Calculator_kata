using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Price_Calculator_kata
{
    public class SelectiveRelativeDiscountCalculator : RelativeDiscountCalculator
    {
        private int _UPC;
        public int UPC { get=> _UPC; }
        public SelectiveRelativeDiscountCalculator(double discount, int UPC) 
            :base(discount)
        {
            _UPC = UPC;
        }
        public SelectiveRelativeDiscountCalculator(double discount, int UPC, bool isBeforeTax)
            : base(discount, isBeforeTax)
        {
            _UPC = UPC;
        }

        public override Money CalculateDiscountAmount(Product product)
        {
            if (product.UPC == this._UPC) return base.CalculateDiscountAmount(product);
            else return new Money(0);

        }

        public override string ToString()
        {
            if (isBeforeTax())
            {
                return $"Befor-Tax-UPC-discount= %{Discount} for UPC={this.UPC}";
            }
            else return $"After-Tax-UPC-discount= %{Discount} for UPC={this.UPC}";

        }
    }
}
