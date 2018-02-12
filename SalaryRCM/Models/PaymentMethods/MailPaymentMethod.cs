using System;

namespace PayrollSystem.Models.PaymentMethods
{
    public class MailPaymentMethod : PaymentMethod
    {
        public new static PaymentMethodType Type = PaymentMethodType.Mail;
        public string Address { get; }

        public MailPaymentMethod(string address)
        {
            Address = address ?? throw new ArgumentNullException(nameof(address));
        }

        public override void Pay(Paycheck paycheck)
        {
            throw new NotImplementedException();
        }
    }
}