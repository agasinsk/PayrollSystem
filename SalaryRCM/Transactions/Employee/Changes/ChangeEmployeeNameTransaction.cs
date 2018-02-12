namespace PayrollSystem.Transactions.Employee.Changes
{
    public class ChangeEmployeeNameTransaction : ChangeEmployeeTransaction
    {
        private readonly string newName;

        public ChangeEmployeeNameTransaction(int employeeId, string newName) : base(employeeId)
        {
            this.newName = newName;
        }

        protected override void Change(Models.Employee employee)
        {
            employee.Name = newName;
        }
    }
}