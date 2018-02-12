using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PayrollSystem;
using PayrollSystem.Models;
using PayrollSystem.Models.PaymentMethods;
using PayrollSystem.Transactions.Employee;
using PayrollSystem.Transactions.Payday;

namespace PayrollSystemTests
{
    [TestClass]
    public class PaydayTransactionsTests
    {
        private PayrollDatabase payrollDatabase;

        [TestInitialize]
        public void SetUp()
        {
            payrollDatabase = PayrollDatabase.GetInstance();
        }

        [TestMethod]
        public void TestPaySingleSalariedEmployee()
        {
            // Arrange
            var employeeId = 1;
            var salary = 2500M;
            new AddSalariedEmployeeTransaction(employeeId, "Adam", "Address", salary).Execute();

            var date = DateTime.Parse("2001-01-31");
            var paydayTransaction = new PaydayTransaction(date);

            // Act
            paydayTransaction.Execute();
            var paycheck = paydayTransaction.GetPaycheck(employeeId);

            // Assert
            Assert.IsNotNull(paycheck);
            Assert.AreEqual(salary, paycheck.GrossPay);
            Assert.AreEqual(PaymentMethodType.Hold, paycheck.Disposition);
            Assert.AreEqual(salary, paycheck.NetPay);
            Assert.AreEqual(0M, paycheck.Deductions);
        }

        [TestMethod]
        public void TestpaySingleSalariedEmployeeOnWrongDate()
        {
            // Arrange
            var employeeId = 1;
            var salary = 2500M;
            new AddSalariedEmployeeTransaction(employeeId, "Adam", "Address", salary).Execute();

            var date = DateTime.Parse("2001-02-11");
            var paydayTransaction = new PaydayTransaction(date);

            // Act
            paydayTransaction.Execute();
            var paycheck = paydayTransaction.GetPaycheck(employeeId);

            // Assert
            Assert.IsNull(paycheck);
        }
    }
}