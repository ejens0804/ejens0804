using System;

namespace BankAccountManagementSystem
{
    public class ValidationHelper
    {
        // Methods
        public static bool ValidateEmailFormat(string email)
        {
            if (string.IsNullOrEmpty(email))
                return false;
            
            return email.Contains("@") && email.Contains(".") && email.IndexOf("@")< email.LastIndexOf(".");
        }

        public static bool ValidatePhoneNumber(string phone)
        {
            if (string.IsNullOrEmpty(phone))
            return false;

            string cleanPhone = phone.Replace("-", "").Replace(")", "").Replace(" ", "").Replace(".", "");
            return cleanPhone.Length == 10 && long.TryParse(cleanPhone, out _);
        }

        public static bool ValidatePositiveAmount(decimal amount)
        {
            return amount > 0;
        }

        public static bool ValidateAccountNumberFormat(string accountNumber)
        {
            if (string.IsNullOrEmpty(accountNumber))
                return false;

            // Account number should be numeric and 8-12 digits
            return accountNumber.Length >= 8 && accountNumber.Length <= 12 && long.TryParse(accountNumber, out _);
        }

        public static bool ValidateDateRange(DateTime startDate, DateTime endDate)
        {
            return endDate >= startDate && endDate <= DateTime.Now;
        }

        public static bool ValidateSufficientBalance(BaseAccount account, decimal amount)
        {
            if (account == null)
                return false;
            
            return account.CanWithdraw(amount);
        }

        public static bool ValidateNonEmptyString(string input)
        {
            return !string.IsNullOrWhiteSpace(input);
        }
    }
}