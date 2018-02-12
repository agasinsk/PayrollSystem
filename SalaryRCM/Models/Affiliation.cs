using System;

namespace PayrollSystem.Models
{
    public abstract class Affiliation
    {
        public abstract decimal CalculatePay(DateTime paycheckDate);
    }
}