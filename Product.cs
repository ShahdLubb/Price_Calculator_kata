using System;
using System.Text;

namespace Price_Calculator_kata
{
    public class Product
    {
        private double _Price;
        public ITaxCalculator? TaxCalculator;
        public Currency currency;
        public string Name { get; set; }
        public int UPC { get; set; }
        public Product(string Name, int UPC, double Price, Currency currency)
        {
            this.Name = Name;
            this.UPC = UPC;
            this.Price = Price;
            this.currency= currency;
            TaxServices.ApplyFlatRateTax(this);
        }
        
        public double Price {
            get => _Price;
            set {
                if (value > 0)
                   _Price = Math.Round(value, 2); } 
        }
        public override string ToString()
        {
            return $"- Product Name:{this.Name}   Product UPC:{this.UPC}   Product Price:{this.Price} {currency.ToString()}";
        }

    }
 
}