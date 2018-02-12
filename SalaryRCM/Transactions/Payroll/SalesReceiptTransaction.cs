using System;
using SalaryRCM.Models;
using SalaryRCM.PaymentClassifications;

namespace SalaryRCM.Transactions.Payroll
{
    public class SalesReceiptTransaction : BaseTransaction
    {
        private readonly decimal amount;
        private readonly DateTime date;
        private readonly int employeeId;

        public SalesReceiptTransaction(DateTime date, decimal amount, int employeeId)
        {
            this.date = date;
            this.amount = amount;
            this.employeeId = employeeId;
        }

        public override void Execute()
        {
            var employee = payrollDatabase.GetEmployee(employeeId);
            if (employee == null)
            {
                throw new ApplicationException($"Employye of id {employeeId} cannot be found");
            }
            var paymentClassification = employee.PaymentClassification;
            if (!(paymentClassification is CommisionedPaymentClassification))
            {
                throw new ApplicationException(
                    $"Employye of id {employeeId} does not work in hourly payment classification!");
            }
            var salesReceipt = new SalesReceipt { Amount = amount, Date = date };

            (paymentClassification as CommisionedPaymentClassification).AddSalesReceipt(salesReceipt);
        }
    }
}