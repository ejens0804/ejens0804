# Bank Management System

A complete banking application built with C# and MySQL that allows you to manage customer accounts, perform transactions, and track financial activity.

## What This Program Does

- **Create and manage customer accounts** - Store customer information securely
- **Multiple account types** - Savings, Checking, and Business accounts with unique features
- **Banking transactions** - Deposits, withdrawals, and transfers between accounts
- **Transaction history** - View complete records of all account activity
- **Account statements** - Generate detailed statements for any date range
- **Interest calculation** - Automatic interest on eligible accounts
- **Fee management** - Overdraft protection and account maintenance fees

## Requirements

To run this program, you need:

1. **Windows, macOS, or Linux**
2. **.NET 8.0 SDK** - Download from: https://dotnet.microsoft.com/download
3. **Visual Studio Code** - Download from: https://code.visualstudio.com/
4. **C# Extension for VS Code** - Install from VS Code Extensions marketplace
5. **MySQL Workbench** - Download from: https://dev.mysql.com/downloads/workbench/

## Quick Start Guide

### Step 1: Install Prerequisites

#### Install .NET 8.0 SDK
1. Go to https://dotnet.microsoft.com/download
2. Download the .NET 8.0 SDK for your operating system
3. Run the installer
4. Verify installation by opening a terminal and typing:
   ```bash
   dotnet --version
   ```
   You should see version 8.0.x

#### Install Visual Studio Code
1. Go to https://code.visualstudio.com/
2. Download for your operating system
3. Install VS Code
4. Open VS Code

#### Install C# Extension
1. In VS Code, click the Extensions icon (or press `Ctrl+Shift+X`)
2. Search for "C# Dev Kit" by Microsoft
3. Click **Install**
4. Also install "C#" extension by Microsoft if not already installed

### Step 2: Install and Setup MySQL

1. Download **MySQL Installer** from https://dev.mysql.com/downloads/installer/
2. Run the installer and choose "Custom" installation
3. Select:
   - MySQL Server (latest version)
   - MySQL Workbench
4. During setup:
   - Create a **root password** - **Write this down!**
   - Use default port 3306
5. Complete the installation

### Step 3: Create the Database

1. Open **MySQL Workbench**
2. Click on your local connection (usually "Local instance MySQL80")
3. Enter your root password
4. In MySQL Workbench, go to **File → Open SQL Script**
5. Navigate to your project folder and select `database_setup.sql`
6. Click the lightning bolt icon ⚡ to run the script
7. You should see "BankManagementSystem" appear in the SCHEMAS panel on the left

**Verify the database was created:**
- In the left sidebar under "SCHEMAS", you should see:
  - BankManagementSystem
    - Tables
      - Customers
      - Accounts
      - Transactions

### Step 4: Open Project in VS Code

1. Open Visual Studio Code
2. Click **File → Open Folder**
3. Navigate to and select your `FinalProject` folder
4. VS Code will open with your project files in the Explorer panel

### Step 5: Configure Database Connection

1. In VS Code Explorer, find and click on `DatabaseConfig.cs`
2. Find line 14 that says:
   ```csharp
   private const string Password = "USER_PASSWORD";
   ```
3. Replace `"USER_PASSWORD"` with **your MySQL root password**
4. Save the file (`Ctrl+S` or `Cmd+S` on Mac)

### Step 6: Install NuGet Package

1. Open the **Terminal** in VS Code:
   - Go to **View → Terminal** or press `` Ctrl+` ``
2. Make sure you're in the FinalProject directory:
   ```bash
   cd FinalProject
   ```
3. Run this command to restore packages:
   ```bash
   dotnet restore
   ```
4. You should see "Restore completed" message

### Step 7: Build the Project

In the terminal, run:
```bash
dotnet build
```

If successful, you'll see:
```
Build succeeded.
    0 Warning(s)
    0 Error(s)
