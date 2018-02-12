using PayrollSystem.Models.PaymentMethods;

namespace PayrollSystem.Transactions.Employee.Changes
{
    public class ChangeEmployeeMailMethodTransaction : ChangeEmployeeMethodTransaction
    {
        private readonly string address;

        public ChangeEmployeeMailMethodTransaction(int employeeId, string address) : base(employeeId)
        {
            this.address = address;
        }

        protected override PaymentMethod GetMethod()
        {
            return new MailPaymentMethod(address);
        }
    }
}