namespace FinalProject
{
    public class BankAndAccountManager
    {
        // Attributes/Member Variables
        private List<Customer> _customerList = new List<Customer>();
        private List<BaseAccount> _accountsList = new List<BaseAccount>();
        private DatabaseRepositoryAndHelper _databaseHelperReference;
        private int _nextAccountNumber;


        // Constructor 
        public BankAndAccountManager()
        {
            _databaseHelperReference = new DatabaseRepositoryAndHelper();

            int maxAccountNumber = _databaseHelperReference.GetMaxAccountNumber();

            if (maxAccountNumber > 0)
            {
                _nextAccountNumber = maxAccountNumber + 1;
            }
            else
            {
                _nextAccountNumber = 10000000;
            }
        }


        // Methods
        public int CreateCustomer(string firstName, string lastName, string email, string phone, string address)
        {
            Customer newCustomer = new Customer();
            newCustomer.FirstName = firstName;
            newCustomer.LastName = lastName;
            newCustomer.Email = email;
            newCustomer.PhoneNumber = phone;
            newCustomer.Address = address;

            int customerId = _databaseHelperReference.SaveCustomer(newCustomer);
            if (customerId > 0)
            {
                newCustomer.CustomerID = customerId;
                _customerList.Add(newCustomer);
            }

            return customerId;
        }

        public Customer GetCustomerByID(int customerId)
        {
            return _databaseHelperReference.LoadCustomerByID(customerId);
        }

        public List<Customer> SearchCustomersByName(string searchTerm)
        {
            // I need to make sure I implement this in the DatabaseHelper
            return new List<Customer>();
        }

        public int CreateAccount(int customerId, string accountType, decimal initialDeposit)
        {
            BaseAccount newAccount = null;

            if (accountType == "Savings")
            {
                newAccount = new SavingsAccount();
            }
            else if (accountType == "Checking")
            {
                newAccount = new CheckingAccount();
            }
            else if (accountType == "Business")
            {
                newAccount = new BusinessAccount();
            }

            if (newAccount != null)
            {
                newAccount.CustomerID = customerId;
                newAccount.AccountNumber = GenerateAccountNumber();
                newAccount.Balance = initialDeposit;

                int accountId = _databaseHelperReference.SaveAccount(newAccount);
                if (accountId > 0)
                {
                    newAccount.AccountID = accountId;
                    _accountsList.Add(newAccount);
                    return accountId;
                }
            }
            return -1;
        }

        public BaseAccount GetAccountByID(int accountId)
        {
            return _databaseHelperReference.LoadAccountByID(accountId);
        }

        public BaseAccount GetAccountByAccountNumber(string accountNumber)
        {
            foreach (var account in _accountsList)
            {
                if (account.AccountNumber == accountNumber)
                {
                    return account;
                }
            }
            return null;
        }

        public List<BaseAccount> GetAllAccountsForCustomer(int customerId)
        {
            return _databaseHelperReference.LoadAllAccountsForCustomer(customerId);
        }

        public bool ProcessTransaction(int accountId, decimal amount, TransactionType type, string description)
        {
            BaseAccount account = GetAccountByID(accountId);
            if (account == null)
            {
                return false;
            }

            try
            {
                if (type == TransactionType.Deposit)
                {
                    account.Deposit(amount);
                }
                else if (type == TransactionType.Withdrawal)
                {
                    if (!account.Withdraw(amount))
                    {
                        return false;
                    }
                }

                Transaction transaction = new Transaction();
                transaction.AccountID = accountId;
                transaction.Type = type;
                transaction.Amount = amount;
                transaction.Description = description;
                transaction.BalanceAfter = account.Balance;

                _databaseHelperReference.SaveTransaction(transaction);
                _databaseHelperReference.UpdateAccountBalance(accountId, account.Balance);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool TransferBetweenAccounts(int fromAccountId, int toAccountId, decimal amount)
        {
            BaseAccount fromAccount = GetAccountByID(fromAccountId);
            BaseAccount toAccount = GetAccountByID(toAccountId);

            if (fromAccount == null || toAccount == null)
            {
                return false;
            }

            if (!fromAccount.CanWithdraw(amount))
            {
                return false;
            }

            try
            {
                fromAccount.Withdraw(amount);
                toAccount.Deposit(amount);

                // Transaction for FROM account (withdrawal account)
                Transaction fromTransaction = new Transaction();
                fromTransaction.AccountID = fromAccountId;
                fromTransaction.Type = TransactionType.Transfer;
                fromTransaction.Amount = amount;
                fromTransaction.Description = $"Transfer to Account {toAccount.AccountNumber}";
                fromTransaction.BalanceAfter = fromAccount.Balance;

                int fromTransId = _databaseHelperReference.SaveTransaction(fromTransaction);

                // Transaction for To account (deposit account)
                Transaction toTransaction = new Transaction();
                toTransaction.AccountID = toAccountId;
                toTransaction.Type = TransactionType.Transfer;
                toTransaction.Amount = amount;
                toTransaction.Description = $"Transfer from Account {fromAccount.AccountNumber}";
                toTransaction.BalanceAfter = toAccount.Balance;
                toTransaction.RelatedTransactionID = fromTransId;

                _databaseHelperReference.SaveTransaction(toTransaction);

                // Update balances
                _databaseHelperReference.UpdateAccountBalance(fromAccountId, fromAccount.Balance);
                _databaseHelperReference.UpdateAccountBalance(toAccountId, toAccount.Balance);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public void CloseAccount(int accountId)
        {
            BaseAccount account = GetAccountByID(accountId);
            if (account != null)
            {
                account.CloseAccount();
                _databaseHelperReference.UpdateAccountStatus(accountId, AccountStatus.Closed);
            }
        }

        public void GenerateStatementForAccount(int accountId, DateTime startDate, DateTime endDate)
        {
            BaseAccount account = GetAccountByID(accountId);
            if (account == null)
            {
                Console.WriteLine("Account not found.");
                return;
            }

            List<Transaction> transactions = _databaseHelperReference.LoadTransactionsByDateRange(accountId, startDate, endDate);

            Statement statement = new Statement();
            statement.GenerateStatement(account, transactions, startDate, endDate);
            statement.DisplayStatement();
        }

        public void ApplyInterestToAllAccounts()
        {
            foreach (var account in _accountsList)
            {
                if (account.Status == AccountStatus.Active)
                {
                    account.ApplyInterest();
                    _databaseHelperReference.UpdateAccountBalance(account.AccountID, account.Balance);
                }
            }
        }

        public void ChargeMonthlyFeesToAllAccounts()
        {
            foreach (var account in _accountsList)
            {
                if (account is CheckingAccount checking)
                {
                    checking.ChargeMaintenanceFee();
                    _databaseHelperReference.UpdateAccountBalance(account.AccountID, account.Balance);
                }
            }
        }

        public decimal GetTotalSystemBalance()
        {
            decimal total = 0m;
            foreach (var account in _accountsList)
            {
                total += account.Balance;
            }
            return total;
        }

        public void FindAccountsBelowMinimumBalance()
        {

        }

        public string GenerateAccountNumber()
        {
            _nextAccountNumber++;
            return _nextAccountNumber.ToString();
        }
    }
}