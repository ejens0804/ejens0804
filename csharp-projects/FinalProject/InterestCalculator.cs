namespace FinalProject
{
    public interface InterestCalculator
    {
        // Interface Methods
        public abstract decimal CalculateInterest(decimal balance, decimal rate, DateTime timePeriod);
    }
}