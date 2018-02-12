using PayrollSystem.Models.PaymentClassifications;
using PayrollSystem.Models.PaymentSchedules;

namespace PayrollSystem.Transactions.Employee
{
    public class AddCommisionedEmployeeTransaction : AddEmployeeTransaction
    {
        private readonly decimal commisionRate;
        private readonly decimal salary;

        public AddCommisionedEmployeeTransaction(int employeeId, string employeeName, string employeeAddress, decimal salary, decimal commisionRate) : base(employeeId, employeeName, employeeAddress)
        {
            this.salary = salary;
            this.commisionRate = commisionRate;
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