using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SalaryRCM;
using SalaryRCM.Models;
using SalaryRCM.PaymentClassifications;
using SalaryRCM.PaymentMethods;
using SalaryRCM.PaymentSchedules;
using SalaryRCM.Transactions.Employee;
using SalaryRCM.Transactions.Payroll;

namespace SalaryRCMTests
{
    [TestClass]
    public class PayrollTransactionsTests
    {
        private PayrollDatabase payrollDatabase;

        [TestInitialize]
        public void SetUp()
        {
            payrollDatabase = PayrollDatabase.GetInstance();
        }

        [TestMethod]
        public void TestAddServiceChargeTransaction()
        {
            // Arrange
            var employeeId = 1;
            var employeeName = "Bogdan";
            var employeeAddress = "Address";
            var hourlyRate = 2500M;

            new AddHourlyEmployeeTransaction(employeeId, employeeName, employeeAddress, hourlyRate).Execute();
            var employee = payrollDatabase.GetEmployee(employeeId);
            var chargeRate = 12.5M;
            var unionAffiliation = new UnionAffiliation(chargeRate);
            employee.Affiliation = unionAffiliation;

            var memberId = 86;
            payrollDatabase.AddUnionMember(86, employee);

            var date = DateTime.Parse("2001-10-31");
            var amount = 12.76M;

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
            var salary = 2500M;
            var commisionRate = 100M;

            new AddCommisionedEmployeeTransaction(employeeId, employeeName, employeeAddress, salary, commisionRate).Execute();

            var date = DateTime.Parse("2001-10-31");
            var amount = 256M;

            // Act
            new SalesReceiptTransaction(date, amount, employeeId).Execute();
            var employee = payrollDatabase.GetEmployee(employeeId);
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
            var hourlyRate = 2500M;
            var date = DateTime.Parse("2001-10-31");
            const int hours = 8;

            new AddHourlyEmployeeTransaction(employeeId, employeeName, employeeAddress, hourlyRate).Execute();

            // Act

            new TimeCardTransaction(date, employeeId, hours).Execute();
            var employee = payrollDatabase.GetEmployee(employeeId);
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