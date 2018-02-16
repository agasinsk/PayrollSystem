using PayrollSystem.Models.PaymentClassification;
using PayrollSystem.Models.PaymentSchedule;

namespace PayrollSystem.Transactions.Employee.Changes.Classification
{
    public class ChangeEmployeeHourlyClassificationTransaction : ChangeEmployeeClassificationTransaction
    {
        private readonly double hourlyRate;

        public ChangeEmployeeHourlyClassificationTransaction(int employeeId, double hourlyRate) : base(employeeId)
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