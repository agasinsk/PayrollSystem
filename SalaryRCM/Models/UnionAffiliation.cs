using System;
using System.Collections.Generic;

namespace PayrollSystem.Models
{
    public class UnionAffiliation : Affiliation
    {
        private readonly List<ServiceCharge> serviceCharges;
        public decimal Dues { get; }
        public int MemberId { get; }

        public UnionAffiliation(int memberId, decimal dues)
        {
            Dues = dues;
            MemberId = memberId;
            serviceCharges = new List<ServiceCharge>();
        }

        public void AddServiceChange(ServiceCharge serviceCharge)
        {
            serviceCharges.Add(serviceCharge);
        }

        public ServiceCharge GetServiceCharge(DateTime date)
        {
            return serviceCharges.Find(sc => sc.Date == date);
        }
    }
}