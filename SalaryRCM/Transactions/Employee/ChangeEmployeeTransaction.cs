using System;

namespace PayrollSystem.Transactions.Employee
{
    public abstract class ChangeEmployeeTransaction : BaseTransaction
    {
        private readonly int employeeId;

        protected ChangeEmployeeTransaction(int employeeId)
        {
            this.employeeId = employeeId;
        }

        public override void Execute()
        {
            var employee = payrollDatabase.GetEmployee(employeeId);
            if (employee == null)
            {
                throw new ApplicationException($"Employee of id {employeeId} cannot be found");
            }
            Change(employee);
        }

        protected abstract void Change(Models.Employee employee);
    }
}