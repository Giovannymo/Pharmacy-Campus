namespace Domain.Entities;

public class Person : BaseEntity
{
  public string Name { get; set; }
  public int IdentificationType_Fk { get; set; }
  public IdentificationType IdentificationType { get; set; }
  public string Identification { get; set; }
  public int PersonType_Fk { get; set; }
  public PersonType PersonType { get; set; }
  public int? Role_Fk { get; set; }
  public Role Role { get; set; }
  public int? JobTitle_Fk { get; set; }
  public JobTitle JobTitle { get; set; }
  public DateTime? HireDate { get; set; }

  public ICollection<Address> Addresses { get; set; }
  public ICollection<Email> Emails { get; set; }
  public ICollection<Product> Products { get; set; }
  public ICollection<Purchase> Purchases { get; set; }
  public ICollection<Sale> SalesEmp { get; set; }
  public ICollection<Sale> SalesPat { get; set; }
  public ICollection<User> Users { get; set; }
  public ICollection<Telephone> Telephones { get; set; }
   public ICollection<Recipe> RecipesDoc { get; set; }
  public ICollection<Recipe> RecipesPat { get; set; }

    public object Include(Func<object, object> value)
    {
        throw new NotImplementedException();
    }
}
