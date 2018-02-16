using System;

namespace PayrollSystem.Models.PaymentSchedule
{
    public abstract class PaymentSchedule
    {
        public abstract DateTime GetPayPeriodStartDate(DateTime date);

        public abstract bool IsPayDay(DateTime date);
    }
}