```

### Step 8: Run the Program

In the terminal, run:
```bash
dotnet run
```

The program will:
1. Test the database connection
2. Display the login menu if successful
3. You're ready to use the banking system!

## Using the Program

### First Time Setup

**1. Create a Customer Account**
- Choose option 1 from the login screen
- Enter your personal information
- **Write down your Customer ID!** You'll need it to log in

**2. Open a Bank Account**
- Login with your Customer ID
- Choose "Open New Account"
- Select account type and make initial deposit

### Available Account Types

**Savings Account**
- 2.5% annual interest rate
- $100 minimum balance
- Limited to 6 withdrawals per month
- Best for: Long-term savings

**Checking Account**
- Overdraft protection available
- $10 monthly maintenance fee (waived with $1000+ balance)
- $25 minimum balance
- Best for: Daily transactions

**Business Account**
- 1.5% annual interest rate
- 100 free transactions per month
- $0.50 fee per transaction after limit
- $500 minimum balance
- Best for: Business operations

### Main Features

**Make Deposits/Withdrawals**
- Select your account from the list
- Enter the amount
- Add optional description
- Transaction is recorded immediately

**Transfer Money**
- Move money between your accounts instantly
- Full transaction history maintained for both accounts

**View Transaction History**
- See all deposits, withdrawals, and transfers
- Transactions shown with dates, amounts, and descriptions
- Listed in chronological order

**Generate Statements**
- Choose start and end date range
- View detailed summary of all activity
- Shows opening balance, closing balance, and totals

**Manage Your Profile**
- Update email, phone, and address anytime
- Changes are saved to the database

**Close Accounts**
- Accounts must have zero balance to close
- Status is permanently changed to "Closed"

## VS Code Tips

### Running the Program
- **Method 1:** Use the terminal: `dotnet run`
- **Method 2:** Press `F5` to run with debugger
- **Method 3:** Use the "Run" button in the top-right corner

### Stopping the Program
- Press `Ctrl+C` in the terminal
- Or close the terminal window

### Debugging
1. Click on the line number to set a breakpoint (red dot appears)
2. Press `F5` to start debugging
3. Program will pause at breakpoints
4. Use the debug toolbar to step through code

### Building Without Running
```bash
dotnet build
```

### Cleaning Build Files
```bash
dotnet clean
```

### Viewing Output
- The **Terminal** panel shows program output
- Look for the "DEBUG CONSOLE" tab for debugging info

## Troubleshooting

### "Cannot connect to database"

**Windows:**
1. Open **Services** (search in Start menu)
2. Find "MySQL80" service
3. Make sure Status says "Running"
4. If not, right-click → **Start**

**macOS/Linux:**
1. Check if MySQL is running:
   ```bash
   mysql --version
   ```
2. If not installed, follow MySQL installation guide for your OS

**All Platforms:**
- Open `DatabaseConfig.cs` and verify password is correct
- Open MySQL Workbench and test connection
- Make sure database "BankManagementSystem" exists

### "dotnet command not found"

**.NET SDK not installed properly:**
1. Download .NET 8.0 SDK again
2. Reinstall
3. Restart VS Code
4. Open new terminal and try again

**Verify installation:**
```bash
dotnet --version
```

### "MySqlConnector not found" error

Run in terminal:
```bash
dotnet restore
```

If that doesn't work:
```bash
dotnet add package MySqlConnector
```

### Database doesn't exist

1. Open MySQL Workbench
2. Look in SCHEMAS panel (left side)
3. If "BankManagementSystem" is missing:
   - Go to **File → Open SQL Script**
   - Select `database_setup.sql`
   - Click lightning bolt ⚡ to run

### Build errors

**Common fixes:**
```bash
# Clean and rebuild
dotnet clean
dotnet build

