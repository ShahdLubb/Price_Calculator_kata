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
        public Currency currency { get; set; }
        public Money CostValue { get; set; }
        public AbsoluteValueCost(Money value, string description, Currency currency)
        {
            CostValue = value;
            Description = description;
            this.currency = currency;
        }
        public Money GetCostAmount(Product product)
        {
            Money cost = new Money(CostValue.ValueHigherPrecision);
            if(!product.currency.Code.Equals(this.currency.Code)) {
                cost = currency.ConvertToBase(cost);
                cost= product.currency.ConvertFromBase(cost);
            }
            return cost;
        }
        public override string ToString()
        {
            return $"{Description} =%{CostValue.ToString()}";
        }
    }
}
