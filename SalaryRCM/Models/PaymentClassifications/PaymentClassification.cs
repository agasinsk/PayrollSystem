using System;

namespace PayrollSystem.Models.PaymentClassifications
{
    public abstract class PaymentClassification
    {
        public abstract double CalculatePay(Paycheck paycheck);
    }
}