# Restore packages
dotnet restore
dotnet build
```

### Program closes immediately

Make sure:
- MySQL is running
- Database exists
- Password in `DatabaseConfig.cs` is correct
- Check terminal for error messages

### Port 3306 already in use

Another MySQL instance might be running:
1. Check Services (Windows) or Activity Monitor (Mac)
2. Stop other MySQL services
3. Restart your MySQL instance

## VS Code Keyboard Shortcuts

**Essential shortcuts:**
- `Ctrl+` ` ` - Toggle Terminal
- `Ctrl+Shift+P` - Command Palette
- `F5` - Start Debugging
- `Ctrl+F5` - Run Without Debugging
- `Ctrl+S` - Save File
- `Ctrl+Shift+F` - Find in All Files
- `Ctrl+/` - Toggle Comment

## Project Structure

```
FinalProject/
├── Program.cs                        # Entry point with menu interface
├── DatabaseConfig.cs                 # Database connection settings
├── DatabaseRepositoryAndHelper.cs    # Database operations
├── Enums.cs                         # Enum definitions
├── database_setup.sql               # Database creation script
│
├── Customer.cs                      # Customer entity
├── BaseAccount.cs                   # Abstract account base class
├── CheckingAccount.cs               # Checking account implementation
├── SavingsAccount.cs                # Savings account implementation
├── BusinessAccount.cs               # Business account implementation
│
├── Transaction.cs                   # Transaction entity
├── Transfer.cs                      # Transfer operations
├── Statement.cs                     # Account statement generation
│
├── BankAndAccountManager.cs         # Main business logic
├── FeeManager.cs                    # Fee calculation
├── ValidationHelper.cs              # Input validation
│
├── InterestCalculator.cs            # Interest calculator interface
├── SimpleInterestCalculator.cs      # Simple interest
├── CompoundInterestCalculator.cs    # Compound interest
│
└── FinalProject.csproj              # Project configuration
```

## Database Schema

### Customers Table
- Stores customer personal information
- CustomerID (Primary Key, Auto-increment)
- FirstName, LastName, Email, Phone, Address
- DateCreated, IsActive

### Accounts Table
- Stores all bank accounts
- AccountID (Primary Key, Auto-increment)
- CustomerID (Foreign Key → Customers)
- AccountNumber, AccountType, Balance
- InterestRate, MinimumBalance, AccountStatus
- OverdraftLimit, HasOverdraftProtection, MonthlyFee
- DateOpened

### Transactions Table
- Records all financial transactions
- TransactionID (Primary Key, Auto-increment)
- AccountID (Foreign Key → Accounts)
- TransactionType, Amount, TransactionDate
- Description, BalanceAfter
- RelatedTransactionID (for linking transfers)

## OOP Principles Demonstrated

This project showcases all four main Object-Oriented Programming principles:

1. **Encapsulation**
   - Private member variables with public properties
   - Data hiding and controlled access

2. **Inheritance**
   - SavingsAccount, CheckingAccount, BusinessAccount inherit from BaseAccount
   - Transfer inherits from Transaction
   - Shared functionality in parent classes

3. **Polymorphism**
   - Different account types override `Withdraw()` and `CalculateInterest()`
   - Same method calls produce different behavior based on object type

4. **Abstraction**
   - BaseAccount is abstract - cannot be instantiated directly
   - InterestCalculator interface defines contract
   - Implementation details hidden from users

## Security Note

⚠️ **This is an educational project.**

In a production environment, you should:
- Encrypt passwords and sensitive data
- Implement proper user authentication
- Store database credentials securely (environment variables, secrets manager)
- Add logging and audit trails
- Implement proper error handling
- Add SQL injection prevention (already using parameterized queries)
- Add transaction rollback capability for failures

## Additional Resources

**Learn more about .NET:**
- Official docs: https://docs.microsoft.com/dotnet/
- C# guide: https://docs.microsoft.com/dotnet/csharp/

**MySQL Documentation:**
- MySQL Workbench: https://dev.mysql.com/doc/workbench/en/
- MySQL tutorials: https://dev.mysql.com/doc/

**VS Code for C#:**
- C# in VS Code: https://code.visualstudio.com/docs/languages/csharp
- Debugging: https://code.visualstudio.com/docs/editor/debugging

## Support

If you encounter issues:

1. **Check MySQL is running** (Services on Windows, System Preferences on Mac)
2. **Verify database exists** (open MySQL Workbench, check SCHEMAS)
3. **Confirm password** in `DatabaseConfig.cs` matches your MySQL password
4. **Check terminal output** for specific error messages
5. **Try cleaning and rebuilding**: `dotnet clean && dotnet build`

## Common Terminal Commands

```bash
# Navigate to project directory
cd FinalProject

# Build the project
dotnet build

# Run the program
dotnet run

# Clean build files
dotnet clean

# Restore NuGet packages
dotnet restore

# Add a package
dotnet add package PackageName

# Check .NET version
dotnet --version

# List installed SDKs
dotnet --list-sdks
```

---

**Created as an educational project demonstrating:**
- C# programming
- Object-Oriented Programming
- MySQL database integration  
- Console application development
- Transaction management

**Important:** This is a learning project. Do not use with real financial data.