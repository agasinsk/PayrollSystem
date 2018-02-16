namespace PayrollSystem.Models.PaymentMethod
{
    public abstract class PaymentMethod
    {
        public static PaymentMethodType Type;

        public abstract void Pay(Paycheck paycheck);
    }
}