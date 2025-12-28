using System;

namespace BankAccountManagementSystem
{
    public class SavingsAccount : BaseAccount
    {
        // Attributes/Member Variables
        private int _maximumWithdrawalsPerMonth;
        private int _currentWithdrawalCount;
        private DateTime _lastWithdrawalResetDate;


        // Constructor
        public SavingsAccount()
        {
            _maximumWithdrawalsPerMonth = 6;
            _currentWithdrawalCount = 0;
            _lastWithdrawalResetDate = DateTime.Now;
            _interestRate = 2.5m;
            _minimumBalance = 100.00m;
        }


        // Methods
        public override bool Withdraw(decimal amount)
        {
            // Check if we need to reset the monthly withdrawal count
            if (DateTime.Now.Month != _lastWithdrawalResetDate.Month ||
            DateTime.Now.Year != _lastWithdrawalResetDate.Year)
            {
                ResetMonthlyWithdrawal();
            }

            // Check withdrawal limit
            if (CheckIfWithdrawalLimitReached())
            {
                Console.WriteLine("Monthly withdrawal limit reached for savings account");
                return false;
            }

            // Attempt withdrawal
            bool success = base.Withdraw(amount);

            if (success)
            {
                _currentWithdrawalCount++;
            }

            return success;
        }

        public override decimal CalculateInterest()
        {
            // Use higher interest rate from Savings Account
            return _balance * (_interestRate / 100) / 12;
        }

        public void ResetMonthlyWithdrawal()
        {
            _currentWithdrawalCount = 0;
            _lastWithdrawalResetDate = DateTime.Now;
        }

        public bool CheckIfWithdrawalLimitReached()
        {
            return _currentWithdrawalCount >= _maximumWithdrawalsPerMonth;
        }

        public override void DisplayAccountInfo()
        {
            base.DisplayAccountInfo();
            Console.WriteLine($"Withdrawals This Month: {_currentWithdrawalCount}/{_maximumWithdrawalsPerMonth}");
        }
    }
}