using Domain.Entities;

namespace ApiPharmacy.Dtos;

public class SupplierProductDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Identification { get; set; }
    public List<ProductDto> Products { get; set; }
}
