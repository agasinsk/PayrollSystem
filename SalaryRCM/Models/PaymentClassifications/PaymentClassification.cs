using System;

namespace PayrollSystem.Models.PaymentClassifications
{
    public abstract class PaymentClassification
    {
        public abstract double CalculatePay(Paycheck paycheck);

        public bool IsInPayPeriod(DateTime date, Paycheck paycheck)
        {
            var payPeriodEndDate = paycheck.EndDate;
            var payPeriodStartDate = paycheck.StartDate;

            return date >= payPeriodStartDate && date <= payPeriodEndDate;
        }
    }
}