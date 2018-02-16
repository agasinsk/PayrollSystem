using System.Collections.Generic;
using PayrollSystem.Models;

namespace PayrollSystem
{
    public interface IPayrollRepository
    {
        void AddEmployee(int employeeId, Employee employee);

        void AddUnionMember(int memberId, Employee employee);

        Employee DeleteEmployee(int employeeId);

        Employee DeleteUnionMember(int memberId);

        IEnumerable<Employee> GetAllEmployees();

        IEnumerable<Employee> GetAllUnionMembers();

        Employee GetEmployee(int employeeId);

        Employee GetUnionMember(int memberId);
    }
}