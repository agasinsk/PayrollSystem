using System;

namespace PayrollSystem.Models
{
    public class NoAffiliation : Affiliation
    {
        public override decimal CalculatePay(DateTime paycheckDate)
        {
            return 0;
        }
    }
}