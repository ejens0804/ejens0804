namespace FinalProject
{
    public class CompoundInterestCalculator : InterestCalculator
    {
        // Attributes/Member Variables
        private CompoundingFrequency _compoundingFrequency;


        // Constructors
        public CompoundInterestCalculator()
        {
            _compoundingFrequency = CompoundingFrequency.Monthly;
        }

        public CompoundInterestCalculator(CompoundingFrequency frequency)
        {
            _compoundingFrequency = frequency;
        }


        // Methods
        public decimal CalculateInterest(decimal balance, decimal rate, DateTime timePeriod)
        {
            int compoundsPerYear = GetCoumpoundsPerYear();
            double years = (DateTime.Now - timePeriod).Days / 365.0;

            if (years <= 0)
                years = 1.0 / 12.0;

            double rateDecimal = (double)(rate / 100);
            double amount = (double)balance * Math.Pow(1 + (rateDecimal / compoundsPerYear), compoundsPerYear * years);

            return (decimal)(amount - (double)balance);
        }

        private int GetCoumpoundsPerYear()
        {
            switch (_compoundingFrequency)
            {
                case CompoundingFrequency.Daily:
                    return 365;
                case CompoundingFrequency.Monthly:
                    return 12;
                case CompoundingFrequency.Quarterly:
                    return 4;
                case CompoundingFrequency.Annually:
                    return 1;
                default:
                    return 12;
            }
        }
    }
}