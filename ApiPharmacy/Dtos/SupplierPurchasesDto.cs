namespace ApiPharmacy.Dtos;

public class SupplierPurchasesDto
{
    public int Id { get; set; }
    public string Identification { get; set; }
    public string Name { get; set; }
    public RoleDto Role { get; set; }
    public List<ProductNameDto> Products  { get; set; }
    
}
