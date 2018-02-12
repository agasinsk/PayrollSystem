using SalaryRCM.PaymentClassifications;
using SalaryRCM.PaymentMethods;
using SalaryRCM.PaymentSchedules;

namespace SalaryRCM.Transactions.Employee
{
    public abstract class AddEmployeeTransaction : BaseTransaction
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
            var employee = new Models.Employee(employeeId, employeeName, employeeAddress)
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