using System;
using PayrollSystem.Models;
using PayrollSystem.Models.PaymentClassifications;
using PayrollSystem.Models.PaymentMethods;

namespace PayrollSystem.Transactions.Payroll
{
    public class TimeCardTransaction : BaseTransaction
    {
        private readonly DateTime date;
        private readonly int employeeId;
        private readonly double hours;

        public TimeCardTransaction(int employeeId, DateTime date, double hours)
        {
            this.date = date;
            this.employeeId = employeeId;
            this.hours = hours;
        }

        public override void Execute()
        {
            var employee = payrollDatabase.GetEmployee(employeeId);
            if (employee == null)
            {
                throw new ApplicationException($"Employye of id {employeeId} cannot be found");
            }
            var paymentClassification = employee.PaymentClassification;
            if (!(paymentClassification is HourlyPaymentClassification))
            {
                throw new ApplicationException(
                    $"Employye of id {employeeId} does not work in hourly payment classification!");
            }
            var timeCard = new TimeCard { Hours = hours, Date = date };

            (paymentClassification as HourlyPaymentClassification).AddTimeCard(timeCard);
        }
    }
}