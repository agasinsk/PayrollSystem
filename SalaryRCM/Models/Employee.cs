using System;
using PayrollSystem.Models.Affiliation;

namespace PayrollSystem.Models
{
    public class Employee
    {
        public string Address { get; set; }

        public EmployeeAffiliation Affiliation { get; set; }

        public int Id { get; set; }

        public string Name { get; set; }

        public PaymentClassification.PaymentClassification PaymentClassification { get; set; }

        public PaymentMethod.PaymentMethod PaymentMethod { get; set; }

        public PaymentSchedule.PaymentSchedule PaymentSchedule { get; set; }

        public Employee(int id, string name, string address)
        {
            Id = id;
            Name = name;
            Address = address;
            Affiliation = new NoEmployeeAffiliation();
        }

        public DateTime GetPayPeriodStartDate(DateTime date)
        {
            return PaymentSchedule.GetPayPeriodStartDate(date);
        }

        public bool IsPayDay(DateTime date)
        {
            return PaymentSchedule.IsPayDay(date);
        }

        public void PayDay(Paycheck paycheck)
        {
            paycheck.GrossPay = PaymentClassification.CalculatePay(paycheck);
            paycheck.Deductions = Affiliation.CalculateDeductions(paycheck);

            PaymentMethod.Pay(paycheck);
        }
    }
}