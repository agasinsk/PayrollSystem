using System;

namespace PayrollSystem.Models
{
    public abstract class Affiliation
    {
        public abstract double CalculatePay(DateTime paycheckDate);
    }
}