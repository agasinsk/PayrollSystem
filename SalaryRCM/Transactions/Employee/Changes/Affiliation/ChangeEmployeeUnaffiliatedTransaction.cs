using PayrollSystem.Models.Affiliation;

namespace PayrollSystem.Transactions.Employee.Changes.Affiliation
{
    public class ChangeEmployeeUnaffiliatedTransaction : ChangeEmployeeAffiliationTransaction
    {
        public ChangeEmployeeUnaffiliatedTransaction(int employeeId) : base(employeeId)
        {
        }

        protected override EmployeeAffiliation GetAffiliation()
        {
            return new NoEmployeeAffiliation();
        }

        protected override void RecordAffiliation(Models.Employee employee)
        {
            if (employee.Affiliation is UnionEmployeeAffiliation)
            {
                var memberId = (employee.Affiliation as UnionEmployeeAffiliation).MemberId;
                payrollRepository.DeleteUnionMember(memberId);
            }
        }
    }
}