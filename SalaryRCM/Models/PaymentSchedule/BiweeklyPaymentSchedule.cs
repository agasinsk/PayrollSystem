using System;
using PayrollSystem.Extensions;

namespace PayrollSystem.Models.PaymentSchedule
{
    public class BiweeklyPaymentSchedule : PaymentSchedule
    {
        public override DateTime GetPayPeriodStartDate(DateTime date)
        {
            return date.StartOfWeek();
        }

        public override bool IsPayDay(DateTime date)
        {
            return IsLastDayOfCycle(date);
        }

        private bool IsLastDayOfCycle(DateTime date)
        {
            return date.IsFriday() && date.IsSecondFriday();
        }
    }
}