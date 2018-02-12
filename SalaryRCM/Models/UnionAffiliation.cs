using System;
using System.Collections.Generic;

namespace SalaryRCM.Models
{
    public class UnionAffiliation : Affiliation
    {
        private readonly List<ServiceCharge> dues;
        private decimal chargeRate;

        public UnionAffiliation(decimal chargeRate)
        {
            this.chargeRate = chargeRate;
            dues = new List<ServiceCharge>();
        }

        public void AddServiceChange(ServiceCharge serviceCharge)
        {
            dues.Add(serviceCharge);
        }

        public ServiceCharge GetServiceCharge(DateTime date)
        {
            return dues.Find(sc => sc.Date == date);
        }
    }
}