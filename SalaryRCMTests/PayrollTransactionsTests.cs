using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PayrollSystem;
using PayrollSystem.Models;
using PayrollSystem.Models.Affiliation;
using PayrollSystem.Models.PaymentClassification;
using PayrollSystem.Transactions.Employee;
using PayrollSystem.Transactions.Payroll;

namespace PayrollSystemTests
{
    [TestClass]
    public class PayrollTransactionsTests
    {
        private PayrollRepository payrollRepository;

        [TestInitialize]
        public void SetUp()
        {
            payrollRepository = PayrollRepository.GetInstance();
        }

        [TestMethod]
        public void TestAddServiceChargeTransaction()
        {
            // Arrange
            var employeeId = 1;
            var employeeName = "Bogdan";
            var employeeAddress = "Address";
            var hourlyRate = 25;

            new AddHourlyEmployeeTransaction(employeeId, employeeName, employeeAddress, hourlyRate).Execute();
            var employee = payrollRepository.GetEmployee(employeeId);

            var dues = 12.5;
            var memberId = 86;
            var unionAffiliation = new UnionEmployeeAffiliation(memberId, dues);
            employee.Affiliation = unionAffiliation;

            payrollRepository.AddUnionMember(memberId, employee);

            var date = DateTime.Parse("2001-10-31");
            var amount = 12.76;

            // Act
            new ServiceChargeTransaction(memberId, date, amount).Execute();
            var serviceCharge = unionAffiliation.GetServiceCharge(date);

            // Assert
            Assert.IsNotNull(serviceCharge);
            Assert.AreEqual(amount, serviceCharge.Amount);
            Assert.AreEqual(date, serviceCharge.Date);
        }

        [TestMethod]
        public void TestSalesReceiptTransaction()
        {
            // Arrange
            var employeeId = 1;
            var employeeName = "Bogdan";
            var employeeAddress = "Address";
            var salary = 2500;
            var commisionRate = 0.1;

            new AddCommisionedEmployeeTransaction(employeeId, employeeName, employeeAddress, salary, commisionRate).Execute();

            var date = DateTime.Parse("2001-10-31");
            var amount = 256;

            // Act
            new SalesReceiptTransaction(employeeId, date, amount).Execute();
            var employee = payrollRepository.GetEmployee(employeeId);
            var salesReceipt = (employee.PaymentClassification as CommisionedPaymentClassification)?.GetSalesReceipt(date);

            // Assert
            Assert.IsTrue(employee.PaymentClassification is CommisionedPaymentClassification);
            Assert.IsNotNull(salesReceipt);
            Assert.AreEqual(amount, salesReceipt.Amount);
            Assert.AreEqual(date, salesReceipt.Date);
        }

        [TestMethod]
        public void TestTimeCardTransaction()
        {
            // Arrange
            var employeeId = 1;
            var employeeName = "Bogdan";
            var employeeAddress = "Address";
            var hourlyRate = 25;
            var date = DateTime.Parse("2001-10-31");
            const int hours = 8;

            new AddHourlyEmployeeTransaction(employeeId, employeeName, employeeAddress, hourlyRate).Execute();

            // Act

            new TimeCardTransaction(employeeId, date, hours).Execute();
            var employee = payrollRepository.GetEmployee(employeeId);
            var timeCard = (employee.PaymentClassification as HourlyPaymentClassification)?.GetTimeCard(date);

            // Assert
            Assert.IsTrue(employee.PaymentClassification is HourlyPaymentClassification);
            Assert.AreEqual(hourlyRate, (employee.PaymentClassification as HourlyPaymentClassification).HourlyRate);
            Assert.IsNotNull(timeCard);
            Assert.AreEqual(hours, timeCard.Hours);
            Assert.AreEqual(date, timeCard.Date);
        }
    }
}