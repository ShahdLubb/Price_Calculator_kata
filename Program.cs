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

            //Product Repositry
            var products=new ProductRepository();
            //Initialize  Product
=======
            TaxServices MyTaxService= new TaxServices();
            PriceCalculator MyPriceCalculator = new PriceCalculator(MyTaxService);
>>>>>>> 1093ff0c3d374856a59a55d48cfb2415375798e0:Program.cs
            Product Book = new Product("The Little Prince", 12345, 20.25);
            products.Add(Book);
            Console.WriteLine(Book.ToString());


            //Discount Task
            var MyDiscountService = new DiscountService(products);
            var Discount = DiscountService.CreateRelativeDiscount(15.0);
            MyDiscountService.ApplyDiscountForAllProducts(Discount);
            Console.WriteLine($"Taxed and discount Price with Tax of %20 and discount of %15: ${Book.CalculateTotalPrice()}");

=======
            Console.WriteLine($"Taxed Price with default Tax of 20% :{MyPriceCalculator.CalculateTotalPrice(Book)}");
            PriceCalculatorConfigurations.FlatRateTax = 21;
            Console.WriteLine($"Taxed Price with Tax of 21% :{MyPriceCalculator.CalculateTotalPrice(Book)}");
>>>>>>> 1093ff0c3d374856a59a55d48cfb2415375798e0:Program.cs
        
        }
    }
}
