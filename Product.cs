using System;
using System.Text;

namespace Price_Calculator_kata
{
    public class Product
    {
        private double _Price;
        public ITaxCalculator? TaxCalculator;
        public List<IDiscountCalculator> Discounts = new List<IDiscountCalculator>();
        public string Name { get; set; }
        public int UPC { get; set; }
        public Product(string Name, int UPC, double Price)
        {
            this.Name = Name;
            this.UPC = UPC;
            this.Price = Price;
            TaxServices.ApplyFlatRateTax(this);
        }
        
        public double Price {
            get => _Price;
            set {
                if (value > 0)
                   _Price = Math.Round(value, 2); } 
        }

        public double CalculateTotalPrice()
        {   if (TaxCalculator is null) throw new TaxNotAppliedException();
            double TaxAmount= TaxCalculator.CalculateTaxAmount(this.Price);
            double DiscountAmount = 0.0;
            foreach( IDiscountCalculator Discount in Discounts){
                DiscountAmount += Discount.CalculateDiscountAmount(this);
            }
            return Math.Round(Price+ TaxAmount- DiscountAmount, 2);
        }
        public string ReportPriceDetails()
        {
            StringBuilder report = new StringBuilder();
            report.AppendLine(this.ToString());
            if (TaxCalculator is null) throw new TaxNotAppliedException();
            report.Append(TaxCalculator.ToString());
            double TaxAmount = TaxCalculator.CalculateTaxAmount(this.Price);
            double DiscountAmount = 0.0;
            foreach (IDiscountCalculator Discount in Discounts)
            {
                DiscountAmount += Discount.CalculateDiscountAmount(this);
                report.Append(Discount.ToString()+ ",");
            }
            double TotalPrice = Math.Round(Price + TaxAmount - DiscountAmount, 2);
            report.AppendLine($"\nPrice=${TotalPrice}");
            report.AppendLine($"Discount Amount=${Math.Round(DiscountAmount,2)}");
            return report.ToString();
        }

        public override string ToString()
        {
            return $"- Product Name:{this.Name}   Product UPC:{this.UPC}   Product Name:{this.Price}";
        }

    }
 
}