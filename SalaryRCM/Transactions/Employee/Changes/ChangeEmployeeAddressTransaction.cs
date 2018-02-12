namespace PayrollSystem.Transactions.Employee.Changes
{
    public class ChangeEmployeeAddressTransaction : ChangeEmployeeTransaction
    {
        private readonly string newAddress;

        public ChangeEmployeeAddressTransaction(int employeeId, string newAddress) : base(employeeId)
        {
            this.newAddress = newAddress;
        }

        protected override void Change(Models.Employee employee)
        {
            employee.Address = newAddress;
        }
    }
}