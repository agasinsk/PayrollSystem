using PayrollSystem.Models;

namespace PayrollSystem.Transactions.Employee.Changes.Affiliation
{
    public class ChangeEmployeeUnaffiliatedTransaction : ChangeEmployeeAffiliationTransaction
    {
        public ChangeEmployeeUnaffiliatedTransaction(int employeeId) : base(employeeId)
        {
        }

        protected override Models.Affiliation GetAffiliation()
        {
            return new NoAffiliation();
        }

        protected override void RecordAffiliation(Models.Employee employee)
        {
            if (employee.Affiliation is UnionAffiliation)
            {
                var memberId = (employee.Affiliation as UnionAffiliation).MemberId;
                payrollDatabase.DeleteUnionMember(memberId);
            }
        }
    }
}