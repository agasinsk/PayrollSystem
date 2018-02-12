using PayrollSystem.Models.PaymentMethods;

namespace PayrollSystem.Transactions.Employee.Changes
{
    public abstract class ChangeEmployeeMethodTransaction : ChangeEmployeeTransaction
    {
        protected ChangeEmployeeMethodTransaction(int employeeId) : base(employeeId)
        {
        }

        protected override void Change(Models.Employee employee)
        {
            employee.PaymentMethod = GetMethod();
        }

        protected abstract PaymentMethod GetMethod();
    }
}