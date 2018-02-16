namespace PayrollSystem.Models.PaymentClassification
{
    public abstract class PaymentClassification
    {
        public abstract double CalculatePay(Paycheck paycheck);
    }
}