using System;
using PayrollSystem.Extensions;

namespace PayrollSystem.Models.PaymentSchedules
{
    public class MonthlyPaymentSchedule : PaymentSchedule
    {
        public override DateTime GetPayPeriodStartDate(DateTime date)
        {
            return date.StartOfMonth();
        }

        public override bool IsPayDay(DateTime date)
        {
            return IsLastDayOfMonth(date);
        }

        private bool IsLastDayOfMonth(DateTime date)
        {
            return DateTime.DaysInMonth(date.Year, date.Month) == date.Day;
        }
    }
}