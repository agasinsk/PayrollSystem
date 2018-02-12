using System;

namespace PayrollSystem.Models.PaymentMethods
{
    public class HoldPaymentMethod : PaymentMethod
    {
        public new static PaymentMethodType Type = PaymentMethodType.Hold;
        public string Address { get; }

        public HoldPaymentMethod(string address)
        {
            Address = address ?? throw new ArgumentNullException(nameof(address));
        }

        public override void Pay(Paycheck paycheck)
        {
            Console.WriteLine($"Paycheck {paycheck} has been given");
        }
    }
}