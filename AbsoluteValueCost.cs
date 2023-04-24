using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Price_Calculator_kata
{
    public class AbsoluteValueCost : ICost
    {
        string Description { get; set; }
        private double _CostValue;
        public Currency currency { get; set; }
        public double CostValue { 
            get=>_CostValue;
            set { 
                _CostValue =Math.Round(value,2);
            } 
        }
        public AbsoluteValueCost(double value, string description, Currency currency)
        {
            CostValue = value;
            Description = description;
            this.currency = currency;
        }
        public double GetCostAmount(Product product)
        {
            double cost = _CostValue;
            if(!product.currency.Code.Equals(this.currency.Code)) {
                cost = currency.ConvertToBase(cost);
                cost= product.currency.ConvertFromBase(cost);
            }
            return Math.Round(cost,2);
        }
        public override string ToString()
        {
            return $"{Description} =%{_CostValue}";
        }
    }
}
