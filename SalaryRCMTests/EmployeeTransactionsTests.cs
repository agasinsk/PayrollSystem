using Microsoft.VisualStudio.TestTools.UnitTesting;
using PayrollSystem;
using PayrollSystem.PaymentClassifications;
using PayrollSystem.PaymentMethods;
using PayrollSystem.PaymentSchedules;
using PayrollSystem.Transactions.Employee;

namespace PayrollSystemTests
{
    [TestClass]
    public class EmployeeTransactionsTests
    {
        private PayrollDatabase payrollDatabase;

        [TestInitialize]
        public void SetUp()
        {
            payrollDatabase = PayrollDatabase.GetInstance();
        }

        [TestMethod]
        public void TestAddCommisionedEmployee()
        {
            // Arrange
            var employeeId = 1;
            var employeeName = "Bogdan";
            var employeeAddress = "Address";
            var salary = 2500M;
            var commisionRate = 100M;

            // Act
            new AddCommisionedEmployeeTransaction(employeeId, employeeName, employeeAddress, salary, commisionRate).Execute();

            var employee = payrollDatabase.GetEmployee(employeeId);

            // Assert
            Assert.IsTrue(employee.PaymentClassification is CommisionedPaymentClassification);
            Assert.AreEqual(salary, (employee.PaymentClassification as CommisionedPaymentClassification).Salary);
            Assert.AreEqual(commisionRate, (employee.PaymentClassification as CommisionedPaymentClassification).CommisionRate);
            Assert.IsTrue(employee.PaymentSchedule is BiweeklyPaymentSchedule);
            Assert.IsTrue(employee.PaymentMethod is HoldPaymentMethod);
        }

        [TestMethod]
        public void TestAddHourlyEmployee()
        {
            // Arrange
            var employeeId = 1;
            var employeeName = "Bogdan";
            var employeeAddress = "Address";
            var hourlyRate = 2500M;

            // Act
            new AddHourlyEmployeeTransaction(employeeId, employeeName, employeeAddress, hourlyRate).Execute();

            var employee = payrollDatabase.GetEmployee(employeeId);

            // Assert
            Assert.IsTrue(employee.PaymentClassification is HourlyPaymentClassification);
            Assert.AreEqual(hourlyRate, (employee.PaymentClassification as HourlyPaymentClassification).HourlyRate);
            Assert.IsTrue(employee.PaymentSchedule is WeeklyPaymentSchedule);
            Assert.IsTrue(employee.PaymentMethod is HoldPaymentMethod);
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

        [TestMethod]
        public void TestDeleteEmployee()
        {
            // Arrange
            var employeeId = 1;
            var employeeName = "Bogdan";
            var employeeAddress = "Address";
            var salary = 2500M;
            var commisionRate = 100M;

            // Act
            new AddCommisionedEmployeeTransaction(employeeId, employeeName, employeeAddress, salary, commisionRate).Execute();

            var employee = payrollDatabase.GetEmployee(employeeId);

            new DeleteEmployeeTransaction(employeeId).Execute();
            var deletedEmployee = payrollDatabase.GetEmployee(employeeId);

            // Assert
            Assert.IsNotNull(employee);
            Assert.IsNull(deletedEmployee);
        }
    }
}