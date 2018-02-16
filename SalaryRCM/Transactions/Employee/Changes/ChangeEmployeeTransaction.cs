using System;

namespace PayrollSystem.Transactions.Employee.Changes
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
            var employee = payrollRepository.GetEmployee(employeeId);
            if (employee == null)
            {
                throw new ApplicationException($"Employee of id {employeeId} cannot be found");
            }
            Change(employee);
        }

        protected abstract void Change(Models.Employee employee);
    }
}