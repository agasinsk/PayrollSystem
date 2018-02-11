using System;
using System.Collections.Generic;
using System.Text;

namespace SalaryRCM
{
    public class PayrollDatabase
    {
        private static PayrollDatabase theInstance;
        private readonly IDictionary<int, Employee> employees = new Dictionary<int, Employee>();

        public static PayrollDatabase GetInstance()
        {
            return theInstance ?? (theInstance = new PayrollDatabase());
        }

        public void AddEmployee(int employeeId, Employee employee)
        {
            employees[employeeId] = employee;
        }

        public void Clear()
        {
            employees.Clear();
        }

        public Employee GetEmployee(int employeeId)
        {
            return employees[employeeId];
        }
    }
}