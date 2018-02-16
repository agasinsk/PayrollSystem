namespace PayrollSystem.Transactions
{
    public abstract class BaseTransaction : ITransaction
    {
        protected IPayrollRepository payrollRepository;

        protected BaseTransaction()
        {
            payrollRepository = PayrollRepository.GetInstance();
        }

        public abstract void Execute();
    }
}