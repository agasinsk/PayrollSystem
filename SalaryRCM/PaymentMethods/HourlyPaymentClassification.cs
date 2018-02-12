﻿using System;
using System.Collections.Generic;
using SalaryRCM.Models;
using SalaryRCM.PaymentClassifications;

namespace SalaryRCM.PaymentMethods
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

        public TimeCard GetTimeCard(DateTime date)
        {
            return timeCards.Find(tc => tc.Date == date);
        }
    }
}