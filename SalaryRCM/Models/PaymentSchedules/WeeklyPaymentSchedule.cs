using System;
using PayrollSystem.Extensions;

namespace PayrollSystem.Models.PaymentSchedules
{
    public class WeeklyPaymentSchedule : PaymentSchedule
    {
        public override DateTime GetPayPeriodStartDate(DateTime date)
        {
            return date.StartOfWeek();
        }

        public override bool IsPayDay(DateTime date)
        {
            return IsLastDayOfWeek(date);
        }

        private bool IsLastDayOfWeek(DateTime date)
        {
            return date.DayOfWeek == DayOfWeek.Friday;
        }
    }
}