using System;

namespace PayrollSystem.Models.PaymentClassifications
{
    public class SalariedPaymentClassification : PaymentClassification
    {
        public decimal Salary { get; set; }

        public SalariedPaymentClassification(decimal salary)
        {
            Salary = salary;
        }

        public override decimal CalculatePay(DateTime paycheckDate)
        {
            return Salary;
        }
    }
}