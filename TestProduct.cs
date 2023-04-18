using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Price_Calculator_kata
{
    public class TestProduct
    {
        public static void Main(string[] args)
        {
            Product Book = new Product("The Little Prince", 12345, 20.25);
            ITaxCalculator FlatRateTax=TaxServices.CreateFlatRateTaxCalculator();
            TaxServices.ApplyTax(Book, FlatRateTax);
            Console.WriteLine(Book.ToString());
            Console.WriteLine($"Taxed Price with default Tax of 20% :{Book.CalculateTotalPrice()}");
            TaxServices.ChangeFlateRateTax(21.0);
            Console.WriteLine($"Taxed Price with Tax of 21% :{Book.CalculateTotalPrice()}");

        }
    }
}
