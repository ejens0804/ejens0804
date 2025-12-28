using System;
using System.Collections.Generic;
using MySqlConnector;

namespace BankAccountManagementSystem
{
    public class DatabaseRepositoryAndHelper
    {
        // Attributes/Member Variables
        private string _connectionString;

        // Constructor
        public DatabaseRepositoryAndHelper()
        {
            _connectionString = DatabaseConfig.ConnectionString;
        }

        // Methods
        public MySqlDataReader ExecuteQuery(string query, MySqlParameter[] parameters = null)
        {
            var conn = new MySqlConnection(_connectionString);
            var cmd = new MySqlCommand(query, conn);

            if (parameters != null)
            {
                cmd.Parameters.AddRange(parameters);
            }

            conn.Open();
            return cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
        }

        public int ExecuteNonQuery(string query, MySqlParameter[] parameters = null)
        {
            try
            {
                using (var conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        if (parameters != null)
                        {
                            cmd.Parameters.AddRange(parameters);
                        }
                        return cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error executing non-query: {ex.Message}");
                return -1;
            }
        }

        public object ExecuteScalar(string query, MySqlParameter[] parameters = null)
        {
            try
            {
                using (var conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        if (parameters != null)
                        {
                            cmd.Parameters.AddRange(parameters);
                        }
                        return cmd.ExecuteScalar();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error executing scalar: {ex.Message}");
                return null;
            }
        }

        public int SaveCustomer(Customer customer)
        {
            string query = @"INSERT INTO Customers 
                           (FirstName, LastName, Email, Phone, Address, DateCreated, IsActive) 
                           VALUES (@firstName, @lastName, @email, @phone, @address, @dateCreated, @isActive);
                           SELECT LAST_INSERT_ID();";

            MySqlParameter[] parameters = {
                new MySqlParameter("@firstName", customer.FirstName),
                new MySqlParameter("@lastName", customer.LastName),
                new MySqlParameter("@email", customer.Email ?? ""),
                new MySqlParameter("@phone", customer.PhoneNumber ?? ""),
                new MySqlParameter("@address", customer.Address ?? ""),
                new MySqlParameter("@dateCreated", customer.DateCreated),
                new MySqlParameter("@isActive", true)
            };

            object result = ExecuteScalar(query, parameters);
            return result != null ? Convert.ToInt32(result) : -1;
        }

        public Customer LoadCustomerByID(int customerId)
        {
            string query = "SELECT * FROM Customers WHERE CustomerID = @customerId AND IsActive = TRUE";

            MySqlParameter[] parameters = {
                new MySqlParameter("@customerId", customerId)
            };

            try
            {
                using (var reader = ExecuteQuery(query, parameters))
                {
                    if (reader.Read())
                    {
                        return new Customer
                        {
                            CustomerID = reader.GetInt32("CustomerID"),
                            FirstName = reader.GetString("FirstName"),
                            LastName = reader.GetString("LastName"),
                            Email = reader.GetString("Email"),
                            PhoneNumber = reader.GetString("Phone"),
                            Address = reader.GetString("Address"),
                            DateCreated = reader.GetDateTime("DateCreated")
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading customer: {ex.Message}");
            }

            return null;
        }

        public void UpdateCustomer(Customer customer)
        {
            string query = @"UPDATE Customers 
                           SET FirstName = @firstName, LastName = @lastName, 
                               Email = @email, Phone = @phone, Address = @address
                           WHERE CustomerID = @customerId";

            MySqlParameter[] parameters = {
                new MySqlParameter("@firstName", customer.FirstName),
                new MySqlParameter("@lastName", customer.LastName),
                new MySqlParameter("@email", customer.Email ?? ""),
                new MySqlParameter("@phone", customer.PhoneNumber ?? ""),
                new MySqlParameter("@address", customer.Address ?? ""),
                new MySqlParameter("@customerId", customer.CustomerID)
            };

            ExecuteNonQuery(query, parameters);
        }

        public int SaveAccount(BaseAccount account)
        {
            string query = @"INSERT INTO Accounts 
                           (CustomerID, AccountNumber, AccountType, Balance, InterestRate, 
                            MinimumBalance, DateOpened, AccountStatus, OverdraftLimit, 
                            HasOverdraftProtection, MonthlyFee) 
                           VALUES (@customerId, @accountNumber, @accountType, @balance, @interestRate,
                                   @minimumBalance, @dateOpened, @status, @overdraftLimit,
                                   @hasOverdraft, @monthlyFee);
                           SELECT LAST_INSERT_ID();";

            MySqlParameter[] parameters = {
                new MySqlParameter("@customerId", account.CustomerID),
                new MySqlParameter("@accountNumber", account.AccountNumber),
                new MySqlParameter("@accountType", account.GetType().Name),
                new MySqlParameter("@balance", account.Balance),
                new MySqlParameter("@interestRate", account.InterestRate),
                new MySqlParameter("@minimumBalance", account.MinimumBalance),
                new MySqlParameter("@dateOpened", account.DateOpened),
                new MySqlParameter("@status", account.Status.ToString()),
                new MySqlParameter("@overdraftLimit", account is CheckingAccount checking ? checking.OverdraftLimit : 0),
                new MySqlParameter("@hasOverdraft", account is CheckingAccount check ? check.HasOverdraftProtection : false),
                new MySqlParameter("@monthlyFee", account is CheckingAccount chk ? chk.MonthlyFee : 0)
            };

            object result = ExecuteScalar(query, parameters);
            return result != null ? Convert.ToInt32(result) : -1;
        }

        public BaseAccount LoadAccountByID(int accountId)
        {
            string query = "SELECT * FROM Accounts WHERE AccountID = @accountId";

            MySqlParameter[] parameters = {
                new MySqlParameter("@accountId", accountId)
            };

            try
            {
                using (var reader = ExecuteQuery(query, parameters))
                {
                    if (reader.Read())
                    {
                        string accountType = reader.GetString("AccountType");
                        BaseAccount account = null;

                        if (accountType == "SavingsAccount")
                        {
                            account = new SavingsAccount();
                        }
                        else if (accountType == "CheckingAccount")
                        {
                            var checking = new CheckingAccount();
                            checking.OverdraftLimit = reader.GetDecimal("OverdraftLimit");
                            checking.HasOverdraftProtection = reader.GetBoolean("HasOverdraftProtection");
                            checking.MonthlyFee = reader.GetDecimal("MonthlyFee");
                            account = checking;
                        }
                        else if (accountType == "BusinessAccount")
                        {
                            account = new BusinessAccount();
                        }

                        if (account != null)
                        {
                            account.AccountID = reader.GetInt32("AccountID");
                            account.CustomerID = reader.GetInt32("CustomerID");
                            account.AccountNumber = reader.GetString("AccountNumber");
                            account.Balance = reader.GetDecimal("Balance");
                            account.InterestRate = reader.GetDecimal("InterestRate");
                            account.MinimumBalance = reader.GetDecimal("MinimumBalance");
                            account.DateOpened = reader.GetDateTime("DateOpened");
                            account.Status = Enum.Parse<AccountStatus>(reader.GetString("AccountStatus"));
                        }

                        return account;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading account: {ex.Message}");
            }

            return null;
        }

        public List<BaseAccount> LoadAllAccountsForCustomer(int customerId)
        {
            List<BaseAccount> accounts = new List<BaseAccount>();
            string query = "SELECT AccountID FROM Accounts WHERE CustomerID = @customerId";

            MySqlParameter[] parameters = {
                new MySqlParameter("@customerId", customerId)
            };

            try
            {
                using (var reader = ExecuteQuery(query, parameters))
                {
                    while (reader.Read())
                    {
                        int accountId = reader.GetInt32("AccountID");
                        BaseAccount account = LoadAccountByID(accountId);
                        if (account != null)
                        {
                            accounts.Add(account);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading accounts: {ex.Message}");
            }

            return accounts;
        }

        public int SaveTransaction(Transaction transaction)
        {
            string query = @"INSERT INTO Transactions 
                           (AccountID, TransactionType, Amount, TransactionDate, Description, BalanceAfter, RelatedTransactionID) 
                           VALUES (@accountId, @type, @amount, @date, @description, @balanceAfter, @relatedId);
                           SELECT LAST_INSERT_ID();";

            MySqlParameter[] parameters = {
                new MySqlParameter("@accountId", transaction.AccountID),
                new MySqlParameter("@type", transaction.Type.ToString()),
                new MySqlParameter("@amount", transaction.Amount),
                new MySqlParameter("@date", transaction.TransactionDate),
                new MySqlParameter("@description", transaction.Description ?? ""),
                new MySqlParameter("@balanceAfter", transaction.BalanceAfter),
                new MySqlParameter("@relatedId", transaction.RelatedTransactionID.HasValue ?
                    (object)transaction.RelatedTransactionID.Value : DBNull.Value)
            };

            object result = ExecuteScalar(query, parameters);
            return result != null ? Convert.ToInt32(result) : -1;
        }

        public List<Transaction> LoadTransactionsForAccount(int accountId)
        {
            List<Transaction> transactions = new List<Transaction>();
            string query = @"SELECT * FROM Transactions 
                           WHERE AccountID = @accountId 
                           ORDER BY TransactionDate DESC";

            MySqlParameter[] parameters = {
                new MySqlParameter("@accountId", accountId)
            };

            try
            {
                using (var reader = ExecuteQuery(query, parameters))
                {
                    while (reader.Read())
                    {
                        transactions.Add(new Transaction
                        {
                            TransactionID = reader.GetInt32("TransactionID"),
                            AccountID = reader.GetInt32("AccountID"),
                            Type = Enum.Parse<TransactionType>(reader.GetString("TransactionType")),
                            Amount = reader.GetDecimal("Amount"),
                            TransactionDate = reader.GetDateTime("TransactionDate"),
                            Description = reader.GetString("Description"),
                            BalanceAfter = reader.GetDecimal("BalanceAfter"),
                            RelatedTransactionID = reader.IsDBNull(reader.GetOrdinal("RelatedTransactionID"))
                                ? null : reader.GetInt32("RelatedTransactionID")
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading transactions: {ex.Message}");
            }

            return transactions;
        }

        public List<Transaction> LoadTransactionsByDateRange(int accountId, DateTime startDate, DateTime endDate)
        {
            List<Transaction> transactions = new List<Transaction>();

            DateTime endDateInclusive = endDate.Date.AddDays(1).AddSeconds(-1);

            string query = @"SELECT * FROM Transactions 
                           WHERE AccountID = @accountId 
                           AND TransactionDate BETWEEN @startDate AND @endDate
                           ORDER BY TransactionDate ASC";

            MySqlParameter[] parameters = {
                new MySqlParameter("@accountId", accountId),
                new MySqlParameter("@startDate", startDate.Date),
                new MySqlParameter("@endDate", endDateInclusive)
            };

            try
            {
                using (var reader = ExecuteQuery(query, parameters))
                {
                    while (reader.Read())
                    {
                        transactions.Add(new Transaction
                        {
                            TransactionID = reader.GetInt32("TransactionID"),
                            AccountID = reader.GetInt32("AccountID"),
                            Type = Enum.Parse<TransactionType>(reader.GetString("TransactionType")),
                            Amount = reader.GetDecimal("Amount"),
                            TransactionDate = reader.GetDateTime("TransactionDate"),
                            Description = reader.GetString("Description"),
                            BalanceAfter = reader.GetDecimal("BalanceAfter")
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading transactions by date: {ex.Message}");
            }

            return transactions;
        }

        public void UpdateAccountBalance(int accountId, decimal newBalance)
        {
            string query = "UPDATE Accounts SET Balance = @balance WHERE AccountID = @accountId";

            MySqlParameter[] parameters = {
                new MySqlParameter("@balance", newBalance),
                new MySqlParameter("@accountId", accountId)
            };

            ExecuteNonQuery(query, parameters);
        }

        public void UpdateAccountStatus(int accountId, AccountStatus newStatus)
        {
            string query = "UPDATE Accounts SET AccountStatus = @status WHERE AccountID = @accountId";

            MySqlParameter[] parameters =
            {
                new MySqlParameter("@status", newStatus.ToString()),
                new MySqlParameter("@accountId", accountId)
            };

            ExecuteNonQuery(query, parameters);
        }

        public MySqlTransaction BeginTransaction(MySqlConnection connection)
        {
            return connection.BeginTransaction();
        }

        public void CommitTransaction(MySqlTransaction transaction)
        {
            transaction.Commit();
        }

        public void RollbackTransaction(MySqlTransaction transaction)
        {
            transaction.Rollback();
        }

        public int GetMaxAccountNumber()
        {
            string query = "SELECT MAX(CAST(AccountNumber AS UNSIGNED)) FROM Accounts";

            try
            {
                object result = ExecuteScalar(query);

                if (result != null && result != DBNull.Value)
                {
                    return Convert.ToInt32(result);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting max account number: {ex.Message}");
            }

            return 0;
        }
    }
}