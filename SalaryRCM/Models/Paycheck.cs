using System;
using PayrollSystem.Models.PaymentMethods;

namespace PayrollSystem.Models
{
    public class Paycheck
    {
        public DateTime Date { get; set; }

        public decimal Deductions { get; set; }

        public PaymentMethodType Disposition { get; set; }

        public decimal GrossPay { get; set; }

        public decimal NetPay => GrossPay - Deductions;

        public Paycheck(DateTime date)
        {
            Date = date;
        }

        public override string ToString()
        {
            return $"GrossPay: {GrossPay}, Deductions:{Deductions}, NetPay: {NetPay}, Disposition: {Disposition.ToString()}";
        }
    }
}