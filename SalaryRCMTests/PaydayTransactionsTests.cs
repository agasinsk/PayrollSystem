using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PayrollSystem;
using PayrollSystem.Extensions;
using PayrollSystem.Models.PaymentMethod;
using PayrollSystem.Transactions.Employee;
using PayrollSystem.Transactions.Employee.Changes.Affiliation;
using PayrollSystem.Transactions.Payday;
using PayrollSystem.Transactions.Payroll;

namespace PayrollSystemTests
{
    [TestClass]
    public class PaydayTransactionsTests
    {
        private IPayrollRepository payrollRepository;

        [TestInitialize]
        public void SetUp()
        {
            payrollRepository = PayrollRepository.GetInstance();
        }

        [TestMethod]
        public void TestPayHourlyUnionMemberServiceCharge()
        {
            // Arrange
            var employeeId = 1;
            var hourlyRate = 24;
            new AddHourlyEmployeeTransaction(employeeId, "Adam", "Address", hourlyRate).Execute();

            var commisionRate = 9.25;
            var unionMemberId = 252;
            new ChangeEmployeeUnionAffiliationTransaction(employeeId, unionMemberId, commisionRate).Execute();

            var fridayDate = new DateTime(2018, 02, 16);
            var serviceChargeAmount = 11;
            new ServiceChargeTransaction(unionMemberId, fridayDate, serviceChargeAmount).Execute();

            var hours = 8;
            new TimeCardTransaction(employeeId, fridayDate, hours).Execute();
            var paydayTransaction = new PaydayTransaction(fridayDate);

            // Act
            paydayTransaction.Execute();
            var paycheck = paydayTransaction.GetPaycheck(employeeId);
            var expectedPay = hours * hourlyRate;

            // Assert
            Assert.IsNotNull(paycheck);
            Assert.AreEqual(fridayDate, paycheck.EndDate);
            Assert.AreEqual(expectedPay, paycheck.GrossPay);
            Assert.AreEqual(PaymentMethodType.Hold, paycheck.Disposition);
            Assert.AreEqual(commisionRate + serviceChargeAmount, paycheck.Deductions);
            Assert.AreEqual(expectedPay - commisionRate - serviceChargeAmount, paycheck.NetPay);
        }

        [TestMethod]
        public void TestPaySalariedUnionMemberDues()
        {
            // Arrange
            var employeeId = 1;
            var salary = 2400;
            new AddSalariedEmployeeTransaction(employeeId, "Adam", "Address", salary).Execute();

            var commisionRate = 9.25;
            var unionMemberId = 22;
            new ChangeEmployeeUnionAffiliationTransaction(employeeId, unionMemberId, commisionRate).Execute();

            var fridayDate = new DateTime(2011, 1, 31);
            var paydayTransaction = new PaydayTransaction(fridayDate);

            // Act
            paydayTransaction.Execute();
            var paycheck = paydayTransaction.GetPaycheck(employeeId);
            var expectedDeductions = 4 * commisionRate;

            // Assert
            Assert.IsNotNull(paycheck);
            Assert.AreEqual(fridayDate, paycheck.EndDate);
            Assert.AreEqual(salary, paycheck.GrossPay);
            Assert.AreEqual(PaymentMethodType.Hold, paycheck.Disposition);
            Assert.AreEqual(expectedDeductions, paycheck.Deductions);
            Assert.AreEqual(salary - expectedDeductions, paycheck.NetPay);
        }

        [TestMethod]
        public void TestPayServiceChargesSpanningMultiplePayPeriods()
        {
            // Arrange
            var employeeId = 1;
            var hourlyRate = 24;
            new AddHourlyEmployeeTransaction(employeeId, "Adam", "Address", hourlyRate).Execute();

            var commisionRate = 9.25;
            var unionMemberId = 252;
            new ChangeEmployeeUnionAffiliationTransaction(employeeId, unionMemberId, commisionRate).Execute();

            var serviceChargeAmount = 11;
            var fridayDate = new DateTime(2018, 02, 16);
            new ServiceChargeTransaction(unionMemberId, fridayDate, serviceChargeAmount).Execute();

            var earlierFridayDay = fridayDate - TimeSpan.FromDays(7);
            var serviceChargeAmount1 = 100;
            new ServiceChargeTransaction(unionMemberId, earlierFridayDay, serviceChargeAmount1).Execute();

            var nextFridayDay = fridayDate + TimeSpan.FromDays(7);
            var serviceChargeAmount2 = 100;
            new ServiceChargeTransaction(unionMemberId, nextFridayDay, serviceChargeAmount2).Execute();

            var hours = 8;
            new TimeCardTransaction(employeeId, fridayDate, hours).Execute();
            var paydayTransaction = new PaydayTransaction(fridayDate);

            // Act
            paydayTransaction.Execute();
            var paycheck = paydayTransaction.GetPaycheck(employeeId);
            var expectedPay = hours * hourlyRate;

            // Assert
            Assert.IsNotNull(paycheck);
            Assert.AreEqual(fridayDate, paycheck.EndDate);
            Assert.AreEqual(expectedPay, paycheck.GrossPay);
            Assert.AreEqual(PaymentMethodType.Hold, paycheck.Disposition);
            Assert.AreEqual(commisionRate + serviceChargeAmount, paycheck.Deductions);
            Assert.AreEqual(expectedPay - commisionRate - serviceChargeAmount, paycheck.NetPay);
        }

