using System;
using System.Data.Common;

namespace FinalProject
{
    public class CheckingAccount : BaseAccount
    {
        // Attributes/Member Variables
        private decimal _overdraftLimit;
        private decimal _overdraftFee;
        private bool _hasOverdraftProtection;
        private decimal _monthlyMaintenanceFee;

        // Public properties for database access
        public decimal OverdraftLimit 
        { 
            get { return _overdraftLimit; } 
            set { _overdraftLimit = value; } 
        }

        public decimal OverdraftFee 
        { 
            get { return _overdraftFee; } 
            set { _overdraftFee = value; } 
        }

        public bool HasOverdraftProtection 
        { 
            get { return _hasOverdraftProtection; } 
            set { _hasOverdraftProtection = value; } 
        }

        public decimal MonthlyFee 
        { 
            get { return _monthlyMaintenanceFee; } 
            set { _monthlyMaintenanceFee = value; } 
        }


        // Constructor
        public CheckingAccount()
        {
            _overdraftLimit = 50.00m;
            _overdraftFee = 35.00m;
            _hasOverdraftProtection = true;
            _monthlyMaintenanceFee = 10.00m;
            _interestRate = 0.1m;
            _minimumBalance = 25.00m;
        }


        // Methods
        public override bool Withdraw(decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Withdrawal amount must be positive");
            }

            if (_accountStatus != AccountStatus.Active)
            {
                throw new InvalidOperationException("Cannot withdraw from inactive account");
            }

            if (_hasOverdraftProtection)
            {
                decimal availableBalance = _balance + _overdraftLimit;
                if (amount <= availableBalance)
                {
                    _balance -= amount;

                    if (_balance < 0)
                    {
                        ChargeOverdraftFee();
                    }
                    return true;
                }
                return false;
            }
            else
            {
                return base.Withdraw(amount);
            }
        }

        public override decimal CalculateInterest()
        {
            return _balance * (_interestRate / 100) / 12;
        }

        public void ChargeOverdraftFee()
        {
            _balance -= _overdraftFee;
            Console.WriteLine($"Overdraft fee of ${_overdraftFee:N2} charged");
        }

        public void ChargeMaintenanceFee()
        {
            _balance -= _monthlyMaintenanceFee;
            Console.WriteLine($"Monthly maintenance fee of ${_monthlyMaintenanceFee:N2} charged");
        }

        public decimal GetAvailableBalance()
        {
            if (_hasOverdraftProtection)
            {
                return _balance + _overdraftLimit;
            }
            return _balance;
        }

        public override void DisplayAccountInfo()
        {
            base.DisplayAccountInfo();
            Console.WriteLine($"Overdraft Protection: {(_hasOverdraftProtection ? "Yes" : "No")}");
            if (_hasOverdraftProtection)
            {
                Console.WriteLine($"Overdraft Limit: ${_overdraftLimit:N2}");
                Console.WriteLine($"Available Balance: ${GetAvailableBalance():N2}");
            }
            Console.WriteLine($"Monthly Fee: ${_monthlyMaintenanceFee:N2}");
        }
    }
}