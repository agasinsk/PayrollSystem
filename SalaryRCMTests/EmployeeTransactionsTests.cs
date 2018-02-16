using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PayrollSystem;
using PayrollSystem.Models.PaymentClassification;
using PayrollSystem.Models.PaymentMethod;
using PayrollSystem.Models.PaymentSchedule;
using PayrollSystem.Transactions.Employee;

namespace PayrollSystemTests
{
    [TestClass]
    public class EmployeeTransactionsTests
    {
        private IPayrollRepository payrollRepository;

        [TestInitialize]
        public void SetUp()
        {
            payrollRepository = PayrollRepository.GetInstance();
        }

        [TestMethod]
        public void TestAddCommisionedEmployee()
        {
            // Arrange
            var employeeId = 1;
            var employeeName = "Bogdan";
            var employeeAddress = "Address";
            var salary = 2500;
            var commisionRate = 100;

            // Act
            new AddCommisionedEmployeeTransaction(employeeId, employeeName, employeeAddress, salary, commisionRate).Execute();

            var employee = payrollRepository.GetEmployee(employeeId);

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
            var hourlyRate = 2500;

            // Act
            new AddHourlyEmployeeTransaction(employeeId, employeeName, employeeAddress, hourlyRate).Execute();

            var employee = payrollRepository.GetEmployee(employeeId);

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
            var salary = 2500;

            // Act
            new AddSalariedEmployeeTransaction(employeeId, employeeName, employeeAddress, salary).Execute();

            var employee = payrollRepository.GetEmployee(employeeId);

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
            var salary = 2500;
            var commisionRate = 100;

            // Act
            new AddCommisionedEmployeeTransaction(employeeId, employeeName, employeeAddress, salary, commisionRate).Execute();

            var employee = payrollRepository.GetEmployee(employeeId);

            new DeleteEmployeeTransaction(employeeId).Execute();
            var deletedEmployee = payrollRepository.GetEmployee(employeeId);

            // Assert
            Assert.IsNotNull(employee);
            Assert.IsNull(deletedEmployee);
        }
    }
}