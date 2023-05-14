namespace Price_Calculator_kata
{
    public interface IDiscountService
    {
        void ApplyDiscountForAllProducts(IDiscountCalculator Discount);
        List<IDiscountCalculator> GetDiscounts();
    }
}