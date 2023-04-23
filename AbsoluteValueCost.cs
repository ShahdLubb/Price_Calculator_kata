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
        public double CostValue { 
            get=>_CostValue;
            set { 
                _CostValue =Math.Round(value,2);
            } 
        }
        public AbsoluteValueCost(double value, string description)
        {
            CostValue = value;
            Description = description;
        }
        public double GetCostAmount(double price)
        {
            return _CostValue;
        }
        public override string ToString()
        {
            return $"{Description} =%{_CostValue}";
        }
    }
}
