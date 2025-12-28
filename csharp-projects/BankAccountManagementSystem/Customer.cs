using System;
using System.Collections.Generic;

namespace BankAccountManagementSystem
{
    public class Customer
    {
        // Attributes/Member Variables
        public int _customerID;
        public string _firstName;
        public string _lastName;
        public string _email;
        public string _phoneNumber;
        public string _address;
        public DateTime _dateCreated;
        public List<int> _accountIDs = new List<int>();

        // Getters and Setters
        public int CustomerID
        {
            get { return _customerID; }
            set { _customerID = value; }
        }

        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }

        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set { _phoneNumber = value; }
        }

        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }

        public DateTime DateCreated
        {
            get { return _dateCreated; }
            set { _dateCreated = value; }
        }

        public List<int> AccountIDs
        {
            get { return _accountIDs; }
            set { _accountIDs = value; }
        }


        // Constructor
        public Customer() // Call other setting functions here 
        {
            _dateCreated = DateTime.Now;
            _accountIDs = new List<int>();
        }

        // Methods
        public string GetFullName()
        {
            return $"{_firstName} {_lastName}";
        }

        public void UpdateContactInfo(string email, string phone, string address)
        {
            _email = email;
            _phoneNumber = phone;
            _address = address;
        }

        public void DisplayCustomerDetails()
        {
            Console.WriteLine($"\n=== Customer Details ===");
            Console.WriteLine($"ID: {_customerID}");
            Console.WriteLine($"Name: {GetFullName()}");
            Console.WriteLine($"Email: {_email}");
            Console.WriteLine($"Phone: {_phoneNumber}");
            Console.WriteLine($"Address: {_address}");
            Console.WriteLine($"Customer Since: {_dateCreated:MM/dd/yyyy}");
            Console.WriteLine($"Number of Accounts: {_accountIDs.Count}");
        }
    }
}