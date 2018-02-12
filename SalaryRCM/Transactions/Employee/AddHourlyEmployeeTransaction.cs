using PayrollSystem.PaymentClassifications;
using PayrollSystem.PaymentMethods;
using PayrollSystem.PaymentSchedules;

namespace PayrollSystem.Transactions.Employee
{
    public class AddHourlyEmployeeTransaction : AddEmployeeTransaction
    {
        private readonly decimal hourlyRate;

        public AddHourlyEmployeeTransaction(int employeeId, string employeeName, string employeeAddress, decimal hourlyRate) : base(employeeId, employeeName, employeeAddress)
        {
            this.hourlyRate = hourlyRate;
        }

        protected override PaymentClassification GetPaymentClassification()
        {
            return new HourlyPaymentClassification(hourlyRate);
        }

        protected override PaymentSchedule GetPaymentSchedule()
        {
            return new WeeklyPaymentSchedule();
        }
    }
}