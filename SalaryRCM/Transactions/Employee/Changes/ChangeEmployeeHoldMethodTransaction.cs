using PayrollSystem.Models.PaymentMethods;

namespace PayrollSystem.Transactions.Employee.Changes
{
    public class ChangeEmployeeHoldMethodTransaction : ChangeEmployeeMethodTransaction
    {
        private readonly string address;

        public ChangeEmployeeHoldMethodTransaction(int employeeId, string address) : base(employeeId)
        {
            this.address = address;
        }

        protected override PaymentMethod GetMethod()
        {
            return new HoldPaymentMethod(address);
        }
    }
}