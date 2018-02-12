using System;
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
            throw new NotImplementedException();
        }

        protected abstract PaymentMethod GetMethod();
    }
}