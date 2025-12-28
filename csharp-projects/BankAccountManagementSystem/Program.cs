using System;
using System.Collections.Generic;

namespace BankAccountManagementSystem
{
    class Program
    {
        private static BankAndAccountManager bankManager;
        private static int currentCustomerId = -1;

        static void Main(string[] args)
        {
            Console.Clear();
            Console.WriteLine("╔══════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║           WELCOME TO BANK MANAGEMENT SYSTEM                      ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════════════╝\n");
            // Console.WriteLine("Press any key to continue...");
            // Console.ReadKey();

            Console.WriteLine("Testing database connection...");
            if (!DatabaseConfig.TestConnection())
            {
                Console.WriteLine("\n✗ ERROR: Cannot connect to database.");
                Console.WriteLine("\nPlease ensure:");
                Console.WriteLine("1. MySQL Server is running");
                Console.WriteLine("2. Database 'BankManagementSystem' exists");
                Console.WriteLine("3. Connection credentials in DatabaseConfig.cs are correct");
                Console.WriteLine("\nPress any key to exit...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
            bankManager = new BankAndAccountManager();


            bool exitProgram = false;
            while (!exitProgram)
            {
                if (currentCustomerId == -1)
                {
                    exitProgram = !ShowLoginMenu();
                }
                else
                {
                    ShowMainMenu();
                }
            }

            Console.WriteLine("\nThank you for using the Bank Management System!");
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        static bool ShowLoginMenu()
        {
            Console.Clear();
            Console.WriteLine("╔══════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                    LOGIN / REGISTRATION                          ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════════════╝\n");
            Console.WriteLine("1. Create New Customer Account");
            Console.WriteLine("2. Login to Existing Account");
            Console.WriteLine("3. Exit");
            Console.Write("\nSelect an option: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    CreateNewCustomer();
                    break;
                case "2":
                    LoginCustomer();
                    break;
                case "3":
                    return false;
                default:
                    Console.WriteLine("\n✗ Invalid option. Press any key to continue...");
                    Console.ReadKey();
                    break;
            }
            return true;
        }

        static void ShowMainMenu()
        {
            Console.Clear();
            Customer customer = bankManager.GetCustomerByID(currentCustomerId);

            Console.WriteLine("╔══════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                      MAIN MENU                                   ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════════════╝\n");
            Console.WriteLine($"Welcome, {customer.GetFullName()}!\n");
            Console.WriteLine("1.  Open New Account");
            Console.WriteLine("2.  View All My Accounts");
            Console.WriteLine("3.  Make a Deposit");
            Console.WriteLine("4.  Make a Withdrawal");
            Console.WriteLine("5.  Transfer Between Accounts");
            Console.WriteLine("6.  View Transaction History");
            Console.WriteLine("7.  Generate Account Statement");
            Console.WriteLine("8.  View Account Details");
            Console.WriteLine("9.  Update My Contact Information");
            Console.WriteLine("10. Close an Account");
            Console.WriteLine("11. Logout");
            Console.Write("\nSelect an option: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1": // Open New Account
                    OpenNewAccount();
                    break;
                case "2": // View All My Accounts
                    ViewAllAccounts();
                    break;
                case "3": // Make a Deposit
                    MakeDeposit();
                    break;
                case "4": // Make a Withdrawal
                    MakeWithdrawal();
                    break;
                case "5": // Transfer Between Accounts
                    TransferMoney();
                    break;
                case "6": // View Transaction History
                    ViewTransactionHistory();
                    break;
                case "7": // Generate Account Statement
                    GenerateStatement();
                    break;
                case "8": // View Account Details
                    ViewAccountDetails();
                    break;
                case "9": // Update My Contact Information
                    UpdateContactInfo();
                    break;
                case "10": // Close an Account
                    CloseAccount();
                    break;
                case "11": // Logout
                    Logout();
                    break;
                default:
                    Console.WriteLine("\n✗ Invalid option. Press any key to continue...");
                    Console.ReadKey();
                    break;
            }
        }

        static void CreateNewCustomer()
        {
            Console.Clear();
            Console.WriteLine("╔══════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║               CREATE NEW CUSTOMER ACCOUNT                        ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════════════╝\n");

            Console.Write("First Name: ");
            string firstName = Console.ReadLine();

            Console.Write("Last Name: ");
            string lastName = Console.ReadLine();

            Console.Write("Email: ");
            string email = Console.ReadLine();

            Console.Write("Phone Number: ");
            string phone = Console.ReadLine();

            Console.Write("Address: ");
            string address = Console.ReadLine();

            // Validate inputs
            if (!ValidationHelper.ValidateNonEmptyString(firstName) || !ValidationHelper.ValidateNonEmptyString(lastName))
            {
                Console.WriteLine("\n✗ First name and last name are required.");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                return;
            }

            int customerId = bankManager.CreateCustomer(firstName, lastName, email, phone, address);

            if (customerId > 0)
            {
                Console.WriteLine($"\n✓ Customer account created successfully!");
                Console.WriteLine($"Your Customer ID is: {customerId}");
                Console.Write("\nWould you like to login now? (Y/N): ");
                string response = Console.ReadLine();

                if (response?.ToUpper() == "Y")
                {
                    currentCustomerId = customerId;
                }
            }
            else
            {
                Console.WriteLine("\n✗ Failed to create customer account.");
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        static void LoginCustomer()
        {
            Console.Clear();
            Console.WriteLine("╔══════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                       LOGIN                                      ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════════════╝\n");

            Console.Write("Enter your Customer ID: ");
            if (int.TryParse(Console.ReadLine(), out int customerId))
            {
                Customer customer = bankManager.GetCustomerByID(customerId);

                if (customer != null)
                {
                    currentCustomerId = customerId;
                    Console.WriteLine($"\n✓ Welcome back, {customer.GetFullName()}!");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("\n✗ Customer ID not found.");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
            }
            else
            {
                Console.WriteLine("\n✗ Invalid Customer ID.");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }

        static void OpenNewAccount()
        {
            Console.Clear();
            Console.WriteLine("╔══════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                    OPEN NEW ACCOUNT                              ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════════════╝\n");

            Console.WriteLine("Select Account Type:");
            Console.WriteLine("1. Savings Account (2.5% interest, $100 minimum balance)");
            Console.WriteLine("2. Checking Account (Overdraft protection available)");
            Console.WriteLine("3. Business Account (Transaction fees apply)");
            Console.Write("\nEnter choice (1-3): ");

            string typeChoice = Console.ReadLine();
            string accountType = "";

            switch (typeChoice)
            {
                case "1": // Savings Account
                    accountType = "Savings";
                    break;
                case "2": // Checking Account
                    accountType = "Checking";
                    break;
                case "3": // Business Account
                    accountType = "Business";
                    break;
                default:
                    Console.WriteLine("\n✗ Invalid account type.");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    return;
            }

            Console.Write("\nInitial Deposit Amount: $");
            if (decimal.TryParse(Console.ReadLine(), out decimal initialDeposit))
            {
                if (!ValidationHelper.ValidatePositiveAmount(initialDeposit))
                {
                    Console.WriteLine("\n✗ Deposit amount must be positive.");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    return;
                }

                int accountId = bankManager.CreateAccount(currentCustomerId, accountType, initialDeposit);

                if (accountId > 0)
                {
                    BaseAccount account = bankManager.GetAccountByID(accountId);
                    Console.WriteLine($"\n✓ {accountType} account created successfully!");
                    Console.WriteLine($"Account Number: {account.AccountNumber}");
                    Console.WriteLine($"Account ID: {accountId}");
                    Console.WriteLine($"Initial Balance: ${initialDeposit:N2}");
                }
                else
                {
                    Console.WriteLine("\n✗ Failed to create account.");
                }
            }
            else
            {
                Console.WriteLine("\n✗ Invalid amount.");
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        static void ViewAllAccounts()
        {
            Console.Clear();
            Console.WriteLine("╔══════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                      MY ACCOUNTS                                 ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════════════╝\n");

            List<BaseAccount> accounts = bankManager.GetAllAccountsForCustomer(currentCustomerId);

            if (accounts.Count == 0)
            {
                Console.WriteLine("You have no accounts yet.");
            }
            else
            {
                Console.WriteLine($"{"Account ID",-12} {"Account #",-15} {"Type",-20} {"Balance",15} {"Status",-10}");
                Console.WriteLine(new string('─', 75));

                foreach (var account in accounts)
                {
                    Console.WriteLine($"{account.AccountID,-12} {account.AccountNumber,-15} {account.GetType().Name,-20} ${account.Balance,12:N2} {account.Status,-10}");
                }
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        static void MakeDeposit()
        {
            Console.Clear();
            Console.WriteLine("╔══════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                      MAKE DEPOSIT                                ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════════════╝\n");

            int accountId = SelectAccount();
            if (accountId == -1) return;

            Console.Write("\nDeposit Amount: $");
            if (decimal.TryParse(Console.ReadLine(), out decimal amount))
            {
                if (amount <= 0)
                {
                    Console.WriteLine("\n✗ Deposit amount must be positive.");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    return;
                }

                if (amount > 1000000)
                {
                    Console.WriteLine("\n✗ Deposit amount exceeds maximum limit of $1,000,000.");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    return;
                }

                Console.Write("Description (optional): ");
                string description = Console.ReadLine();
                if (string.IsNullOrEmpty(description))
                    description = "Deposit";

                bool success = bankManager.ProcessTransaction(accountId, amount, TransactionType.Deposit, description);

                if (success)
                {
                    BaseAccount account = bankManager.GetAccountByID(accountId);
                    Console.WriteLine($"\n✓ Deposit successful!");
                    Console.WriteLine($"New Balance: ${account.Balance:N2}");
                }
                else
                {
                    Console.WriteLine("\n✗ Deposit failed.");
                }
            }
            else
            {
                Console.WriteLine("\n✗ Invalid amount.");
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        static void MakeWithdrawal()
        {
            Console.Clear();
            Console.WriteLine("╔══════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                     MAKE WITHDRAWAL                              ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════════════╝\n");

            int accountId = SelectAccount();
            if (accountId == -1) return;

            Console.Write("\nWithdrawal Amount: $");
            if (decimal.TryParse(Console.ReadLine(), out decimal amount))
            {
                Console.Write("Description (optional): ");
                string description = Console.ReadLine();
                if (string.IsNullOrEmpty(description))
                    description = "Withdrawal";

                bool success = bankManager.ProcessTransaction(accountId, amount, TransactionType.Withdrawal, description);

                if (success)
                {
                    BaseAccount account = bankManager.GetAccountByID(accountId);
                    Console.WriteLine($"\n✓ Withdrawal successful!");
                    Console.WriteLine($"New Balance: ${account.Balance:N2}");
                }
                else
                {
                    Console.WriteLine("\n✗ Withdrawal failed. Check account balance and limits.");
                }
            }
            else
            {
                Console.WriteLine("\n✗ Invalid amount.");
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        static void TransferMoney()
        {
            Console.Clear();
            Console.WriteLine("╔══════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                   TRANSFER BETWEEN ACCOUNTS                      ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════════════╝\n");

            Console.WriteLine("Select SOURCE account:");
            int fromAccountId = SelectAccount();
            if (fromAccountId == -1) return;

            Console.WriteLine("\nSelect DESTINATION account:");
            int toAccountId = SelectAccount();
            if (toAccountId == -1) return;

            if (fromAccountId == toAccountId)
            {
                Console.WriteLine("\n✗ Cannot transfer to the same account.");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                return;
            }

            Console.Write("\nTransfer Amount: $"); ;
            if (decimal.TryParse(Console.ReadLine(), out decimal amount))
            {
                bool success = bankManager.TransferBetweenAccounts(fromAccountId, toAccountId, amount);

                if (success)
                {
                    Console.WriteLine($"\n✓ Transfer successful!");
                    BaseAccount fromAccount = bankManager.GetAccountByID(fromAccountId);
                    BaseAccount toAccount = bankManager.GetAccountByID(toAccountId);
                    Console.WriteLine($"From Account ({fromAccount.AccountNumber}) New Balance: ${fromAccount.Balance:N2}");
                    Console.WriteLine($"To Account ({toAccount.AccountNumber}) New Balance: ${toAccount.Balance:N2}");
                }
                else
                {
                    Console.WriteLine("\n✗ Transfer failed. Check account balances and limits.");
                }
            }
            else
            {
                Console.WriteLine("\n✗ Invalid amount.");
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        static void ViewTransactionHistory()
        {
            Console.Clear();
            Console.WriteLine("╔══════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                  TRANSACTION HISTORY                             ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════════════╝\n");

            int accountId = SelectAccount();
            if (accountId == -1) return;

            DatabaseRepositoryAndHelper db = new DatabaseRepositoryAndHelper();
            List<Transaction> transactions = db.LoadTransactionsForAccount(accountId);

            if (transactions.Count == 0)
            {
                Console.WriteLine("\nNo transactions found for this account.");
            }
            else
            {
                Console.WriteLine($"\n{"Date",-18} {"Type",-12} {"Amount",12} {"Balance",15} {"Description",-25}");
                Console.WriteLine(new string('─', 90));

                foreach (var trans in transactions)
                {
                    string sign;
                    if (trans.Type == TransactionType.Transfer)
                    {
                        sign = trans.Description.Contains("from") ? "+" : "-";
                    }
                    else
                    {
                        sign = (trans.Type == TransactionType.Deposit || trans.Type == TransactionType.Interest) ? "+" : "-"; // The end part is like a compact if else statement to determine if the transaction is positive or negative
                    }

                    Console.WriteLine($"{trans.TransactionDate:MM/dd/yyyy HH:mm} {trans.Type,-12} {sign}${trans.Amount,10:N2} ${trans.BalanceAfter,13:N2} {trans.Description,-30}");
                }
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        static void GenerateStatement()
        {
            Console.Clear();
            Console.WriteLine("╔══════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                  GENERATE STATEMENT                              ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════════════╝\n");

            int accountId = SelectAccount();
            if (accountId == -1) return;

            Console.WriteLine("\nStatement Options:");
            Console.WriteLine("1. ALL Transactions (Complete History)");
            Console.WriteLine("2. Transactions by Date Range");
            Console.Write("\nSelect option (1-2): ");

            string choice = Console.ReadLine();

            if (choice == "1") // ALL Transactions
            {
                DatabaseRepositoryAndHelper db = new DatabaseRepositoryAndHelper();
                List<Transaction> allTransactions = db.LoadTransactionsForAccount(accountId);
                BaseAccount account = bankManager.GetAccountByID(accountId);

                if (allTransactions.Count == 0)
                {
                    Console.WriteLine("\nNo transactions found for this account.");
                }
                else
                {
                    DateTime firstDate = allTransactions[allTransactions.Count - 1].TransactionDate;
                    DateTime lastDate = allTransactions[0].TransactionDate;

                    Statement statement = new Statement();
                    statement.GenerateStatement(account, allTransactions, firstDate, lastDate);
                    statement.DisplayStatement();
                }
            }
            else if (choice == "2")
            {
                Console.Write("\nStart Date (MM/DD/YYYY): ");
                if (!DateTime.TryParse(Console.ReadLine(), out DateTime startDate))
                {
                    Console.WriteLine("✗ Invalid date format.");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    return;
                }

                Console.Write("End Date (MM/DD/YYYY): ");
                if (!DateTime.TryParse(Console.ReadLine(), out DateTime endDate))
                {
                    Console.WriteLine("✗ Invalid date format.");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    return;
                }

                if (!ValidationHelper.ValidateDateRange(startDate, endDate))
                {
                    Console.WriteLine("✗ Invalid date range.");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    return;
                }

                bankManager.GenerateStatementForAccount(accountId, startDate, endDate);
            }
            else
            {
                Console.WriteLine("\n✗ Invalid option.");
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        static void ViewAccountDetails()
        {
            Console.Clear();
            Console.WriteLine("╔══════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                    ACCOUNT DETAILS                               ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════════════╝\n");

            int accountId = SelectAccount();
            if (accountId == -1) return;

            BaseAccount account = bankManager.GetAccountByID(accountId);
            if (account != null)
            {
                account.DisplayAccountInfo();
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        static void UpdateContactInfo()
        {
            Console.Clear();
            Console.WriteLine("╔══════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                UPDATE CONTACT INFORMATION                        ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════════════╝\n");

            Customer customer = bankManager.GetCustomerByID(currentCustomerId);
            customer.DisplayCustomerDetails();

            Console.WriteLine("\nEnter new information (press Enter to keep current):\n");

            Console.Write($"Email [{customer.Email}]: ");
            string email = Console.ReadLine();
            if (string.IsNullOrEmpty(email))
                email = customer.Email;

            Console.Write($"Phone [{customer.PhoneNumber}]: ");
            string phone = Console.ReadLine();
            if (string.IsNullOrEmpty(phone))
                phone = customer.PhoneNumber;

            Console.Write($"Address [{customer.Address}]: ");
            string address = Console.ReadLine();
            if (string.IsNullOrEmpty(address))
                address = customer.Address;

            customer.UpdateContactInfo(email, phone, address);

            DatabaseRepositoryAndHelper db = new DatabaseRepositoryAndHelper();
            db.UpdateCustomer(customer);

            Console.WriteLine("\n✓ Contact information updated successfully!");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        static void CloseAccount()
        {
            Console.Clear();
            Console.WriteLine("╔══════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                      CLOSE ACCOUNT                               ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════════════╝\n");

            int accountId = SelectAccount();
            if (accountId == -1) return;

            BaseAccount account = bankManager.GetAccountByID(accountId);

            Console.WriteLine($"\nAccount: {account.AccountNumber}");
            Console.WriteLine($"Balance: ${account.Balance:N2}");

            if (account.Balance != 0)
            {
                Console.WriteLine("\n✗ Cannot close account with non-zero balance.");
                Console.WriteLine("Please withdraw or transfer all funds first.");
            }
            else
            {
                Console.Write("\nAre you sure you want to close this account? (Y/N): ");
                string confirm = Console.ReadLine();

                if (confirm?.ToUpper() == "Y")
                {
                    try
                    {
                        bankManager.CloseAccount(accountId);
                        Console.WriteLine("\n✓ Account closed successfully!");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"\n✗ Error closing account: {ex.Message}");
                    }
                }
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        static void Logout()
        {
            currentCustomerId = -1;
            Console.WriteLine("\n✓ Logged out successfully!");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        static int SelectAccount()
        {
            List<BaseAccount> accounts = bankManager.GetAllAccountsForCustomer(currentCustomerId);

            if (accounts.Count == 0)
            {
                Console.WriteLine("You have no accounts. Please open an account first.");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                return -1;
            }

            Console.WriteLine($"\n{"#",-4} {"Account ID",-12} {"Account #",-15} {"Type",-20} {"Balance",15}");
            Console.WriteLine(new string('─', 70));

            for (int i = 0; i < accounts.Count; i++)
            {
                var account = accounts[i];
                Console.WriteLine($"{i + 1,-4} {account.AccountID,-12} {account.AccountNumber,-15} {account.GetType().Name,-20} ${account.Balance,12:N2}");
            }

            Console.Write("\nSelect account number (# on the left): ");
            if (int.TryParse(Console.ReadLine(), out int selection) && selection > 0 && selection <= accounts.Count)
            {
                return accounts[selection - 1].AccountID;
            }

            Console.WriteLine("✗ Invalid selection.");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            return -1;
        }
    }
}