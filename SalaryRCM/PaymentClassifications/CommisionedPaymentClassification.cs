using System;
using System.Collections.Generic;
using PayrollSystem.Models;

namespace PayrollSystem.PaymentClassifications
{
    public class CommisionedPaymentClassification : PaymentClassification
    {
        private List<SalesReceipt> salesReceipts;
        public decimal CommisionRate { get; }
        public decimal Salary { get; }

        public CommisionedPaymentClassification(decimal salary, decimal commisionRate)
        {
            Salary = salary;
            CommisionRate = commisionRate;
            salesReceipts = new List<SalesReceipt>();
        }

        public void AddSalesReceipt(SalesReceipt salesReceipt)
        {
            salesReceipts.Add(salesReceipt);
        }

        public SalesReceipt GetSalesReceipt(DateTime date)
        {
            return salesReceipts.Find(sr => sr.Date == date);
        }
    }
}