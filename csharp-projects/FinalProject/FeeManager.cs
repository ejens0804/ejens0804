using System;

namespace FinalProject
{
    public class FeeManager
    {
        // Attributes/Member Variables
        private decimal _standardOverdraftFee;
        private decimal _monthlyMaintenanceFee;
        private decimal _transferFee;
        private decimal _belowMinimumBalanceFee;


        // Constructor
        public FeeManager()
        {
            _standardOverdraftFee = 35.00m;
            _monthlyMaintenanceFee = 10.00m;
            _transferFee = 2.00m;
            _belowMinimumBalanceFee = 5.00m;
        }


        // Methods
        public decimal CalculateOverdraftFee(BaseAccount account)
        {
            if (account.Balance < 0)
            {
                return _standardOverdraftFee;
            }
            return 0m;
        }

        public decimal CalculateMaintenanceFee(BaseAccount account)
        {
            if (account.Balance >= 1000.00m)
            {
                return 0m;
            }

            if (account is CheckingAccount checking)
            {
                return checking.MonthlyFee;
            }

            return _monthlyMaintenanceFee;
        }

        public decimal CalculateTransferFee()
        {
            return _transferFee;
        }

        public void ApplyFeeToAccount(BaseAccount account, decimal feeAmount, string description)
        {
            if (feeAmount > 0)
            {
                account.Balance -= feeAmount;
                Console.WriteLine($"Fee of ${feeAmount:N2} applied: {description}");
            }
        }

        public bool CheckIfFeeShouldBeWaived(BaseAccount account)
        {
            return account.Balance >= 1000.00m; // This will wave fees if the balance is over $1000
        }

        public decimal CalculateBelowMinimumFee(BaseAccount account)
        {
            if (account.Balance < account.MinimumBalance)
            {
                return _belowMinimumBalanceFee;
            }
            return 0m;
        }
    }
}