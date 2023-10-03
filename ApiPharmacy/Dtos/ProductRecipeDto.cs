namespace ApiPharmacy.Dtos;

public class ProductRecipeDto
{
    public int Id { get; set; }
    public ProductNameDto Product { get; set; }
    public int Quantity { get; set; }
}
