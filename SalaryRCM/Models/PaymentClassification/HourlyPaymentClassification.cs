using System;
using System.Collections.Generic;
using System.Linq;
using PayrollSystem.Extensions;

namespace PayrollSystem.Models.PaymentClassification
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

        public override double CalculatePay(Paycheck paycheck)
        {
            var hours = timeCards.Where(tc => tc.Date.IsBetween(paycheck.StartDate, paycheck.EndDate)).Sum(tc => tc.Hours);
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
    }
}