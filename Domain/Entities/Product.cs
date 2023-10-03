namespace Domain.Entities;

public class Product : BaseEntity
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public double Stock { get; set; }
    public DateTime ExpirationDate  { get; set; }
    public int Supplier_Fk { get; set; }
    public Person Supplier { get; set; }
    public ICollection<SaleProduct> SaleProducts { get; set; }
    public ICollection<RecipeProduct> RecipeProducts { get; set; }
    public ICollection<PurchaseProduct> PurchaseProducts { get; set; }
    
}
