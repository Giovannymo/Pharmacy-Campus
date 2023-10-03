namespace ApiPharmacy.Dtos;

public class EmployeeDto
{
   public int Id { get; set; }
    public string Name { get; set; }
    public string Identification { get; set; }
    public TypeIdenDto IdentificationType { get; set; }
    public TypePDto PersonType { get; set; }
    public RoleDto Role { get; set; }
    public JobTitleDto JobTitle { get; set; }
    public DateTime HireDate { get; set; }
}
