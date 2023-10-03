namespace ApiPharmacy.Helpers;

public class Authorization
{
    public enum JobsTitle
        {
            Administrador,
            Gerente,
            Cajero,
            Doctor,

        }

        public const JobsTitle rol_default = JobsTitle.Cajero;


}