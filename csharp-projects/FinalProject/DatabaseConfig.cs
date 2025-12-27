using System;
using MySqlConnector;

namespace FinalProject
{
    public static class DatabaseConfig
    {
        // Attributes/Member Variables
        // Database Connection Details
        private const string Host = "localhost";
        private const string Database = "BankManagementSystem";
        private const string User = "root";
        private const string Password = "USER_PASSWORD"; // Replace with your actual password
        private const string Port = "3306";


        // Build the connection string
        public static string ConnectionString =>
            $"Server={Host};Port={Port};Database={Database};User ID={User};Password={Password};";


        // Test method to verify connection works
        public static bool TestConnection()
        {
            try
            {
                using (var conn = new MySqlConnector.MySqlConnection(ConnectionString))
                {
                    conn.Open();
                    Console.WriteLine("✓ Database connection successful!");
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"✗ Database connection failed: {ex.Message}");
                return false;
            }
        }
    }
}