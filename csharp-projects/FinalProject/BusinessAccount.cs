using System;

namespace FinalProject
{
    public class BusinessAccount : BaseAccount
    {
        // Attributes/Member Variables
        private string _businessName;
        private decimal _transactionFeePerOperation;
        private int _freeTransactionLimit;
        private int _currentTransactionCount;


        // Constructor
        public BusinessAccount()
        {
            _businessName = "";
            _transactionFeePerOperation = 0.50m;
            _freeTransactionLimit = 100;
            _currentTransactionCount = 0;
            _interestRate = 1.5m;
            _minimumBalance = 500.00m;
        }


        // Methods
        public override void Deposit(decimal amount)
        {
            base.Deposit(amount);
            _currentTransactionCount++;

            // Charge fee if over the limit
            if (_currentTransactionCount > _freeTransactionLimit)
            {
                decimal fee = CalculateTransactionFees();
                _balance -= fee;
                Console.WriteLine($"Transaction fee of ${fee:N2} charged");
            }
        }

        public override bool Withdraw(decimal amount)
        {
            bool success = base.Withdraw(amount);

            if (success)
            {
                _currentTransactionCount++;

                // Charge fee if over the limit
                if (_currentTransactionCount > _freeTransactionLimit)
                {
                    decimal fee = CalculateTransactionFees();

                    _balance -= fee;
                    Console.WriteLine($"Transaction fee of ${fee:N2} charged");
                }
            }

            return success;
        }

        public override decimal CalculateInterest()
        {
            return _balance * (_interestRate / 100) / 12;
        }

        public void ResetMonthlyTransactionCount()
        {
            _currentTransactionCount = 0;
        }

        public decimal CalculateTransactionFees()
        {
            if (_currentTransactionCount > _freeTransactionLimit)
            {
                return _transactionFeePerOperation;
            }
            return 0m;
        }

        public override void DisplayAccountInfo()
        {
            base.DisplayAccountInfo();
            Console.WriteLine($"Business Name: {_businessName}");
            Console.WriteLine($"Transactions This Month: {_currentTransactionCount}");
            Console.WriteLine($"Free Transaction Limit: {_freeTransactionLimit}");
            Console.WriteLine($"Transaction Fee: ${_transactionFeePerOperation:N2}");
        }
    }
}