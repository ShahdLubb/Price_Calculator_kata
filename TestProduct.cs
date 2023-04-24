using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Price_Calculator_kata.PriceCalculator;

namespace Price_Calculator_kata
{
    public class TestProduct
    {
        public static void Main(string[] args)
        {
            Currency dollar = new Currency("USD",1);
            Currency euro = new Currency("EUR", 0.91);
            //Product Repositry
            var products=new ProductRepository();
            //Initialize  Product
            Product Book = new Product("The Little Prince", 12345, 20.25, euro);
            //Product Chair = new Product("Wooden Chair", 789, 20.25);
            products.Add(Book);
            //products.Add(Chair);
            FlatRateTaxCalculator.TaxPercentageDefault = 21;

            CostService MyCostService = new CostService();
            var transCost = CostService.CreateAbsoluteValueCost(2.2,"Transport Cost", euro);
            var packagingCost = CostService.CreatePercentageCost(1, "Packaging Cost");
            MyCostService.AddCost(transCost);
            MyCostService.AddCost(packagingCost);

            DiscountService MyDiscountService = new DiscountService();
            var Discount = DiscountService.CreateRelativeDiscount(15.0);
            var BookDiscount = DiscountService.CreateSelectiveRelativeDiscount(7.0,Book.UPC);

            CapService MycapService = new CapService(4,false,euro);
            ReportGenerator Report = new(MyDiscountService, MyCostService, MycapService, products);
            PriceCalculator calc=new PriceCalculator(MyDiscountService, MyCostService, MycapService, products);
            MyDiscountService.ApplyDiscountForAllProducts(Discount);
            MyDiscountService.ApplyDiscountForAllProducts(BookDiscount);
            Console.WriteLine(calc.CalculateTotalPrice(Book, DiscountCombinationMethod.Additive));
            

        }
    }
}
