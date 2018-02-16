using System;
using PayrollSystem.Models.PaymentMethods;

namespace PayrollSystem.Models
{
    public class Paycheck
    {
        public double Deductions { get; set; }
        public PaymentMethodType Disposition { get; set; }
        public DateTime EndDate { get; set; }
        public double GrossPay { get; set; }
        public double NetPay => GrossPay - Deductions;
        public DateTime StartDate { get; set; }

        public Paycheck(DateTime startDate, DateTime endDate)
        {
            StartDate = startDate;
            EndDate = endDate;
        }

        public override string ToString()
        {
            return $"GrossPay: {GrossPay}, Deductions:{Deductions}, NetPay: {NetPay}, Disposition: {Disposition.ToString()}";
        }
    }
}