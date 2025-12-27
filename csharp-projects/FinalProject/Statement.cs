using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace FinalProject
{
    public class Statement
    {
        // Attributes/Member Variables
        private int _accountID;
        private string _accountNumber;
        private string _accountType;
        private DateTime _statementPeriodStartDate;
        private DateTime _statementPeriodEndDate;
        private decimal _openingBalance;
        private decimal _closingBalance;
        private List<Transaction> _transactionList = new List<Transaction>();
        private decimal _totalDeposits;
        private decimal _totalWithdrawals;
        private decimal _totalFees;
        private decimal _interestEarned;


        // Constructor
        public Statement()
        {

        }


        // Methods
        public void GenerateStatement(BaseAccount account, List<Transaction> transactions, DateTime startDate, DateTime endDate)
        {
            _accountID = account.AccountID;
            _accountNumber = account.AccountNumber;
            _accountType = account.GetType().Name;
            _statementPeriodStartDate = startDate;
            _statementPeriodEndDate = endDate;
            _transactionList = transactions;
            _closingBalance = account.Balance;

            // Calculate opening balance by working backwards from closing balance
            _openingBalance = _closingBalance;
            foreach (var trans in transactions)
            {
                if (trans.Type == TransactionType.Deposit || trans.Type == TransactionType.Interest)
                {
                    _openingBalance -= trans.Amount;
                }
                else if (trans.Type == TransactionType.Transfer && trans.Description.Contains("from"))
                {
                    _openingBalance -= trans.Amount;
                }
                else
                {
                    _openingBalance += trans.Amount;
                }
            }
            CalculateSummaryTotals();
        }

        public void CalculateSummaryTotals()
        {
            _totalDeposits = 0m;
            _totalWithdrawals = 0m;
            _totalFees = 0m;
            _interestEarned = 0m;

            foreach (var transaction in _transactionList)
            {
                if (transaction.Type == TransactionType.Deposit)
                {
                    _totalDeposits += transaction.Amount;
                }
                else if (transaction.Type == TransactionType.Withdrawal || transaction.Type == TransactionType.Transfer)
                {
                    _totalWithdrawals += transaction.Amount;
                }
                else if (transaction.Type == TransactionType.Fee)
                {
                    _totalFees += transaction.Amount;
                }
                else if (transaction.Type == TransactionType.Interest)
                {
                    _interestEarned += transaction.Amount;
                }
            }
        }

        public void DisplayStatement()
        {
            Console.WriteLine("\n╔═══════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                       ACCOUNT STATEMENT                           ║");
            Console.WriteLine("╚═══════════════════════════════════════════════════════════════════╝");
            Console.WriteLine($"\nAccount Number: {_accountNumber}");
            Console.WriteLine($"Account Type: {_accountType}");
            Console.WriteLine($"Statement Period: {_statementPeriodStartDate:MM/dd/yyyy} - {_statementPeriodEndDate:MM/dd/yyyy}");
            Console.WriteLine($"\nOpening Balance: ${_openingBalance:N2}");

            Console.WriteLine("\n─────────────────────────────────────────────────────────────────────");
            Console.WriteLine("                         TRANSACTIONS");
            Console.WriteLine("─────────────────────────────────────────────────────────────────────");

            if (_transactionList.Count == 0)
            {
                Console.WriteLine("No transactions during this period.");
            }
            else
            {
                Console.WriteLine($"{"Date",-12} {"Type",-12} {"Amount",12} {"Balance",15} {"Description",-25}");
                Console.WriteLine(new string('─', 80));

                foreach (var transaction in _transactionList.OrderBy(t => t.TransactionDate))
                {
                    string sign;
                    if (transaction.Type == TransactionType.Transfer)
                    {
                        sign = transaction.Description.Contains("from") ? "+" : "-";
                    }
                    else
                    {
                        sign = (transaction.Type == TransactionType.Deposit || transaction.Type == TransactionType.Interest) ? "+" : "-";
                    }

                    Console.WriteLine($"{transaction.TransactionDate:MM/dd/yyyy,-12} {transaction.Type,-12} {sign}${transaction.Amount,10:N2} ${transaction.BalanceAfter,13:N2} {transaction.Description,-20}");
                }
            }

            Console.WriteLine("\n─────────────────────────────────────────────────────────────────────");
            Console.WriteLine("                          SUMMARY");
            Console.WriteLine("─────────────────────────────────────────────────────────────────────");
            Console.WriteLine($"Total Deposits:        ${_totalDeposits,10:N2}");
            Console.WriteLine($"Total Withdrawals:     ${_totalWithdrawals,10:N2}");
            Console.WriteLine($"Total Fees:            ${_totalFees,10:N2}");
            Console.WriteLine($"Interest Earned:       ${_interestEarned,10:N2}");
            Console.WriteLine($"\nClosing Balance:       ${_closingBalance,10:N2}");
            Console.WriteLine("═════════════════════════════════════════════════════════════════════\n");
        }

        public void ExportToFile()
        {
            Console.WriteLine("Export to file feature not yet implemented.");
        }

        public List<Transaction> FilterTransactionsByType(TransactionType type)
        {
            return _transactionList.Where(t => t.Type == type).ToList();
        }

        public List<Transaction> GetTransactionsForPeriod()
        {
            return _transactionList;
        }
    }
}