// Houses the global enum variables that are used throughout the rest of the program

namespace FinalProject
{
    public enum AccountStatus
    {
        Active,
        Frozen,
        Closed,
        Dormant
    }

    public enum TransactionType
    {
        Deposit,
        Withdrawal,
        Transfer,
        Fee,
        Interest
    }

    public enum TransferStatus
    {
        Pending,
        Completed,
        Failed
    }

    public enum CompoundingFrequency
    {
        Daily,
        Monthly,
        Quarterly,
        Annually
    }
}