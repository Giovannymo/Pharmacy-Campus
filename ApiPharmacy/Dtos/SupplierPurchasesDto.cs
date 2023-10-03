namespace ApiPharmacy.Dtos;

public class SupplierPurchasesDto
{
    public int Id { get; set; }
    public string Identification { get; set; }
    public TypeIdenDto IdentificationType { get; set; }
    public string Name { get; set; }
    public RoleDto Role { get; set; }
    public List<ProductDto> Products  { get; set; }
    
}
