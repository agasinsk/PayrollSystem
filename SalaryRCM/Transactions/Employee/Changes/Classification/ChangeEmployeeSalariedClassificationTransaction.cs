using PayrollSystem.Models.PaymentClassifications;
using PayrollSystem.Models.PaymentSchedules;

namespace PayrollSystem.Transactions.Employee.Changes.Classification
{
    public class ChangeEmployeeSalariedClassificationTransaction : ChangeEmployeeClassificationTransaction
    {
        private readonly double salary;

        public ChangeEmployeeSalariedClassificationTransaction(int employeeId, double salary) : base(employeeId)
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