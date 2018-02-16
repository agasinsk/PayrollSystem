namespace PayrollSystem.Models.Affiliation
{
    public abstract class EmployeeAffiliation
    {
        public abstract double CalculateDeductions(Paycheck paycheck);
    }
}