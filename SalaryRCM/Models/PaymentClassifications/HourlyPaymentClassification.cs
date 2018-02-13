using System;
using System.Collections.Generic;
using System.Linq;

namespace PayrollSystem.Models.PaymentClassifications
{
    public class HourlyPaymentClassification : PaymentClassification
    {
        public static readonly double OverTimeBonusFactor = 1.5;
        public static readonly int StandardHoursPerDay = 8;

        private readonly List<TimeCard> timeCards;
        public double HourlyRate { get; }

        public HourlyPaymentClassification(double hourlyRate)
        {
            HourlyRate = hourlyRate;
            timeCards = new List<TimeCard>();
        }

        public void AddTimeCard(TimeCard timeCard)
        {
            timeCards.Add(timeCard);
        }

        public override double CalculatePay(DateTime paycheckDate)
        {
            var hours = timeCards.Where(tc => IsTimeCardInPayPeriod(tc, paycheckDate)).Sum(tc => tc.Hours);
            var overTimeHours = GetOverTimeHours(hours);
            hours = hours - overTimeHours;

            var standardPay = HourlyRate * hours;
            var overTimePay = OverTimeBonusFactor * HourlyRate * overTimeHours;
            return standardPay + overTimePay;
        }

        public TimeCard GetTimeCard(DateTime date)
        {
            return timeCards.Find(tc => tc.Date == date);
        }

        private static double GetOverTimeHours(double hours)
        {
            return hours >= StandardHoursPerDay ? hours % StandardHoursPerDay : 0;
        }

        private bool IsTimeCardInPayPeriod(TimeCard tc, DateTime paycheckDate)
        {
            var payPeriodStart = paycheckDate.Subtract(TimeSpan.FromDays(5));

            return tc.Date <= paycheckDate && tc.Date >= payPeriodStart;
        }
    }
}