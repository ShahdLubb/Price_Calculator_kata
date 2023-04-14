using System;

namespace Price_Calculator_kata
{
    public class Product
    {
        public static double TaxPercentageDefault = 20.0;
        public string Name { get; set; }
        public int UPC { get; set; }
        private double _Price;
        public Product(string Name, int UPC, double Price) {
            this.Name = Name;
            this.UPC = UPC;
            this.Price = Price;
        }
        
        public double Price {
            get => _Price; 
            set => _Price = Math.Round(value,2); 
        }
        
        public double CalculateTax()
        {
            double taxedPrice= Price * (1.0 + TaxPercentageDefault / 100.0);
            return Math.Round(taxedPrice,2);
        }
        public double CalculateTax(double TaxPercentage)
        {
            double taxedPrice = Price * (1.0 + TaxPercentage / 100.0);
            return Math.Round(taxedPrice, 2);
        }

    }
 
}