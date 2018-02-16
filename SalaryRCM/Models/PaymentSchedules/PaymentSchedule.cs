using System;

namespace PayrollSystem.Models.PaymentSchedules
{
    public abstract class PaymentSchedule
    {
        public abstract DateTime GetPayPeriodStartDate(DateTime date);

        public abstract bool IsPayDay(DateTime date);
    }
}