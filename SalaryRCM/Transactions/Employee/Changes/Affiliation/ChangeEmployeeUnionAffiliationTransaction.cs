using PayrollSystem.Models.Affiliation;

namespace PayrollSystem.Transactions.Employee.Changes.Affiliation
{
    public class ChangeEmployeeUnionAffiliationTransaction : ChangeEmployeeAffiliationTransaction
    {
        private readonly double dues;
        private readonly int memberId;

        public ChangeEmployeeUnionAffiliationTransaction(int employeeId, int memberId, double dues) : base(employeeId)
        {
            this.dues = dues;
            this.memberId = memberId;
        }

        protected override EmployeeAffiliation GetAffiliation()
        {
            return new UnionEmployeeAffiliation(memberId, dues);
        }

        protected override void RecordAffiliation(Models.Employee employee)
        {
            payrollRepository.AddUnionMember(memberId, employee);
        }
    }
}