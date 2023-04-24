using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Price_Calculator_kata
{
    public class CostService
    {
        private List<ICost> _costs;
        public CostService() {
            _costs = new List<ICost>();
        }
        public static ICost CreateAbsoluteValueCost(double CostValue, string Description, Currency currency)
        {
            return new AbsoluteValueCost(new Money(CostValue), Description,currency);
        }
        public static ICost CreatePercentageCost(double percentage, string Description)
        {
            return new PercentageCost(percentage, Description);
        }
        public  void AddCost(ICost cost)
        {
           _costs.Add(cost);
        }
       
        public List<ICost> GetAll()
        {
            return _costs;
        }
    }
}
