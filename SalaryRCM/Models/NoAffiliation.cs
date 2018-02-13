using System;

namespace PayrollSystem.Models
{
    public class NoAffiliation : Affiliation
    {
        public override double CalculatePay(DateTime paycheckDate)
        {
            return 0;
        }
    }
}