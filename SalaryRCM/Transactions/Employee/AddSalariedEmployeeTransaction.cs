using PayrollSystem.Models.PaymentClassifications;
using PayrollSystem.Models.PaymentSchedules;

namespace PayrollSystem.Transactions.Employee
{
    public partial class AddSalariedEmployeeTransaction : AddEmployeeTransaction
    {
        private readonly decimal salary;

        public AddSalariedEmployeeTransaction(int employeeId, string employeeName, string employeeAddress, decimal salary) : base(employeeId, employeeName, employeeAddress)
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