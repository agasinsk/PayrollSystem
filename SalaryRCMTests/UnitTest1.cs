using Microsoft.VisualStudio.TestTools.UnitTesting;
using SalaryRCM;
using SalaryRCM.Transactions;

namespace SalaryRCMTests
{
    [TestClass]
    public class UnitTest1
    {
        private PayrollDatabase payrollDatabase;

        [TestInitialize]
        public void SetUp()
        {
            payrollDatabase = PayrollDatabase.GetInstance();
        }

        [TestMethod]
        public void TestAddSalariedEmployee()
        {
            // Arrange
            var employeeId = 1;
            var employeeName = "Bogdan";
            var employeeAddress = "Address";
            var salary = 2500M;

            // Act
            new AddSalariedEmployeeTransaction(employeeId, employeeName, employeeAddress, salary).Execute();

            var employee = payrollDatabase.GetEmployee(employeeId);

            // Assert
            Assert.IsTrue(employee.PaymentClassification is SalariedPaymentClassification);
            Assert.AreEqual(salary, (employee.PaymentClassification as SalariedPaymentClassification).Salary);
            Assert.IsTrue(employee.PaymentSchedule is MonthlyPaymentSchedule);
            Assert.IsTrue(employee.PaymentMethod is HoldPaymentMethod);
        }
    }
}