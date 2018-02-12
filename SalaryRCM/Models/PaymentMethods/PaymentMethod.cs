namespace PayrollSystem.Models.PaymentMethods
{
    public abstract class PaymentMethod
    {
        public static PaymentMethodType Type;

        public abstract void Pay(Paycheck paycheck);
    }
}