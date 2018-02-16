using PayrollSystem.Models.PaymentClassification;
using PayrollSystem.Models.PaymentSchedule;

namespace PayrollSystem.Transactions.Employee
{
    public class AddCommisionedEmployeeTransaction : AddEmployeeTransaction
    {
        private readonly double commisionRate;
        private readonly double salary;

        public AddCommisionedEmployeeTransaction(int employeeId, string employeeName, string employeeAddress, double salary, double commisionRate) : base(employeeId, employeeName, employeeAddress)
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