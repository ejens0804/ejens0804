using System.Runtime.InteropServices.Marshalling;

namespace FinalProject
{
    public class Transfer : Transaction
    {
        // Attributes/Member Variables
        private int _fromAccountID;
        private int _toAccountID;
        private decimal _transferAmount;
        private DateTime _transferDate;
        private TransferStatus _transferStatus;
        private decimal _transferFee;


        // Constructor
        public Transfer()
        {
            _transferDate = DateTime.Now;
            _transferStatus = TransferStatus.Pending;
            _transferFee = 0m;
        }

        public Transfer(int fromAccountId, int toAccountId, decimal amount)
        {
            _fromAccountID = fromAccountId;
            _toAccountID = toAccountId;
            _transferAmount = amount;
            _transferDate = DateTime.Now;
            _transferStatus = TransferStatus.Pending;
            _transferFee = 0m;
        }

        public int FromAccountID
        {
            get { return _fromAccountID; }
            set { _fromAccountID = value; }
        }

        public int ToAccountID
        {
            get { return _toAccountID; }
            set { _toAccountID = value; }
        }

        public decimal TransferAmount
        {
            get { return _transferAmount; }
            set { _transferAmount = value; }
        }

        public TransferStatus Status
        {
            get { return _transferStatus; }
            set { _transferStatus = value; }
        }


        // Methods
        public bool ExecuteTransfer(BaseAccount fromAccount, BaseAccount toAccount)
        {
            if (!ValidateTransfer(fromAccount, toAccount))
            {
                _transferStatus = TransferStatus.Failed;
                return false;
            }

            try
            {
                _transferStatus = TransferStatus.Pending;

                if (!fromAccount.Withdraw(_transferAmount))
                {
                    _transferStatus = TransferStatus.Failed;
                    return false;
                }
                
                toAccount.Deposit(_transferAmount);

                _transferStatus = TransferStatus.Completed;
                return true;
            }
            catch (Exception)
            {
                _transferStatus = TransferStatus.Failed;

                RollbackTransfer(fromAccount, toAccount);
                return false;
            }
        }

        public bool ValidateTransfer(BaseAccount fromAccount, BaseAccount toAccount)
        {
            if (fromAccount == null || toAccount == null)
            {
                Console.WriteLine("Invalid account(s).");
                return false;
            }

            if (fromAccount.Status != AccountStatus.Active)
            {
                Console.WriteLine("Source account is not active.");
                return false;
            }

            if (toAccount.Status != AccountStatus.Active)
            {
                Console.WriteLine("Destination account is not active.");
                return false;
            }

            if (_transferAmount <= 0)
            {
                Console.WriteLine("Transfer amount must be positive.");
                return false;
            }

            if (!fromAccount.CanWithdraw(_transferAmount))
            {
                Console.WriteLine("Insufficient funds in source account.");
                return false;
            }

            return true;
        }

        public void RollbackTransfer(BaseAccount fromAccount, BaseAccount toAccount)
        {
            try
            {
                if (_transferStatus == TransferStatus.Completed)
                {
                    toAccount.Withdraw(_transferAmount);
                    fromAccount.Deposit(_transferAmount);
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Warning: Transfer rollback failed. Manual intervention may be required.");
            }
        }

        public void GetTransferDetails()
        {
            Console.WriteLine($"\n=== Transfer Details ===");
            Console.WriteLine($"From Account ID: {_fromAccountID}");
            Console.WriteLine($"To Account ID: {_toAccountID}");
            Console.WriteLine($"Amount: ${_transferAmount:N2}");
            Console.WriteLine($"Transfer Fee: ${_transferFee:N2}");
            Console.WriteLine($"Status: {_transferStatus}");
            Console.WriteLine($"Date: {_transferDate:MM/dd/yyyy HH:mm}");
        }
    }
}