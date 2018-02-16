using System;
using PayrollSystem.Models;
using PayrollSystem.Models.PaymentClassification;

namespace PayrollSystem.Transactions.Payroll
{
    public class SalesReceiptTransaction : BaseTransaction
    {
        private readonly double amount;
        private readonly DateTime date;
        private readonly int employeeId;

        public SalesReceiptTransaction(int employeeId, DateTime date, double amount)
        {
            this.date = date;
            this.amount = amount;
            this.employeeId = employeeId;
        }

        public override void Execute()
        {
            var employee = payrollRepository.GetEmployee(employeeId);
            if (employee == null)
            {
                throw new ApplicationException($"Employye of id {employeeId} cannot be found");
            }
            var paymentClassification = employee.PaymentClassification;
            if (!(paymentClassification is CommisionedPaymentClassification))
            {
                throw new ApplicationException(
                    $"Employye of id {employeeId} does not work in commisioned payment classification!");
            }
            var salesReceipt = new SalesReceipt { Amount = amount, Date = date };

            (paymentClassification as CommisionedPaymentClassification).AddSalesReceipt(salesReceipt);
        }
    }
}