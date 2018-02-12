using System;

namespace PayrollSystem.Models.PaymentMethods
{
    public class MailPaymentMethod : PaymentMethod
    {
        public string Address { get; }

        public MailPaymentMethod(string address)
        {
            Address = address ?? throw new ArgumentNullException(nameof(address));
        }
    }
}