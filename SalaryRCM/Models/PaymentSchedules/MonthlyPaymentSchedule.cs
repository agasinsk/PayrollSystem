using System;

namespace PayrollSystem.Models.PaymentSchedules
{
    public class MonthlyPaymentSchedule : PaymentSchedule
    {
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