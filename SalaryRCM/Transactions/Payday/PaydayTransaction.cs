using System;
using System.Collections.Generic;
using PayrollSystem.Models;

namespace PayrollSystem.Transactions.Payday
{
    public class PaydayTransaction : BaseTransaction
    {
        private readonly DateTime date;
        private readonly IDictionary<int, Paycheck> paychecks;

        public PaydayTransaction(DateTime date)
        {
            this.date = date;
            paychecks = new Dictionary<int, Paycheck>();
        }

        public override void Execute()
        {
            foreach (var employee in payrollDatabase.GetAllEmployees())
            {
                if (employee.IsPayDay(date))
                {
                    var paycheck = new Paycheck(date);
                    paychecks[employee.Id] = paycheck;
                    employee.PayDay(paycheck);
                }
            }
        }

        public Paycheck GetPaycheck(int employeeId)
        {
            try
            {
                return paychecks[employeeId];
            }
            catch (KeyNotFoundException)
            {
                return null;
            }
        }
    }
}