using PayrollSystem.Models.PaymentClassifications;
using PayrollSystem.Models.PaymentMethods;
using PayrollSystem.Models.PaymentSchedules;

namespace PayrollSystem.Transactions.Employee
{
    public class AddHourlyEmployeeTransaction : AddEmployeeTransaction
    {
        private readonly double hourlyRate;

        public AddHourlyEmployeeTransaction(int employeeId, string employeeName, string employeeAddress, double hourlyRate) : base(employeeId, employeeName, employeeAddress)
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