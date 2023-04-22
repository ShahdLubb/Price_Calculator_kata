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
            //Initialize  Product
            Product Book = new Product("The Little Prince", 12345, 20.25);
            Console.WriteLine(Book.ToString());

            //Tax Task
            Console.WriteLine($"Taxed Price with default Tax of 20% : ${Book.CalculateTotalPrice()}");
            FlatRateTaxCalculator.TaxPercentageDefault = 21.0;
            Console.WriteLine($"Taxed Price with Tax of 21% : ${Book.CalculateTotalPrice()}");
            FlatRateTaxCalculator.TaxPercentageDefault = 20.0;


            //Discount Task
            var Discount = DiscountService.CreateRelativeDiscount(15.0);
            DiscountService.ApplyDiscount(Book, Discount);
            Console.WriteLine($"Taxed Price with default Tax of 20% and relative discount of 15% : ${Book.CalculateTotalPrice()}");

        }
    }
}
