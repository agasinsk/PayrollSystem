﻿using System;
using PayrollSystem.PaymentClassifications;
using PayrollSystem.PaymentMethods;
using PayrollSystem.PaymentSchedules;

namespace PayrollSystem.Transactions.Employee.Changes
{
    public class ChangeEmployeeHourlyClassificationTransaction : ChangeEmployeeClassificationTransaction
    {
        private readonly decimal hourlyRate;

        public ChangeEmployeeHourlyClassificationTransaction(int employeeId, decimal hourlyRate) : base(employeeId)
        {
            this.hourlyRate = hourlyRate;
        }

        protected override PaymentClassification GetPaymentClassification()
        {
            return new HourlyPaymentClassification(hourlyRate);
        }

        protected override PaymentSchedule GetPaymentSchedule()
        {
            return new WeeklyPaymentSchedule();
        }
    }
}