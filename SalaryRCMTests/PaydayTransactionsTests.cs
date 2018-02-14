using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PayrollSystem;
using PayrollSystem.Models.PaymentMethods;
using PayrollSystem.Transactions.Employee;
using PayrollSystem.Transactions.Payday;
using PayrollSystem.Transactions.Payroll;

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
        public void TestPaySingleCommisionedEmployeeOneSalesReceipt()
        {
            // Arrange
            var employeeId = 1;
            var commisionRate = 0.25;
            var salary = 2400;
            new AddCommisionedEmployeeTransaction(employeeId, "Adam", "Address", salary, commisionRate).Execute();

            var payPeriodDate = DateTime.Parse("2018-02-13");
            var salesAmount = 200;
            new SalesReceiptTransaction(employeeId, payPeriodDate, salesAmount).Execute();

            var fridayDate = DateTime.Parse("2018-02-16");
            var paydayTransaction = new PaydayTransaction(fridayDate);

            // Act
            paydayTransaction.Execute();
            var paycheck = paydayTransaction.GetPaycheck(employeeId);
            var expectedPay = salary + salesAmount * commisionRate;

            // Assert
            Assert.IsNotNull(paycheck);
            Assert.AreEqual(fridayDate, paycheck.Date);
            Assert.AreEqual(expectedPay, paycheck.GrossPay);
            Assert.AreEqual(PaymentMethodType.Hold, paycheck.Disposition);
            Assert.AreEqual(0, paycheck.Deductions);
            Assert.AreEqual(expectedPay, paycheck.NetPay);
        }

        [TestMethod]
        public void TestPaySingleCommisionedEmployeeOnWrongDate()
        {
            // Arrange
            var employeeId = 1;
            var commisionRate = 0.25;
            var salary = 2400;
            new AddCommisionedEmployeeTransaction(employeeId, "Adam", "Address", salary, commisionRate).Execute();

            var payPeriodDate = DateTime.Parse("2018-02-13");
            var salesAmount = 200;
            new SalesReceiptTransaction(employeeId, payPeriodDate, salesAmount).Execute();

            var thursdayDate = DateTime.Parse("2018-02-15");
            var paydayTransaction = new PaydayTransaction(thursdayDate);

            // Act
            paydayTransaction.Execute();
            var paycheck = paydayTransaction.GetPaycheck(employeeId);

            // Assert
            Assert.IsNull(paycheck);
        }

        [TestMethod]
        public void TestPaySingleHourlyEmployeeNoTimeCards()
        {
            // Arrange
            var employeeId = 1;
            var hourlyRate = 25;
            new AddHourlyEmployeeTransaction(employeeId, "Adam", "Address", hourlyRate).Execute();

            var fridayDate = DateTime.Parse("2018-02-16");
            var paydayTransaction = new PaydayTransaction(fridayDate);

            // Act
            paydayTransaction.Execute();
            var paycheck = paydayTransaction.GetPaycheck(employeeId);

            // Assert
            Assert.IsNotNull(paycheck);
            Assert.AreEqual(fridayDate, paycheck.Date);
            Assert.AreEqual(0, paycheck.GrossPay);
            Assert.AreEqual(PaymentMethodType.Hold, paycheck.Disposition);
            Assert.AreEqual(0, paycheck.Deductions);
            Assert.AreEqual(0, paycheck.NetPay);
        }

        [TestMethod]
        public void TestPaySingleHourlyEmployeeOneTimeCard()
        {
            // Arrange
            var employeeId = 1;
            var hourlyRate = 15.25;

            new AddHourlyEmployeeTransaction(employeeId, "Adam", "Address", hourlyRate).Execute();

            var fridayDate = DateTime.Parse("2018-02-16");
            var hours = 2;
            new TimeCardTransaction(employeeId, fridayDate, hours).Execute();
            var paydayTransaction = new PaydayTransaction(fridayDate);

            // Act
            paydayTransaction.Execute();
            var paycheck = paydayTransaction.GetPaycheck(employeeId);
            var expectedPay = hourlyRate * hours;

            // Assert
            Assert.IsNotNull(paycheck);
            Assert.AreEqual(fridayDate, paycheck.Date);
            Assert.AreEqual(expectedPay, paycheck.GrossPay);
            Assert.AreEqual(PaymentMethodType.Hold, paycheck.Disposition);
            Assert.AreEqual(0, paycheck.Deductions);
            Assert.AreEqual(expectedPay, paycheck.NetPay);
        }

        [TestMethod]
        public void TestPaySingleHourlyEmployeeOnWrongDate()
        {
            // Arrange
            var employeeId = 1;
            var hourlyRate = 15.25;

            new AddHourlyEmployeeTransaction(employeeId, "Adam", "Address", hourlyRate).Execute();

            var tuesdayDate = DateTime.Parse("2018-02-13");
            var hours = 9;
            new TimeCardTransaction(employeeId, tuesdayDate, hours).Execute();
            var paydayTransaction = new PaydayTransaction(tuesdayDate);

            // Act
            paydayTransaction.Execute();
            var paycheck = paydayTransaction.GetPaycheck(employeeId);

            // Assert
            Assert.IsNull(paycheck);
        }

        [TestMethod]
        public void TestPaySingleHourlyEmployeeOvertimeOneTimeCard()
        {
            // Arrange
            var employeeId = 1;
            var hourlyRate = 15.25;

            new AddHourlyEmployeeTransaction(employeeId, "Adam", "Address", hourlyRate).Execute();

            var fridayDate = DateTime.Parse("2018-02-16");
            var hours1 = 5;
            var hours2 = 2;
            new TimeCardTransaction(employeeId, fridayDate, hours1).Execute();
            new TimeCardTransaction(employeeId, fridayDate.Subtract(TimeSpan.FromDays(2)), hours2).Execute();
            var paydayTransaction = new PaydayTransaction(fridayDate);

            // Act
            paydayTransaction.Execute();
            var paycheck = paydayTransaction.GetPaycheck(employeeId);
            var expectedPay = (hours1 + hours2) * hourlyRate;

            // Assert
            Assert.IsNotNull(paycheck);
            Assert.AreEqual(fridayDate, paycheck.Date);
            Assert.AreEqual(expectedPay, paycheck.GrossPay);
            Assert.AreEqual(PaymentMethodType.Hold, paycheck.Disposition);
            Assert.AreEqual(0, paycheck.Deductions);
            Assert.AreEqual(expectedPay, paycheck.NetPay);
        }

        [TestMethod]
        public void TestPaySingleHourlyEmployeeWithTimeCardsSpanningTwoPayPeriod()
        {
            // Arrange
            var employeeId = 1;
            var hourlyRate = 15.25;

            new AddHourlyEmployeeTransaction(employeeId, "Adam", "Address", hourlyRate).Execute();

            var fridayDate = DateTime.Parse("2018-02-16");
            var hours = 2;
            new TimeCardTransaction(employeeId, fridayDate, hours).Execute();
            var dateInPreviousPayPeriod = fridayDate.Subtract(TimeSpan.FromDays(8));
            new TimeCardTransaction(employeeId, dateInPreviousPayPeriod, hours).Execute();
            var paydayTransaction = new PaydayTransaction(fridayDate);

            // Act
            paydayTransaction.Execute();
            var paycheck = paydayTransaction.GetPaycheck(employeeId);
            var expectedPay = hourlyRate * hours;

            // Assert
            Assert.IsNotNull(paycheck);
            Assert.AreEqual(fridayDate, paycheck.Date);
            Assert.AreEqual(expectedPay, paycheck.GrossPay);
            Assert.AreEqual(PaymentMethodType.Hold, paycheck.Disposition);
            Assert.AreEqual(0, paycheck.Deductions);
            Assert.AreEqual(expectedPay, paycheck.NetPay);
        }

        [TestMethod]
        public void TestPaySingleHourlyEmployeeWithTwoTimeCards()
        {
            // Arrange
            var employeeId = 1;
            var hourlyRate = 15.25;

            new AddHourlyEmployeeTransaction(employeeId, "Adam", "Address", hourlyRate).Execute();

            var fridayDate = DateTime.Parse("2018-02-16");
            var hours1 = 5;
            var hours2 = 2;
            new TimeCardTransaction(employeeId, fridayDate, hours1).Execute();
            new TimeCardTransaction(employeeId, fridayDate.Subtract(TimeSpan.FromDays(2)), hours2).Execute();
            var paydayTransaction = new PaydayTransaction(fridayDate);

            // Act
            paydayTransaction.Execute();
            var paycheck = paydayTransaction.GetPaycheck(employeeId);
            var expectedPay = (hours1 + hours2) * hourlyRate;

            // Assert
            Assert.IsNotNull(paycheck);
            Assert.AreEqual(fridayDate, paycheck.Date);
            Assert.AreEqual(expectedPay, paycheck.GrossPay);
            Assert.AreEqual(PaymentMethodType.Hold, paycheck.Disposition);
            Assert.AreEqual(0, paycheck.Deductions);
            Assert.AreEqual(expectedPay, paycheck.NetPay);
        }

        [TestMethod]
        public void TestPaySingleSalariedEmployee()
        {
            // Arrange
            var employeeId = 1;
            var salary = 2500;
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
            Assert.AreEqual(0, paycheck.Deductions);
        }

        [TestMethod]
        public void TestPaySingleSalariedEmployeeOnWrongDate()
        {
            // Arrange
            var employeeId = 1;
            var salary = 2500;
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