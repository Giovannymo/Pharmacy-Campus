
using Domain.Entities;

namespace ApiPharmacy.Dtos;
public class SupplierDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Identification { get; set; }
    public TypeIdenDto IdentificationType { get; set; }
    public TypePDto PersonType { get; set; }
    public RoleDto Role { get; set; }
}
