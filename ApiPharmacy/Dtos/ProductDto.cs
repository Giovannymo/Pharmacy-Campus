namespace ApiPharmacy.Dtos;

public class ProductDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public double Stock { get; set; }
    public DateTime ExpirationDate  { get; set; }
}
