using System;
using PayrollSystem.Models.PaymentMethods;

namespace PayrollSystem.Models
{
    public class Paycheck
    {
        public DateTime Date { get; set; }

        public double Deductions { get; set; }

        public PaymentMethodType Disposition { get; set; }

        public double GrossPay { get; set; }

        public double NetPay => GrossPay - Deductions;

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