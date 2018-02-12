using System;
using PayrollSystem.PaymentClassifications;
using PayrollSystem.PaymentMethods;
using PayrollSystem.PaymentSchedules;

namespace PayrollSystem.Transactions.Employee.Changes
{
    public class ChangeEmployeeSalariedClassificationTransaction : ChangeEmployeeClassificationTransaction
    {
        private readonly decimal salary;

        public ChangeEmployeeSalariedClassificationTransaction(int employeeId, decimal salary) : base(employeeId)
        {
            this.salary = salary;
        }

        protected override PaymentClassification GetPaymentClassification()
        {
            return new SalariedPaymentClassification(salary);
        }

        protected override PaymentSchedule GetPaymentSchedule()
        {
            return new MonthlyPaymentSchedule();
        }
    }
}