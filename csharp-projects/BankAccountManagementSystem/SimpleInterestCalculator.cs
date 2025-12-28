using System;

namespace BankAccountManagementSystem
{
    public class SimpleInterestCalculator : InterestCalculator
    {
        // Methods
        public decimal CalculateInterest(decimal balance, decimal rate, DateTime timePeriod)
        {
            int monthsElapsed = ((DateTime.Now.Year - timePeriod.Year) * 12) + (DateTime.Now.Month - timePeriod.Month);

            if (monthsElapsed <= 0)
                monthsElapsed = 1;

            return balance * (rate / 100) * (monthsElapsed / 12m);
        }
    }
}