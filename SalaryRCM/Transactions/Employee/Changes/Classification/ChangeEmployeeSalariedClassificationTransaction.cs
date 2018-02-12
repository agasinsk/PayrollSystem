using PayrollSystem.Models.PaymentClassifications;
using PayrollSystem.Models.PaymentSchedules;

namespace PayrollSystem.Transactions.Employee.Changes.Classification
{
    public class ChangeEmployeeSalariedClassificationTransaction : ChangeEmployeeClassificationTransaction
    {
        private readonly decimal salary;

        public ChangeEmployeeSalariedClassificationTransaction(int employeeId, decimal salary) : base(employeeId)
        {
            this.salary = salary;
        }

        protected override PaymentClassification GetPaymentClassification()
        {
            return new SalariedPaymentClassification(salary);
        }

        protected override PaymentSchedule GetPaymentSchedule()
        {
            return new MonthlyPaymentSchedule();
        }
    }
}