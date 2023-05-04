using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Price_Calculator_kata
{
    public class Program
    {
        public static void Main(string[] args)
        {
            TaxServices MyTaxService= new TaxServices();
            PriceCalculator MyPriceCalculator = new PriceCalculator(MyTaxService);
            Product Book = new Product("The Little Prince", 12345, 20.25);
            Console.WriteLine(Book.ToString());
            Console.WriteLine($"Taxed Price with default Tax of 20% :{MyPriceCalculator.CalculateTotalPrice(Book)}");
            PriceCalculatorConfigurations.FlatRateTax = 21;
            Console.WriteLine($"Taxed Price with Tax of 21% :{MyPriceCalculator.CalculateTotalPrice(Book)}");

        }
    }
}
