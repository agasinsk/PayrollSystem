using System;

namespace PayrollSystem.Models.PaymentSchedules
{
    public class BiweeklyPaymentSchedule : PaymentSchedule
    {
        public override bool IsPayDay(DateTime date)
        {
            return IsLastDayOfCycle(date);
        }

        private bool IsLastDayOfCycle(DateTime date)
        {
            var isLastDayOfWeek = date.DayOfWeek == DayOfWeek.Friday;
            var isLastDayOfMonth = DateTime.DaysInMonth(date.Year, date.Month) == date.Day;
            var isLastDayOfSecondWeek = DateTime.DaysInMonth(date.Year, date.Month) / 2 == date.Day;
            return isLastDayOfWeek && (isLastDayOfSecondWeek || isLastDayOfMonth);
        }
    }
}