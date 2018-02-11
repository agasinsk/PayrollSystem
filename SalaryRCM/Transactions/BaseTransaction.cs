namespace SalaryRCM.Transactions
{
    public abstract class BaseTransaction : ITransaction
    {
        protected PayrollDatabase payrollDatabase;

        protected BaseTransaction()
        {
            payrollDatabase = PayrollDatabase.GetInstance();
        }

        public abstract void Execute();
    }
}