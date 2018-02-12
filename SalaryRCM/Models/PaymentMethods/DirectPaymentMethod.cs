using System;

namespace PayrollSystem.Models.PaymentMethods
{
    public class DirectPaymentMethod : PaymentMethod
    {
        public new static PaymentMethodType Type = PaymentMethodType.Direct;

        public string Account { get; }

        public string Bank { get; }

        public DirectPaymentMethod(string bank, string account)
        {
            Bank = bank ?? throw new ArgumentNullException(nameof(bank));
            Account = account ?? throw new ArgumentNullException(nameof(account));
        }

        public override void Pay(Paycheck paycheck)
        {
            throw new NotImplementedException();
        }
    }
}