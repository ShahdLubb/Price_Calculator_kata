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
            //Product Repositry
            var products=new ProductRepository();
            //Initialize  Product
            Product Book = new Product("The Little Prince", 12345, 20.25);
            Product Chair = new Product("Wooden Chair", 789, 20.25);
            products.Add(Book);
            products.Add(Chair);

            
            var MyDiscountService = new DiscountService();
            var Discount = DiscountService.CreateRelativeDiscount(15.0);
            var ChairsDiscount = DiscountService.CreateSelectiveRelativeDiscount(7.0,Chair.UPC,true);
            ReportGenerator Report = new(MyDiscountService, null, products);
            MyDiscountService.ApplyDiscountForAllProducts(Discount);
            MyDiscountService.ApplyDiscountForAllProducts(ChairsDiscount);


        }
    }
}
