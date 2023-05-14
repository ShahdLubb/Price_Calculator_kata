namespace Price_Calculator_kata
{
    public class DiscountService : IDiscountService
    {
        private List<IDiscountCalculator> Discounts = new List<IDiscountCalculator>();

        public static IDiscountCalculator CreateRelativeDiscount(double discountPercentage)
        {
            return new RelativeDiscountCalculator(discountPercentage);
        }
        public void ApplyDiscountForAllProducts(IDiscountCalculator Discount)
        {
            Discounts.Add(Discount);

        }
        public List<IDiscountCalculator> GetDiscounts()
        {
            return Discounts;
        }
    }
}
