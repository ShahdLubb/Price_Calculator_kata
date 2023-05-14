namespace Price_Calculator_kata
{
    public class TaxServices : ITaxServices
    {
        public ITaxCalculator getFlatRateTaxCalculator()
        {
            return new FlatRateTaxCalculator();
        }


    }
}
