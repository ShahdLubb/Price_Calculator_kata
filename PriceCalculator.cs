using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Price_Calculator_kata
{
    public class PriceCalculator
    {
        TaxServices MyTaxService;
        public PriceCalculator(TaxServices MyTaxService) {
           
            this.MyTaxService = MyTaxService;
        
         }
       
        public double CalculateTotalPrice(Product product)
        {
            double taxAmount = CalculateTaxAmount(product);
            double totalPrice = Math.Round(product.Price + taxAmount, 2);
            return totalPrice;
        }
        private void CheckTax()
        {
            if (MyTaxService is null)
            {
                throw new TaxNotAppliedException();
            }

        }

        public double CalculateTaxAmount(Product product)
        {
            ITaxCalculator TaxCalculator= MyTaxService.getFlatRateTaxCalculator();
            double TaxAmount = TaxCalculator.CalculateTaxAmount(product.Price);
            return TaxAmount;
        }


    }
}
