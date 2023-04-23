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
        
        public override double CalculateDiscountAmount(Product product)
        {
            if (product.UPC == this._UPC) return base.CalculateDiscountAmount(product);
            else return 0;

        }

        public override string ToString()
        {
            return $"UPC-discount= %{Discount} for UPC={this.UPC}";
        }
    }
}
