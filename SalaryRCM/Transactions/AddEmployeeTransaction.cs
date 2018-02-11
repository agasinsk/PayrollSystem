using System;
using System.Collections.Generic;
using System.Text;

namespace SalaryRCM.Transactions
{
    public abstract partial class AddEmployeeTransaction : BaseTransaction
    {
        private readonly string employeeAddress;
        private readonly int employeeId;
        private readonly string employeeName;

        protected AddEmployeeTransaction(int employeeId, string employeeName, string employeeAddress)
        {
            this.employeeId = employeeId;
            this.employeeAddress = employeeAddress;
            this.employeeName = employeeName;
        }

        public override void Execute()
        {
            var paymentClassification = GetPaymentClassification();
            var paymentSchedule = GetPaymentSchedule();
            PaymentMethod paymentMethod = new HoldPaymentMethod();
            var employee = new Employee(employeeId, employeeName, employeeAddress)
            {
                PaymentClassification = paymentClassification,
                PaymentSchedule = paymentSchedule,
                PaymentMethod = paymentMethod
            };
            payrollDatabase.AddEmployee(employeeId, employee);
        }

        protected abstract PaymentClassification GetPaymentClassification();

        protected abstract PaymentSchedule GetPaymentSchedule();
    }
}