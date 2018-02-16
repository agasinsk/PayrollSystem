namespace PayrollSystem.Models.PaymentClassification
{
    public class SalariedPaymentClassification : PaymentClassification
    {
        public double Salary { get; set; }

        public SalariedPaymentClassification(double salary)
        {
            Salary = salary;
        }

        public override double CalculatePay(Paycheck paycheck)
        {
            return Salary;
        }
    }
}