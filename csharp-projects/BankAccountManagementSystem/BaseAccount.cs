using System;

namespace BankAccountManagementSystem
{
    public abstract class BaseAccount
    {
        // Attributes/Member Variables
        protected int _accountID;
        protected string _accountNumber;
        protected int _customerID;
        protected decimal _balance;
        protected DateTime _dateOpened;
        protected AccountStatus _accountStatus;
        protected decimal _interestRate;
        protected decimal _minimumBalance;

        // public properties for database access

        public int AccountID 
            { 
                get { return _accountID; } 
                set { _accountID = value; } 
            }

            public string AccountNumber 
            { 
                get { return _accountNumber; } 
                set { _accountNumber = value; } 
            }

            public int CustomerID 
            { 
                get { return _customerID; } 
                set { _customerID = value; } 
            }

            public decimal Balance 
            { 
                get { return _balance; } 
                set { _balance = value; } 
            }

            public DateTime DateOpened 
            { 
                get { return _dateOpened; } 
                set { _dateOpened = value; } 
            }

            public AccountStatus Status 
            { 
                get { return _accountStatus; } 
                set { _accountStatus = value; } 
            }

            public decimal InterestRate 
            { 
                get { return _interestRate; } 
                set { _interestRate = value; } 
            }

            public decimal MinimumBalance 
            { 
                get { return _minimumBalance; } 
                set { _minimumBalance = value; } 
            }

        // Constructor
        public BaseAccount()
        {
            _dateOpened = DateTime.Now;
            _accountStatus = AccountStatus.Active;
            _balance = 0m;
        }


        // Methods
        public virtual void Deposit(decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Deposit amount must be positive");
            }

            if (_accountStatus != AccountStatus.Active)
            {
                throw new InvalidOperationException("Cannot deposit to inactive account");
            }

            _balance += amount;
        }

        public virtual bool Withdraw(decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Withdrawal amount must be positive");
            }

            if (_accountStatus != AccountStatus.Active)
            {
                throw new InvalidOperationException("Cannot withdraw from inactive account");
            }

            if (!CanWithdraw(amount))
            {
                return false;
            }

            _balance -= amount;
            return true;
        }

        public abstract decimal CalculateInterest();

        public void ApplyInterest()
        {
            decimal interest = CalculateInterest();
            _balance += interest;
        }

        public decimal GetBalance()
        {
            return _balance;
        }

        public virtual bool CanWithdraw(decimal amount)
        {
            return _balance >= amount;
        }

        public void FreezeAccount()
        {
            _accountStatus = AccountStatus.Frozen;
        }

        public void CloseAccount()
        {
            if (_balance!= 0)
            {
                throw new InvalidOperationException("Cannot close account with non-zero balance");
            }
            _accountStatus = AccountStatus.Closed;
        }

        public virtual void DisplayAccountInfo()
        {
            Console.WriteLine($"\n=== Account Information ===");
            Console.WriteLine($"Account Number: {_accountNumber}");
            Console.WriteLine($"Account Type: {GetType().Name}");
            Console.WriteLine($"Balance: ${_balance:N2}");
            Console.WriteLine($"Status: {_accountStatus}");
            Console.WriteLine($"Interest Rate: {_interestRate}%");
            Console.WriteLine($"Minimum Balance: ${_minimumBalance:N2}");
            Console.WriteLine($"Date Opened: {_dateOpened:MM/dd/yyyy}");
        }
    }
}