        [TestMethod]
        public void TestPaySingleCommisionedEmployeeNoSalesReceipts()
        {
            // Arrange
            var employeeId = 1;
            var commisionRate = 0.25;
            var salary = 2400;
            new AddCommisionedEmployeeTransaction(employeeId, "Adam", "Address", salary, commisionRate).Execute();

            var fridayDate = DateTime.Parse("2018-02-9");
            var paydayTransaction = new PaydayTransaction(fridayDate);

            // Act
            paydayTransaction.Execute();
            var paycheck = paydayTransaction.GetPaycheck(employeeId);

            // Assert
            Assert.IsNotNull(paycheck);
            Assert.AreEqual(fridayDate, paycheck.EndDate);
            Assert.AreEqual(salary, paycheck.GrossPay);
            Assert.AreEqual(PaymentMethodType.Hold, paycheck.Disposition);
            Assert.AreEqual(0, paycheck.Deductions);
            Assert.AreEqual(salary, paycheck.NetPay);
        }

        [TestMethod]
        public void TestPaySingleCommisionedEmployeeOneSalesReceipt()
        {
            // Arrange
            var employeeId = 1;
            var commisionRate = 0.25;
            var salary = 2400;
            new AddCommisionedEmployeeTransaction(employeeId, "Adam", "Address", salary, commisionRate).Execute();

            var payPeriodDate = DateTime.Parse("2018-02-6");
            var salesAmount = 200;
            new SalesReceiptTransaction(employeeId, payPeriodDate, salesAmount).Execute();

            var fridayDate = DateTime.Parse("2018-02-9");
            var paydayTransaction = new PaydayTransaction(fridayDate);

            // Act
            paydayTransaction.Execute();
            var paycheck = paydayTransaction.GetPaycheck(employeeId);
            var expectedPay = salary + salesAmount * commisionRate;

            // Assert
            Assert.IsNotNull(paycheck);
            Assert.AreEqual(fridayDate, paycheck.EndDate);
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
        public void TestPaySingleCommisionedEmployeeTwoSalesReceipts()
        {
            // Arrange
            var employeeId = 1;
            var commisionRate = 0.25;
            var salary = 2400;
            new AddCommisionedEmployeeTransaction(employeeId, "Adam", "Address", salary, commisionRate).Execute();

            var payPeriodDate = DateTime.Parse("2018-02-7");
            var salesAmount = 200;
            new SalesReceiptTransaction(employeeId, payPeriodDate, salesAmount).Execute();

            var payPeriodDate2 = DateTime.Parse("2018-02-6");
            var salesAmount1 = 150;
            new SalesReceiptTransaction(employeeId, payPeriodDate2, salesAmount1).Execute();

            var fridayDate = DateTime.Parse("2018-02-9");
            var paydayTransaction = new PaydayTransaction(fridayDate);

            // Act
            paydayTransaction.Execute();
            var paycheck = paydayTransaction.GetPaycheck(employeeId);
            var expectedPay = salary + salesAmount * commisionRate + salesAmount1 * commisionRate;

            // Assert
            Assert.IsNotNull(paycheck);
            Assert.AreEqual(fridayDate, paycheck.EndDate);
            Assert.AreEqual(expectedPay, paycheck.GrossPay);
            Assert.AreEqual(PaymentMethodType.Hold, paycheck.Disposition);
            Assert.AreEqual(0, paycheck.Deductions);
            Assert.AreEqual(expectedPay, paycheck.NetPay);
        }

        [TestMethod]
        public void TestPaySingleCommisionedEmployeeWithSalesReceiptsSpanningTwoPayPeriods()
        {
            // Arrange
            var employeeId = 1;
            var commisionRate = 0.25;
            var salary = 2400;
            new AddCommisionedEmployeeTransaction(employeeId, "Adam", "Address", salary, commisionRate).Execute();

            var date = DateTime.Parse("2018-02-4");
            var salesAmount = 200;
            new SalesReceiptTransaction(employeeId, date, salesAmount).Execute();

            var lastPayPeriodDate = date - TimeSpan.FromDays(14);
            var salesAmount1 = 150;
            new SalesReceiptTransaction(employeeId, lastPayPeriodDate, salesAmount1).Execute();

            var fridayDate = DateTime.Parse("2018-02-9");
            var paydayTransaction = new PaydayTransaction(fridayDate);

            // Act
            paydayTransaction.Execute();
            var paycheck = paydayTransaction.GetPaycheck(employeeId);
            var expectedPay = salary + salesAmount * commisionRate;

            // Assert
            Assert.IsNotNull(paycheck);
            Assert.AreEqual(fridayDate, paycheck.EndDate);
            Assert.AreEqual(expectedPay, paycheck.GrossPay);
            Assert.AreEqual(PaymentMethodType.Hold, paycheck.Disposition);
            Assert.AreEqual(0, paycheck.Deductions);
            Assert.AreEqual(expectedPay, paycheck.NetPay);
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
            Assert.AreEqual(fridayDate, paycheck.EndDate);
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
            Assert.AreEqual(fridayDate, paycheck.EndDate);
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
            Assert.AreEqual(fridayDate, paycheck.EndDate);
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
            Assert.AreEqual(fridayDate, paycheck.EndDate);
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
            Assert.AreEqual(fridayDate, paycheck.EndDate);
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