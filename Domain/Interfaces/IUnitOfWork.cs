using Domain.Entities;

namespace Domain.Interfaces;

public interface IUnitOfWork
{
     IAddress Addresses { get; }
     ICity Cities { get; }
     ICountry Countries { get; }
     IEmail Emails { get; }
     IEmailType EmailTypes { get; }
     IIdentificationType IdentificationTypes { get; }
     IJobTitle JobTitles { get; }
     INeighborhood Neighborhoods { get; }
     IPaymentMethod PaymentMethods { get; }
     IPerson People { get; }
     IPersonType PersonTypes { get; }
     IProduct Products { get; }
     IPurchase Purchases { get; }
     IRecipe Recipes{ get; }
     IRecipeProduct RecipeProducts{ get; }
     ISale Sales { get; }
     ISaleProduct SaleProducts { get; }
     IState States { get; }
     ITelephone Telephones { get; }
     ITelephoneType TelephoneTypes { get; }
     IUser Users { get; }
     //se cambio, ya que, el jobtitle actual como rol en este caso
     
     Task<int> SaveAsync();


}
