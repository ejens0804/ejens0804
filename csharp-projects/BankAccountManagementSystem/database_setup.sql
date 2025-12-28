-- Create the database
CREATE DATABASE IF NOT EXISTS BankManagementSystem;
USE BankManagementSystem;

-- Create Customers table
CREATE TABLE IF NOT EXISTS Customers (
    CustomerID INT PRIMARY KEY AUTO_INCREMENT,
    FirstName VARCHAR(50) NOT NULL,
    LastName VARCHAR(50) NOT NULL,
    Email VARCHAR(100),
    Phone VARCHAR(20),
    Address VARCHAR(200),
    DateCreated DATETIME NOT NULL,
    IsActive BOOLEAN DEFAULT TRUE
);

-- Create Accounts table
CREATE TABLE IF NOT EXISTS Accounts (
    AccountID INT PRIMARY KEY AUTO_INCREMENT,
    CustomerID INT NOT NULL,
    AccountNumber VARCHAR(20) UNIQUE NOT NULL,
    AccountType VARCHAR(20) NOT NULL,
    Balance DECIMAL(15,2) DEFAULT 0.00,
    InterestRate DECIMAL(5,2) DEFAULT 0.00,
    MinimumBalance DECIMAL(15,2) DEFAULT 0.00,
    DateOpened DATETIME NOT NULL,
    AccountStatus VARCHAR(20) DEFAULT 'Active',
    OverdraftLimit DECIMAL(15,2) DEFAULT 0.00,
    HasOverdraftProtection BOOLEAN DEFAULT FALSE,
    MonthlyFee DECIMAL(10,2) DEFAULT 0.00,
    FOREIGN KEY (CustomerID) REFERENCES Customers(CustomerID)
);

-- Create Transactions table
CREATE TABLE IF NOT EXISTS Transactions (
    TransactionID INT PRIMARY KEY AUTO_INCREMENT,
    AccountID INT NOT NULL,
    TransactionType VARCHAR(20) NOT NULL,
    Amount DECIMAL(15,2) NOT NULL,
    TransactionDate DATETIME NOT NULL,
    Description VARCHAR(200),
    BalanceAfter DECIMAL(15,2) NOT NULL,
    RelatedTransactionID INT NULL,
    FOREIGN KEY (AccountID) REFERENCES Accounts(AccountID)
);

-- Create index for faster queries
CREATE INDEX idx_customer_name ON Customers(LastName, FirstName);
CREATE INDEX idx_account_number ON Accounts(AccountNumber);
CREATE INDEX idx_transaction_date ON Transactions(TransactionDate);
CREATE INDEX idx_account_transactions ON Transactions(AccountID, TransactionDate);