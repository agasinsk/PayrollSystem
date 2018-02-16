using PayrollSystem.Transactions;

namespace PayrollSystem
{
    public interface ITransactionSource
    {
        ITransaction GetTransaction();
    }
}