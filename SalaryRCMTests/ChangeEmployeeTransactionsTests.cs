using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PayrollSystem;
using PayrollSystem.PaymentClassifications;
using PayrollSystem.PaymentMethods;
using PayrollSystem.PaymentSchedules;
using PayrollSystem.Transactions.Employee;
using PayrollSystem.Transactions.Employee.Changes;

namespace PayrollSystemTests
{
    [TestClass]
    public class ChangeEmployeeTransactionsTests
    {
        private PayrollDatabase payrollDatabase;

        [TestInitialize]
        public void SetUp()
        {
            payrollDatabase = PayrollDatabase.GetInstance();
        }

        [TestMethod]
        public void TestChangeAddressTransaction()
        {
            // Arrange
            var employeeId = 1;
            var employeeName = "Bogdan";
            var employeeAddress = "Address";
            var hourlyRate = 2500M;

            new AddHourlyEmployeeTransaction(employeeId, employeeName, employeeAddress, hourlyRate).Execute();

            const string newAddress = "new York";

            // Act
            new ChangeEmployeeAddressTransaction(employeeId, newAddress).Execute();
            var employee = payrollDatabase.GetEmployee(employeeId);

            // Assert
            Assert.IsNotNull(employee);
            Assert.AreEqual(newAddress, employee.Address);
        }

        [TestMethod]
        public void TestChangeCommisionedClassificationTransaction()
        {
            // Arrange
            var employeeId = 1;
            var employeeName = "Bogdan";
            var employeeAddress = "Address";
            var hourlyRate = 25M;

            new AddHourlyEmployeeTransaction(employeeId, employeeName, employeeAddress, hourlyRate).Execute();
            const decimal salary = 2500M;
            const decimal commisionRate = 15.5M;

            // Act
            new ChangeEmployeeCommisionedClassificationTransaction(employeeId, salary, commisionRate).Execute();
            var employee = payrollDatabase.GetEmployee(employeeId);

            // Assert
            Assert.IsNotNull(employee);
            Assert.IsTrue(employee.PaymentClassification is CommisionedPaymentClassification);
            Assert.AreEqual(salary, (employee.PaymentClassification as CommisionedPaymentClassification).Salary);
            Assert.AreEqual(commisionRate, (employee.PaymentClassification as CommisionedPaymentClassification).CommisionRate);
            Assert.IsTrue(employee.PaymentSchedule is BiweeklyPaymentSchedule);
        }

        [TestMethod]
        public void TestChangeHourlyClassificationTransaction()
        {
            // Arrange
            var employeeId = 1;
            var employeeName = "Bogdan";
            var employeeAddress = "Address";
            var salary = 2500M;

            new AddCommisionedEmployeeTransaction(employeeId, employeeName, employeeAddress, salary, 12.5M).Execute();
            const decimal hourlyRate = 27.52M;

            // Act
            new ChangeEmployeeHourlyClassificationTransaction(employeeId, hourlyRate).Execute();
            var employee = payrollDatabase.GetEmployee(employeeId);

            // Assert
            Assert.IsNotNull(employee);
            Assert.IsTrue(employee.PaymentClassification is HourlyPaymentClassification);
            Assert.AreEqual(hourlyRate, (employee.PaymentClassification as HourlyPaymentClassification).HourlyRate);
            Assert.IsTrue(employee.PaymentSchedule is WeeklyPaymentSchedule);
        }

        [TestMethod]
        public void TestChangeNameTransaction()
        {
            // Arrange
            var employeeId = 1;
            var employeeName = "Bogdan";
            var employeeAddress = "Address";
            var hourlyRate = 2500M;

            new AddHourlyEmployeeTransaction(employeeId, employeeName, employeeAddress, hourlyRate).Execute();

            const string newName = "Bartosz";

            // Act
            new ChangeEmployeeNameTransaction(employeeId, newName).Execute();
            var employee = payrollDatabase.GetEmployee(employeeId);

            // Assert
            Assert.IsNotNull(employee);
            Assert.AreEqual(newName, employee.Name);
        }

        [TestMethod]
        public void TestChangeSalariedClassificationTransaction()
        {
            // Arrange
            var employeeId = 1;
            var employeeName = "Bogdan";
            var employeeAddress = "Address";
            var hourlyRate = 25M;

            new AddHourlyEmployeeTransaction(employeeId, employeeName, employeeAddress, hourlyRate).Execute();
            const decimal salary = 500M;

            // Act
            new ChangeEmployeeSalariedClassificationTransaction(employeeId, salary).Execute();
            var employee = payrollDatabase.GetEmployee(employeeId);

            // Assert
            Assert.IsNotNull(employee);
            Assert.IsTrue(employee.PaymentClassification is SalariedPaymentClassification);
            Assert.AreEqual(salary, (employee.PaymentClassification as SalariedPaymentClassification).Salary);
            Assert.IsTrue(employee.PaymentSchedule is MonthlyPaymentSchedule);
        }
    }
}