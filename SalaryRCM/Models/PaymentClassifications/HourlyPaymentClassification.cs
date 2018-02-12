using System;
using System.Collections.Generic;
using System.Linq;

namespace PayrollSystem.Models.PaymentClassifications
{
    public class HourlyPaymentClassification : PaymentClassification
    {
        private List<TimeCard> timeCards;
        public decimal HourlyRate { get; }

        public HourlyPaymentClassification(decimal hourlyRate)
        {
            this.HourlyRate = hourlyRate;
            timeCards = new List<TimeCard>();
        }

        public void AddTimeCard(TimeCard timeCard)
        {
            timeCards.Add(timeCard);
        }

        public override decimal CalculatePay(DateTime paycheckDate)
        {
            var hours = timeCards.Find(tc => tc.Date == paycheckDate).Hours;
            return HourlyRate * hours;
        }

        public TimeCard GetTimeCard(DateTime date)
        {
            return timeCards.Find(tc => tc.Date == date);
        }
    }
}