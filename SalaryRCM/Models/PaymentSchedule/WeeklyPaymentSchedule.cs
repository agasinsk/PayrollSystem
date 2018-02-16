using System;
using PayrollSystem.Extensions;

namespace PayrollSystem.Models.PaymentSchedule
{
    public class WeeklyPaymentSchedule : PaymentSchedule
    {
        public override DateTime GetPayPeriodStartDate(DateTime date)
        {
            return date.StartOfWeek();
        }

        public override bool IsPayDay(DateTime date)
        {
            return date.IsFriday();
        }
    }
}