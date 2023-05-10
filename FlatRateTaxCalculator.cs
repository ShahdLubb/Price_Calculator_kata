namespace Price_Calculator_kata
{
    public class FlatRateTaxCalculator : ITaxCalculator
    {

        public double CalculateTaxAmount(double Price)
        {
            double TaxPercentageDefault = PriceCalculatorConfigurations.FlatRateTax;
            double taxAmount = Price * (TaxPercentageDefault / 100.0);
            return taxAmount;
        }

        public override string ToString()
        {
            double TaxPercentageDefault = PriceCalculatorConfigurations.FlatRateTax;
            return $"Flat Rate Tax= %{TaxPercentageDefault}  ";
        }

    }
}
