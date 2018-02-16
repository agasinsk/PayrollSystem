using Microsoft.VisualStudio.TestTools.UnitTesting;
using PayrollSystem;
using PayrollSystem.Models;
using PayrollSystem.Models.Affiliation;
using PayrollSystem.Models.PaymentClassification;
using PayrollSystem.Models.PaymentMethod;
using PayrollSystem.Models.PaymentSchedule;
using PayrollSystem.Transactions.Employee;
using PayrollSystem.Transactions.Employee.Changes;
using PayrollSystem.Transactions.Employee.Changes.Affiliation;
using PayrollSystem.Transactions.Employee.Changes.Classification;
using PayrollSystem.Transactions.Employee.Changes.Method;

namespace PayrollSystemTests
{
    [TestClass]
    public class ChangeEmployeeTransactionsTests
    {
        private IPayrollRepository payrollRepository;

        [TestInitialize]
        public void SetUp()
        {
            payrollRepository = PayrollRepository.GetInstance();
        }

        [TestMethod]
        public void TestChangeAddressTransaction()
        {
            // Arrange
            var employeeId = 1;
            var employeeName = "Bogdan";
            var employeeAddress = "Address";
            var hourlyRate = 2500;

            new AddHourlyEmployeeTransaction(employeeId, employeeName, employeeAddress, hourlyRate).Execute();

            const string newAddress = "new York";

            // Act
            new ChangeEmployeeAddressTransaction(employeeId, newAddress).Execute();
            var employee = payrollRepository.GetEmployee(employeeId);

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
            var hourlyRate = 25;

            new AddHourlyEmployeeTransaction(employeeId, employeeName, employeeAddress, hourlyRate).Execute();
            var salary = 2500;
            var commisionRate = 15.5;

            // Act
            new ChangeEmployeeCommisionedClassificationTransaction(employeeId, salary, commisionRate).Execute();
            var employee = payrollRepository.GetEmployee(employeeId);

            // Assert
            Assert.IsNotNull(employee);
            Assert.IsTrue(employee.PaymentClassification is CommisionedPaymentClassification);
            Assert.AreEqual(salary, (employee.PaymentClassification as CommisionedPaymentClassification).Salary);
            Assert.AreEqual(commisionRate, (employee.PaymentClassification as CommisionedPaymentClassification).CommisionRate);
            Assert.IsTrue(employee.PaymentSchedule is BiweeklyPaymentSchedule);
        }

        [TestMethod]
        public void TestChangeDirectMethodTransaction()
        {
            // Arrange
            var employeeId = 1;
            var employeeName = "Bogdan";
            var employeeAddress = "Address";
            var hourlyRate = 25;

            new AddHourlyEmployeeTransaction(employeeId, employeeName, employeeAddress, hourlyRate).Execute();
            var bank = "Bank";
            var account = "0000 12223 1234 1234 1234 5678";

            // Act
            new ChangeEmployeeDirectMethodTransaction(employeeId, bank, account).Execute();
            var employee = payrollRepository.GetEmployee(employeeId);

            // Assert
            Assert.IsNotNull(employee);
            Assert.IsTrue(employee.PaymentMethod is DirectPaymentMethod);
            Assert.AreEqual(bank, (employee.PaymentMethod as DirectPaymentMethod).Bank);
            Assert.AreEqual(account, (employee.PaymentMethod as DirectPaymentMethod).Account);
        }

        [TestMethod]
        public void TestChangeHoldMethodTransaction()
        {
            // Arrange
            var employeeId = 1;
            var employeeName = "Bogdan";
            var employeeAddress = "Address";
            var hourlyRate = 25;

            new AddHourlyEmployeeTransaction(employeeId, employeeName, employeeAddress, hourlyRate).Execute();

            // Act
            new ChangeEmployeeHoldMethodTransaction(employeeId, employeeAddress).Execute();
            var employee = payrollRepository.GetEmployee(employeeId);

            // Assert
            Assert.IsNotNull(employee);
            Assert.IsTrue(employee.PaymentMethod is HoldPaymentMethod);
            Assert.AreEqual(employeeAddress, (employee.PaymentMethod as HoldPaymentMethod).Address);
        }

