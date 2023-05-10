namespace Price_Calculator_kata
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var products = new ProductRepository();

            var MyDiscountService = new DiscountService();

            TaxServices MyTaxService = new TaxServices();
            PriceCalculator MyPriceCalculator = new PriceCalculator(MyTaxService, MyDiscountService);

            Product Book = new Product("The Little Prince", 12345, 20.25);
            products.Add(Book);
            Console.WriteLine(Book.ToString());

            //create a Discount 
            var Discount = DiscountService.CreateRelativeDiscount(15.0);
            MyDiscountService.ApplyDiscountForAllProducts(Discount);

            Console.WriteLine($"Taxed Price with default Tax of 20% :{MyPriceCalculator.CalculateTotalPrice(Book)}");

        }
    }
}
