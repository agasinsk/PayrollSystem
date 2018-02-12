using PayrollSystem.Models.PaymentMethods;

namespace PayrollSystem.Transactions.Employee.Changes
{
    public class ChangeEmployeeDirectMethodTransaction : ChangeEmployeeMethodTransaction
    {
        private readonly string account;
        private readonly string bank;

        public ChangeEmployeeDirectMethodTransaction(int employeeId, string bank, string account) : base(employeeId)
        {
            this.bank = bank;
            this.account = account;
        }

        protected override PaymentMethod GetMethod()
        {
            return new DirectPaymentMethod(bank, account);
        }
    }
}