        [TestMethod]
        public void TestChangeHourlyClassificationTransaction()
        {
            // Arrange
            var employeeId = 1;
            var employeeName = "Bogdan";
            var employeeAddress = "Address";
            var salary = 2500;

            new AddCommisionedEmployeeTransaction(employeeId, employeeName, employeeAddress, salary, 12.5).Execute();
            var hourlyRate = 27.52;

            // Act
            new ChangeEmployeeHourlyClassificationTransaction(employeeId, hourlyRate).Execute();
            var employee = payrollRepository.GetEmployee(employeeId);

            // Assert
            Assert.IsNotNull(employee);
            Assert.IsTrue(employee.PaymentClassification is HourlyPaymentClassification);
            Assert.AreEqual(hourlyRate, (employee.PaymentClassification as HourlyPaymentClassification).HourlyRate);
            Assert.IsTrue(employee.PaymentSchedule is WeeklyPaymentSchedule);
        }

        [TestMethod]
        public void TestChangeMailMethodTransaction()
        {
            // Arrange
            var employeeId = 1;
            var employeeName = "Bogdan";
            var employeeAddress = "Address";
            var hourlyRate = 25;

            new AddHourlyEmployeeTransaction(employeeId, employeeName, employeeAddress, hourlyRate).Execute();

            // Act
            new ChangeEmployeeMailMethodTransaction(employeeId, employeeAddress).Execute();
            var employee = payrollRepository.GetEmployee(employeeId);

            // Assert
            Assert.IsNotNull(employee);
            Assert.IsTrue(employee.PaymentMethod is MailPaymentMethod);
            Assert.AreEqual(employeeAddress, (employee.PaymentMethod as MailPaymentMethod).Address);
        }

        [TestMethod]
        public void TestChangeMemberAffiliationTransaction()
        {
            // Arrange
            var employeeId = 1;
            var employeeName = "Bogdan";
            var employeeAddress = "Address";
            var hourlyRate = 25;

            new AddHourlyEmployeeTransaction(employeeId, employeeName, employeeAddress, hourlyRate).Execute();
            var memberId = 24;
            var dues = 92.42;

            // Act
            new ChangeEmployeeUnionAffiliationTransaction(employeeId, memberId, dues).Execute();
            var employee = payrollRepository.GetEmployee(employeeId);
            var unionMember = payrollRepository.GetUnionMember(memberId);

            // Assert
            Assert.IsNotNull(employee);
            Assert.IsNotNull(unionMember);
            Assert.AreEqual(unionMember, employee);
            Assert.IsTrue(employee.Affiliation is UnionEmployeeAffiliation);
            Assert.AreEqual(dues, (employee.Affiliation as UnionEmployeeAffiliation).Dues);
        }

        [TestMethod]
        public void TestChangeNameTransaction()
        {
            // Arrange
            var employeeId = 1;
            var employeeName = "Bogdan";
            var employeeAddress = "Address";
            var hourlyRate = 2500;

            new AddHourlyEmployeeTransaction(employeeId, employeeName, employeeAddress, hourlyRate).Execute();

            const string newName = "Bartosz";

            // Act
            new ChangeEmployeeNameTransaction(employeeId, newName).Execute();
            var employee = payrollRepository.GetEmployee(employeeId);

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
            var hourlyRate = 25;

            new AddHourlyEmployeeTransaction(employeeId, employeeName, employeeAddress, hourlyRate).Execute();
            var salary = 500;

            // Act
            new ChangeEmployeeSalariedClassificationTransaction(employeeId, salary).Execute();
            var employee = payrollRepository.GetEmployee(employeeId);

            // Assert
            Assert.IsNotNull(employee);
            Assert.IsTrue(employee.PaymentClassification is SalariedPaymentClassification);
            Assert.AreEqual(salary, (employee.PaymentClassification as SalariedPaymentClassification).Salary);
            Assert.IsTrue(employee.PaymentSchedule is MonthlyPaymentSchedule);
        }

        [TestMethod]
        public void TestChangeUnaffiliatedTransaction()
        {
            // Arrange
            var employeeId = 1;
            var employeeName = "Bogdan";
            var employeeAddress = "Address";
            var hourlyRate = 25;

            new AddHourlyEmployeeTransaction(employeeId, employeeName, employeeAddress, hourlyRate).Execute();

            // Act
            new ChangeEmployeeUnaffiliatedTransaction(employeeId).Execute();
            var employee = payrollRepository.GetEmployee(employeeId);

            // Assert
            Assert.IsNotNull(employee);
            Assert.IsTrue(employee.Affiliation is NoEmployeeAffiliation);
        }
    }
}