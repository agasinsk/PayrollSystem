using PayrollSystem.Models.PaymentMethods;

namespace PayrollSystem.Transactions.Employee.Changes.Method
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