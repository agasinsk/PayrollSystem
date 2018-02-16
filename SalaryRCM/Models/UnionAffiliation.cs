using System;
using System.Collections.Generic;
using System.Linq;
using PayrollSystem.Extensions;

namespace PayrollSystem.Models
{
    public class UnionAffiliation : Affiliation
    {
        private readonly List<ServiceCharge> serviceCharges;
        public double Dues { get; }
        public int MemberId { get; }

        public UnionAffiliation(int memberId, double dues)
        {
            Dues = dues;
            MemberId = memberId;
            serviceCharges = new List<ServiceCharge>();
        }

        public void AddServiceChange(ServiceCharge serviceCharge)
        {
            serviceCharges.Add(serviceCharge);
        }

        public override double CalculateDeductions(Paycheck paycheck)
        {
            var fridaysInMonth = GetFridaysCountInPayPeriod(paycheck.StartDate, paycheck.EndDate);
            var totalDues = Dues * fridaysInMonth;
            var charges = serviceCharges.Where(sc => sc.Date.IsBetween(paycheck.StartDate, paycheck.EndDate))
                .Sum(sc => sc.Amount);
            return totalDues + charges;
        }

        public ServiceCharge GetServiceCharge(DateTime date)
        {
            return serviceCharges.Find(sc => sc.Date == date);
        }

        private int GetFridaysCountInPayPeriod(DateTime startDate, DateTime endDate)
        {
            return DateTimeExtensions.CountDays(DayOfWeek.Friday, startDate, endDate);
        }
    }
}