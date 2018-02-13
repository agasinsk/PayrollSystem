using System;

namespace PayrollSystem.Models.PaymentClassifications
{
    public class SalariedPaymentClassification : PaymentClassification
    {
        public double Salary { get; set; }

        public SalariedPaymentClassification(double salary)
        {
            Salary = salary;
        }

        public override double CalculatePay(DateTime paycheckDate)
        {
            return Salary;
        }
    }
}