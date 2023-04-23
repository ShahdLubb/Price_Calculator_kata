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
            products.Add(Book);
            
            //Discount Task
            var MyDiscountService = new DiscountService(products);
            var Discount = DiscountService.CreateRelativeDiscount(15.0);
            MyDiscountService.ApplyDiscountForAllProducts(Discount);
            

        }
    }
}
