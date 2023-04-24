using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Price_Calculator_kata
{
    internal class PercentageCost : ICost
    {
        public string Description { get; set; } 
        private double _Percentage;
        public double Percentage { 
            get=> _Percentage; 
            set {
                if (value > 0 ) { 
                    _Percentage = Math.Round(value,2);
                }
            } 
        }
        public PercentageCost(double percentage, string description) { 
            this.Percentage = percentage;
            this.Description = description;
        }
        public Money GetCostAmount(Product product)
        {
            return new Money(product.Price.ValueHigherPrecision*(Percentage/100));
        }
        public override string ToString()
        {
            return $"{Description} =%{_Percentage} of price";
        }
    }
}
