using System;

namespace PayrollSystem.Models.PaymentSchedules
{
    public abstract class PaymentSchedule
    {
        public abstract bool IsPayDay(DateTime date);
    }
}