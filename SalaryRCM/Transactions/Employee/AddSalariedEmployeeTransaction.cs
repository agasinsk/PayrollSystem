using PayrollSystem.Models.PaymentClassification;
using PayrollSystem.Models.PaymentSchedule;

namespace PayrollSystem.Transactions.Employee
{
    public class AddSalariedEmployeeTransaction : AddEmployeeTransaction
    {
        private readonly double salary;

        public AddSalariedEmployeeTransaction(int employeeId, string employeeName, string employeeAddress, double salary) : base(employeeId, employeeName, employeeAddress)
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