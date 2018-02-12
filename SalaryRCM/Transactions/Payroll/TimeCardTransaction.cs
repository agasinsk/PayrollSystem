using System;
using PayrollSystem.Models;
using PayrollSystem.PaymentMethods;

namespace PayrollSystem.Transactions.Payroll
{
    public class TimeCardTransaction : BaseTransaction
    {
        private DateTime date;
        private int employeeId;
        private int hours;

        public TimeCardTransaction(DateTime date, int employeeId, int hours)
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