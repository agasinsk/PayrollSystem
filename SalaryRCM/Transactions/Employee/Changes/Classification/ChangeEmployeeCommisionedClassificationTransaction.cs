using PayrollSystem.Models.PaymentClassification;
using PayrollSystem.Models.PaymentSchedule;

namespace PayrollSystem.Transactions.Employee.Changes.Classification
{
    public class ChangeEmployeeCommisionedClassificationTransaction : ChangeEmployeeClassificationTransaction
    {
        private readonly double commisionRate;
        private readonly double salary;

        public ChangeEmployeeCommisionedClassificationTransaction(int employeeId, double salary, double commisionRate) : base(employeeId)
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