
namespace Price_Calculator_kata
{
    public interface ITaxCalculator
    {
        public double CalculatePriceAfterTax(double Price);
        public double CalculatePriceAfterTax(double Price, double TaxPercentage);
    }
}
