using System;

namespace SalaryRCM
{
    public class SalariedPaymentClassification : PaymentClassification
    {
        public decimal Salary { get; set; }

        public SalariedPaymentClassification(decimal salary)
        {
            Salary = salary;
        }
    }
}