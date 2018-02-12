namespace PayrollSystem.Transactions.Employee.Changes.Affiliation
{
    public abstract class ChangeEmployeeAffiliationTransaction : ChangeEmployeeTransaction
    {
        protected ChangeEmployeeAffiliationTransaction(int employeeId) : base(employeeId)
        {
        }

        protected override void Change(Models.Employee employee)
        {
            RecordAffiliation(employee);
            employee.Affiliation = GetAffiliation();
        }

        protected abstract Models.Affiliation GetAffiliation();

        protected abstract void RecordAffiliation(Models.Employee employee);
    }
}