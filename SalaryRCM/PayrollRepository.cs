using System.Collections.Generic;
using PayrollSystem.Models;

namespace PayrollSystem
{
    public class PayrollRepository : IPayrollRepository
    {
        private static PayrollRepository theInstance;
        private readonly IDictionary<int, Employee> employees = new Dictionary<int, Employee>();
        private readonly IDictionary<int, Employee> unionMembers = new Dictionary<int, Employee>();

        public static PayrollRepository GetInstance()
        {
            return theInstance ?? (theInstance = new PayrollRepository());
        }

        public void AddEmployee(int employeeId, Employee employee)
        {
            employees[employeeId] = employee;
        }

        public void AddUnionMember(int memberId, Employee employee)
        {
            unionMembers[memberId] = employee;
        }

        public void Clear()
        {
            employees.Clear();
            unionMembers.Clear();
        }

        public Employee DeleteEmployee(int employeeId)
        {
            employees.Remove(employeeId, out var employee);
            return employee;
        }

        public Employee DeleteUnionMember(int memberId)
        {
            unionMembers.Remove(memberId, out var employee);
            return employee;
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return employees.Values;
        }

        public IEnumerable<Employee> GetAllUnionMembers()
        {
            return unionMembers.Values;
        }

        public Employee GetEmployee(int employeeId)
        {
            try
            {
                return employees[employeeId];
            }
            catch (KeyNotFoundException)
            {
                return null;
            }
        }

        public Employee GetUnionMember(int memberId)
        {
            try
            {
                return unionMembers[memberId];
            }
            catch (KeyNotFoundException)
            {
                return null;
            }
        }
    }
}