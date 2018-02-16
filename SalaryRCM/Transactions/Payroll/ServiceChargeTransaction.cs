using System;
using PayrollSystem.Models;

namespace PayrollSystem.Transactions.Payroll
{
    public class ServiceChargeTransaction : BaseTransaction
    {
        private readonly double amount;
        private readonly DateTime date;
        private readonly int memberId;

        public ServiceChargeTransaction(int memberId, DateTime date, double amount)
        {
            this.memberId = memberId;
            this.date = date;
            this.amount = amount;
        }

        public override void Execute()
        {
            var employee = payrollDatabase.GetUnionMember(memberId);
            if (employee == null)
            {
                throw new ApplicationException($"Union member of id { memberId } cannot be found!");
            }

            var affiliation = employee.Affiliation;
            if (!(affiliation is UnionAffiliation))
            {
                throw new ApplicationException($"Union member of id { memberId } does not have any affiliation!");
            }
            var serviceCharge = new ServiceCharge { Date = date, Amount = amount };
            (affiliation as UnionAffiliation).AddServiceChange(serviceCharge);
        }
    }
}