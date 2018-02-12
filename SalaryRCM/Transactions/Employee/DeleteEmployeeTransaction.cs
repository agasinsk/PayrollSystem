namespace SalaryRCM.Transactions.Employee
{
    public class DeleteEmployeeTransaction : BaseTransaction
    {
        private readonly int employeeId;

        public DeleteEmployeeTransaction(int employeeId)
        {
            this.employeeId = employeeId;
        }

        public override void Execute()
        {
            payrollDatabase.DeleteEmployee(employeeId);
        }
    }
}