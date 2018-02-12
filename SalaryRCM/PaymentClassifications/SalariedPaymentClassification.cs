namespace SalaryRCM.PaymentClassifications
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