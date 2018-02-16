using System;
using PayrollSystem.Extensions;

namespace PayrollSystem.Models.PaymentSchedule
{
    public class MonthlyPaymentSchedule : PaymentSchedule
    {
        public override DateTime GetPayPeriodStartDate(DateTime date)
        {
            return date.StartOfMonth();
        }

        public override bool IsPayDay(DateTime date)
        {
            return date.IsLastDayOfMonth();
        }
    }
}