using System;
using System.Text;

namespace Price_Calculator_kata
{
    public class Product
    {
        private Money _Price;
        public ITaxCalculator? TaxCalculator;
        public Currency currency;
        public string Name { get; set; }
        public int UPC { get; set; }
        public Product(string Name, int UPC, double Price, Currency currency)
        {
            this.Name = Name;
            this.UPC = UPC;
            this.Price = new Money(Price);
            this.currency= currency;
            TaxServices.ApplyFlatRateTax(this);
        }
        public Product(string Name, int UPC, Money Price, Currency currency)
        {
            this.Name = Name;
            this.UPC = UPC;
            this.Price = Price;
            this.currency = currency;
            TaxServices.ApplyFlatRateTax(this);
        }
        public Money Price {
            get => _Price;
            set {
                if (value.Value > 0)
                   _Price = value; } 
        }
        public override string ToString()
        {
            return $"- Product Name:{this.Name}   Product UPC:{this.UPC}   Product Price:{this.Price.ToString()} {currency.ToString()}";
        }

    }
 
}