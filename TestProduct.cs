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
            Product Book = new Product("The Little Prince", 12345, 20.25, new FlatRateTaxCalculator());
            Console.WriteLine(Book.ToString());
            Console.WriteLine($"Taxed Price with default Tax of 20% :{Book.CalculatePriceAfterTax()}");
            Console.WriteLine($"Taxed Price with Tax of 21% :{Book.CalculatePriceAfterTax(21.0)}");

        }
    }
}
