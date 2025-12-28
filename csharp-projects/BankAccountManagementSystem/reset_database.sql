-- Clear all data from BankManagementSystem database
-- This preserves table structure but removes all records

USE BankManagementSystem;

-- Disable foreign key checks temporarily to avoid constraint errors
SET FOREIGN_KEY_CHECKS = 0;

-- Delete all records from each table
DELETE FROM Transactions;
DELETE FROM Accounts;
DELETE FROM Customers;

-- Re-enable foreign key checks
SET FOREIGN_KEY_CHECKS = 1;

-- Reset AUTO_INCREMENT counters to start fresh
ALTER TABLE Customers AUTO_INCREMENT = 1;
ALTER TABLE Accounts AUTO_INCREMENT = 1;
ALTER TABLE Transactions AUTO_INCREMENT = 1;

-- Verify tables are empty (shows row count)
SELECT 'Customers' AS TableName, COUNT(*) AS RowCount FROM Customers
UNION ALL
SELECT 'Accounts', COUNT(*) FROM Accounts
UNION ALL
SELECT 'Transactions', COUNT(*) FROM Transactions;