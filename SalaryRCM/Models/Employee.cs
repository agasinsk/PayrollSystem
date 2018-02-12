using System;
using PayrollSystem.Models.PaymentClassifications;
using PayrollSystem.Models.PaymentMethods;
using PayrollSystem.Models.PaymentSchedules;

namespace PayrollSystem.Models
{
    public class Employee
    {
        public string Address { get; set; }

        public Affiliation Affiliation { get; set; }

        public int Id { get; set; }

        public string Name { get; set; }

        public PaymentClassification PaymentClassification { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public PaymentSchedule PaymentSchedule { get; set; }

        public Employee(int id, string name, string address)
        {
            Id = id;
            Name = name;
            Address = address;
            Affiliation = new NoAffiliation();
        }

        public bool IsPayDay(DateTime date)
        {
            return PaymentSchedule.IsPayDay(date);
        }

        public void PayDay(Paycheck paycheck)
        {
            paycheck.GrossPay = PaymentClassification.CalculatePay(paycheck.Date);
            paycheck.Deductions = Affiliation.CalculatePay(paycheck.Date);

            PaymentMethod.Pay(paycheck);
        }
    }
}