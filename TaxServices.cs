namespace Price_Calculator_kata
{
    public class TaxServices
    {
        public ITaxCalculator getFlatRateTaxCalculator()
        {
            return new FlatRateTaxCalculator();
        }


    }
}
