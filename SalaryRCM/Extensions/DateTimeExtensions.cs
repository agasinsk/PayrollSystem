﻿using System;
using PayrollSystem.Models;

namespace PayrollSystem.Extensions
{
    public static class DateTimeExtensions
    {
        public static int CountDays(DayOfWeek day, DateTime startDate, DateTime endDate)
        {
            var timeSpan = endDate - startDate;                       // Total duration
            var wholeWeeksCount = (int)Math.Floor(timeSpan.TotalDays / 7);   // Number of whole weeks
            var remainingDays = (int)(timeSpan.TotalDays % 7);         // Number of remaining days
            var daysSinceLastDay = endDate.DayOfWeek - day;   // Number of days since last [day]
            if (daysSinceLastDay < 0)
            {
                daysSinceLastDay += 7;         // Adjust for negative days since last [day]
            }

            // If the days in excess of an even week are greater than or equal to the number days since the last [day], then count this one, too.
            if (remainingDays >= daysSinceLastDay)
            {
                wholeWeeksCount++;
            }

            return wholeWeeksCount;
        }

        public static bool IsBetween(this DateTime date, DateTime startdate, DateTime enddate)
        {
            return date >= startdate && date <= enddate;
        }

        public static bool IsFriday(this DateTime date)
        {
            return date.DayOfWeek == DayOfWeek.Friday;
        }

        public static bool IsLastDayOfMonth(this DateTime date)
        {
            return DateTime.DaysInMonth(date.Year, date.Month) == date.Day;
        }

        public static bool IsSecondFriday(this DateTime date)
        {
            var daysInMonth = DateTime.DaysInMonth(date.Year, date.Month);
            var isInSecondWeek = (date.Day < 0.5 * daysInMonth && date.Day > 0.25 * daysInMonth);
            var isInLastWeek = (date.Day > 0.5 * daysInMonth / 2 && date.Day > 0.75 * daysInMonth);
            return IsFriday(date) && (isInLastWeek || isInSecondWeek);
        }

        public static DateTime StartOfMonth(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1);
        }

        public static DateTime StartOfWeek(this DateTime date, DayOfWeek startOfWeek)
        {
            var diff = (7 + (date.DayOfWeek - startOfWeek)) % 7;
            return date.AddDays(-1 * diff).Date;
        }

        public static DateTime StartOfWeek(this DateTime date)
        {
            return StartOfWeek(date, DayOfWeek.Monday);
        }
    }
}