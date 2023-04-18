using System;

namespace Price_Calculator_kata
{
    public class Product
    {

        private double _Price;
        private ITaxCalculator _TaxCalculator;
        public string Name { get; set; }
        public int UPC { get; set; }
        public Product(string Name, int UPC, double Price, ITaxCalculator TaxCalculator) {
            this.Name = Name;
            this.UPC = UPC;
            this.Price = Price;
            this._TaxCalculator = TaxCalculator;
        }
        
        public double Price {
            get => _Price;
            set {
                if (value > 0)
                   _Price = Math.Round(value, 2); } 
        }

        public double CalculatePriceAfterTax()
        {
            return _TaxCalculator.CalculatePriceAfterTax(this.Price);
        }
        public double CalculatePriceAfterTax(double TaxPercentage)
        {
            return _TaxCalculator.CalculatePriceAfterTax(this.Price, TaxPercentage);
        }


        public override string ToString()
        {
            return $"Product Name:{this.Name}\nProduct UPC:{this.UPC}\nProduct Name:{this.Price}";
        }

    }
 
}