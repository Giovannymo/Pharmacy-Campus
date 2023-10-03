namespace ApiPharmacy.Helpers;

public class Authorization
{
    public enum JobsTitle
        {
            Administrator,
            Manager,
            Employee
        }

        public const JobsTitle rol_default = JobsTitle.Employee;


}