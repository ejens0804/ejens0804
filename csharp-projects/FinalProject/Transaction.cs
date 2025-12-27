using System;

namespace FinalProject
{
    public class Transaction // Parent Class
    {
        // Attributes/Member Variables
        protected int _transactionID;
        protected int _accountID;
        protected TransactionType _transactionType;
        protected decimal _amount;
        protected DateTime _transactionDate;
        protected string _description;
        protected decimal _balanceAfterTransaction;
        protected int? _relatedTransactionID;

        // Public properties for database access
        public int TransactionID 
        { 
            get { return _transactionID; } 
            set { _transactionID = value; } 
        }
        public int AccountID 
        { 
            get { return _accountID; } 
            set { _accountID = value; } 
        }
        public TransactionType Type 
        { 
            get { return _transactionType; } 
            set { _transactionType = value; } 
        }
        public decimal Amount 
        { 
            get { return _amount; } 
            set { _amount = value; } 
        }
        public DateTime TransactionDate 
        { 
            get { return _transactionDate; } 
            set { _transactionDate = value; } 
        }
        public string Description 
        { 
            get { return _description; } 
            set { _description = value; } 
        }
        public decimal BalanceAfter 
        { 
            get { return _balanceAfterTransaction; } 
            set { _balanceAfterTransaction = value; } 
        }
        public int? RelatedTransactionID 
        { 
            get { return _relatedTransactionID; } 
            set { _relatedTransactionID = value; } 
        }


        // Constructor
        public Transaction()
        {
            _transactionDate = DateTime.Now;
        }


        // Methods
        public void DisplayTransactionDetails()
        {
            Console.WriteLine($"{_transactionDate:MM/dd/yyyy HH:mm} | {_transactionType,-12} | ${_amount,10:N2} | Balance: ${_balanceAfterTransaction:N2}");
            if (!string.IsNullOrEmpty(_description))
            {
                Console.WriteLine($"  Description: {_description}");
            }
        }

        public string FormatTransactionForStatement()
        {
            string sign = (_transactionType == TransactionType.Deposit || _transactionType == TransactionType.Interest) ? "+" : "-";
            return $"{_transactionDate:MM/dd/yyyy} | {_transactionType,-12} | {sign}${Math.Abs(_amount),10:N2} | ${_balanceAfterTransaction,10:N2} | {_description}";
        }

        public bool IsSameDayAs(DateTime day)
        {
            return _transactionDate.Date == day.Date;
        }

        public string GetFormattedAmount()
        {
            string sign = (_transactionType == TransactionType.Deposit || _transactionType == TransactionType.Interest) ? "+" : "-";
            return $"{sign}${Math.Abs(_amount):N2}";
        }
    }
}