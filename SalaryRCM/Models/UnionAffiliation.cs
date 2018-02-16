using System;
using System.Collections.Generic;

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
            throw new NotImplementedException();
        }

        public ServiceCharge GetServiceCharge(DateTime date)
        {
            return serviceCharges.Find(sc => sc.Date == date);
        }
    }
}