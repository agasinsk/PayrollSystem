﻿using System;

namespace PayrollSystem.Models
{
    public class NoAffiliation : Affiliation
    {
        public override double CalculateDeductions(Paycheck paycheck)
        {
            return 0;
        }
    }
}