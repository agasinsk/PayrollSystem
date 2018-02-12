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
    }
}