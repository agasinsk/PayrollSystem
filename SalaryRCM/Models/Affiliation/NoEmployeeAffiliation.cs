namespace PayrollSystem.Models.Affiliation
{
    public class NoEmployeeAffiliation : EmployeeAffiliation
    {
        public override double CalculateDeductions(Paycheck paycheck)
        {
            return 0;
        }
    }
}