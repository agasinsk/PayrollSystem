using PayrollSystem.Models.PaymentClassifications;
using PayrollSystem.Models.PaymentSchedules;

namespace PayrollSystem.Transactions.Employee.Changes
{
    public class ChangeEmployeeCommisionedClassificationTransaction : ChangeEmployeeClassificationTransaction
    {
        private readonly decimal commisionRate;
        private readonly decimal salary;

        public ChangeEmployeeCommisionedClassificationTransaction(int employeeId, decimal salary, decimal commisionRate) : base(employeeId)
        {
            this.commisionRate = commisionRate;
            this.salary = salary;
        }

        protected override PaymentClassification GetPaymentClassification()
        {
            return new CommisionedPaymentClassification(salary, commisionRate);
        }

        protected override PaymentSchedule GetPaymentSchedule()
        {
            return new BiweeklyPaymentSchedule();
        }
    }
}