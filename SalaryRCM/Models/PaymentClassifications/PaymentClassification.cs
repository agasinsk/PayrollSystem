using System;

namespace PayrollSystem.Models.PaymentClassifications
{
    public abstract class PaymentClassification
    {
        public abstract decimal CalculatePay(DateTime paycheckDate);
    }
}